namespace Carlton.Base.TestBedFramework;
public record EventRecorded : ComponentEventBase<ComponentViewerViewModel>
{
    public object Evt { get; }

    public EventRecorded(ICarltonComponent<ComponentViewerViewModel> sender, object evt) : base(sender)
        => Evt = evt;
}