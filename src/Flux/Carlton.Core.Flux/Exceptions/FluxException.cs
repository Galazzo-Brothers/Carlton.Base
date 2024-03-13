namespace Carlton.Core.Flux.Exceptions;

/// <summary>
/// Abstract base class for exceptions thrown by the Flux framework.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="FluxException"/> class with the specified message, event ID, Flux operation kind, and inner exception.
/// </remarks>
/// <param name="message">The error message that explains the reason for the exception.</param>
/// <param name="eventId">The event ID associated with the exception.</param>
/// <param name="fluxOperationKind">The kind of Flux operation associated with the exception.</param>
/// <param name="innerException">The exception that caused the current exception.</param>
public abstract class FluxException(
	string message,
	int eventId,
	FluxOperationKind fluxOperationKind,
	Exception innerException) : Exception(message, innerException)
{
	/// <summary>
	/// Gets the kind of Flux operation associated with the exception.
	/// </summary>
	public FluxOperationKind FluxOperationKind { get; init; } = fluxOperationKind;

	/// <summary>
	/// Gets the event ID associated with the exception.
	/// </summary>
	public int EventId { get; init; } = eventId;

	/// <summary>
	/// Initializes a new instance of the <see cref="FluxException"/> class with the specified message, event ID, Flux operation kind, and inner exception.
	/// </summary>
	/// <param name="message">The error message that explains the reason for the exception.</param>
	/// <param name="eventId">The event ID associated with the exception.</param>
	/// <param name="fluxOperationKind">The kind of Flux operation associated with the exception.</param>
	public FluxException(
		string message,
		int eventId,
		FluxOperationKind fluxOperationKind)
		: this(message, eventId, fluxOperationKind, null)
	{
	}
}


