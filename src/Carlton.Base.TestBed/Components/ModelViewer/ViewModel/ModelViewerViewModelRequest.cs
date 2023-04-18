namespace Carlton.Base.TestBedFramework;

[ObserveStateEvents<TestBedStateEvents>(TestBedStateEvents.SelectedItem)]
public sealed class ModelViewerViewModelRequest : IViewModelRequest<ModelViewerViewModel>
{
}


