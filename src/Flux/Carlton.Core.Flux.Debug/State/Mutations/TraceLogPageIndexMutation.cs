namespace Carlton.Core.Flux.Debug.State.Mutations;

internal class TraceLogPageIndexMutation : IFluxStateMutation<FluxDebugState, ChangeTraceLogPageCommand>
{
    public string StateEvent => FluxDebugStateEvents.TraceLogTablePageChanged.ToString();

    public FluxDebugState Mutate(FluxDebugState state, ChangeTraceLogPageCommand command)
    {
        var updatedTableState = state.TraceLogTableState with { CurrentPage = command.SelectedPageIndex };
        return state with { TraceLogTableState =  updatedTableState };
    }
}
