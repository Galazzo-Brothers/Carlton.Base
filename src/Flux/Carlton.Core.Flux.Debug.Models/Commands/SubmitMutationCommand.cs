namespace Carlton.Core.Flux.Debug.Models.Commands;

/// <summary>
/// Represents a command to submit a mutation.
/// </summary>
public sealed record SubmitMutationCommand
{
	/// <summary>
	/// Gets or sets the mutation command to be submitted.
	/// </summary>
	[Required]
	public object MutationCommandToSubmit { get; set; }
}
