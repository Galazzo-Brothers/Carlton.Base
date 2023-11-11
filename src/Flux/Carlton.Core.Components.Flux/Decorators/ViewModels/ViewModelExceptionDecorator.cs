namespace Carlton.Core.Components.Flux.Decorators.Queries;

public class ViewModelExceptionDecorator<TState> : IViewModelQueryDispatcher<TState>
{
    private readonly IViewModelQueryDispatcher<TState> _decorated;
    private readonly ILogger<ViewModelExceptionDecorator<TState>> _logger;

    public ViewModelExceptionDecorator(IViewModelQueryDispatcher<TState> decorated, ILogger<ViewModelExceptionDecorator<TState>> logger)
        => (_decorated, _logger) = (decorated, logger);

    public async Task<TViewModel> Dispatch<TViewModel>(object sender, ViewModelQuery query, CancellationToken cancellationToken)
    {
        try
        {
            using(_logger.BeginScope(Log.ViewModelRequestScopeMessage, typeof(TViewModel).GetDisplayName(), query))
            Log.ViewModelStarted(_logger, typeof(TViewModel).GetDisplayName());
            var result = await _decorated.Dispatch<TViewModel>(sender, query, cancellationToken);
            Log.ViewModelCompleted(_logger, typeof(TViewModel).GetDisplayName());
            return result;
        }
        catch(ViewModelFluxException<TState, TViewModel>)
        {
            //Exception was already caught, logged and wrapped by other middleware decorators
            throw;
        }
        catch (Exception ex)
        {
            //Unhandled Exception
            Log.ViewModelUnhandledError(_logger, ex, typeof(TViewModel).GetDisplayName());
            throw new ViewModelFluxException<TState, TViewModel>(query, ex);
        }
    }
}
