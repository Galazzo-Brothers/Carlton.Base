namespace Carlton.Core.Components.Flux.Decorators.Queries;

public class ViewModelExceptionDecorator<TState> : IViewModelQueryDispatcher<TState>
{
    private readonly IViewModelQueryDispatcher<TState> _decorated;
    private readonly ILogger<ViewModelExceptionDecorator<TState>> _logger;

    public ViewModelExceptionDecorator(IViewModelQueryDispatcher<TState> decorated, ILogger<ViewModelExceptionDecorator<TState>> logger)
        => (_decorated, _logger) = (decorated, logger);

    public async Task<TViewModel> Dispatch<TViewModel>(object sender, ViewModelQuery query, CancellationToken cancellationToken)
    {
        var traceGuid = Guid.NewGuid();
        var vmDisplayName = typeof(TViewModel).GetDisplayName();
        var vmQueryTraceGuid = $"ViewModelQuery_{vmDisplayName}_{traceGuid}";

        try
        {
            TViewModel result;
            using(_logger.BeginScope(vmQueryTraceGuid))
            {
                _logger.ViewModelStarted(vmDisplayName);
                result = await _decorated.Dispatch<TViewModel>(sender, query, cancellationToken);
                _logger.ViewModelCompleted(vmDisplayName);
            }
            return result;
        }
        catch(ViewModelFluxException<TState, TViewModel>)
        {
            //Exception was already caught, logged and wrapped by other middleware decorators
            throw;
        }
        catch(Exception ex)
        {
            //Unhandled Exception
            using(_logger.BeginScope(vmQueryTraceGuid))
            {
                _logger.ViewModelUnhandledError(ex, vmDisplayName);
                throw new ViewModelFluxException<TState, TViewModel>(query, ex);
            }
        }
    }
}
