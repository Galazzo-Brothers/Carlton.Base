using Carlton.Core.Components.Flux.Attributes;
using Carlton.Core.Components.Flux.Decorators.Base;
using System.Net.Http.Json;

namespace Carlton.Core.Components.Flux.Decorators.Mutations;

public class MutationHttpDecorator<TState> : BaseHttpDecorator<TState>, IMutationCommandDispatcher<TState>
{
    private readonly IMutationCommandDispatcher<TState> _decorated;
    private readonly ILogger<MutationHttpDecorator<TState>> _logger;

    public MutationHttpDecorator(IMutationCommandDispatcher<TState> decorated, HttpClient client, IFluxState<TState> fluxState, ILogger<MutationHttpDecorator<TState>> logger)
        : base(client, fluxState)
        => (_decorated, _logger) = (decorated, logger);

    public async Task<Unit> Dispatch<TCommand>(TCommand command, CancellationToken cancellationToken)
        where TCommand : MutationCommand
    {
        var attributes = command.Sender.GetType().GetCustomAttributes();
        var httpRefreshAttribute = attributes.OfType<MutationHttpRefreshAttribute>().FirstOrDefault();
        var requiresRefresh = GetRefreshPolicy(httpRefreshAttribute);
        var commandType = typeof(TCommand).GetDisplayName();

        if (requiresRefresh)
        {
            //Start the Http Interception
            Log.MutationHttpInterceptionStarted(_logger, commandType);

            //Construct Http Refresh URL
            var urlParameterAttributes = attributes.OfType<HttpRefreshParameterAttribute>() ?? new List<HttpRefreshParameterAttribute>();
            var serverUrl = GetServerUrl(httpRefreshAttribute, urlParameterAttributes, command.Sender);
            
            //Send Request
            var response = await SendRequest(httpRefreshAttribute.HttpVerb, serverUrl, command, cancellationToken);
            response.EnsureSuccessStatusCode();

            //Parse the Server Response and Update the command
            var serverResponse = await response.Content.ReadFromJsonAsync<TCommand>(cancellationToken: cancellationToken);
            command.UpdateCommandWithExternalResponse(serverResponse);

            //End the Http Interception
            Log.MutationHttpInterceptionCompleted(_logger, commandType);
        }
        else
        {
            //Skip Http Interception
            Log.MutationHttpInterceptionSkipped(_logger, commandType);
        }

        //Continue the Dispatch Pipeline
        return await _decorated.Dispatch(command, cancellationToken);
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
            _ => throw new InvalidOperationException("This HTTP verb is not supported here."),
        };
    }
}
