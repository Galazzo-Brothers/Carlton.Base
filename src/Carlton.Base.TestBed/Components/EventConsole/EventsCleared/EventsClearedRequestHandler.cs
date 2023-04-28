namespace Carlton.Base.TestBed;

public sealed class EventsClearedRequestHandler : TestBedCommandRequestHandler<EventsClearedCommand>
{
    public EventsClearedRequestHandler(ICommandProcessor processor) : base(processor)
    {
    }
}


