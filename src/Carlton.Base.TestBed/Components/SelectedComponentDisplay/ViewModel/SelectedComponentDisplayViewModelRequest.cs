namespace Carlton.Base.TestBed;

[ObserveStateEvents<TestBedStateEvents>(TestBedStateEvents.SelectedItem)]
public sealed class SelectedComponentDisplayViewModelRequest : IViewModelRequest<SelectedComponentDisplayViewModel>
{
}

