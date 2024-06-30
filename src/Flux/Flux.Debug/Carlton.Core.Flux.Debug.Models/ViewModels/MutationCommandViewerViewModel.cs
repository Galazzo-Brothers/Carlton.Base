namespace Carlton.Core.Flux.Debug.Models.ViewModels;

/// <summary>
/// ViewModel for viewing mutation commands.
/// </summary>
public sealed record MutationCommandViewerViewModel
{
	/// <summary>
	/// Gets the currently selected mutation command.
	/// </summary>
	public required object SelectedMutationCommand { get; init; } = new();
}
