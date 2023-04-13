namespace Carlton.Base.TestBedFramework;

public class EventsClearedRequestHandler : TestBedEventRequestHandlerBase<EventsClearedRequest>
{
    public EventsClearedRequestHandler(TestBedState state) : base(state)
    {
    }

    public async override Task<Unit> Handle(EventsClearedRequest request, CancellationToken cancellationToken)
    {
        await State.ClearComponentEvents(request.Sender);
        return Unit.Value;
    }
}


