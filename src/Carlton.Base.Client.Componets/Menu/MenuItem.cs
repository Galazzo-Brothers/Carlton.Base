namespace Carlton.Base.Components;

public record MenuItems(IEnumerable<MenuItem> Items);
public record MenuItem(string MenuItemName, Func<Task> MenuItemEvent);
