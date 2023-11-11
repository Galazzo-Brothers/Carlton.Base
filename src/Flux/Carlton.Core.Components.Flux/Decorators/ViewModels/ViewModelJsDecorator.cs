using Microsoft.JSInterop;

namespace Carlton.Core.Components.Flux.Decorators.Queries;
public class ViewModelJsDecorator<TState> : IViewModelQueryDispatcher<TState>
{
    private const string Import = "import";

    private readonly IViewModelQueryDispatcher<TState> _decorated;
    private readonly IJSRuntime _jsRuntime;
    private readonly IMutableFluxState<TState> _fluxState;
    private readonly ILogger<ViewModelJsDecorator<TState>> _logger;

    public ViewModelJsDecorator(IViewModelQueryDispatcher<TState> decorated, IJSRuntime jsRuntime, IMutableFluxState<TState> fluxState, ILogger<ViewModelJsDecorator<TState>> logger)
        => (_decorated, _jsRuntime, _fluxState, _logger) = (decorated, jsRuntime, fluxState, logger);

    public async Task<TViewModel> Dispatch<TViewModel>(object sender, ViewModelQuery query, CancellationToken cancellationToken)
    {
        try
        {
            //Get RefreshPolicy Attribute
            var attributes = sender.GetType().GetCustomAttributes();
            var jsInteropAttribute = attributes.OfType<ViewModelJsInteropRefreshAttribute>().FirstOrDefault();
            var requiresRefresh = jsInteropAttribute != null;
            var vmType = typeof(TViewModel).GetDisplayName();

            if (requiresRefresh)
            {
                Log.ViewModelJsInteropRefreshStarted(_logger, vmType);
                await using var module = await _jsRuntime.InvokeAsync<IJSObjectReference>(Import, jsInteropAttribute.Module);
                var result = await module.InvokeAsync<TViewModel>(jsInteropAttribute.Function, cancellationToken, jsInteropAttribute.Parameters);
                await _fluxState.MutateState(result);
                Log.ViewModelJsInteropRefreshCompleted(_logger, vmType);
            }
            else
            {
                Log.ViewModelJsInteropRefreshSkipped(_logger, vmType);
            }

            return await _decorated.Dispatch<TViewModel>(sender, query, cancellationToken);
        }
        catch (JSException ex)
        {
            Log.ViewModelJsInteropRefreshError(_logger, ex, typeof(TViewModel).GetDisplayName());
            throw ViewModelFluxException<TState, TViewModel>.JSInteropError(query, ex);
        }
    }
}

