namespace Carlton.Core.Flux.Debug.State.Mutations;

public class ChangeSelectedLogMessageMutation : FluxStateMutationBase<FluxDebugState, ChangeSelectedLogMessageCommand>
{
    public override string StateEvent => FluxDebugStateEvents.SelectedLogMessageChanged.ToString();

    public override FluxDebugState Mutate(FluxDebugState state, ChangeSelectedLogMessageCommand command)
    {
        return state with { SelectedLogMessage = command.SelectedLogMessage };
    }
}
