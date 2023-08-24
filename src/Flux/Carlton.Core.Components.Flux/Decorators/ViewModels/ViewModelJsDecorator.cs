﻿using Microsoft.JSInterop;

namespace Carlton.Core.Components.Flux.Decorators.Queries;
public class ViewModelJsDecorator<TState> : IViewModelQueryDispatcher<TState>
{
    private const string Import = "import";

    private readonly IViewModelQueryDispatcher<TState> _decorated;
    private readonly IJSRuntime _jsRuntime;
    private readonly IFluxState<TState> _fluxState;
    private readonly ILogger<ViewModelJsDecorator<TState>> _logger;

    public ViewModelJsDecorator(IViewModelQueryDispatcher<TState> decorated, IJSRuntime jsRuntime, IFluxState<TState> fluxState, ILogger<ViewModelJsDecorator<TState>> logger)
        => (_decorated, _jsRuntime, _fluxState, _logger) = (decorated, jsRuntime, fluxState, logger);

    public async Task<TViewModel> Dispatch<TViewModel>(ViewModelQuery query, CancellationToken cancellationToken)
    {
        //Get RefreshPolicy Attribute
        var attributes = query.Sender.GetType().GetCustomAttributes();
        var jsInteropAttribute = attributes.OfType<ViewModelJsInteropAttribute>().FirstOrDefault();
        var requiresRefresh = jsInteropAttribute != null;
        var vmType = typeof(TViewModel).GetDisplayName();

        if(requiresRefresh)
        {
            Log.ViewModelJsInteropRefreshStarted(_logger, vmType);
            await using var module = await _jsRuntime.InvokeAsync<IJSObjectReference>(Import, jsInteropAttribute.Module);
            var result = await module.InvokeAsync<TViewModel>(jsInteropAttribute.Function, cancellationToken, jsInteropAttribute.Parameters);
            result.Adapt(_fluxState.State);
            Log.ViewModelJsInteropRefreshCompleted(_logger, vmType);
        }
        else
        {
            Log.ViewModelJsInteropRefreshSkipped(_logger, vmType);
        }

        return await _decorated.Dispatch<TViewModel>(query, cancellationToken);
    }
}
