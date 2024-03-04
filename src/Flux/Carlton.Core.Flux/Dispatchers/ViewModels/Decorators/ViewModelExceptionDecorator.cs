using Carlton.Core.Flux.Errors;
namespace Carlton.Core.Flux.Dispatchers.ViewModels.Decorators;

public class ViewModelExceptionDecorator<TState>(
    IViewModelQueryDispatcher<TState> _decorated,
    ILogger<ViewModelExceptionDecorator<TState>> _logger)
    : IViewModelQueryDispatcher<TState>
{
    public async Task<Result<TViewModel, ViewModelFluxError>> Dispatch<TViewModel>(object sender, ViewModelQueryContext<TViewModel> context, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _decorated.Dispatch(sender, context, cancellationToken);
            _logger.ViewModelQueryCompleted(context.ViewModelTypeName);
            return result;
        }
        catch (Exception ex)
        {
            return HandleException(ex, context);
        }
    }

    private Result<TViewModel, ViewModelFluxError> HandleException<TViewModel, TException>(TException ex, ViewModelQueryContext<TViewModel> context)
        where TException : Exception
    {
        context.MarkAsErrored(ex);
        using (_logger.BeginRequestExceptionLoggingScopes())
            _logger.ViewModelQueryErrored(context.ViewModelTypeName, ex);

        // Return a specific error based on the exception type
        return ex switch
        {
            InvalidOperationException when ex.Message.Contains(FluxLogs.InvalidRefreshUrlMsg) => (Result<TViewModel, ViewModelFluxError>)new ViewModelQueryErrors.HttpUrlError(typeof(TViewModel)),
            JsonException => (Result<TViewModel, ViewModelFluxError>)new JsonError(typeof(TViewModel)),
            NotSupportedException when ex.Message.Contains("Serialization and deserialization") => (Result<TViewModel, ViewModelFluxError>)new JsonError(typeof(TViewModel)),
            HttpRequestException => (Result<TViewModel, ViewModelFluxError>)new ViewModelQueryErrors.HttpError(typeof(TViewModel)),
            _ => (Result<TViewModel, ViewModelFluxError>)new ViewModelQueryErrors.UnhandledError(typeof(TViewModel)),
        };
    }
}
