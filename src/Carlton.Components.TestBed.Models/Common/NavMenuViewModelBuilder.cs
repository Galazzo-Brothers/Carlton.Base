namespace Carlton.Base.TestBed.Models;

internal sealed record NavMenuBuilderItemState(string DisplayName, Type ComponentType, object ComponentParameters, bool IsViewModelComponent);

public sealed class NavMenuViewModelBuilder
{
    private readonly List<NavMenuBuilderItemState> _internalState;

    public NavMenuViewModelBuilder()
    {
        _internalState = new List<NavMenuBuilderItemState>();
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
        var testComp = new NavMenuBuilderItemState(displayName, typeof(T), vm, isViewModel);
        _internalState.Add(testComp);

        return this;
    }

    public IEnumerable<ComponentState> Build()
    {
        return _internalState.Select(BuildComponentState);


        static ComponentState BuildComponentState(NavMenuBuilderItemState state)
        {
            var paramObjType = state.IsViewModelComponent ? ParameterObjectType.ViewModel : ParameterObjectType.ParameterObject;
            var compParams = new ComponentParameters(state.ComponentParameters, paramObjType);
            return new ComponentState(state.DisplayName, state.ComponentType, compParams);
        }
    }
}
