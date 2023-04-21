namespace Carlton.Base.TestBed;

public sealed class EventRecordedRequestHandler : TestBedEventRequestHandlerBase<EventRecordedRequest>
{
    public EventRecordedRequestHandler(TestBedState state) : base(state) { }

    public async override Task<Unit> Handle(EventRecordedRequest request, CancellationToken cancellationToken)
    {
        await State.RecordComponentEvent(request.Sender, request.ComponentEvent.Name, request.ComponentEvent.Obj);
        return Unit.Value;
    }
}