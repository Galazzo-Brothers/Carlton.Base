namespace Carlton.Base.TestBedFramework;

public class EventRecordedRequestFactory : IComponentEventRequestFactory<EventRecorded, ComponentViewerViewModel>
{
    public IComponentEventRequest<EventRecorded, ComponentViewerViewModel> CreateEventRequest(ICarltonComponent<ComponentViewerViewModel> sender, EventRecorded evt)
    {
        return new EventRecordedRequest(sender, evt);
    }
}

