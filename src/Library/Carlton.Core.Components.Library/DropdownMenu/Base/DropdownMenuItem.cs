namespace Carlton.Core.Components.Library;

public record DropdownMenuItem<T>(string MenuItemName, T Value, string MenuIcon, int AccentColorIndex);
