using Carlton.Core.Flux.Internals.Logging;
namespace Carlton.Core.Flux.Internals.Dispatchers.ViewModels.Decorators;

internal sealed class ViewModelExceptionDecorator<TState>(
	IViewModelQueryDispatcher<TState> _decorated,
	ILogger<ViewModelExceptionDecorator<TState>> _logger)
	: IViewModelQueryDispatcher<TState>
{
	public async Task<Result<TViewModel, FluxError>> Dispatch<TViewModel>(object sender, ViewModelQueryContext<TViewModel> context, CancellationToken cancellationToken)
	{
		using (_logger.BeginViewModelRequestLoggingScopes(context))
		{
			context.MarkAsStarted();

			return await ResultExtensions.SafeExecuteAsync
			(
				async () => await _decorated.Dispatch(sender, context, cancellationToken), //Progress through the pipeline wrapped in a try/catch
				success => HandleSuccess(success, context), //Success Handler
				err => HandleError(err, context), //Error Handler
				ex => HandleException(ex, context) //Exception handler
			);
		}
	}

	private TViewModel HandleSuccess<TViewModel>(TViewModel vm, ViewModelQueryContext<TViewModel> context)
	{
		context.MarkAsSucceeded(vm);
		_logger.ViewModelQueryCompleted(context.FluxOperationTypeName);
		return vm;
	}

	private FluxError HandleError<TViewModel>(FluxError error, ViewModelQueryContext<TViewModel> context)
	{
		//Mark as errored
		context.MarkAsErrored(new ViewModelQueryFluxException<TViewModel>(error.Message, error.EventId, error.Exception));

		//Log the result
		using (_logger.BeginRequestErrorLoggingScopes(error))
			GetErrorLoggingAction(_logger, error, context.FluxOperationTypeName)();

		//Return
		return error;
	}

	private UnhandledFluxError HandleException<TViewModel>(Exception ex, ViewModelQueryContext<TViewModel> context)
	{
		//Mark as errored
		context.MarkAsErrored(ex);

		//Log the result
		using (_logger.BeginRequestErrorLoggingScopes(FluxLogs.Flux_Unhandled_Error))
			_logger.ViewModelQueryErrored(context.FluxOperationTypeName, ex);

		//Return
		return UnhandledFluxError(ex);
	}

	private static Action GetErrorLoggingAction(ILogger<ViewModelExceptionDecorator<TState>> _logger, FluxError error, string fluxOperationTypeName)
	{
		return error switch
		{
			ValidationError _ => () => _logger.ViewModelQueryValidationFailure(fluxOperationTypeName), //Validation Errors will be logged as warnings
			_ => () => _logger.ViewModelQueryErrored(fluxOperationTypeName) //Other FluxErrors will be logged as errors
		};
	}
}
