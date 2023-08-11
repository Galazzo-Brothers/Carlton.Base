namespace Carlton.Core.Components.Flux.Decorators.Queries;
//public class ViewModelJsDecorator<TState> : IViewModelDispatcher
//{
//    private const string Import = "import";

//    private readonly IViewModelDispatcher _decorated;
//    private readonly IJSRuntime _jsRuntime;
//    private readonly ILogger<ViewModelJsDecorator<TState>> _logger;

//    public ViewModelJsDecorator(IViewModelDispatcher decorated, IJSRuntime jsRuntime, ILogger<ViewModelJsDecorator<TState>> logger)
//        => (_decorated, _jsRuntime, _logger) = (decorated, jsRuntime, logger);

//    public async Task<TViewModel> Dispatch<TViewModel>(ViewModelQueryRequest<TViewModel> request, CancellationToken cancellationToken)
//    {
//        //Get RefreshPolicy Attribute
//        var attributes = request.Sender.WrappedComponentType.GetCustomAttributes();
//        var jsInteropAttribute = attributes.OfType<ViewModelJsInteropAttribute>().FirstOrDefault();
//        var requiresRefresh = jsInteropAttribute != null;

//        if (requiresRefresh)
//        {
//            try
//            {
//                Log.ViewModelRequestJsInteropRefreshStarting(_logger, request.DisplayName, request);
//                await using var module = await _jsRuntime.InvokeAsync<IJSObjectReference>(Import, jsInteropAttribute.Module);
//                var result = await module.InvokeAsync<TViewModel>(jsInteropAttribute.Function, cancellationToken, jsInteropAttribute.Parameters);
//                result.Adapt((TState)request.State);
//                request.MarkAsJsInteropCalled(jsInteropAttribute.Module, jsInteropAttribute.Function);
//                Log.ViewModelRequestJsInteropRefreshCompleted(_logger, request.DisplayName, request);
//            }
//            catch (JSException ex)
//            {
//                Log.ViewModelRequestJsInteropRefreshError(_logger, ex, request.DisplayName, request);
//                throw;
//            }
//        }
//        else
//        {
//            Log.ViewModelRequestSkippingJsInteropRefresh(_logger, request.DisplayName, request);
//        }

//        return await _decorated.Dispatch(request, cancellationToken);
//    }
//}

