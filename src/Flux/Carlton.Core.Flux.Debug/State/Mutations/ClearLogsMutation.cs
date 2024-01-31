namespace Carlton.Core.Flux.Debug.State.Mutations;

public class ClearLogsMutation : FluxStateMutationBase<FluxDebugState, LogsClearedCommand> 
{
    public override string StateEvent => FluxDebugStateEvents.LogsCleared.ToString();

    public override FluxDebugState Mutate(FluxDebugState originalState, LogsClearedCommand command)
    {
        return originalState with { LogMessages = new List<LogMessage>() };
    }
}


 
