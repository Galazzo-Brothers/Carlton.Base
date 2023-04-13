namespace Carlton.Base.TestBedFramework;

public class EventsClearedRequestFactory : IComponentEventRequestFactory<EventsCleared, EventConsoleViewModel>
{
    public IComponentEventRequest<EventsCleared, EventConsoleViewModel> CreateEventRequest(ICarltonComponent<EventConsoleViewModel> sender, EventsCleared evt)
    {
        return new EventsClearedRequest(sender, evt);
    }
}

