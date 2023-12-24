namespace Carlton.Core.Components.DropdownMenu;

public record DropdownMenuItem<T>(string MenuItemName, T Value, string MenuIcon, int AccentColorIndex);
