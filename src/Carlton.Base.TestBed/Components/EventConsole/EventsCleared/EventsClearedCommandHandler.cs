namespace Carlton.Base.TestBed;

public sealed class EventsClearedCommandHandler : CommandHandler<ClearEventsCommand>
{
    public EventsClearedCommandHandler(IStateProcessor processor) : base(processor)
    {
    }
}


