using Carlton.Core.Flux.Exceptions;
namespace Carlton.Core.Flux.Extensions;

internal static class ResultExtensions
{
    internal static TViewModel GetViewModelResultOrThrow<TViewModel>(this Result<TViewModel, FluxError> result)
    {
        return result.Match
               (
                   vm => vm,
                   err => throw new ViewModelQueryException(err)
               );
    }

    internal static MutationCommandResult GetMutationResultOrThrow(this Result<MutationCommandResult, FluxError> result)
    {
        return result.Match
               (
                   r => r,
                   err => throw new MutationCommandException(err)
               );
    }
}
