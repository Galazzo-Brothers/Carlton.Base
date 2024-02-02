namespace Carlton.Core.Flux.Debug.State.Mutations;

internal class EventLogPageIndexMutation : FluxStateMutationBase<FluxDebugState, ChangeEventLogPageCommand>
{
    public override string StateEvent => FluxDebugStateEvents.EventLogPageChanged.ToString();

    public override FluxDebugState Mutate(FluxDebugState state, ChangeEventLogPageCommand command)
    {
        var updatedTableState = state.EventLogTableState with { CurrentPage = command.SelectedPageIndex };
        return state with { EventLogTableState =  updatedTableState };
    }
}
