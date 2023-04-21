namespace Carlton.Base.TestBed;

public sealed class NavMenuViewModelRequestHandler : TestBedRequestHandlerViewModelBase<NavMenuViewModelRequest, NavMenuViewModel>
{
    public NavMenuViewModelRequestHandler(TestBedState state)
        :base(state)
    {
    }

    public override Task<NavMenuViewModel> Handle(NavMenuViewModelRequest request, CancellationToken cancellationToken)
    {
        var selectGroups = State.ComponentStates.Select((group, groupIndex) =>
        {
            var items = group.Select((item, itemIndex) => new SelectItem<ComponentState>(item.DisplayName, itemIndex, item));
            var displayName = group.Key.GetDisplayName();
            return new SelectGroup<ComponentState>(displayName, groupIndex, items);
        });

        return Task.FromResult(new NavMenuViewModel(selectGroups, State.SelectedComponentState));
    }
}
