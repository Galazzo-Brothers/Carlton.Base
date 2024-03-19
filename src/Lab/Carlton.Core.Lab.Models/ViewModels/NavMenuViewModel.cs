using Carlton.Core.Utilities.Validation;
namespace Carlton.Core.Lab.Models.ViewModels;

/// <summary>
/// Represents the view model for the navigation menu.
/// </summary>
public sealed record NavMenuViewModel
{
	/// <summary>
	/// Gets or initializes the menu items in the navigation menu.
	/// </summary>
	[Required]
	public IEnumerable<ComponentConfigurations> MenuItems { get; init; } = new List<ComponentConfigurations>();

	/// <summary>
	/// Gets or initializes the index of the selected component in the navigation menu.
	/// </summary>
	[NonNegativeInteger]
	public int SelectedComponentIndex { get; init; }

	/// <summary>
	/// Gets or initializes the index of the selected state in the navigation menu.
	/// </summary>
	[NonNegativeInteger]
	public int SelectedStateIndex { get; init; }
};


