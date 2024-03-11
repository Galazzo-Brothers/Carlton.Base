using Carlton.Core.Flux.Dispatchers;
namespace Carlton.Core.Flux.Internals;

internal static class FluxResultExtensions
{

	internal static TViewModel GetViewModelResultOrThrow<TViewModel>(this Result<TViewModel, FluxError> result, ViewModelQueryContext<TViewModel> context)
		=> result.Match
		(
			vm => vm,
			err => throw new FluxException(err.Message, err.EventId, context)
		);


	internal static MutationCommandResult GetMutationResultOrThrow<TCommand>(this Result<MutationCommandResult, FluxError> result, MutationCommandContext<TCommand> context)
		=> result.Match
		(
			r => r,
			err => throw new FluxException(err.Message, err.EventId, context)
		);
}
