namespace Carlton.Core.Components.Dropdowns;

/// <summary>
/// Represents a menu item used in dropdown menus.
/// </summary>
/// <typeparam name="T">The type of value associated with the menu item.</typeparam>
public sealed record DropdownMenuItem<T>(
    /// <summary>
    /// Gets the name of the menu item.
    /// </summary>
    string MenuItemName,

    /// <summary>
    /// Gets the value associated with the menu item.
    /// </summary>
    T Value,

    /// <summary>
    /// Gets the icon for the menu item.
    /// </summary>
    string MenuIcon,

    /// <summary>
    /// Gets the index of the accent color for the menu item.
    /// </summary>
    int AccentColorIndex,

    /// <summary>
    /// Gets the action to be performed when the menu item is selected.
    /// </summary>
    Action MenuItemSelected
);