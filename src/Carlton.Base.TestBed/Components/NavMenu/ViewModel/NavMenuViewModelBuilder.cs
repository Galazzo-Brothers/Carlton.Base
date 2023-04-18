namespace Carlton.Base.TestBed;
public class NavMenuViewModelBuilder
{
    private readonly List<TestComponent> _internalState;

    public NavMenuViewModelBuilder()
    {
        _internalState = new List<TestComponent>();
    }

    public NavMenuViewModelBuilder AddComponent<T>(object parameters)
    {
        return AddComponent<T>("Default", parameters);
    }


    public NavMenuViewModelBuilder AddComponent<T>(string displayName, object parameters)
    {
        var testComp = new TestComponent(displayName, typeof(T), false, parameters);
        _internalState.Add(testComp);

        return this;
    }

    public NavMenuViewModelBuilder AddCarltonComponent<T>(string displayName, object parameters)
    {
        var testComp = new TestComponent(displayName, typeof(T), true, parameters);
        _internalState.Add(testComp);

        return this;
    }

    public NavMenuViewModel Build()
    {
        var groups = new List<SelectGroup<NavMenuItem>>();

        foreach(var (group, groupIndex) in _internalState.GroupBy(_ => _.Type).WithIndex())
        {
            var selectItems = new List<SelectItem<NavMenuItem>>();

            foreach(var (state, stateIndex) in group.WithIndex())
            {
                var navItem = new NavMenuItem(state.DisplayName, state.Type, state.IsCarltonComponent, state.Parameters);
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


public record TestComponent
{
    public string DisplayName { get; private set; }
    public Type Type { get; init; }
    public bool IsCarltonComponent { get; init; }
    public object Parameters { get; init; }

    public TestComponent(string nodeTitle, Type type, bool isCarltonComponent, object parameters)
        => (DisplayName, Type, IsCarltonComponent, Parameters) = (nodeTitle, type, isCarltonComponent, parameters);

}