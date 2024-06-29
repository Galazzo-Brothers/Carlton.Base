using Microsoft.Extensions.Logging;
namespace Carlton.Core.Flux.Debug.Models.Common;

/// <summary>
/// Represents a log message summary.
/// </summary>
public sealed record LogMessageSummary
{
	/// <summary>
	/// Gets the Id of the message.
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
}