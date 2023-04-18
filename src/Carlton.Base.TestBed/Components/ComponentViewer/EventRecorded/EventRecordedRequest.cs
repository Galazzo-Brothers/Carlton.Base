namespace Carlton.Base.TestBedFramework;

public sealed class EventRecordedRequest : ComponentEventRequestBase<EventRecorded, EventConsoleViewModel>
{
    public EventRecordedRequest(ICarltonComponent<EventConsoleViewModel> sender, EventRecorded evt) : base(sender, evt)
    {
    }
}