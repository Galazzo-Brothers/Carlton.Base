namespace Carlton.Core.Components.Layouts.State.Viewport;

/// <summary>
/// Represents the interface for managing viewport state.
/// </summary>
public interface IViewportState
{
    /// <summary>
    /// Occurs when the viewport changes.
    /// </summary>
    public event EventHandler<ViewportChangedEventArgs> ViewportChanged;

    /// <summary>
    /// Retrieves the current viewport asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation that returns the current viewport.</returns>
    public Task<ViewportModel> GetCurrentViewport();
}
