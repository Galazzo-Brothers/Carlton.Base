using Carlton.Core.Flux.Dispatchers;
using Carlton.Core.Flux.Internals.Errors;
using Carlton.Core.Flux.Internals.Logging;
namespace Carlton.Core.Flux.Internals.Dispatchers.Mutations.Decorators;

internal sealed class MutationExceptionDecorator<TState>(
	IMutationCommandDispatcher<TState> _decorated,
	ILogger<MutationExceptionDecorator<TState>> _logger)
	: IMutationCommandDispatcher<TState>
{
	public async Task<Result<MutationCommandResult, FluxError>> Dispatch<TCommand>(
		object sender, MutationCommandContext<TCommand> context,
		CancellationToken cancellationToken)
	{
		using (_logger.BeginMutationCommandRequestLoggingScopes(context))
		{
			context.MarkAsStarted();

			return await ResultExtensions.SafeExecuteAsync
			(
				async () => await _decorated.Dispatch(sender, context, cancellationToken), //Wrapped Func
				success => HandleSuccess(success, context), //Success Handler
				err => HandleError(err, context), //Error Handler
				ex => HandleException(ex, context) //Exception Handler
			);
		}
	}

	private MutationCommandResult HandleSuccess<TCommand>(MutationCommandResult result, MutationCommandContext<TCommand> context)
	{
		_logger.MutationCommandCompleted(context.FluxOperationTypeName);
		return result;
	}

	private FluxError HandleError<TCommand>(FluxError error, MutationCommandContext<TCommand> context)
	{
		//Mark as errored
		context.MarkAsErrored(error);

		//Log the result
		using (_logger.BeginRequestErrorLoggingScopes(error.EventId))
			GetErrorLoggingAction(_logger, error, context.FluxOperationTypeName)();

		//Return
		return error;
	}

	private UnhandledFluxError HandleException<TCommand>(Exception ex, MutationCommandContext<TCommand> context)
	{
		//Mark as errored
		context.MarkAsErrored(ex);

		//Log the result
		using (_logger.BeginRequestErrorLoggingScopes(FluxLogs.Flux_Unhandled_Error))
			_logger.MutationCommandErrored(context.FluxOperationTypeName, ex);

		//Return
		return UnhandledFluxError(ex);
	}

	private static Action GetErrorLoggingAction(ILogger<MutationExceptionDecorator<TState>> _logger, FluxError error, string fluxOperationTypeName)
	{
		return error switch
		{
			ValidationError _ => () => _logger.MutationCommandValidationFailure(fluxOperationTypeName), //Validation Errors will be logged as warnings
			_ => () => _logger.MutationCommandErrored(fluxOperationTypeName) //Other FluxErrors will be logged as errors
		};
	}
}

