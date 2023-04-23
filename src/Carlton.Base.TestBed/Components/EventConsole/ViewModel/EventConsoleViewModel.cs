namespace Carlton.Base.TestBed;

[ObserveStateEvents<TestBedStateEvents>(TestBedStateEvents.ComponentEventRecorded)]
[ObserveStateEvents<TestBedStateEvents>(TestBedStateEvents.ComponentEventsCleared)]
public sealed record EventConsoleViewModel(IEnumerable<ComponentRecordedEvent> RecordedEvents);