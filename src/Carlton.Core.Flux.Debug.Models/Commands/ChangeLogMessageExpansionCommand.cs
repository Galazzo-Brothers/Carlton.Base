namespace Carlton.Core.Flux.Debug.Models.Commands;

/// <summary>
/// Represents a command to change the expansion state of a log message.
/// </summary>
public sealed record ChangeLogMessageExpansionCommand
{
	/// <summary>
	/// Gets or initializes the index of the trace log message.
	/// </summary>
	[NonNegativeInteger]
	public required int TraceLogMessageIndex { get; init; }

	/// <summary>
	/// Gets or initializes the index of the trace log message.
	/// </summary>
	public required bool IsExpanded { get; init; }
};
