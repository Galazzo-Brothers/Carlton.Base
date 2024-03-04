using static Carlton.Core.Flux.Errors.FluxMutationCommandErrors;
namespace Carlton.Core.Flux.Exceptions;

internal class MutationCommandFluxException(MutationCommandFluxError error) : Exception
{
    public MutationCommandFluxError Error { get; } = error;
}
