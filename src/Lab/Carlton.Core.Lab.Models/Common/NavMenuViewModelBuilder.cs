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

    public NavMenuViewModelBuilder AddComponentState<T>(object parameterObj)
    {
        return AddComponentState<T>("Default", parameterObj);
    }

    public NavMenuViewModelBuilder AddComponentState<T>(string displayName, object parameterObj)
    {
        var testComp = new NavMenuBuilderItemState(displayName, typeof(T), parameterObj);
        _internalState.Add(testComp);

        return this;
    }

    public IEnumerable<ComponentAvailableStates> Build()
    {
        return _internalState.GroupBy(_ => _.ComponentType)
                      .Select(group => new ComponentAvailableStates
                      {
                          ComponentType = group.Key,
                          ComponentStates = group.Select(BuildComponentState),
                          IsExpanded = IsExpanded(group.Key)
                      });


        bool IsExpanded(Type type)
            => type == _internalState[0].ComponentType;

        static ComponentState BuildComponentState(NavMenuBuilderItemState state)
        {
            var compParams = new ComponentParameters(state.ComponentParameters);
            return new()
            {
                 DisplayName = state.DisplayName,
                 ComponentParameters = compParams
            };
        }
    }
}
