namespace Carlton.Base.TestBed;

public sealed class NavItemSelectedRequestHandler : TestBedEventRequestHandlerBase<NavItemSelectedRequest>
{
    public NavItemSelectedRequestHandler(TestBedState state) : base(state)
    {
    }

    public async override Task<Unit> Handle(NavItemSelectedRequest request, CancellationToken cancellationToken)
    {
        await State.SelectComponentState(request.Sender, request.ComponentEvent.SelectedGroupID, request.ComponentEvent.SelectedItemID);
        return Unit.Value;
    }
}
