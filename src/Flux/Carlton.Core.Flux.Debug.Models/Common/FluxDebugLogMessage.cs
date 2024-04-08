using Microsoft.Extensions.Logging;
namespace Carlton.Core.Flux.Debug.Models.Common;

/// <summary>
/// Represents a debug log message specific to the Flux debugging system.
/// </summary>
public sealed record FluxDebugLogMessage
{
	/// <summary>
	/// Gets the unique identifier of the log message.
	/// </summary>
	public required int Id { get; init; }

	/// <summary>
	/// Gets the log level of the message.
	/// </summary>
	public required LogLevel LogLevel { get; init; }

	/// <summary>
	/// Gets the event ID associated with the message.
	/// </summary>
	public required EventId EventId { get; init; }

	/// <summary>
	/// Gets the textual content of the message.
	/// </summary>
	public required string Message { get; init; }

	/// <summary>
	/// Gets the exception associated with the message.
	/// </summary>
	public required ExceptionSummary? Exception { get; init; }

	/// <summary>
	/// Gets the timestamp when the message was logged.
	/// </summary>
	public required DateTime Timestamp { get; init; }

	/// <summary>
	/// Gets the dictionary of scopes associated with the message.
	/// </summary>
	public Dictionary<string, object> Scopes { get; init; } = [];
}