namespace Carlton.Core.Flux.Debug.State.Mutations;

public class ClearLocalStorageMutation : IFluxStateMutation<FluxDebugState, ClearLocalStorageCommand> 
{
    public bool IsRefreshMutation => false;
    public string StateEvent => FluxDebugStateEvents.LocalStorageCleared.ToString();

    public FluxDebugState Mutate(FluxDebugState originalState, ClearLocalStorageCommand command)
    {
        return originalState with { LogMessages = new List<LogMessage>() };
    }
}


 
