namespace Carlton.Base.State;
//todo consider renaming
public interface IComponentEventRequestFactory<TComponentEvent, TViewModel>
    where TComponentEvent : IComponentEvent<TViewModel>
{
    public IComponentEventRequest<TComponentEvent, TViewModel> CreateEventRequest(ICarltonComponent<TViewModel> sender, TComponentEvent evt);

}
