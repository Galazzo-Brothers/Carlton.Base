namespace Carlton.Core.Flux.Debug.Models.Commands;

/// <summary>
/// Represents a command to load log messages.
/// </summary>
public sealed record LoadLogMessagesCommand
{
	/// <summary>
	/// Gets or initializes the list of log messages.
	/// </summary>
	public IReadOnlyList<LogMessage> LogMessages { get; init; } = new List<LogMessage>();
}
