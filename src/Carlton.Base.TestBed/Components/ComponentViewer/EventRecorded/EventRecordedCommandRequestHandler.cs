namespace Carlton.Base.TestBed;

public sealed class EventRecordedCommandRequestHandler : TestBedCommandRequestHandler<RecordEventCommand>
{
    public EventRecordedCommandRequestHandler(ICommandProcessor processor) : base(processor)
    {
    }
}