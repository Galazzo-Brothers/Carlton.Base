namespace Carlton.Core.Flux.Debug.State.Mutations;

public class EventLogTableOrderingMutation : IFluxStateMutation<FluxDebugState, ChangeEventLogTableOrderingCommand>
{
	public string StateEvent => FluxDebugStateEvents.EventLogTableOrderingChanged.ToString();

	public FluxDebugState Mutate(FluxDebugState state, ChangeEventLogTableOrderingCommand command)
	{
		var updatedTableState = state.EventLogTableState with
		{
			OrderByColum = command.OrderByColum,
			OrderAscending = command.OrderAscending
		};
		return state with { EventLogTableState = updatedTableState };
	}
}
