namespace Carlton.Core.Flux.Debug.Models.ViewModels;

/// <summary>
/// Represents a view model for displaying Flux state information.
/// </summary>
public sealed record ViewModelProjectionsViewerViewModel
{
	/// <summary>
	/// Gets or initializes the Flux state object.
	/// </summary>
	[Required]
	public required object FluxState { get; init; }

	/// <summary>
	/// Gets or initializes the view model types.
	/// </summary>
	public required IEnumerable<Type> ViewModelTypes { get; init; } = new List<Type>();
}
