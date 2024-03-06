using Carlton.Core.Flux.Attributes;
using Carlton.Core.Flux.Errors;
using Carlton.Core.Flux.Handlers.Base;
using System.Net.Http.Json;
namespace Carlton.Core.Flux.Dispatchers.Mutations.Decorators;

public class MutationHttpDecorator<TState>(IMutationCommandDispatcher<TState> _decorated, HttpClient _client, IFluxState<TState> _state)
    : BaseHttpDecorator<TState>(_client, _state), IMutationCommandDispatcher<TState>
{
    public async Task<Result<MutationCommandResult, FluxError>> Dispatch<TCommand>(object sender, MutationCommandContext<TCommand> context, CancellationToken cancellationToken)
    {
        var attributes = sender.GetType().GetCustomAttributes();
        var httpRefreshAttribute = attributes.OfType<MutationHttpRefreshAttribute>().FirstOrDefault();
        var requiresRefresh = GetRefreshPolicy(httpRefreshAttribute);

        //Continue on if no refresh required
        if (requiresRefresh)
        {
            //Update the context for logging and auditing
            context.MarkAsRequiresHttpRefresh();

            //Construct Http Refresh URL
            var urlParameterAttributes = attributes.OfType<HttpRefreshParameterAttribute>() ?? new List<HttpRefreshParameterAttribute>();
            var serverUrlResult = GetServerUrl(httpRefreshAttribute, urlParameterAttributes, sender, context);

            //Send Request
            var response = await SendRequest(httpRefreshAttribute.HttpVerb, serverUrlResult, context, cancellationToken);

            //Update context with response
            await UpdateCommandWithServerResponse(response, context, cancellationToken);
        }

        return await _decorated.Dispatch(sender, context, cancellationToken); ;
    }

    protected async Task<Result<HttpResponseMessage, FluxError>> SendRequest<TCommand>(
        HttpVerb httpVerb,
        Result<string, HttpUrlConstructionError> serverUrlResult,
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
                        _ => new UnsupportedHttpVerbError(httpVerb, context).ToResult<HttpResponseMessage, FluxError>(),
                    };

                    context.MarkAsHttpCallMade(serverUrl, System.Net.HttpStatusCode.OK, response);

                    return response;
                }
                catch (HttpRequestException ex)
                {
                    return new FluxErrors.HttpRequestError(ex, context);
                }
                catch (JsonException ex)
                {
                    return new JsonError(ex, context);
                }
                catch (NotSupportedException ex) when (ex.Message.Contains("Serialization and deserialization"))
                {
                    return new JsonError(ex, context);
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
                        return new HttpError(response.RequestMessage.Method.ToString(), response.StatusCode, response, context);

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
                    return new JsonError(ex, context);
                }
                catch (NotSupportedException ex) when (ex.Message.Contains("Serialization and deserialization"))
                {
                    return new JsonError(ex, context);
                }
            },
            err => err.ToResultTask<bool, FluxError>()
        );
    }

    //Static inline helper predicate
    static bool Predicate(PropertyInfo prop) => Attribute.IsDefined(prop, typeof(HttpResponsePropertyAttribute));
}


