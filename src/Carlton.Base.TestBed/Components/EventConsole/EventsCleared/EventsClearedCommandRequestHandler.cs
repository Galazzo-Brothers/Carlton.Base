namespace Carlton.Base.TestBed;

public sealed class EventsClearedCommandRequestHandler : TestBedCommandRequestHandler<ClearEventsCommand>
{
    public EventsClearedCommandRequestHandler(ICommandProcessor processor) : base(processor)
    {
    }
}


