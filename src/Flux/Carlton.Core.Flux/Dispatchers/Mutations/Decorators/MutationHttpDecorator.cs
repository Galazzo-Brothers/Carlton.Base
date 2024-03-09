using Carlton.Core.Flux.Attributes;
using Carlton.Core.Flux.Handlers.Base;
using System.Net.Http.Json;
namespace Carlton.Core.Flux.Dispatchers.Mutations.Decorators;

public class MutationHttpDecorator<TState>(IMutationCommandDispatcher<TState> _decorated, HttpClient _client)
    : BaseHttpDecorator<TState>(_client), IMutationCommandDispatcher<TState>
{
    public async Task<Result<MutationCommandResult, FluxError>> Dispatch<TCommand>(object sender, MutationCommandContext<TCommand> context, CancellationToken cancellationToken)
    {
        var attributes = sender.GetType().GetCustomAttributes();
        var fluxServerCommunicationAttribute = attributes.OfType<FluxServerCommunicationAttribute>().FirstOrDefault();
        var requiresRefresh = GetRefreshPolicy(fluxServerCommunicationAttribute.ServerCommunicationPolicy);

        //Continue on if no refresh required
        if (requiresRefresh)
        {
            //Update the context for logging and auditing
            context.MarkAsRequiresHttpRefresh();

            //Construct Http Refresh URL
            var urlParameterAttributes = attributes.OfType<FluxServerCommunicationParameterAttribute>() ?? new List<FluxServerCommunicationParameterAttribute>();
            var serverUrlResult = GetServerUrl(fluxServerCommunicationAttribute, urlParameterAttributes, sender);

            //Send Request
            var response = await SendRequest(fluxServerCommunicationAttribute.HttpVerb, serverUrlResult, context, cancellationToken);

            //Update context with response
            await UpdateCommandWithServerResponse(response, context, cancellationToken);
        }

        return await _decorated.Dispatch(sender, context, cancellationToken); ;
    }

    protected async Task<Result<HttpResponseMessage, FluxError>> SendRequest<TCommand>(
        HttpVerb httpVerb,
        Result<string, FluxError> serverUrlResult,
        MutationCommandContext<TCommand> context,
        CancellationToken cancellationToken)
    {
        return await serverUrlResult.Match
        (
            async serverUrl =>
            {
                try
                {
                    Result<HttpResponseMessage, FluxError> response = httpVerb switch
                    {
                        HttpVerb.POST => await Client.PostAsJsonAsync(serverUrl, context, cancellationToken),
                        HttpVerb.PUT => await Client.PutAsJsonAsync(serverUrl, context, cancellationToken),
                        HttpVerb.PATCH => await Client.PatchAsJsonAsync(serverUrl, context, cancellationToken),
                        HttpVerb.DELETE => await Client.DeleteAsync(serverUrl, cancellationToken),
                        _ => UnsupportedHttpVerbError(httpVerb).ToResult<HttpResponseMessage, FluxError>(),
                    };

                    context.MarkAsHttpCallMade(serverUrl, System.Net.HttpStatusCode.OK, response);

                    return response;
                }
                catch (HttpRequestException ex)
                {
                    return HttpError(ex);
                }
                catch (JsonException ex)
                {
                    return JsonError(ex);
                }
                catch (NotSupportedException ex) when (ex.Message.Contains("Serialization and deserialization"))
                {
                    return JsonError(ex);
                }
            },
            err => err.ToResultTask<HttpResponseMessage, FluxError>()
        );
    }

    private static async Task<Result<bool, FluxError>> UpdateCommandWithServerResponse<TCommand>(
        Result<HttpResponseMessage, FluxError> responseResult,
        MutationCommandContext<TCommand> context,
        CancellationToken cancellationToken)
    {
        return await responseResult.Match
        (
            async response =>
            {
                try
                {
                    //return error
                    if (!response.IsSuccessStatusCode)
                        return HttpRequestFailedError(response);

                    //parse json
                    var json = await response.Content.ReadAsStringAsync(cancellationToken);

                    //Find HttpResponseType attribute
                    var serverResponseTypeAttribute = context.MutationCommand.GetType().GetCustomAttribute(typeof(HttpResponseTypeAttribute<>));

                    //Exit if response type attribute not present
                    if (serverResponseTypeAttribute == null)
                        return true;

                    //Find HttpResponseProperty attributes
                    var replacementProperties = context.MutationCommand.GetType().GetProperties().Where(Predicate);

                    //Exit if response property attributes not present
                    if (!replacementProperties.Any())
                        return true;

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
                        prop.SetValue(context.MutationCommand, serverResponseValue);
                    }

                    return true;
                }
                catch (JsonException ex)
                {
                    return JsonError(ex);
                }
                catch (NotSupportedException ex) when (ex.Message.Contains("Serialization and deserialization"))
                {
                    return JsonError(ex);
                }
            },
            err => err.ToResultTask<bool, FluxError>()
        );
    }

    //Static inline helper predicate
    static bool Predicate(PropertyInfo prop) => Attribute.IsDefined(prop, typeof(HttpResponsePropertyAttribute));
}


