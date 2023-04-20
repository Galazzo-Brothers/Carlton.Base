namespace Carlton.Base.TestBed;

public class NavMenuViewModelBuilder
{
    private readonly List<NavMenuBuilderTestComponent> _internalState;

    public NavMenuViewModelBuilder()
    {
        _internalState = new List<NavMenuBuilderTestComponent>();
    }

    public NavMenuViewModelBuilder AddParameterObjComponent<T>(object parameters)
    {
        return AddComponent<T>("Default", parameters, false);
    }

    public NavMenuViewModelBuilder AddParameterObjComponent<T>(string displayName, object parameters)
    {
        return AddComponent<T>(displayName, parameters, false);
    }

    public NavMenuViewModelBuilder AddViewModelComponent<T>(object vm)
    {
        return AddComponent<T>("Default", vm, true);
    }

    public NavMenuViewModelBuilder AddViewModelComponent<T>(string displayName, object vm)
    {
        return AddComponent<T>(displayName, vm, true);
    }

    public NavMenuViewModelBuilder AddComponent<T>(string displayName, object vm, bool isViewModel)
    {
        var testComp = new NavMenuBuilderTestComponent(displayName, typeof(T), vm, isViewModel);
        _internalState.Add(testComp);

        return this;
    }

    public NavMenuViewModel Build()
    {
        var groups = new List<SelectGroup<NavMenuItem>>();

        foreach(var (group, groupIndex) in _internalState.GroupBy(_ => _.ComponentType).WithIndex())
        {
            var selectItems = new List<SelectItem<NavMenuItem>>();

            foreach(var (state, stateIndex) in group.WithIndex())
            {
                var paramObjType = state.IsViewModelComponent ? ParameterObjectType.ViewModel : ParameterObjectType.ParameterObject;
                var componentParameters = new ComponentParameters(state.ComponentParameters, paramObjType);
                var navItem = new NavMenuItem(state.DisplayName, state.ComponentType, componentParameters);
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
