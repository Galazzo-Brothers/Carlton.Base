namespace Carlton.Base.State;

public interface IComponentEventRequestFactory
{
    public IComponentEventRequest CreateRequest<TComponentEvent, TViewModel>(ICarltonComponent<TViewModel> sender, TComponentEvent evt)
        where TComponentEvent : IComponentEvent<TViewModel>;

}
