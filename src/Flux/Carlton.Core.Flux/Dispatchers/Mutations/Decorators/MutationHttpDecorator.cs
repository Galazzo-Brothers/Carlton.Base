using Carlton.Core.Flux.Attributes;
using Carlton.Core.Flux.Handlers.Base;
using System.Net.Http.Json;
namespace Carlton.Core.Flux.Dispatchers.Mutations.Decorators;

public class MutationHttpDecorator<TState>(IMutationCommandDispatcher<TState> _decorated, HttpClient _client, IFluxState<TState> _state)
    : BaseHttpDecorator<TState>(_client, _state), IMutationCommandDispatcher<TState>
{
    public async Task<Result<MutationCommandResult, MutationCommandError>> Dispatch<TCommand>(object sender, MutationCommandContext<TCommand> context, CancellationToken cancellationToken)
    {
        var attributes = sender.GetType().GetCustomAttributes();
        var httpRefreshAttribute = attributes.OfType<MutationHttpRefreshAttribute>().FirstOrDefault();
        var requiresRefresh = GetRefreshPolicy(httpRefreshAttribute);

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
        return await _decorated.Dispatch(sender, context, cancellationToken);
    }

    protected async Task<HttpResponseMessage> SendRequest<TPayload>(HttpVerb httpVerb, string serverUrl, TPayload payload, CancellationToken cancellation)
    {
        return httpVerb switch
        {
            HttpVerb.POST => await Client.PostAsJsonAsync(serverUrl, payload, cancellation),
            HttpVerb.PUT => await Client.PutAsJsonAsync(serverUrl, payload, cancellation),
            HttpVerb.PATCH => await Client.PatchAsJsonAsync(serverUrl, payload, cancellation),
            HttpVerb.DELETE => await Client.DeleteAsync(serverUrl, cancellation),
            _ => throw new NotSupportedException("This HTTP verb is not supported here."),
        };
    }

    private static void UpdateCommandWithServerResponse<TCommand>(TCommand command, string json)
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
}


