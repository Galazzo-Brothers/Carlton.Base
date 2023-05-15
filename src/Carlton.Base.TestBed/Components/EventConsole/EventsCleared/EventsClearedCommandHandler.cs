namespace Carlton.Base.TestBed;

public sealed class EventsClearedCommandHandler : TestBedCommandRequestHandler<ClearEventsCommand>
{
    public EventsClearedCommandHandler(ICommandProcessor processor, ILogger<TestBedCommandRequestHandler<ClearEventsCommand>> logger) : base(processor, logger)
    {
    }
}


