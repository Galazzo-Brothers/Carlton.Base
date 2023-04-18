namespace Carlton.Base.TestBed;

[ObserveStateEvents<TestBedStateEvents>(TestBedStateEvents.ComponentEventAdded)]
[ObserveStateEvents<TestBedStateEvents>(TestBedStateEvents.ComponentEventsCleared)]
public sealed record EventConsoleViewModelRequest : IViewModelRequest<EventConsoleViewModel>
{
}


