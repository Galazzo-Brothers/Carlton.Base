namespace Carlton.Base.TestBedFramework;


[ObserveStateEvents<TestBedStateEvents>(TestBedStateEvents.SelectedItem, TestBedStateEvents.ViewModelChanged)]
public sealed record ComponentViewerViewModelRequest : IViewModelRequest<ComponentViewerViewModel>
{
}

