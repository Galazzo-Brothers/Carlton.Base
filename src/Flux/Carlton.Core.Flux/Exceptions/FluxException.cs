namespace Carlton.Core.Flux.Exceptions;

public class FluxException(
	string message,
	int eventId,
	FluxOperationKind fluxOperationKind,
	Exception innerException) : Exception(message, innerException)
{
	public FluxOperationKind FluxOperationKind { get; init; } = fluxOperationKind;
	public int EventId { get; init; } = eventId;

	public FluxException(
		string message,
		int eventId,
		FluxOperationKind fluxOperationKind)
		: this(message, eventId, fluxOperationKind, null)
	{
	}
}


