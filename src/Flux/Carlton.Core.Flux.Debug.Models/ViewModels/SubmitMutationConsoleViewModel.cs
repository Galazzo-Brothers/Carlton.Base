namespace Carlton.Core.Flux.Debug.Models.ViewModels;

/// <summary>
/// Represents the view model for submitting mutation commands in the console.
/// </summary>
public record SubmitMutationConsoleViewModel
{
	/// <summary>
	/// Gets the collection of mutation command types that can be submitted.
	/// </summary>
	public required IEnumerable<Type> MutationCommandTypes { get; init; } = new List<Type>();
}
