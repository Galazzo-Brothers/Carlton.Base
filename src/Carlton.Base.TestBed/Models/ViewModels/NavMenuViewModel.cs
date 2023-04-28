namespace Carlton.Base.TestBed;

public sealed record NavMenuViewModel
(
    IEnumerable<ComponentState> MenuItems,
    ComponentState SelectedItem
);

