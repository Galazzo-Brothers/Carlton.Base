namespace Carlton.Base.TestBed;

public sealed record NavMenuViewModel
(
    IEnumerable<SelectGroup<ComponentState>> MenuItems,
    ComponentState SelectedItem
);

