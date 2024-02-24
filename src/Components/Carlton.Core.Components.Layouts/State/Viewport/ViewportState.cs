using Microsoft.JSInterop;
using System.Threading;

namespace Carlton.Core.Components.Layouts.State.Viewport;

public class ViewportState : IViewportState, IAsyncDisposable
{
    const string ModulePath = $"./_content/{Constants.ProjectName}/scripts/viewport.js";
    public event EventHandler<ViewportChangedEventArgs> ViewportChanged;
    
    private readonly IJSRuntime _js;
    private readonly SemaphoreSlim _semaphore = new(1);
    private IJSObjectReference _module;
    private bool IsInitalized = false;
    private ViewportModel currentViewport;

    private readonly Task initTask;

    public ViewportState(IJSRuntime js)
    {
        _js = js;
        initTask = Initialize();
    }

    public async Task<ViewportModel> GetCurrentViewport()
    {
        await initTask.ConfigureAwait(false);
        return currentViewport;
    }

    private async Task Initialize()
    {
        try
        {
            await _semaphore.WaitAsync();

            if (IsInitalized == true)
                return;

            //Set module
            _module = await _js.InvokeAsync<IJSObjectReference>("import", ModulePath);

            //Get current viewport
            currentViewport = await _module.InvokeAsync<ViewportModel>("viewport.getViewport");

            //Register Callback
            await _module.InvokeAsync<ViewportModel>("viewport.registerViewportChangedHandler", DotNetObjectReference.Create(this), nameof(ViewportUpdated));

            IsInitalized = true;
        }
        finally 
        {
            _semaphore.Release();
        }
    }

    public async ValueTask DisposeAsync()
    {
        await _module.InvokeAsync<ViewportModel>("viewport.removeViewportChangedHandlers", DotNetObjectReference.Create(this));
    }

    [JSInvokable("ViewportUpdated")]
    public async Task ViewportUpdated(ViewportModel updatedViewport)
    {
        await initTask.ConfigureAwait(false);

        var viewportChanged = currentViewport.IsMobile != updatedViewport.IsMobile;
        currentViewport = updatedViewport;

        if (viewportChanged)
            ViewportChanged?.Invoke(this, new ViewportChangedEventArgs(currentViewport));
    }
}
