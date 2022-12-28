namespace Carlton.Base.Components;

public record DropdownMenuItems(IEnumerable<DropdownMenuItem> Items);
public record DropdownMenuItem(string MenuItemName, string MenuIcon, int accentColorIndex, Func<Task> MenuItemEvent);
