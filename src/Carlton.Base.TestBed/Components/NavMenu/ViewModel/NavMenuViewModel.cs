namespace Carlton.Base.TestBed;

public record NavMenuViewModel
(
    IEnumerable<SelectGroup<NavMenuItem>> MenuItems,
    NavMenuItem SelectedItem
);

public record NavMenuItem(string DisplayName, Type Type, object ComponentParameters);
