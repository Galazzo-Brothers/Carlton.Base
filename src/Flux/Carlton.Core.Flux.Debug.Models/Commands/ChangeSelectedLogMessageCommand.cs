namespace Carlton.Core.Flux.Debug.Models.Commands;

/// <summary>
/// Represents a command to change the selected log message.
/// </summary>
public sealed record ChangeSelectedLogMessageCommand
{
	/// <summary>
	/// Gets or initializes the index of the selected log message.
	/// </summary>
	[NonNegativeInteger]
	public required int SelectedLogMessageIndex { get; init; }
};
