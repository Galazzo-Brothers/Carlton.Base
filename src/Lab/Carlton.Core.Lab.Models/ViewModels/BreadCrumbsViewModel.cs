namespace Carlton.Core.Lab.Models.ViewModels;

/// <summary>
/// Represents the view model for breadcrumbs.
/// </summary>
public record BreadCrumbsViewModel
{
	/// <summary>
	/// Gets or initializes the selected component for the breadcrumbs.
	/// </summary>
	[Required]
	public required string SelectedComponent { get; init; }

	/// <summary>
	/// Gets or initializes the selected state of the component for the breadcrumbs.
	/// </summary>
	[Required]
	public required string SelectedComponentState { get; init; }
};