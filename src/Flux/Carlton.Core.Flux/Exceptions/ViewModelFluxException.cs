namespace Carlton.Core.Flux.Exceptions;

internal class ViewModelFluxException(ViewModelFluxError error) : Exception
{
    public ViewModelFluxError Error { get; } = error;
}
