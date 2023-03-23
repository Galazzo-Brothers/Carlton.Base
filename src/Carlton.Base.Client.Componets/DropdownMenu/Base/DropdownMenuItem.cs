namespace Carlton.Base.Components;

public record DropdownMenuItem<T>(string MenuItemName, T Value, string MenuIcon, int AccentColorIndex);
