namespace Carlton.Core.Flux.Debug.State.Mutations;

public class SelectedTraceLogMessageMutation : FluxStateMutationBase<FluxDebugState, ChangeSelectedTraceLogMessageCommand>
{
    public override string StateEvent => FluxDebugStateEvents.SelectedTraceLogMessageChanged.ToString();

    public override FluxDebugState Mutate(FluxDebugState state, ChangeSelectedTraceLogMessageCommand command)
    {
        return state with { SelectedTraceLogMessageIndex = command.SelectedTraceLogMessageIndex };
    }
}
