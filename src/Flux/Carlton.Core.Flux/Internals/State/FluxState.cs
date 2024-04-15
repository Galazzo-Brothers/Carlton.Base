namespace Carlton.Core.Flux.Internals.State;
internal sealed record RecordedMutation<TState>(Func<TState, object, TState> MutationFunc, object Command, string StateEvent);

internal sealed class FluxState<TState>(TState _state, IServiceProvider _provider)
	: IMutableFluxState<TState>
{
	public event Func<FluxStateChangedEventArgs, Task> StateChanged;

	private readonly Queue<RecordedMutation<TState>> _recordedMutations = [];

	public IReadOnlyCollection<RecordedMutation<TState>> RecordedMutations { get => _recordedMutations.ToList(); }

	public TState CurrentState { get; private set; } = _state;

	private TState RollbackState { get; set; } = _state;

	private bool EventRecorded { get; set; } = false;

	public async Task<Result<string, FluxError>> ApplyMutationCommand<TCommand>(TCommand command)
	{
		try
		{
			//Find Mutation
			var mutation = _provider.GetService<IFluxStateMutation<TState, TCommand>>();

			//Return Error
			if (mutation == null)
				return MutationNotRegisteredError(command.GetType().GetDisplayNameWithGenerics());

			//Apply Mutation
			CurrentState = mutation.Mutate(CurrentState, command);

			//Record Mutation
			_recordedMutations.Enqueue(new RecordedMutation<TState>(mutationFunc, command, mutation.StateEvent));

			//Event Recorded
			EventRecorded = true;

			//Notify Listeners
			var args = new FluxStateChangedEventArgs(mutation.StateEvent);
			await (StateChanged?.GetInvocationList()?.RaiseAsyncDelegates(args) ?? Task.CompletedTask);

			//Update the Rollback State
			RollbackState = CurrentState;

			//Reset EventRecorded
			EventRecorded = false;

			//Return StateEvent
			return mutation.StateEvent;

			//Capture MutationFunc for auditing
			TState mutationFunc(TState state, object command) => mutation.Mutate(state, command);
		}
		catch (Exception ex)
		{
			//Rollback
			CurrentState = RollbackState;

			//The Event was recorded but rolled back
			if (EventRecorded)
				_recordedMutations.Dequeue();

			return MutationError(command.GetType().GetDisplayNameWithGenerics(), ex);
		}
	}
}

