namespace Carlton.Base.State;

public class UtilityCommandDecorator : ICommandDispatcher
{
    private readonly ICommandDispatcher _decorated;
    private readonly ILogger<UtilityCommandDecorator> _logger;

    public UtilityCommandDecorator(ICommandDispatcher decorated, ILogger<UtilityCommandDecorator> logger)
        => (_decorated, _logger) = (decorated, logger);

    public async Task<Unit> Dispatch<TCommand>(CommandRequest<TCommand> request, CancellationToken cancellationToken)
    {
        try
        {
            Log.CommandRequestHandlingStarted(_logger, request.DisplayName, request.Command);
            await _decorated.Dispatch(request, cancellationToken);
            request.MarkCompleted();
            Log.CommandRequestHandlingCompleted(_logger, request.DisplayName, request.Command);
            return Unit.Value;
        }
        catch(Exception ex)
        {
            throw new CommandException<TCommand>(request, ex);
        }
    }
}
