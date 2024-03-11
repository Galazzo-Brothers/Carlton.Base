using Carlton.Core.Flux.Dispatchers;
namespace Carlton.Core.Flux.Internals.Errors;

internal class FluxException(FluxError error, FluxOperation fluxOperation) : Exception
{
	public FluxError Error { get; } = error;
	public FluxOperation FuxOperation { get; } = fluxOperation;
}
