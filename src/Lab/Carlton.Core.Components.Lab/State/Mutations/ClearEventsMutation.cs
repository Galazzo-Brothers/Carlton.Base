using Carlton.Core.Components.Flux.Mutations;

namespace Carlton.Core.Components.Lab.State.Mutations;

public class ClearEventsMutation : IStateMutation<LabState, LabStateEvents, ClearEventsCommand>
{
    public LabStateEvents StateEvent => LabStateEvents.EventsCleared;


    public IStateStore<LabStateEvents> Mutate(LabState currentState, object sender, ClearEventsCommand command)
    {
        return currentState with { ComponentEvents = new List<ComponentRecordedEvent>() };
    }
}
