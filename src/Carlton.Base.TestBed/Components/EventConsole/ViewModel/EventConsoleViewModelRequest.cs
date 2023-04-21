namespace Carlton.Base.TestBed;

[ObserveStateEvents<TestBedStateEvents>(TestBedStateEvents.ComponentEventRecorded)]
[ObserveStateEvents<TestBedStateEvents>(TestBedStateEvents.ComponentEventsCleared)]
public sealed record EventConsoleViewModelRequest : IViewModelRequest<EventConsoleViewModel>
{
}


