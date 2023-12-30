using Microsoft.JSInterop;

namespace Carlton.Core.Components.Layouts.State.Viewport;

public interface IViewportState
{
    public Task<ViewportModel> GetCurrentViewport();
    public Task RegisterViewportChangedHandler<T>(DotNetObjectReference<T> dotnetObjectReference, string callbackName)
        where T : class;
}
