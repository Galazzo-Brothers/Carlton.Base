namespace Carlton.Base.TestBed;

public sealed class ModelChangedRequestHandler : TestBedEventRequestHandlerBase<ModelChangedRequest>
{
    public ModelChangedRequestHandler(TestBedState state) : base(state)
    {
    }

    public async override Task<Unit> Handle(ModelChangedRequest request, CancellationToken cancellationToken)
    {
        await State.UpdateSelectedComponentParameters(request.Sender, request.ComponentEvent.ComponentParameters);
        return Unit.Value;
    }
}
