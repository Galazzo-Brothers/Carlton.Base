namespace Carlton.Base.TestBed;

public sealed class EventsClearedCommandHandler : TestBedCommandRequestHandler<ClearEventsCommand>
{
    public EventsClearedCommandHandler(ICommandProcessor processor) : base(processor)
    {
    }
}


