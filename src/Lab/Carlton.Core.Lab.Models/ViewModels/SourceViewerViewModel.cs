namespace Carlton.Core.Lab.Models.ViewModels;

/// <summary>
/// Represents the view model for the source viewer.
/// </summary>
public sealed record SourceViewerViewModel
{
	/// <summary>
	/// Gets or initializes the source of the component.
	/// </summary>
	[Required]
	public string ComponentSource { get; init; } = string.Empty;
}