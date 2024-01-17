using Carlton.Core.Utilities.Events.Extensions;
namespace Carlton.Core.Flux.State;

public record RecordedMutation<TState>(IFluxStateMutation<TState> Mutation, object Command);

public class FluxState<TState>(TState _state, IMutationResolver<TState> _resolver)
    : IMutableFluxState<TState>
{
    public event Func<FluxStateChangedEventArgs, Task> StateChanged;

    private readonly Queue<RecordedMutation<TState>> _recordedMutations = [];
    public IReadOnlyCollection<RecordedMutation<TState>> RecordedMutations { get => _recordedMutations.ToList(); }

    public TState CurrentState { get => _state; }

    public async Task<string> ApplyMutationCommand<TCommand>(TCommand command)
    {
        //Find Mutation
        var mutation = _resolver.Resolve(command.GetType()) ?? throw new InvalidOperationException($"Unable to find mutation for command: {command.GetType()}");

        //Apply Mutation
        _state = mutation.Mutate(CurrentState, command);

        //Record Mutation
        _recordedMutations.Enqueue(new RecordedMutation<TState>(mutation, command));

        //Notify Listeners
        var args = new FluxStateChangedEventArgs(mutation.StateEvent);
        await StateChanged?.GetInvocationList().RaiseAsyncDelegates(args);

        //Return StateEvent
        return mutation.StateEvent;
    }
}

