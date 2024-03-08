namespace Carlton.Core.Flux.Debug.State.Mutations;

public class SelectedTraceLogMessageMutation : IFluxStateMutation<FluxDebugState, ChangeSelectedTraceLogMessageCommand>
{
    public string StateEvent => FluxDebugStateEvents.SelectedTraceLogMessageChanged.ToString();

    public FluxDebugState Mutate(FluxDebugState state, ChangeSelectedTraceLogMessageCommand command)
    {
        return state with { SelectedTraceLogMessageIndex = command.SelectedTraceLogMessageIndex };
    }
}
