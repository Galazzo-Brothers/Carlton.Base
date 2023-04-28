namespace Carlton.Base.TestBed;

public sealed class EventRecordedRequestHandler : TestBedCommandRequestHandler<EventRecordedCommand>
{
    public EventRecordedRequestHandler(ICommandProcessor processor) : base(processor)
    {
    }
}