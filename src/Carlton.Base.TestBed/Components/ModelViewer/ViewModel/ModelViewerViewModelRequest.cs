namespace Carlton.Base.TestBed;

[ObserveStateEvents<TestBedStateEvents>(TestBedStateEvents.ComponentStateSelected)]
[ObserveStateEvents<TestBedStateEvents>(TestBedStateEvents.ParametersChanged)]
public sealed record ModelViewerViewModelRequest : IViewModelRequest<ModelViewerViewModel>
{
}


