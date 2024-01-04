using Microsoft.JSInterop;

namespace Carlton.Core.Components.Layouts.State.Viewport;

public interface IViewportState
{
    public event EventHandler<ViewportChangedEventArgs> ViewportChanged;
    public Task<ViewportModel> GetCurrentViewport();
}
