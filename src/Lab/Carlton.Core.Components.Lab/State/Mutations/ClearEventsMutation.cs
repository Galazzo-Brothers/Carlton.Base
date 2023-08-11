namespace Carlton.Core.Components.Lab.State.Mutations;

public class ClearEventsMutation : IFluxStateMutation<LabState, ClearEventsCommand> 
{
    public string StateEvent => LabStateEvents.EventsCleared.ToString();

    public LabState Mutate(LabState originalState, ClearEventsCommand command)
    {
        return originalState with { ComponentEvents = new List<ComponentRecordedEvent>() };
    }
}


 
