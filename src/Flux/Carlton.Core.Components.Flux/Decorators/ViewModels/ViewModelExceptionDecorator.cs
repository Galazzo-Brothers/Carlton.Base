using Microsoft.JSInterop;
using System.Text.Json;

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
        catch(JsonException ex)
        {
            Log.ViewModelJsonError(_logger, ex, typeof(TViewModel).GetDisplayName());
            throw ViewModelFluxException<TState, TViewModel>.JsonError(query, ex);
        }
        catch(HttpRequestException ex)
        {
            Log.ViewModelHttpRefreshError(_logger, ex, typeof(TViewModel).GetDisplayName());
            throw ViewModelFluxException<TState, TViewModel>.HttpError(query, ex);
        }
        catch(JSException ex)
        {
            Log.ViewModelJsInteropRefreshError(_logger, ex, typeof(TViewModel).GetDisplayName());
            throw ViewModelFluxException<TState, TViewModel>.JSInteropError(query, ex);
        }
        catch(ValidationException ex)
        {
            Log.ViewModelValidationError(_logger, ex, typeof(TViewModel).GetDisplayName());
            throw ViewModelFluxException<TState, TViewModel>.ValidationError(query, ex);
        }
        catch(Exception ex)
        {
            Log.ViewModelUnhandledError(_logger, ex, typeof(TViewModel).GetDisplayName());
            throw new ViewModelFluxException<TState, TViewModel>(query, ex);
        }
    }
}
