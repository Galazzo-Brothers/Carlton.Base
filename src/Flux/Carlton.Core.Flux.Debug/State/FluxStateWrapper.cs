using Carlton.Core.Flux.Internals.Contracts;
using Carlton.Core.Flux.Internals.State;
using System.Reflection;
namespace Carlton.Core.Flux.Debug.State;

internal class FluxStateWrapper<T>(IMutableFluxState<T> fluxState) : IFluxStateWrapper
{
	private readonly IMutableFluxState<T> _fluxState = fluxState;
	public object? CurrentState => _fluxState.CurrentState;

	public IEnumerable<RecordedMutation> RecordedMutations => _fluxState.RecordedMutations.Reverse().Select(x => new RecordedMutation
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

	public void PopMutation()
	{
		var internalQueueField = typeof(FluxState<T>).GetField("_recordedMutations", BindingFlags.Instance | BindingFlags.NonPublic);
		var internalQueue = (Stack<RecordedMutation<T>>)internalQueueField.GetValue(fluxState);
		internalQueue.Pop();

		var newState = Replay(RecordedMutations.Count() - 1);
		var propertyInfo = typeof(FluxState<T>).GetProperty(nameof(FluxState<T>.CurrentState), BindingFlags.Instance | BindingFlags.Public);
		propertyInfo.SetValue(_fluxState, newState);
	}

	public void ApplyMutation<TCommand>(TCommand command)
	{
		_fluxState.ApplyMutationCommand(command);
	}
}