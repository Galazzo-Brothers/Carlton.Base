namespace Carlton.Core.Components.Lab.Models;
public sealed record RecordEventCommand(string RecordedEventName, object  EventArgs)
{
    public RecordEventCommand(string recordedEventName) : this(recordedEventName, null)
    {
    }
}


