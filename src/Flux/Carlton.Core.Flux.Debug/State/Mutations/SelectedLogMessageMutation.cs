namespace Carlton.Core.Flux.Debug.State.Mutations;

public class SelectedLogMessageMutation : FluxStateMutationBase<FluxDebugState, ChangeSelectedLogMessageCommand>
{
    public override string StateEvent => FluxDebugStateEvents.SelectedLogMessageChanged.ToString();

    public override FluxDebugState Mutate(FluxDebugState state, ChangeSelectedLogMessageCommand command)
    {
        return state with { SelectedLogMessageIndex = command.SelectedLogMessageIndex };
    }
}
