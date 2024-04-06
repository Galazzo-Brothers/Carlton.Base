namespace Carlton.Core.Flux.Debug.Models.Commands;

/// <summary>
/// Represents a command to change the selected trace log message.
/// </summary>
public sealed record ChangeSelectedTraceLogMessageCommand
{
	/// <summary>
	/// Gets or initializes the selected trace log message.
	/// </summary>
	[Required]
	public required TraceLogMessage SelectedTraceLogMessage { get; init; }
};
