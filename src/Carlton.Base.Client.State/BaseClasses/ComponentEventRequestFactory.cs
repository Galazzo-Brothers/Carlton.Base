
namespace Carlton.Base.State;

public class ComponentEventRequestFactory
{
    private readonly IServiceProvider _serviceProvider;

    public ComponentEventRequestFactory(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    public IComponentEventRequest<TComponentEvent, TViewModel> Create<TComponentEvent, TViewModel>(ICarltonComponent<TViewModel> sender, TComponentEvent evt)
        where TComponentEvent : IComponentEvent<TViewModel>
    {
        return _serviceProvider.GetService<IComponentEventRequestFactory<TComponentEvent, TViewModel>>()
                               .CreateEventRequest(sender, evt);
    }

}
