namespace Carlton.Base.TestBedFramework;


[ObserveStateEvents<TestBedStateEvents>(TestBedStateEvents.SelectedItem, TestBedStateEvents.ViewModelChanged)]
public sealed class ComponentViewerViewModelRequest : IViewModelRequest<ComponentViewerViewModel>
{
}

