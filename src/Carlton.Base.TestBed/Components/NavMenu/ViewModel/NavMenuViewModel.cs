namespace Carlton.Base.TestBed;

public sealed record NavMenuViewModel
(
    IEnumerable<SelectGroup<RegisteredComponentState>> MenuItems,
    RegisteredComponentState SelectedItem
);

