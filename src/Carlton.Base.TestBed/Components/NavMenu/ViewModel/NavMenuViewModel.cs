namespace Carlton.Base.TestBed;

[ObserveStateEvents<TestBedStateEvents>(TestBedStateEvents.ComponentStateSelected)]
public sealed record NavMenuViewModel
(
    IEnumerable<SelectGroup<ComponentState>> MenuItems,
    ComponentState SelectedItem
);

