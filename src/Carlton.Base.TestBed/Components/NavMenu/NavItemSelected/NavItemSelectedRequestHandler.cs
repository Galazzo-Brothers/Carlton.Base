namespace Carlton.Base.TestBed;

public sealed class NavItemSelectedRequestHandler : TestBedRequestHandlerBase, IRequestHandler<CommandRequest<NavItemSelected>>
{
    public NavItemSelectedRequestHandler(TestBedState state) : base(state)
    {
    }

    public async Task Handle(CommandRequest<NavItemSelected> request, CancellationToken cancellationToken)
    {
        await State.SelectComponentState(request.Sender, request.Command.SelectedGroupID, request.Command.SelectedItemID);
    }
}
