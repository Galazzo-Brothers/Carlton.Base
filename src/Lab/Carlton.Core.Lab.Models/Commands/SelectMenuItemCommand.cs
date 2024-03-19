using Carlton.Core.Utilities.Validation;
namespace Carlton.Core.Lab.Models.Commands;

/// <summary>
/// Represents a command to select a menu item.
/// </summary>
public sealed record SelectMenuItemCommand
{
	/// <summary>
	/// Gets or initializes the index of the selected component.
	/// </summary>
	[NonNegativeInteger]
	public int ComponentIndex { get; init; }

	/// <summary>
	/// Gets or initializes the index of the selected component state.
	/// </summary>
	[NonNegativeInteger]
	public int ComponentStateIndex { get; init; }
};
