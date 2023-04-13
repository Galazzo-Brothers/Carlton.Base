namespace Carlton.Base.TestBedFramework;

public class EventRecordedRequest : ComponentEventRequestBase<EventRecorded, ComponentViewerViewModel>
{
    public EventRecordedRequest(object sender, EventRecorded evt) : base(sender, evt)
    {
    }
}