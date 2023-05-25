namespace Carlton.Base.State;

public class CommandHttpDecorator<TState> : ICommandDispatcher
{
    private readonly ICommandDispatcher _decorated;
    private readonly HttpClient _client;
    private readonly ILogger<CommandHttpDecorator<TState>> _logger;

    public CommandHttpDecorator(ICommandDispatcher decorated, HttpClient client, ILogger<CommandHttpDecorator<TState>> logger)
        => (_decorated, _client, _logger) = (decorated, client, logger);

    public async Task<Unit> Dispatch<TCommand>(CommandRequest<TCommand> request, CancellationToken cancellationToken)
    {
        var attributes = request.Sender.GetType().GetCustomAttributes();
        var urlAttribute = attributes.OfType<CommandEndpointAttribute>().FirstOrDefault();
        var shouldCallSever = urlAttribute != null;

        if(shouldCallSever)
            await CallServer(urlAttribute.Route, request, cancellationToken);
        else
            Log.CommandRequestHttpCallSkipped(_logger, request.DisplayName, request);

        return await _decorated.Dispatch(request, cancellationToken);
    }

    public async Task CallServer<TCommand>(string url, CommandRequest<TCommand> request, CancellationToken cancellationToken)
    {
        try
        {
            Log.CommandRequestHttpCallStarted(_logger, request.DisplayName, request);
            var response = await _client.PostAsJsonAsync(url, request, cancellationToken);

            //var responseCommand = await response.Content.ReadFromJsonAsync<TCommand>(cancellationToken: cancellationToken);
            //responseCommand.Adapt(request.Command);

            request.MarkAsServerCalled(url);
            response.EnsureSuccessStatusCode();
            Log.CommandRequestHttpCallCompleted(_logger, request.DisplayName, request);
        }
        catch(HttpRequestException ex)
        {
            request.MarkErrored(LogEvents.Command_HttpCall_Error);
            Log.CommandRequestHttpCallError(_logger, ex, request.DisplayName, request);
            throw;
        }
    }
}
