namespace Carlton.Base.State;

public class TransactionalStateProcessor<TState> : IStateProcessor<TState>
{
    private Memento<TState> BeforeState { get; set; }

    private readonly IStateProcessor<TState> _decorated;
    private readonly ILogger<TransactionalStateProcessor<TState>> _logger;

    public TransactionalStateProcessor(IStateProcessor<TState> stateProcessor, ILogger<TransactionalStateProcessor<TState>> logger)
        => (_decorated, _logger) = (stateProcessor, logger);

    public async Task ProcessCommand<TCommand>(object sender, TCommand command) where TCommand : ICommand
    {
        try
        {
            BeforeState = _decorated.SaveState();
            Log.CommandRequestProcessingStarted(_logger, command.CommandName, command, BeforeState);
            await _decorated.ProcessCommand(sender, command);
            var afterState = _decorated.SaveState();
            Log.CommandRequestProcessingCompleted(_logger, command.CommandName, command, afterState);
        }
        catch(Exception ex)
        {
            Log.CommandRequestProcessingError(_logger, ex, command.CommandName, command);
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