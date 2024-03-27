namespace Carlton.Core.Flux.Debug.Models.Common;

/// <summary>
/// Represents an entry for an exception.
/// </summary>
public sealed record ExceptionEntry
{
	/// <summary>
	/// Gets or initializes the type of the exception.
	/// </summary>
	public required string ExceptionType { get; init; }

	/// <summary>
	/// Gets or initializes the message of the exception.
	/// </summary>
	public required string Message { get; init; }

	/// <summary>
	/// Gets or initializes the stack trace of the exception.
	/// </summary>
	public required string StackTrace { get; init; }
}