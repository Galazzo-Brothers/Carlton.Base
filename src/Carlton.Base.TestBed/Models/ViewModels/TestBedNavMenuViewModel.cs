namespace Carlton.Base.TestBed;

public sealed record TestBedNavMenuViewModel
(
    IEnumerable<ComponentState> MenuItems,
    ComponentState SelectedItem
);


