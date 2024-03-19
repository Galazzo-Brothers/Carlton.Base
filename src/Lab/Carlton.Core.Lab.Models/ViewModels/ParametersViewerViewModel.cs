namespace Carlton.Core.Lab.Models.ViewModels;

/// <summary>
/// Represents the view model for the parameters viewer.
/// </summary>
public sealed record ParametersViewerViewModel
{
	/// <summary>
	/// Gets or initializes the parameters of the component.
	/// </summary>
	[Required]
	public required object ComponentParameters { get; init; }
};
