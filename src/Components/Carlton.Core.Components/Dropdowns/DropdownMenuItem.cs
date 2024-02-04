namespace Carlton.Core.Components.Dropdowns;

public record DropdownMenuItem<T>(string MenuItemName, T Value, string MenuIcon, int AccentColorIndex, Action MenuItemSelected);
