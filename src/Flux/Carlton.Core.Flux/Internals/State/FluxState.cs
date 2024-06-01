namespace Carlton.Core.Flux.Internals.State;

internal sealed class FluxState<TState>
	: IMutableFluxState<TState>
{
	public event Func<FluxStateChangedEventArgs, Task> StateChanged;

	private readonly IServiceProvider _provider;
	private readonly Queue<RecordedMutation<TState>> _recordedMutations = [];

	public IEnumerable<RecordedMutation<TState>> RecordedMutations { get => _recordedMutations.ToList(); }

	public TState InitialState { get; init; }

	public TState CurrentState { get; private set; }

	private TState RollbackState { get; set; }

	private bool EventRecorded { get; set; } = false;

	private int MutationIndex { get; set; } = 0;

	public FluxState(TState state, IServiceProvider provider)
	{
		InitialState = CurrentState = RollbackState = state;
		_provider = provider;
		_recordedMutations.Enqueue(new RecordedMutation<TState>(MutationIndex, DateTime.Now, "Initial State", null, (state, cmd) => state));
		MutationIndex++;
	}

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
			_recordedMutations.Enqueue(new RecordedMutation<TState>(MutationIndex, DateTime.Now, mutation.StateEvent, command, mutationFunc));
			MutationIndex++;

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

