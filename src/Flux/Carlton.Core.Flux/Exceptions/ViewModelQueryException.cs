namespace Carlton.Core.Flux.Exceptions;

internal class ViewModelQueryException(ViewModelQueryError error) : Exception
{
    public ViewModelQueryError Error { get; } = error;
}
