namespace Carlton.Base.TestBed;

public sealed class EventsClearedCommandHandler : TestBedCommandRequestHandler<ClearEventsCommand>
{
    public EventsClearedCommandHandler(IStateProcessor processor, ILogger<TestBedCommandRequestHandler<ClearEventsCommand>> logger) : base(processor, logger)
    {
    }
}


