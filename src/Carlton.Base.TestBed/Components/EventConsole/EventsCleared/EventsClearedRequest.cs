namespace Carlton.Base.TestBedFramework;

[ObserveStateEvents<TestBedStateEvents>(TestBedStateEvents.ComponentEventAdded)]
[ObserveStateEvents<TestBedStateEvents>(TestBedStateEvents.ComponentEventsCleared)]
public sealed class EventsClearedRequest : ComponentEventRequestBase<EventsCleared, EventConsoleViewModel>
{
    public EventsClearedRequest(ICarltonComponent<EventConsoleViewModel> sender, EventsCleared evt) : base(sender, evt)
    {
    }
}
