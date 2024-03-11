using Carlton.Core.Flux.Dispatchers;
namespace Carlton.Core.Flux.Errors;

public class FluxException(
	string message, int eventId, BaseRequestContext context, Exception innerException) : Exception(message, innerException)
{
	public BaseRequestContext Context { get; init; } = context;
	public int EventId { get; init; } = eventId;

	public FluxException(string message, int eventId, BaseRequestContext context)
		: this(message, eventId, context, null)
	{
	}
}
