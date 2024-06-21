namespace Carlton.Core.Flux.Debug.Models.Common;

/// <summary>
/// Represents a recorded mutation with its details.
/// </summary>
public record RecordedMutation
{
	/// <summary>
	/// Gets the index of the mutation.
	/// </summary>
	public required int MutationIndex { get; init; }

	/// <summary>
	/// Gets the date and time when the mutation occurred.
	/// </summary>
	public required DateTime MutationDate { get; init; }

	/// <summary>
	/// Gets the name of the mutation.
	/// </summary>
	public required string MutationName { get; init; }

	/// <summary>
	/// Gets the command associated with the mutation.
	/// </summary>
	public required object MutationCommand { get; init; }
}
