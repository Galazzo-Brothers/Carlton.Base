using Carlton.Core.Flux.Internals.Dispatchers.Mutations;
using Carlton.Core.Flux.Internals.Dispatchers.ViewModels;

namespace Carlton.Core.Flux.Internals;

internal static class FluxResultExtensions
{

	internal static TViewModel GetViewModelResultOrThrow<TViewModel>(this Result<TViewModel, FluxError> result, ViewModelQueryContext<TViewModel> context)
		=> result.Match
		(
			vm => vm,
			err => throw new ViewModelQueryFluxException<TViewModel>(err.Message, err.EventId)
		);


	internal static MutationCommandResult GetMutationResultOrThrow<TCommand>(this Result<MutationCommandResult, FluxError> result, MutationCommandContext<TCommand> context)
		=> result.Match
		(
			r => r,
			err => throw new MutationCommandFluxException<TCommand>(err.Message, err.EventId, context.MutationCommand)
		);
}
