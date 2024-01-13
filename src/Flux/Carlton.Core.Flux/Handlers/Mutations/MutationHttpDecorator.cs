using Carlton.Core.Flux.Attributes;
using Carlton.Core.Flux.Exceptions;
using Carlton.Core.Flux.Handlers.Base;
using System.Net.Http.Json;

namespace Carlton.Core.Flux.Handlers.Mutations;

public class MutationHttpDecorator<TState>: BaseHttpDecorator<TState>, IMutationCommandDispatcher<TState>
{
    private readonly IMutationCommandDispatcher<TState> _decorated;
    private readonly ILogger<MutationHttpDecorator<TState>> _logger;

    public MutationHttpDecorator(IMutationCommandDispatcher<TState> decorated, HttpClient client, TState state, ILogger<MutationHttpDecorator<TState>> logger)
        : base(client, state)
        => (_decorated, _logger) = (decorated, logger);

    public async Task Dispatch<TCommand>(object sender, MutationCommandContext<TCommand> context, CancellationToken cancellationToken)
    {
        try
        {
            var attributes = sender.GetType().GetCustomAttributes();
            var httpRefreshAttribute = attributes.OfType<MutationHttpRefreshAttribute>().FirstOrDefault();
            var requiresRefresh = GetRefreshPolicy(httpRefreshAttribute);
            var commandType = typeof(TCommand).GetDisplayName();

            if (requiresRefresh)
            {
                //Update the context for logging and auditing
                context.MarkAsRequiresHttpRefresh();

                //Construct Http Refresh URL
                var urlParameterAttributes = attributes.OfType<HttpRefreshParameterAttribute>() ?? new List<HttpRefreshParameterAttribute>();
                var serverUrl = GetServerUrl(httpRefreshAttribute, urlParameterAttributes, sender);

                //Send Request
                var response = await SendRequest(httpRefreshAttribute.HttpVerb, serverUrl, context, cancellationToken);
                response.EnsureSuccessStatusCode();

                //Parse the Server Response and Update the command
                var json = await response.Content.ReadAsStringAsync(cancellationToken);
                UpdateCommandWithServerResponse(context, json);

                //End the Http Interception
                context.MarkAsHttpCallMade(serverUrl, response.StatusCode, response);
            }

            //Continue the Dispatch Pipeline
            await _decorated.Dispatch(sender, context, cancellationToken);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains(LogEvents.InvalidRefreshUrlMsg))
        {
            //URL Construction Errors
            _logger.MutationHttpInterceptionUrlConstructionError(ex, typeof(TCommand).GetDisplayName());
            throw MutationCommandFluxException<TState, TCommand>.HttpUrlError(context, ex);
        }
        catch (JsonException ex)
        {
            //Error Serializing JSON
            _logger.MutationHttpInterceptionJsonResponseParseError(ex, typeof(TCommand).GetDisplayName());
            throw MutationCommandFluxException<TState, TCommand>.HttpJsonError(context, ex);
        }
        catch (NotSupportedException ex) when (ex.Message.Contains("Serialization and deserialization"))
        {
            //Error Serializing JSON
            _logger.MutationHttpInterceptionJsonResponseParseError(ex, typeof(TCommand).GetDisplayName());
            throw MutationCommandFluxException<TState, TCommand>.HttpJsonError(context, ex);
        }
        catch (HttpRequestException ex)
        {
            //HTTP Operation Exceptions
            _logger.MutationHttpInterceptionRequestError(ex, typeof(TCommand).GetDisplayName());
            throw MutationCommandFluxException<TState, TCommand>.HttpError(context, ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains(LogEvents.ErrorUpdatingCommandFromServerResponseMsg))
        {
            //Response Update Errors
            _logger.MutationHttpInterceptionResponseUpdateError(ex, typeof(TCommand).GetDisplayName());
            throw MutationCommandFluxException<TState, TCommand>.HttpResponseUpdateError(context, ex);
        }
    }

    protected async Task<HttpResponseMessage> SendRequest<TPayload>(HttpVerb httpVerb, string serverUrl, TPayload payload, CancellationToken cancellation)
    {
        return httpVerb switch
        {
            HttpVerb.POST => await _client.PostAsJsonAsync(serverUrl, payload, cancellation),
            HttpVerb.PUT => await _client.PutAsJsonAsync(serverUrl, payload, cancellation),
            HttpVerb.PATCH => await _client.PatchAsJsonAsync(serverUrl, payload, cancellation),
            HttpVerb.DELETE => await _client.DeleteAsync(serverUrl, cancellation),
            _ => throw new NotSupportedException("This HTTP verb is not supported here."),
        };
    }

    private static void UpdateCommandWithServerResponse<TCommand>(TCommand command, string json)
    {
        try
        {
            //Static inline helper predicate
            static bool predicate(PropertyInfo prop) => Attribute.IsDefined(prop, typeof(HttpResponsePropertyAttribute));

            //Find HttpResponseType attribute
            var serverResponseTypeAttribute = command.GetType().GetCustomAttribute(typeof(HttpResponseTypeAttribute<>));

            //Exit if response type attribute not present
            if (serverResponseTypeAttribute == null)
                return;

            //Find HttpResponseProperty attributes
            var replacementProperties = command.GetType().GetProperties().Where(predicate);

            //Exit if response property attributes not present
            if (!replacementProperties.Any())
                return;

            //Parse the server json response
            var serverResponseType = serverResponseTypeAttribute.GetType().GetGenericArguments()[0];
            var parsedResponse = JsonSerializer.Deserialize(json, serverResponseType, new JsonSerializerOptions(JsonSerializerDefaults.Web));

            //iterate through properties
            foreach (var prop in replacementProperties)
            {
                //Get the property attribute
                var attribute = prop.GetCustomAttribute<HttpResponsePropertyAttribute>();

                //Pull the property value from the server response
                var serverResponseValue = parsedResponse.GetType().GetProperty(attribute.ResponsePropertyName).GetValue(parsedResponse);

                //Update the command with the server response value
                prop.SetValue(command, serverResponseValue);
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"{LogEvents.ErrorUpdatingCommandFromServerResponseMsg} {typeof(HttpResponseTypeAttribute<>).GetDisplayName()}", ex);
        }
    }
}


