using System.Diagnostics.CodeAnalysis;
namespace Carlton.Core.Flux.Debug.Models.Common;

/// <summary>
/// Represents a debug log message specific to the Flux debugging system.
/// </summary>
public sealed record FluxDebugLogMessage : LogMessage
{
	/// <summary>
	/// Gets the unique identifier of the log message.
	/// </summary>
	public required int Id { get; init; }

	/// <summary>
	/// Initializes a new instance of the <see cref="FluxDebugLogMessage"/> class with the specified identifier and log message details.
	/// </summary>
	/// <param name="id">The unique identifier of the log message.</param>
	/// <param name="logMessage">The base log message to include in the debug log message.</param>
	[SetsRequiredMembers]
	public FluxDebugLogMessage(int id, LogMessage logMessage)
	{
		Id = id;
		LogLevel = logMessage.LogLevel;
		EventId = logMessage.EventId;
		Message = logMessage.Message;
		Exception = logMessage.Exception;
		Timestamp = logMessage.Timestamp;
		Scopes = logMessage.Scopes;
	}
}