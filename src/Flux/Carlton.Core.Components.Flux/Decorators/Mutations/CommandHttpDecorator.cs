using System.Net.Http.Json;

namespace Carlton.Core.Components.Flux.Decorators.Mutations;

public class CommandHttpDecorator<TState> : IMutationCommandDispatcher<TState>
{
    private readonly IMutationCommandDispatcher<TState> _decorated;
    private readonly HttpClient _client;
    private readonly ILogger<CommandHttpDecorator<TState>> _logger;

    public CommandHttpDecorator(IMutationCommandDispatcher<TState> decorated, HttpClient client, ILogger<CommandHttpDecorator<TState>> logger)
        => (_decorated, _client, _logger) = (decorated, client, logger);

    public async Task<Unit> Dispatch<TCommand>(TCommand command, CancellationToken cancellationToken)
        where TCommand : MutationCommand
    {
        var attributes = command.Sender.GetType().GetCustomAttributes();
        var urlAttribute = attributes.OfType<CommandEndpointAttribute>().FirstOrDefault();
        var shouldCallSever = urlAttribute != null;
        var commandType = typeof(TCommand).GetDisplayName();

        if(shouldCallSever)
        {
            //Start the Http Interception
            Log.MutationHttpInterceptionStarted(_logger, commandType);
            var response = await _client.PostAsJsonAsync(urlAttribute.Route, command, cancellationToken);
            response.EnsureSuccessStatusCode();

            //Parse the Server Response and Update the command
            var parsedResponse = await response.Content.ReadFromJsonAsync<TCommand>(cancellationToken: cancellationToken);
            command.UpdateCommandWithExternalResponse(parsedResponse);
            
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
}
