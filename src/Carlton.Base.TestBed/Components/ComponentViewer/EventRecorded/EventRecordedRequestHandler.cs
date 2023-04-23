namespace Carlton.Base.TestBed;

public sealed class EventRecordedRequestHandler : TestBedRequestHandlerBase, IRequestHandler<CommandRequest<EventRecorded>>
{
    public EventRecordedRequestHandler(TestBedState state) : base(state)
    {
    }

    public async Task Handle(CommandRequest<EventRecorded> request, CancellationToken cancellationToken)
    {
        await State.RecordComponentEvent(request.Sender, request.Command.Name, request.Command.Obj);
    }
}