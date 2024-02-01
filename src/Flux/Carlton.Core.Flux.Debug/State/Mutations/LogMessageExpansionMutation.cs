namespace Carlton.Core.Flux.Debug.State.Mutations;

internal class LogMessageExpansionMutation : FluxStateMutationBase<FluxDebugState, ChangeLogMessageExpansionCommand>
{
    public override string StateEvent => FluxDebugStateEvents.TraceLogMessageExpandedChanged.ToString();

    public override FluxDebugState Mutate(FluxDebugState state, ChangeLogMessageExpansionCommand command)
    {
        var updatedList = state.ExpandedTraceLogMessageIndexes.ToList();
        
        if (command.IsExpanded)
            updatedList.Add(command.TraceLogMessageIndex);
        else
            updatedList.Remove(command.TraceLogMessageIndex);

        return state with { ExpandedTraceLogMessageIndexes = updatedList };    
    }
}
