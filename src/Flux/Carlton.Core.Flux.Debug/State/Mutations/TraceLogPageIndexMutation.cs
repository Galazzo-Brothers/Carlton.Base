namespace Carlton.Core.Flux.Debug.State.Mutations;

internal class TraceLogPageIndexMutation : FluxStateMutationBase<FluxDebugState, ChangeTraceLogPageCommand>
{
    public override string StateEvent => FluxDebugStateEvents.TraceLogTablePageChanged.ToString();

    public override FluxDebugState Mutate(FluxDebugState state, ChangeTraceLogPageCommand command)
    {
        var updatedTableState = state.TraceLogTableState with { CurrentPage = command.SelectedPageIndex };
        return state with { TraceLogTableState =  updatedTableState };
    }
}
