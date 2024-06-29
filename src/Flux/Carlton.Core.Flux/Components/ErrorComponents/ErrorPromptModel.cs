namespace Carlton.Core.Flux.Components.ErrorComponents;

/// <summary>
/// Represents a model for an error prompt.
/// </summary>
public record ErrorPromptModel
{
	/// <summary>
	/// Gets the header text of the error prompt.
	/// </summary>
	public required string Header { get; init; }

	/// <summary>
	/// Gets the message text of the error prompt.
	/// </summary>
	public required string Message { get; init; }

	/// <summary>
	/// Gets the CSS class for the icon displayed in the error prompt.
	/// </summary>
	public required string IconClass { get; init; }

	/// <summary>
	/// Gets the action to be performed when the user attempts to recover from the error.
	/// </summary>
	public required Action Recover { get; init; }
}
