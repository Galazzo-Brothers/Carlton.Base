namespace Carlton.Core.Flux.Exceptions;

internal class ViewModelQueryException(ViewModelFluxError error) : Exception
{
    public ViewModelFluxError Error { get; } = error;
}
