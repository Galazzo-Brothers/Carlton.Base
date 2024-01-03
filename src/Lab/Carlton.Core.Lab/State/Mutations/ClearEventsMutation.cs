namespace Carlton.Core.Lab.State.Mutations;

public class ClearEventsMutation : FluxStateMutationBase<LabState, ClearEventsCommand> 
{
    public override string StateEvent => LabStateEvents.EventsCleared.ToString();

    public override LabState Mutate(LabState originalState, ClearEventsCommand command)
    {
        return originalState with { ComponentEvents = new List<ComponentRecordedEvent>() };
    }
}


 
