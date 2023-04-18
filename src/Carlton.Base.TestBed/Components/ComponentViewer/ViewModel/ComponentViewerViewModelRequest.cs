namespace Carlton.Base.TestBed;


[ObserveStateEvents<TestBedStateEvents>(TestBedStateEvents.SelectedItem, TestBedStateEvents.ViewModelChanged)]
public sealed record ComponentViewerViewModelRequest : IViewModelRequest<ComponentViewerViewModel>
{
}

