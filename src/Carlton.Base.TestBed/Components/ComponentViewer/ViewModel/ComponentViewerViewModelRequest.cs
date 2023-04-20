namespace Carlton.Base.TestBed;


[ObserveStateEvents<TestBedStateEvents>(TestBedStateEvents.SelectedItem)]
[ObserveStateEvents<TestBedStateEvents>(TestBedStateEvents.ParametersChanged)]
public sealed record ComponentViewerViewModelRequest : IViewModelRequest<ComponentViewerViewModel>
{
}

