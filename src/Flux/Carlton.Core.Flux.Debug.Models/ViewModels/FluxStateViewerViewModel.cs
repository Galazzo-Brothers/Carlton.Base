namespace Carlton.Core.Flux.Debug.Models.ViewModels;

/// <summary>
/// Represents a view model for displaying Flux state information.
/// </summary>
public sealed class FluxStateViewerViewModel
{
	/// <summary>
	/// Gets or sets the Flux state object.
	/// </summary>
	[Required]
	public required object FluxState { get; init; }
}
