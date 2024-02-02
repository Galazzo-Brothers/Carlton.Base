namespace Carlton.Core.Flux.Debug.State.Mutations;

internal class EventLogTableRowsPerPageMutation : FluxStateMutationBase<FluxDebugState, ChangeEventLogMessageTableRowsPerPageOptsCommand>
{
    public override string StateEvent => FluxDebugStateEvents.EventLogRowsPerPageChanged.ToString();

    public override FluxDebugState Mutate(FluxDebugState state, ChangeEventLogMessageTableRowsPerPageOptsCommand command)
    {
        var updatedTableState = state.EventLogTableState with { SelectedRowsPerPageOptsIndex = command.SelectedRowsPerPageIndex };
        return state with { EventLogTableState = updatedTableState };
    }
}
