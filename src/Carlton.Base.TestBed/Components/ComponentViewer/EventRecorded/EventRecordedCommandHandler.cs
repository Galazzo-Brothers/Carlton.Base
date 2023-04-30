namespace Carlton.Base.TestBed;

public sealed class EventRecordedCommandHandler : TestBedCommandRequestHandler<RecordEventCommand>
{
    public EventRecordedCommandHandler(ICommandProcessor processor) : base(processor)
    {
    }
}