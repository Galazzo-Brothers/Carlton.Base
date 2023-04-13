namespace Carlton.Base.TestBedFramework;

public class EventRecordedRequestHandler : TestBedEventRequestHandlerBase<EventRecordedRequest>
{
    public EventRecordedRequestHandler(TestBedState state) : base(state) { }

    public async override Task<Unit> Handle(EventRecordedRequest request, CancellationToken cancellationToken)
    {
        await State.AddTestComponentEvents(request.Sender, request.ComponentEvent.Evt);
        return Unit.Value;
    }
}