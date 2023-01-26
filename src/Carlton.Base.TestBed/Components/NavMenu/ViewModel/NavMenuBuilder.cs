namespace Carlton.Base.TestBedFramework;
public class NavMenuBuilder
{
    private readonly List<TestComponent> _internalState;

    public NavMenuBuilder()
    {
        _internalState = new List<TestComponent>();
    }

    public NavMenuBuilder AddCarltonComponent<T>(string displayName, Action<TestStateBuilder> builder)
    {
        var testComp = new TestComponent(displayName, typeof(T), true);
        builder(new TestStateBuilder(testComp));
        _internalState.Add(testComp);

        return this;
    }

    public NavMenuBuilder AddComponent<T>(string displayName, Action<TestStateBuilder> builder)
    {   
        var testComp = new TestComponent(displayName, typeof(T), false);
        builder(new TestStateBuilder(testComp));
        _internalState.Add(testComp);

        return this;
    }

    public NavMenuViewModel Build()
    {
        var groups = new List<SelectGroup<NavMenuItem>>();

        foreach(var (comp, compIndex) in _internalState.WithIndex())
        {
            var selectItems = new List<SelectItem<NavMenuItem>>();

            foreach(var (state, stateIndex) in comp.TestStates.WithIndex())
            {
                var navItem = new NavMenuItem(state.Key, comp.Type, comp.IsCarltonComponent, state.Value);
                selectItems.Add(new SelectItem<NavMenuItem>(state.Key, stateIndex, navItem));
            }


            groups.Add(new SelectGroup<NavMenuItem>(comp.DisplayName, compIndex, selectItems));
        }

        return new NavMenuViewModel
        (
            groups,
            groups.SelectItem(0, 0)
        );
    }
}


public class TestStateBuilder
{
    private readonly TestComponent _component;

    public TestStateBuilder(TestComponent component)
    {
        _component = component;
    }

    public TestStateBuilder WithTestState(string displayName, object value)
    {
        _component.TestStates.Add(displayName, value);
        return this;
    }
}

public record TestComponent
{
    public string DisplayName { get; private set; }
    public Type Type { get; init; }
    public bool IsCarltonComponent { get; init; }
    public Dictionary<string, object> TestStates { get; init; } = new Dictionary<string, object>();

    public TestComponent(string nodeTitle, Type type, bool isCarltonComponent)
        => (DisplayName, Type, IsCarltonComponent) = (nodeTitle, type, isCarltonComponent);

}