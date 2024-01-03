namespace Carlton.Core.Flux.Debug.State.Mutations;

public class ClearLocalStorageMutation : FluxStateMutationBase<FluxDebugState, ClearLocalStorageCommand> 
{
    public override string StateEvent => FluxDebugStateEvents.LocalStorageCleared.ToString();

    public override FluxDebugState Mutate(FluxDebugState originalState, ClearLocalStorageCommand command)
    {
        return originalState with { LogMessages = new List<LogMessage>() };
    }
}


 
