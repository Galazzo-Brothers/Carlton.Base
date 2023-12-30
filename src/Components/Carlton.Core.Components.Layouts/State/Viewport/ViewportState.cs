using Microsoft.JSInterop;

namespace Carlton.Core.Components.Layouts.State.Viewport;

public class ViewportState(IJSRuntime js) : IViewportState, IAsyncDisposable
{
    public ViewportModel CurrentViewport { get; private set; }
    private readonly IJSRuntime _js = js;
    private IJSObjectReference _module;

    private const string ModulePath = $"./_content/{Constants.ProjectName}/scripts/viewport.js";

    public async Task<ViewportModel> GetCurrentViewport()
    {
        var module = await GetModule();
        return await module.InvokeAsync<ViewportModel>("viewport.getViewport");
    }

    public async Task RegisterViewportChangedHandler<T>(DotNetObjectReference<T> dotnetObjectReference, string callbackName)
        where T : class
    {
        var module = await GetModule();
        await module.InvokeAsync<ViewportModel>("viewport.registerViewportChangedHandler", dotnetObjectReference, callbackName);
    }

    private async Task<IJSObjectReference> GetModule()
        => _module ??= await _js.InvokeAsync<IJSObjectReference>("import", ModulePath);

    public async ValueTask DisposeAsync()
    {
        var module = await GetModule();
        await module.InvokeAsync<ViewportModel>("viewport.removeViewportChangedHandlers", DotNetObjectReference.Create(this));
    }
}
