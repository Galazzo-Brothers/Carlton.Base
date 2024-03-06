namespace Carlton.Core.Flux.Exceptions;

internal class ViewModelQueryException(FluxError error) : Exception
{
    public FluxError Error { get; } = error;
}
