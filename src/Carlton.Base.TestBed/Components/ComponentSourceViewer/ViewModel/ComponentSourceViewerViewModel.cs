namespace Carlton.Base.TestBed;

[ObserveStateEvents<TestBedStateEvents>(TestBedStateEvents.ComponentStateSelected)]
public sealed record ComponentSourceViewerViewModel(string ComponentSource);