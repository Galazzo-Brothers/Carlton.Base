namespace Carlton.Base.TestBed;

[ObserveStateEvents<TestBedStateEvents>(TestBedStateEvents.ComponentStateSelected)]
public sealed record SourceViewerViewModelRequest : IViewModelRequest<SourceViewerViewModel>
{
}
