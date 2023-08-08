using Carlton.Core.Components.Flux.Mutations;

namespace Carlton.Core.Components.Lab.State.Mutations;


public class RecordEventMutation : IStateMutation<LabState, LabStateEvents, RecordEventCommand>
{
    public LabStateEvents StateEvent => LabStateEvents.EventRecorded;

    public IStateStore<LabStateEvents> Mutate(LabState currentState, object sender, RecordEventCommand command)
    {
        var newEvents = currentState.ComponentEvents.Append(new ComponentRecordedEvent(command.RecordedEventName, command.EventArgs));
        return currentState with { ComponentEvents = newEvents };
    }
}
