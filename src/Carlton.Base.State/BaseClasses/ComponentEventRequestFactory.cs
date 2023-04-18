namespace Carlton.Base.State;

public class ComponentEventRequestFactory : IComponentEventRequestFactory
{
    private readonly IDictionary<Type, Type> _lookup = new Dictionary<Type, Type>();

    public ComponentEventRequestFactory(IEnumerable<Type> types)
    {
        foreach(var type in types)
        {
            var eventRequestType = type.GetInterfaces().FirstOrDefault(_ => _.GetGenericTypeDefinition().Equals(typeof(IComponentEventRequest<,>)))
                ?? throw new ArgumentException($"The types provided must implement {typeof(IComponentEventRequest<,>).Name}", nameof(types));

            var eventType = eventRequestType.GenericTypeArguments.First();
            _lookup.Add(eventType, type);
        }
    }

    public IComponentEventRequest CreateRequest<TComponentEvent, TViewModel>(ICarltonComponent<TViewModel> sender, TComponentEvent evt)
        where TComponentEvent : IComponentEvent<TViewModel>
    {
        var constructorParams = new object[] { sender, evt };
        var requestTypeFound = _lookup.TryGetValue(evt.GetType(), out var requestType);

        if(!requestTypeFound)
            throw new InvalidOperationException("The ComponentEvenetRequest type was not registered at startup.");

        return (IComponentEventRequest)Activator.CreateInstance(requestType, constructorParams);
    }
}
