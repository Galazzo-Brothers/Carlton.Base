namespace Carlton.Base.TestBed;


[ObserveStateEvents<TestBedStateEvents>(TestBedStateEvents.ComponentStateSelected)]
[ObserveStateEvents<TestBedStateEvents>(TestBedStateEvents.ParametersChanged)]
public sealed record ModelViewerViewModel(ComponentParameters ComponentParameters);
