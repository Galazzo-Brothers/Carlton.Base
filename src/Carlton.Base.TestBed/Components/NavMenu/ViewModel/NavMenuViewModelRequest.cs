namespace Carlton.Base.TestBedFramework;


[ObserveStateEvents<TestBedStateEvents>(TestBedStateEvents.SelectedItem)]
public sealed class NavMenuViewModelRequest : IViewModelRequest<NavMenuViewModel>
{
}