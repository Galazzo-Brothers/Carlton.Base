namespace Carlton.Base.State;

public class TransactionalStateProcessor<TState> : IStateProcessor<TState>
{
    private readonly IStateProcessor<TState> _decorated;
    private readonly ILogger<TransactionalStateProcessor<TState>> _logger;

    private Memento<TState> BeforeState { get; set; }

    public TransactionalStateProcessor(IStateProcessor<TState> stateProcessor, ILogger<TransactionalStateProcessor<TState>> logger)
        => (_decorated, _logger) = (stateProcessor, logger);

    public async Task ProcessCommand<TCommand>(object sender, TCommand command)
    {
        try
        {
            BeforeState = _decorated.SaveState();
            Log.CommandRequestProcessingStarted(_logger, typeof(TCommand).GetDisplayName(), command, BeforeState);
            await _decorated.ProcessCommand(sender, command);
            var afterState = _decorated.SaveState();
            Log.CommandRequestProcessingCompleted(_logger, typeof(TCommand).GetDisplayName(), command, afterState);
        }
        catch(Exception ex)
        {
            Log.CommandRequestProcessingError(_logger, ex, typeof(TCommand).GetDisplayName(), command);
            _decorated.RestoreState(BeforeState);
            throw;
        }
    }

    public void RestoreState(Memento<TState> memento)
    {
        _decorated.RestoreState(memento);
    }

    public Memento<TState> SaveState()
    {
        return _decorated.SaveState();
    }
}