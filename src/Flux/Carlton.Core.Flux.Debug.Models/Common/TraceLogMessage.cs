using Microsoft.Extensions.Logging;
namespace Carlton.Core.Flux.Debug.Models.Common;

/// <summary>
/// Represents a trace log message.
/// </summary>
public sealed record TraceLogMessage
{
	/// <summary>
	/// Gets or initializes the ID of the log message.
	/// </summary>
	public required int Id { get; init; }

	/// <summary>
	/// Gets or initializes the timestamp of the log message.
	/// </summary>
	public required DateTime Timestamp { get; init; }

	/// <summary>
	/// Gets or initializes the Flux action associated with the log message.
	/// </summary>
	public required FluxActions FluxAction { get; init; }

	/// <summary>
	/// Gets or initializes the event ID of the log message.
	/// </summary>
	public required EventId EventId { get; init; }

	/// <summary>
	/// Gets or initializes the type display name of the log message.
	/// </summary>
	public required string TypeDisplayName { get; init; }

	/// <summary>
	/// Gets or initializes a boolean value indicating whether the associated request succeeded.
	/// </summary>
	public required bool RequestSucceeded { get; init; }
}
