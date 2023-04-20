namespace Carlton.Base.TestBed;

public record NavMenuViewModel
(
    IEnumerable<SelectGroup<RegisteredComponentState>> MenuItems,
    RegisteredComponentState SelectedItem
);

