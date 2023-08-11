namespace Carlton.Core.Components.Lab.Models;

public sealed record RecordEventCommand(string RecordedEventName, object  EventArgs) : MutationCommand
{
    public RecordEventCommand(string recordedEventName) : this(recordedEventName, null)
    {
    }
}


