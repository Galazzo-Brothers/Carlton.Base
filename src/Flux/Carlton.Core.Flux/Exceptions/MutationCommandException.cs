using static Carlton.Core.Flux.Errors.MutationCommandErrors;
namespace Carlton.Core.Flux.Exceptions;

internal class MutationCommandException(MutationCommandFluxError error) : Exception
{
    public MutationCommandFluxError Error { get; } = error;
}
