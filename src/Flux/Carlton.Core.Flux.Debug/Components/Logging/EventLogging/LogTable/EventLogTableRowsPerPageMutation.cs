namespace Carlton.Core.Flux.Debug.Components.Logging.EventLogging.LogTable;

public sealed class EventLogTableRowsPerPageMutation : IFluxStateMutation<FluxDebugState, ChangeEventLogMessageTableRowsPerPageOptsCommand>
{
	public string StateEvent => FluxDebugStateEvents.EventLogRowsPerPageChanged.ToString();

	public FluxDebugState Mutate(FluxDebugState state, ChangeEventLogMessageTableRowsPerPageOptsCommand command)
	{
		var updatedTableState = state.EventLogTableState with
		{
			CurrentPage = 1,
			SelectedRowsPerPageOptsIndex = command.SelectedRowsPerPageIndex
		};
		return state with { EventLogTableState = updatedTableState };
	}
}
