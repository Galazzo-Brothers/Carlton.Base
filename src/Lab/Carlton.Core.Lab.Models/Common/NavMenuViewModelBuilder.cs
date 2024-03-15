namespace Carlton.Core.Lab.Models.Common;

internal sealed record NavMenuBuilderItemState(string DisplayName, Type ComponentType, object ComponentParameters);

public sealed class NavMenuViewModelBuilder
{
    private readonly List<NavMenuBuilderItemState> _internalState;

    public NavMenuViewModelBuilder()
    {
        _internalState = [];
    }

    public NavMenuViewModelBuilder AddComponent<T>()
    {
        return AddComponentState<T>("Default", new object());
    }

    public NavMenuViewModelBuilder AddComponentState<T>(object componentParameters)
    {
        return AddComponentState<T>("Default", componentParameters);
    }

    public NavMenuViewModelBuilder AddComponentState<T>(string displayName, object componentParameters)
    {
        var testComp = new NavMenuBuilderItemState(displayName, typeof(T), componentParameters);
        _internalState.Add(testComp);

        return this;
    }

    public IEnumerable<ComponentConfigurations> Build()
    {
        return _internalState.GroupBy(_ => _.ComponentType)
                      .Select(group => new ComponentConfigurations
                      {
                          ComponentType = group.Key,
                          ComponentStates = group.Select(BuildComponentState),
                          IsExpanded = IsExpanded(group.Key)
                      });


        bool IsExpanded(Type type)
            => type == _internalState[0].ComponentType;

        static ComponentState BuildComponentState(NavMenuBuilderItemState state)
        {
            return new()
            {
                 DisplayName = state.DisplayName,
                 ComponentParameters = state.ComponentParameters
            };
        }
    }
}
