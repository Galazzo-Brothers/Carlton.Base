namespace Carlton.Core.Flux.Exceptions;

internal class MutationCommandException(FluxError error) : Exception
{
    public FluxError Error { get; } = error;
}
