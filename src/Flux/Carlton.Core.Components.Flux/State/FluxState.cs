namespace Carlton.Core.Components.Flux.State;

public class FluxState<TState> : IMutableFluxState<TState>
{
    private readonly IList<string> _recordedEventStore = new List<string>();
    private readonly MutationResolver<TState> _mutationResolver;
    private readonly ILogger<FluxState<TState>> _logger;

    public event Func<string, Task> StateChanged;
    public IReadOnlyList<string> RecordedEventStore { get => _recordedEventStore.AsReadOnly(); }
    public TState State { get; private set; }
    private TState RollbackState { get; set; }

    public FluxState(TState state, MutationResolver<TState> mutationResolver, ILogger<FluxState<TState>> logger)
       => (State, _mutationResolver, _logger) = (state, mutationResolver, logger);

    public async Task MutateState<TCommand>(TCommand command)
    {
        try
        {
            //Find the correct mutations and save a rollback state
           // Log.CommandRequestProcessingStarted(_logger, typeof(TCommand).GetDisplayName(), command, RollbackState);
            var mutation = _mutationResolver.Resolve<TCommand>();
            SetRollbackState();

            //Run the non-destructive mutation to generate a new state from the old, 
            //and replace the old state with the new
            var mutatedState = mutation.Mutate(State, command);
            mutatedState.Adapt(State);

            //Update EventStore and raise event
            _recordedEventStore.Add(mutation.StateEvent);
            await InvokeStateChanged(mutation.StateEvent);
          //  Log.CommandRequestProcessingCompleted(_logger, typeof(TCommand).GetDisplayName(), command, State);
        }
        catch(Exception ex)
        {
           // Log.CommandRequestProcessingError(_logger, ex, typeof(TCommand).GetDisplayName(), command);
            Rollback();
            throw;
        }
    }

    private void SetRollbackState()
    {
        State.Adapt(RollbackState);
    }

    private void Rollback()
    {
        RollbackState.Adapt(State);
    }

    private async Task InvokeStateChanged(string evt)
    {
        await StateChanged.Invoke(evt);
    }
}
