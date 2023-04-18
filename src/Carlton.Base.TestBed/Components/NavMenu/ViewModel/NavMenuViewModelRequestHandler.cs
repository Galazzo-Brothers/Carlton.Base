namespace Carlton.Base.TestBedFramework;

public sealed class NavMenuViewModelRequestHandler : TestBedRequestHandlerViewModelBase<NavMenuViewModelRequest, NavMenuViewModel>
{
    public NavMenuViewModelRequestHandler(TestBedState state)
        :base(state)
    {
    }

    public override Task<NavMenuViewModel> Handle(NavMenuViewModelRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new NavMenuViewModel(State.NavMenuItems, State.SelectedItem));
    }
}
