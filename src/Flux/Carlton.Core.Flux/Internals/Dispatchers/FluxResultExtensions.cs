using Carlton.Core.Flux.Dispatchers;
using Carlton.Core.Flux.Internals.Errors;

namespace Carlton.Core.Flux.Internals.Dispatchers;

internal static class FluxResultExtensions
{
	internal static TViewModel GetViewModelResultOrThrow<TViewModel>(this Result<TViewModel, FluxError> result)
	{
		return result.Match
			   (
				   vm => vm,
				   err => throw new FluxException(err, FluxOperation.ViewModelQuery)
			   );
	}

	internal static MutationCommandResult GetMutationResultOrThrow(this Result<MutationCommandResult, FluxError> result)
	{
		return result.Match
			   (
				   r => r,
				   err => throw new FluxException(err, FluxOperation.CommandMutation)
			   );
	}
}
