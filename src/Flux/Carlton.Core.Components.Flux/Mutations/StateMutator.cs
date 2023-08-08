namespace Carlton.Core.Components.Flux.Mutations;

public class StateMutator<TState, TStateEvents> : IStateMutator
    where TState : IStateStoreEventDispatcher<TStateEvents>
    where TStateEvents : Enum
{
    private readonly TState _state;
    private readonly IServiceProvider _provider;
    private readonly ILogger<StateMutator<TState, TStateEvents>> _logger;

    public StateMutator(TState initialState, ILogger<StateMutator<TState, TStateEvents>> logger)
        => (_state, _logger) = (initialState, logger);


    public async Task Mutate<TCommand>(object sender, TCommand command)
    {
        var mutation = _provider.GetRequiredService<IStateMutation<TState, TStateEvents, TCommand>>();
        var rollbackState = SaveState();
        BeginTransactionalMutation(sender, command, mutation, rollbackState);
        await _state.InvokeStateChanged(sender, mutation.StateEvent);
    }

    private void BeginTransactionalMutation<TCommand>(object sender, TCommand command, IStateMutation<TState, TStateEvents, TCommand> mutation, Memento<TState> rollbackState)
    {
        try
        {
            rollbackState = SaveState();
            Log.CommandRequestProcessingStarted(_logger, typeof(TCommand).GetDisplayName(), command, rollbackState);
            var afterState = mutation.Mutate(_state, sender, command);
            afterState.Adapt(this);
            Log.CommandRequestProcessingCompleted(_logger, typeof(TCommand).GetDisplayName(), command, afterState);
        }
        catch (Exception ex)
        {
            Log.CommandRequestProcessingError(_logger, ex, typeof(TCommand).GetDisplayName(), command);
            RestoreState(rollbackState);
            throw;
        }
    }

    private void RestoreState(Memento<TState> memento)
    {
        memento.State.Adapt(_state);
    }

    private Memento<TState> SaveState()
    {
        return new Memento<TState>(_state);
    }
}