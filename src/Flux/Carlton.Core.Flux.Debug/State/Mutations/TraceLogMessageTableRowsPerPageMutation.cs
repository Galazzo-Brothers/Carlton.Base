using System.Net.Http.Headers;

namespace Carlton.Core.Flux.Debug.State.Mutations;

internal class TraceLogMessageTableRowsPerPageMutation : FluxStateMutationBase<FluxDebugState, ChangeTraceLogMessageTableRowsPerPageOptsCommand>
{
    public override string StateEvent => FluxDebugStateEvents.TraceLogTableRowsPerPageChanged.ToString();

    public override FluxDebugState Mutate(FluxDebugState state, ChangeTraceLogMessageTableRowsPerPageOptsCommand command)
    {
        var updatedTableState = state.TraceLogTableState with { SelectedRowsPerPageOptsIndex = command.SelectedRowsPerPageIndex };
        return state with { TraceLogTableState = updatedTableState };
    }
}
