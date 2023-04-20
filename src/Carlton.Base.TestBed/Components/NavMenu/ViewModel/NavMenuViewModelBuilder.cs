namespace Carlton.Base.TestBed;

public class NavMenuViewModelBuilder
{
    private readonly List<NavMenuBuilderTestComponent> _internalState;

    public NavMenuViewModelBuilder()
    {
        _internalState = new List<NavMenuBuilderTestComponent>();
    }

    public NavMenuViewModelBuilder AddComponent<T>(object parameters)
    {
        return AddComponent<T>("Default", parameters);
    }


    public NavMenuViewModelBuilder AddComponent<T>(string displayName, object parameters)
    {
        var testComp = new NavMenuBuilderTestComponent(displayName, typeof(T), parameters);
        _internalState.Add(testComp);

        return this;
    }

    //public NavMenuViewModelBuilder AddCarltonComponent<T>(string displayName, object parameters)
    //{
    //    var testComp = new TestComponent(displayName, typeof(T), true, parameters);
    //    _internalState.Add(testComp);

    //    return this;
    //}

    public NavMenuViewModel Build()
    {
        var groups = new List<SelectGroup<NavMenuItem>>();

        foreach(var (group, groupIndex) in _internalState.GroupBy(_ => _.ComponentType).WithIndex())
        {
            var selectItems = new List<SelectItem<NavMenuItem>>();

            foreach(var (state, stateIndex) in group.WithIndex())
            {
                var navItem = new NavMenuItem(state.DisplayName, state.ComponentType, state.ComponentParameters);
                selectItems.Add(new SelectItem<NavMenuItem>(state.DisplayName, stateIndex, navItem));
            }

            var displayName = group.Key.Name.ToDisplayFormat().RemoveTypeParamCharacters();
            groups.Add(new SelectGroup<NavMenuItem>(displayName, groupIndex, selectItems));
        }

        return new NavMenuViewModel
        (
            groups,
            groups.SelectItem(0, 0)
        );
    }
}
