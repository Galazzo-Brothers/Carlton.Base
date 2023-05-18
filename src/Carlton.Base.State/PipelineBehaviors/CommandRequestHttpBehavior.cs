namespace Carlton.Base.State;

public abstract class CommandRequestHttpBehavior<TRequest, TCommand> : IPipelineBehavior<TRequest, TCommand>
    where TRequest : CommandRequest<TCommand>
    where TCommand : ICommand
{
    protected HttpClient Client { get; init; }
    private readonly ILogger<CommandRequestHttpBehavior<TRequest, TCommand>> _logger;

    protected CommandRequestHttpBehavior(HttpClient client, ILogger<CommandRequestHttpBehavior<TRequest, TCommand>> logger)
        => (Client, _logger) = (client, logger);

    public async Task<TCommand> Handle(TRequest request, RequestHandlerDelegate<TCommand> next, CancellationToken cancellationToken)
    {
        var attributes = request.Sender.GetType().GetCustomAttributes();
        var urlAttribute = attributes.OfType<CommandEndpointAttribute>().FirstOrDefault();
        var shouldCallSever = urlAttribute != null;

        if(shouldCallSever)
            await CallServer(urlAttribute.Route, request, cancellationToken);
        else
            Log.CommandRequestHttpCallSkipped(_logger, request.RequestName, request);


        return await next();
    }

    public async Task CallServer(string url, CommandRequest<TCommand> request, CancellationToken cancellationToken)
    {
        try
        {
            Log.CommandRequestHttpCallStarted(_logger, request.RequestName, request);
            var response = await Client.PostAsJsonAsync(url, request, cancellationToken);
            request.MarkAsServerCalled();
            response.EnsureSuccessStatusCode();
            Log.CommandRequestHttpCallCompleted(_logger, request.RequestName, request);
        }
        catch(Exception ex)
        {
            Log.CommandRequestHttpCallError(_logger, ex, request.RequestName, request);
            throw;
        }
    }
}
