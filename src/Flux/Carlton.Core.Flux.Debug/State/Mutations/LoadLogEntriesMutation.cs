namespace Carlton.Core.Flux.Debug.State.Mutations;

public class LoadLogMessagesMutation : FluxStateMutationBase<FluxDebugState, LoadLogMessagesCommand>
{
    public override string StateEvent => FluxDebugStateEvents.LoadLogMessages.ToString();

    public override FluxDebugState Mutate(FluxDebugState state, LoadLogMessagesCommand command)
    {
        return state with { LogMessages = command.LogMessages };
    }
}
