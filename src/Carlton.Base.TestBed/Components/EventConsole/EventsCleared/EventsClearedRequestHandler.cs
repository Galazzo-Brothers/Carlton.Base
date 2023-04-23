namespace Carlton.Base.TestBed;

public sealed class EventsClearedRequestHandler : TestBedRequestHandlerBase, IRequestHandler<CommandRequest<EventsCleared>>
{
    public EventsClearedRequestHandler(TestBedState state) : base(state)
    {
    }

    public async Task Handle(CommandRequest<EventsCleared> request, CancellationToken cancellationToken)
    {
        await State.ClearComponentEvents(request.Sender);
    }
}


