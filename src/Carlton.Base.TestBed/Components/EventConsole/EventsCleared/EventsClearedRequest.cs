namespace Carlton.Base.TestBedFramework;

public class EventsClearedRequest : ComponentEventRequestBase<EventsCleared, EventConsoleViewModel>
{
    public EventsClearedRequest(object sender, EventsCleared evt) : base(sender, evt)
    {
    }
}
