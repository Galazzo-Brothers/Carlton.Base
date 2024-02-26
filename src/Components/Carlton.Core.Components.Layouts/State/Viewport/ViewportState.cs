using Microsoft.JSInterop;
using System.Threading;
namespace Carlton.Core.Components.Layouts.State.Viewport;

/// <summary>
/// Represents the state management for the viewport.
/// </summary>
public sealed class ViewportState : IViewportState, IAsyncDisposable
{
    /// <summary>
    /// Path to the JavaScript module for viewport interaction.
    /// </summary>
    public const string ModulePath = $"./_content/{Constants.ProjectName}/scripts/viewport.js";

    /// <summary>
    /// Occurs when the viewport changes.
    /// </summary>
    public event EventHandler<ViewportChangedEventArgs> ViewportChanged;
    
    private readonly IJSRuntime _js;
    private readonly SemaphoreSlim _semaphore = new(1);
    private IJSObjectReference _module;
    private bool IsInitialized = false;
    private ViewportModel currentViewport;
    private readonly Task initTask;

    /// <summary>
    /// Initializes a new instance of the <see cref="ViewportState"/> class.
    /// </summary>
    /// <param name="js">The instance of the JavaScript runtime.</param>
    public ViewportState(IJSRuntime js)
    {
        _js = js;
        initTask = Initialize();
    }

    /// <summary>
    /// Retrieves the current viewport asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the current viewport.</returns>
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

            if (IsInitialized == true)
                return;

            //Set module
            _module = await _js.InvokeAsync<IJSObjectReference>(Constants.Import, ModulePath);

            //Get current viewport
            currentViewport = await _module.InvokeAsync<ViewportModel>("viewport.getViewport");

            //Register Callback
            await _module.InvokeAsync<ViewportModel>("viewport.registerViewportChangedHandler", DotNetObjectReference.Create(this), nameof(ViewportUpdated));

            IsInitialized = true;
        }
        finally 
        {
            _semaphore.Release();
        }
    }

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        await _module.InvokeAsync<ViewportModel>("viewport.removeViewportChangedHandlers", DotNetObjectReference.Create(this));
    }

    /// <summary>
    /// Handles the updated viewport information received from JavaScript.
    /// </summary>
    /// <param name="updatedViewport">The updated viewport model.</param>
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
