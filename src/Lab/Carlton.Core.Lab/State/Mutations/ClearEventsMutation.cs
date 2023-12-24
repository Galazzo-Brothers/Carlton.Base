namespace Carlton.Core.Lab.State.Mutations;

public class ClearEventsMutation : IFluxStateMutation<LabState, ClearEventsCommand> 
{
    public bool IsRefreshMutation => false;
    public string StateEvent => LabStateEvents.EventsCleared.ToString();

    public LabState Mutate(LabState originalState, ClearEventsCommand command)
    {
        return originalState with { ComponentEvents = new List<ComponentRecordedEvent>() };
    }
}


 
