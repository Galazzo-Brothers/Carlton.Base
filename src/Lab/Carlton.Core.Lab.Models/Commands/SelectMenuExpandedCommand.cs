using Carlton.Core.Utilities.Validation;
namespace Carlton.Core.Lab.Models.Commands;

/// <summary>
/// Represents a command to select a menu item and expand or collapse it.
/// </summary>
public sealed record SelectMenuExpandedCommand
{
	/// <summary>
	/// Gets or initializes the index of the selected component.
	/// </summary>
	[NonNegativeInteger]
	public int SelectedComponentIndex { get; init; }

	/// <summary>
	/// Gets or initializes a value indicating whether the menu item is expanded or collapsed.
	/// </summary>
	public bool IsExpanded { get; init; }
};

