using Carlton.Core.Flux.Exceptions;

namespace Carlton.Core.Flux.Handlers.ViewModels;

public class ViewModelExceptionDecorator<TState> : IViewModelQueryDispatcher<TState>
{
    private readonly IViewModelQueryDispatcher<TState> _decorated;
    private readonly ILogger<ViewModelExceptionDecorator<TState>> _logger;

    public ViewModelExceptionDecorator(IViewModelQueryDispatcher<TState> decorated, ILogger<ViewModelExceptionDecorator<TState>> logger)
        => (_decorated, _logger) = (decorated, logger);

    public async Task<TViewModel> Dispatch<TViewModel>(object sender, ViewModelQueryContext<TViewModel> context, CancellationToken cancellationToken)
    {
        var scopes = LogEvents.GetViewModelRequestLoggingScopes(_logger, context);
        using (scopes)
        {
            try
            {
                var viewmodel = await _decorated.Dispatch(sender, context, cancellationToken);
                _logger.ViewModelCompleted(context.ViewModelType);
                return viewmodel;
            }
            catch (ViewModelFluxException<TState, TViewModel>)
            {
                //Exception was already caught, logged and wrapped by other middleware decorators
                throw;
            }
            catch (Exception ex)
            {
                context.MarkAsErrored(ex);
                _logger.ViewModelUnhandledError(ex, context.ViewModelType);
                throw new ViewModelFluxException<TState, TViewModel>(context, ex);
            }
        }
    }
}
