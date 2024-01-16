using Carlton.Core.Flux.Exceptions;
namespace Carlton.Core.Flux.Handlers.ViewModels;

public class ViewModelExceptionDecorator<TState>(
    IViewModelQueryDispatcher<TState> _decorated,
    ILogger<ViewModelExceptionDecorator<TState>> _logger)
    : IViewModelQueryDispatcher<TState>
{
    public async Task<TViewModel> Dispatch<TViewModel>(object sender, ViewModelQueryContext<TViewModel> context, CancellationToken cancellationToken)
    {
        using (_logger.BeginViewModelRequestLoggingScopes(context))
        {
            try
            {
                var vm = await _decorated.Dispatch(sender, context, cancellationToken);
                _logger.ViewModelQueryCompleted(context.ViewModelType);
                return vm;
            }
            catch (Exception ex)
            {
                context.MarkAsErrored(ex);
                var wrappedException = WrapException(context, ex);
                using (_logger.BeginRequestExceptionLoggingScopes(wrappedException))
                    _logger.ViewModelQueryErrored(context.ViewModelType, wrappedException);
                throw wrappedException;
            }
        }
    }

    private static ViewModelFluxException<TState, TViewModel> WrapException<TViewModel>(
        ViewModelQueryContext<TViewModel> context,
        Exception exception) => exception switch
        {
            ValidationException ex => ViewModelFluxException<TState, TViewModel>.ValidationError(context, ex), //Validation Error
            InvalidOperationException ex when ex.Message.Contains(LogEvents.InvalidRefreshUrlMsg) => ViewModelFluxException<TState, TViewModel>.HttpUrlError(context, ex),//URL Construction Error
            JsonException ex => ViewModelFluxException<TState, TViewModel>.JsonError(context, ex),//Error Serializing JSON
            NotSupportedException ex when ex.Message.Contains("Serialization and deserialization") => ViewModelFluxException<TState, TViewModel>.JsonError(context, ex),//Error Serializing JSON
            HttpRequestException ex => ViewModelFluxException<TState, TViewModel>.HttpError(context, ex),//Http Exceptions
            _ => ViewModelFluxException<TState, TViewModel>.UnhandledError(context, exception),//Unhandled Exception
        };
}



