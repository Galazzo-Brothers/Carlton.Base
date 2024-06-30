namespace Carlton.Core.Flux.Debug.Models.Commands;

/// <summary>
/// Represents a command to change the selected mutation command.
/// </summary>
public sealed record ChangeSelectedCommandMutationCommand
{
	/// <summary>
	/// Gets the index of the selected mutation command.
	/// </summary>
	public int SelectedMutationCommandIndex { get; init; }
}
