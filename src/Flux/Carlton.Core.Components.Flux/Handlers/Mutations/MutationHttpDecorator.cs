using Carlton.Core.Components.Flux.Attributes;
using System.Net.Http.Json;
using System.Text.Json;

namespace Carlton.Core.Components.Flux.Handlers.Mutations;

public class MutationHttpDecorator<TState> : BaseHttpDecorator<TState>, IMutationCommandDispatcher<TState>
{
    private readonly IMutationCommandDispatcher<TState> _decorated;
    private readonly ILogger<MutationHttpDecorator<TState>> _logger;

    public MutationHttpDecorator(IMutationCommandDispatcher<TState> decorated, HttpClient client, IMutableFluxState<TState> fluxState, ILogger<MutationHttpDecorator<TState>> logger)
        : base(client, fluxState)
        => (_decorated, _logger) = (decorated, logger);

    public async Task<Unit> Dispatch<TCommand>(object sender, TCommand command, CancellationToken cancellationToken)
        where TCommand : MutationCommand
    {
        try
        {
            var attributes = sender.GetType().GetCustomAttributes();
            var httpRefreshAttribute = attributes.OfType<MutationHttpRefreshAttribute>().FirstOrDefault();
            var requiresRefresh = GetRefreshPolicy(httpRefreshAttribute);
            var commandType = typeof(TCommand).GetDisplayName();

            if (requiresRefresh)
            {
                //Start the Http Interception
                _logger.MutationHttpInterceptionStarted(commandType);

                //Construct Http Refresh URL
                var urlParameterAttributes = attributes.OfType<HttpRefreshParameterAttribute>() ?? new List<HttpRefreshParameterAttribute>();
                var serverUrl = GetServerUrl(httpRefreshAttribute, urlParameterAttributes, sender);

                //Send Request
                var response = await SendRequest(httpRefreshAttribute.HttpVerb, serverUrl, command, cancellationToken);
                response.EnsureSuccessStatusCode();

                //Parse the Server Response and Update the command
                var json = await response.Content.ReadAsStringAsync(cancellationToken);
                UpdateCommandWithServerResponse(command, json);

                //End the Http Interception
                _logger.MutationHttpInterceptionCompleted(commandType);
            }
            else
            {
                //Skip Http Interception
                _logger.MutationHttpInterceptionSkipped(commandType);
            }

            //Continue the Dispatch Pipeline
            return await _decorated.Dispatch(sender, command, cancellationToken);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains(LogEvents.InvalidRefreshUrlMsg))
        {
            //URL Construction Errors
            _logger.MutationHttpUrlError(ex, typeof(TCommand).GetDisplayName());
            throw MutationCommandFluxException<TState, TCommand>.HttpUrlError(command, ex);
        }
        catch (JsonException ex)
        {
            //Error Serializing JSON
            _logger.MutationJsonError(ex, typeof(TCommand).GetDisplayName());
            throw MutationCommandFluxException<TState, TCommand>.HttpJsonError(command, ex);
        }
        catch (NotSupportedException ex) when (ex.Message.Contains("Serialization and deserialization"))
        {
            //Error Serializing JSON
            _logger.MutationJsonError(ex, typeof(TCommand).GetDisplayName());
            throw MutationCommandFluxException<TState, TCommand>.HttpJsonError(command, ex);
        }
        catch (HttpRequestException ex)
        {
            //HTTP Operation Exceptions
            _logger.MutationHttpInterceptionError(ex, typeof(TCommand).GetDisplayName());
            throw MutationCommandFluxException<TState, TCommand>.HttpError(command, ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains(LogEvents.ErrorUpdatingCommandFromServerResponseMsg))
        {
            //Response Update Errors
            _logger.MutationHttpResponseUpdateError(ex, typeof(TCommand).GetDisplayName());
            throw MutationCommandFluxException<TState, TCommand>.HttpResponseUpdateError(command, ex);
        }
    }

    protected async Task<HttpResponseMessage> SendRequest<TPayload>(HttpVerb httpVerb, string serverUrl, TPayload payload, CancellationToken cancellation)
    {
        return httpVerb switch
        {
            // HttpVerb.GET => _client.ge HttpMethod.Get,
            HttpVerb.POST => await _client.PostAsJsonAsync(serverUrl, payload, cancellation),
            HttpVerb.PUT => await _client.PutAsJsonAsync(serverUrl, payload, cancellation),
            HttpVerb.PATCH => await _client.PatchAsJsonAsync(serverUrl, payload, cancellation),
            HttpVerb.DELETE => await _client.DeleteAsync(serverUrl, cancellation),
            _ => throw new NotSupportedException("This HTTP verb is not supported here."),
        };
    }

    private static void UpdateCommandWithServerResponse<TCommand>(TCommand command, string json)
        where TCommand : MutationCommand
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


