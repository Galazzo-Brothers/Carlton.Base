namespace Carlton.Core.Flux.Debug.Models.Commands;

/// <summary>
/// Represents a command to change the selected log message.
/// </summary>
public sealed record ChangeSelectedLogMessageCommand
{
	/// <summary>
	/// Gets or initializes the selected log message.
	/// </summary>
	[Required]
	public required LogMessage SelectedLogMessage { get; init; }
};
