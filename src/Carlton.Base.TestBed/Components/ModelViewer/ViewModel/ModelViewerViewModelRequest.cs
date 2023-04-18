namespace Carlton.Base.TestBedFramework;

[ObserveStateEvents<TestBedStateEvents>(TestBedStateEvents.SelectedItem)]
public sealed record ModelViewerViewModelRequest : IViewModelRequest<ModelViewerViewModel>
{
}


