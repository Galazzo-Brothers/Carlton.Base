namespace Carlton.Core.Components.Dropdowns;

/// <summary>
/// Represents a menu item used in dropdown menus.
/// </summary>
/// <typeparam name="T">The type of value associated with the menu item.</typeparam>
public sealed record DropdownMenuItem<T>
{
	/// <summary>
	/// Gets the name of the menu item.
	/// </summary>
	public required string MenuItemName { get; init; }

	/// <summary>
	/// Gets the value associated with the menu item.
	/// </summary>
	public required T Value { get; init; }

	/// <summary>
	/// Gets the icon for the menu item.
	/// </summary>
	public required string MenuIcon { get; init; }

	/// <summary>
	/// Gets the index of the accent color for the menu item.
	/// </summary>
	public int AccentColorIndex { get; init; }

	/// <summary>
	/// Gets the action to be performed when the menu item is selected.
	/// </summary>
	public required Action MenuItemSelected { get; init; }
}