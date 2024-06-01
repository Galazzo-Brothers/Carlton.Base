namespace Carlton.Core.Flux.Debug.State;

internal interface IFluxStateWrapper
{
	public object CurrentState { get; }
	public IEnumerable<RecordedMutation> RecordedMutations { get; }
	public object Replay(int count);
}