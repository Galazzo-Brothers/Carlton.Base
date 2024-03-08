namespace Carlton.Core.Flux.Debug.State.Mutations;

internal class EventLogTableRowsPerPageMutation : IFluxStateMutation<FluxDebugState, ChangeEventLogMessageTableRowsPerPageOptsCommand>
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
