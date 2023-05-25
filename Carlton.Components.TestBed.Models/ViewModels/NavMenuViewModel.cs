namespace Carlton.Base.TestBed.Models;

public sealed record NavMenuViewModel
(
    IEnumerable<ComponentState> MenuItems,
    ComponentState SelectedItem
);


