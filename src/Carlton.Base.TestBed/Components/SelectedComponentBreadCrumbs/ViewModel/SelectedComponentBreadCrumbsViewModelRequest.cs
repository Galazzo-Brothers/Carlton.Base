namespace Carlton.Base.TestBed;

[ObserveStateEvents<TestBedStateEvents>(TestBedStateEvents.ComponentStateSelected)]
public sealed class SelectedComponentBreadCrumbsViewModelRequest : IViewModelRequest<SelectedComponentBreadCrumbsViewModel>
{
}

