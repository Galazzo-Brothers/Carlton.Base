using Carlton.Core.Utilities.Validation;
namespace Carlton.Core.Lab.Models.ViewModels;

public sealed record NavMenuViewModel
{
	public IEnumerable<ComponentConfigurations> MenuItems = new List<ComponentConfigurations>();

	[NonNegativeInteger]
	public int SelectedComponentIndex { get; init; }

	[NonNegativeInteger]
	public int SelectedStateIndex { get; init; }
};


