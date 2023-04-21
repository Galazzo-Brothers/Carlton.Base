namespace Carlton.Base.TestBed;


[ObserveStateEvents<TestBedStateEvents>(TestBedStateEvents.ComponentStateSelected)]
public sealed record NavMenuViewModelRequest : IViewModelRequest<NavMenuViewModel>
{
}