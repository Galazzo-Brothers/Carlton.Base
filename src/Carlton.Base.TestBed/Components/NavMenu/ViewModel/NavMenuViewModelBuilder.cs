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

    public IEnumerable<IGrouping<Type, RegisteredComponentState>> Build()
    {
        return _internalState.Select(_ =>
        {
            var paramObjType = _.IsViewModelComponent ? ParameterObjectType.ViewModel : ParameterObjectType.ParameterObject;
            var compParams = new ComponentParameters(_.ComponentParameters, paramObjType);
            return new RegisteredComponentState(_.DisplayName, _.ComponentType, compParams);
        }).GroupBy(_ => _.Type);
    }
}
