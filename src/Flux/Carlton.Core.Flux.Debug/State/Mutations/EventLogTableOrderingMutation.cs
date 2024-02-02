namespace Carlton.Core.Flux.Debug.State.Mutations;

internal class EventLogTableOrderingMutation : FluxStateMutationBase<FluxDebugState, ChangeEventLogTableOrderingCommand>
{
    public override string StateEvent => FluxDebugStateEvents.EventLogTableOrderingChanged.ToString();

    public override FluxDebugState Mutate(FluxDebugState state, ChangeEventLogTableOrderingCommand command)
    {
        var updatedTableState = state.EventLogTableState with 
            {
                OrderByColum = command.OrderByColum,
                OrderAscending = command.OrderAscending
            };
        return state with { EventLogTableState = updatedTableState };
    }
}
