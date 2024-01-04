namespace Carlton.Core.Flux.Debug.State.Mutations;

public class ChangeSelectedLogEntryMutation : FluxStateMutationBase<FluxDebugState, ChangeSelectedLogEntryCommand>
{
    public override string StateEvent => FluxDebugStateEvents.SelectedLogEntryChanged.ToString();

    public override FluxDebugState Mutate(FluxDebugState state, ChangeSelectedLogEntryCommand command)
    {
        return state with { SelectedLogEntry = command.SelectedLogEntry };
    }
}
