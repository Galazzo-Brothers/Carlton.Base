namespace Carlton.Base.TestBed;

public sealed class NavMenuViewModelRequestHandler : TestBedRequestHandlerViewModelBase<NavMenuViewModelRequest, NavMenuViewModel>
{
    public NavMenuViewModelRequestHandler(TestBedState state)
        :base(state)
    {
    }

    public override Task<NavMenuViewModel> Handle(NavMenuViewModelRequest request, CancellationToken cancellationToken)
    {
        var selectGroups = State.RegisteredComponentStates.Select((group, groupIndex) =>
        {
            var items = group.Select((item, itemIndex) => new SelectItem<RegisteredComponentState>(item.DisplayName, itemIndex, item));
            var displayName = FormatTypeName(group.Key.Name);
            return new SelectGroup<RegisteredComponentState>(displayName, groupIndex, items);
        });

        return Task.FromResult(new NavMenuViewModel(selectGroups, State.SelectedComponentState));
    }

    private string FormatTypeName(string str)
    {
        var strIndex = str.IndexOf("`");
        if(strIndex == -1)
            return str;

        return str[..strIndex];
    }
}
