namespace Carlton.Base.TestBed;


[ObserveStateEvents<TestBedStateEvents>(TestBedStateEvents.SelectedItem)]
public sealed record NavMenuViewModelRequest : IViewModelRequest<NavMenuViewModel>
{
}