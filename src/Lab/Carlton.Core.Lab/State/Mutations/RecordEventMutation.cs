namespace Carlton.Core.Lab.State.Mutations;

public class RecordEventMutation : FluxStateMutationBase<LabState, RecordEventCommand>
{
    public override string StateEvent => LabStateEvents.EventRecorded.ToString();

    public override LabState Mutate(LabState currentState, RecordEventCommand command)
    {
        var newEvents = currentState.ComponentEvents.Append(new ComponentRecordedEvent(command.RecordedEventName, command.EventArgs));
        return currentState with { ComponentEvents = newEvents };
    }
}
