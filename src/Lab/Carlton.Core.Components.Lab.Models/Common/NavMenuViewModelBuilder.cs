namespace Carlton.Core.Components.Lab.Models;

internal sealed record NavMenuBuilderItemState(string DisplayName, Type ComponentType, object ComponentParameters, bool IsViewModelComponent);

public sealed class NavMenuViewModelBuilder
{
    private readonly List<NavMenuBuilderItemState> _internalState;

    public NavMenuViewModelBuilder()
    {
        _internalState = new List<NavMenuBuilderItemState>();
    }

    public NavMenuViewModelBuilder AddParameterObjComponent<T>()
    {
        return AddComponent<T>("Default", new object(), false);
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

    public IEnumerable<ComponentAvailableStates> Build()
    {
        return _internalState.GroupBy(_ => _.ComponentType)
                      .Select(group => new ComponentAvailableStates(group.Key, IsExpanded(group.Key), group.Select(BuildComponentState)));


        bool IsExpanded(Type type)
            => type == _internalState[0].ComponentType;

        static ComponentState BuildComponentState(NavMenuBuilderItemState state)
        {
            var paramObjType = state.IsViewModelComponent ? ParameterObjectType.ViewModel : ParameterObjectType.ParameterObject;
            var compParams = new ComponentParameters(state.ComponentParameters, paramObjType);
            return new ComponentState(state.DisplayName, state.ComponentType, compParams);
        }
    }
}
