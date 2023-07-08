namespace Carlton.Base.TestBed.Models;
public sealed record RecordEventCommand(string RecordedEventName, object  EventArgs)
{
    public RecordEventCommand(string recordedEventName) : this(recordedEventName, null)
    {
    }
}


