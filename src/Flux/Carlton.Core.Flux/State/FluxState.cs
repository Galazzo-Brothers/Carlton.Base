namespace Carlton.Core.Flux.State;
public record RecordedMutation<TState>(Func<TState, object, TState> MutationFunc, object Command, string StateEvent);

public class FluxState<TState>(TState _state, IServiceProvider _provider)
    : IMutableFluxState<TState>
{
    public event Func<FluxStateChangedEventArgs, Task> StateChanged;

    private readonly Queue<RecordedMutation<TState>> _recordedMutations = [];
    public IReadOnlyCollection<RecordedMutation<TState>> RecordedMutations { get => _recordedMutations.ToList(); }

    public TState CurrentState { get => _state; }

    public async Task<Result<TCommand, FluxError>> ApplyMutationCommand<TCommand>(TCommand command)
    {
        try
        {
            //Find Mutation
            var mutation = _provider.GetService<IFluxStateMutation<TState, TCommand>>();

            //Return Error
            if (mutation == null)
                return new MutationNotRegisteredError(command.GetType().GetDisplayNameWithGenerics());

            //Apply Mutation
            _state = mutation.Mutate(CurrentState, command);

            //Record Mutation
            _recordedMutations.Enqueue(new RecordedMutation<TState>(mutationFunc, command, mutation.StateEvent));

            //Notify Listeners
            var args = new FluxStateChangedEventArgs(mutation.StateEvent);
            await (StateChanged?.GetInvocationList()?.RaiseAsyncDelegates(args) ?? Task.CompletedTask);

            //Return StateEvent
            return command;

            //Capture MutationFunc for auditing
            TState mutationFunc(TState state, object command) => mutation.Mutate(state, command);
        }
        catch(Exception ex)
        {
            return new MutationError(command.GetType().GetDisplayNameWithGenerics(), ex);
        }
    }
}

