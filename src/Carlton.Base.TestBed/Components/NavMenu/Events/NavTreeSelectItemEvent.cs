namespace Carlton.Base.TestBedFramework;

public record NavTreeSelectedItemEvent(int SelectedGroupID, int SelectedItemID);

public class NavTreeSelectNodeRequest : ComponentEventRequestBase<NavTreeSelectedItemEvent>
{
    public NavTreeSelectNodeRequest(object sender, NavTreeSelectedItemEvent componentEvent)
        :base(sender, componentEvent)
    {
    }
}

public class NavTreeSelectNodeRequestHandler : TestBedEventRequestHandlerBase<NavTreeSelectNodeRequest>
{
    public NavTreeSelectNodeRequestHandler(TestBedState state) : base(state)
    {
    }

    public async override Task<Unit> Handle(NavTreeSelectNodeRequest request, CancellationToken cancellationToken)
    {
        await State.UpdateSelectedItemId(request.Sender, request.ComponentEvent.SelectedGroupID, request.ComponentEvent.SelectedItemID);
        return Unit.Value;
    }
}
