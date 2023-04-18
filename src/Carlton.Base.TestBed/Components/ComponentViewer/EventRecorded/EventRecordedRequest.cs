namespace Carlton.Base.TestBed;

public sealed class EventRecordedRequest : ComponentEventRequestBase<EventRecorded, ComponentViewerViewModel>
{
    public EventRecordedRequest(ICarltonComponent<ComponentViewerViewModel> sender, EventRecorded evt) : base(sender, evt)
    {
    }
}