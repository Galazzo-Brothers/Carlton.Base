namespace Carlton.Base.TestBedFramework;

public class NavTreeViewModelRequestHandler : TestBedRequestHandlerViewModelBase<NavTreeViewModelRequest, NavTreeViewModel>
{
    public NavTreeViewModelRequestHandler(TestBedState state)
        :base(state)
    {
    }

    public override Task<NavTreeViewModel> Handle(NavTreeViewModelRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new NavTreeViewModel(State.TreeItems, State.SelectedItem,
            (IEnumerable<TreeItem<NavTreeItemModel>>)(NavTreeViewModel) null));
    }
}
