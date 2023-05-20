namespace Carlton.Base.State;

public class CommandHttpDecorator : ICommandDispatcher
{
    private readonly ICommandDispatcher _decorated;
    private readonly HttpClient _client;
    private readonly ILogger<CommandHttpDecorator> _logger;

    public CommandHttpDecorator(ICommandDispatcher decorated, HttpClient client, ILogger<CommandHttpDecorator> logger)
        => (_decorated, _client, _logger) = (decorated, client, logger);

    public async Task<Unit> Dispatch<TCommand>(CommandRequest<TCommand> request, CancellationToken cancellationToken)
        where TCommand : ICommand
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
        where TCommand : ICommand
    {
        try
        {
            Log.CommandRequestHttpCallStarted(_logger, request.DisplayName, request);
            var response = await _client.PostAsJsonAsync(url, request, cancellationToken);
            request.MarkAsServerCalled();
            response.EnsureSuccessStatusCode();
            Log.CommandRequestHttpCallCompleted(_logger, request.DisplayName, request);
        }
        catch(Exception ex)
        {
            Log.CommandRequestHttpCallError(_logger, ex, request.DisplayName, request);
            throw;
        }
    }
}
