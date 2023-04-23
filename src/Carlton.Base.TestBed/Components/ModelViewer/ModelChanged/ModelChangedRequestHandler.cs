namespace Carlton.Base.TestBed;

public sealed class ModelChangedRequestHandler : TestBedRequestHandlerBase, IRequestHandler<CommandRequest<ModelChanged>>
{
    public ModelChangedRequestHandler(TestBedState state) : base(state)
    {
    }

    public async Task Handle(CommandRequest<ModelChanged> request, CancellationToken cancellationToken)
    {
        await State.UpdateSelectedComponentParameters(request.Sender, request.Command.ComponentParameters);
    }
}
