namespace Carlton.Core.Flux.Debug.State;

internal class FluxStateWrapper<T>(IFluxState<T> fluxState) : IFluxStateWrapper
{
	private readonly IFluxState<T> _fluxState = fluxState;
	public object? CurrentState => _fluxState.CurrentState;

	public IEnumerable<RecordedMutation> RecordedMutations => _fluxState.RecordedMutations.Select(x => new RecordedMutation
	{
		MutationIndex = x.MutationIndex,
		MutationDate = x.TimeStamp,
		MutationName = x.StateEvent,
		MutationCommand = x.Command
	});

	public object Replay(int count)
	{
		var currentState = _fluxState.InitialState;
		foreach (var mutation in _fluxState.RecordedMutations.Take(count))
			currentState = mutation.MutationFunc(currentState, mutation.Command);

		return currentState;
	}
}