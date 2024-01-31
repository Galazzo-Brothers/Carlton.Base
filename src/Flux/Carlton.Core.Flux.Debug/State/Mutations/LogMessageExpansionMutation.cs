namespace Carlton.Core.Flux.Debug.State.Mutations;

internal class LogMessageExpansionMutation : FluxStateMutationBase<FluxDebugState, ChangeLogMessageExpansionCommand>
{
    public override string StateEvent => FluxDebugStateEvents.TraceLogMessageExpandedChanged.ToString();

    public override FluxDebugState Mutate(FluxDebugState state, ChangeLogMessageExpansionCommand command)
    {
        var list = state.TraceLogMessageGroups.SelectMany(tl => tl.FlattenedEntries).ToList();

        var index = list.FindIndex(tl => tl == command.TraceLogMessage);
        
        var itemToUpdate = list[index];
        var updatedItem = itemToUpdate with { IsExpanded = command.IsExpanded };

        list[index] = updatedItem;

        return state;
    }
}
