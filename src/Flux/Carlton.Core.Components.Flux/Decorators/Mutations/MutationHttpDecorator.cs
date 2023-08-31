using Carlton.Core.Components.Flux.Attributes;
using Carlton.Core.Components.Flux.Decorators.Base;
using System.Net.Http.Json;
using System.Text.Json;

namespace Carlton.Core.Components.Flux.Decorators.Mutations;

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
        var attributes = sender.GetType().GetCustomAttributes();
        var httpRefreshAttribute = attributes.OfType<MutationHttpRefreshAttribute>().FirstOrDefault();
        var requiresRefresh = GetRefreshPolicy(httpRefreshAttribute);
        var commandType = typeof(TCommand).GetDisplayName();

        if (requiresRefresh)
        {
            //Start the Http Interception
            Log.MutationHttpInterceptionStarted(_logger, commandType);

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
            Log.MutationHttpInterceptionCompleted(_logger, commandType);
        }
        else
        {
            //Skip Http Interception
            Log.MutationHttpInterceptionSkipped(_logger, commandType);
        }

        //Continue the Dispatch Pipeline
        return await _decorated.Dispatch(sender, command, cancellationToken);
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
            var replacmentProperties = command.GetType().GetProperties().Where(predicate);

            //Exit if response property attributes not present
            if (!replacmentProperties.Any())
                return;

            //Parse the server json response
            var serverResponseType = serverResponseTypeAttribute.GetType().GetGenericArguments()[0];
            var parsedResponse = JsonSerializer.Deserialize(json, serverResponseType, new JsonSerializerOptions(JsonSerializerDefaults.Web));

            //iterate through properties
            foreach (var prop in replacmentProperties)
            {
                //Get the property attribute
                var attribute = prop.GetCustomAttribute<HttpResponsePropertyAttribute>();

                //Pull the property value from the server response
                var serverResponseValue = parsedResponse.GetType().GetProperty(attribute.ResponsePropertyName).GetValue(parsedResponse);

                //Update the command with the server response value
                prop.SetValue(command, serverResponseValue);
            }
        }
        catch(JsonException ex)
        {
            throw new ArgumentException("An error occured updating the command with the server response", typeof(HttpResponseTypeAttribute<>).GetDisplayName(), ex);
        }
        catch(Exception ex)
        {
            throw new ArgumentException("An error occured updating the command with the server response", nameof(HttpResponsePropertyAttribute), ex);
        }
    }
}


