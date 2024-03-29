namespace Carlton.Core.Flux.Debug.Components.Logging.EventLogging.LogTable;

internal sealed class EventLogTableOrderingMutation : IFluxStateMutation<FluxDebugState, ChangeEventLogTableOrderingCommand>
{
	public string StateEvent => FluxDebugStateEvents.EventLogTableOrderingChanged.ToString();

	public FluxDebugState Mutate(FluxDebugState state, ChangeEventLogTableOrderingCommand command)
	{
		var updatedTableState = state.EventLogTableState with
		{
			OrderByColum = command.OrderByColum,
			IsAscending = command.OrderAscending
		};
		return state with { EventLogTableState = updatedTableState };
	}
}
