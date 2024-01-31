namespace Carlton.Core.Flux.Debug.State.Mutations;

internal class EventLogFilterTextMutation : FluxStateMutationBase<FluxDebugState, ChangeEventLogFilterTextCommand>
{
    public override string StateEvent => FluxDebugStateEvents.EventLogFilterTextChanged.ToString();

    public override FluxDebugState Mutate(FluxDebugState state, ChangeEventLogFilterTextCommand command)
    {
        var updatedFilterState = state.EventLogViewerFilterState with { FilterText = command.FilterText.ToLower() };
        return state with { EventLogViewerFilterState = updatedFilterState };
    }
}
