namespace Carlton.Core.Flux.Debug.Models.ViewModels;

/// <summary>
/// Represents a view model for displaying Flux state information.
/// </summary>
public sealed class FluxStateViewerViewModel
{
	/// <summary>
	/// Gets the Flux state object.
	/// </summary>
	[Required]
	public required object SelectedFluxState { get; init; }

	/// <summary>
	/// Gets the Recorded Mutations from the flux application.
	/// </summary>
	[Required]
	public required IEnumerable<RecordedMutation> RecordedMutations { get; init; }

	/// <summary>
	/// Gets the selected mutation index.
	/// </summary>
	[Required]
	public required int SelectedMutationIndex { get; init; }
}
