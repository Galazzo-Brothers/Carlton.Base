namespace Carlton.Base.TestBed;

[ObserveStateEvents<TestBedStateEvents>(TestBedStateEvents.SelectedItem)]
public sealed record SourceViewerViewModelRequest : IViewModelRequest<SourceViewerViewModel>
{
}
