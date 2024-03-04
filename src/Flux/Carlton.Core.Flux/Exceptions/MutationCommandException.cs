namespace Carlton.Core.Flux.Exceptions;

internal class MutationCommandException(MutationCommandError error) : Exception
{
    public MutationCommandError Error { get; } = error;
}
