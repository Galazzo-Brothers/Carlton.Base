namespace Carlton.Base.TestBed;

[ObserveStateEvents<TestBedStateEvents>(TestBedStateEvents.ComponentStateSelected)]
public sealed record SelectedComponentBreadCrumbsViewModel(string SelectedComponent, string SelectedState);
