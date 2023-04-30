namespace Carlton.Base.TestBed;
public sealed record RecordEventCommand(string RecordedEventName, object  EventArgs) : ICommand
{
    public RecordEventCommand(string recordedEventName) : this(recordedEventName, null)
    {
    }
}


