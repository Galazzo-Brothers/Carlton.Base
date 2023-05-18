namespace Carlton.Base.TestBed;

public sealed class EventRecordedCommandHandler : TestBedCommandRequestHandler<RecordEventCommand>
{
    public EventRecordedCommandHandler(IStateProcessor processor, ILogger<TestBedCommandRequestHandler<RecordEventCommand>> logger) : base(processor, logger)
    {
    }
}