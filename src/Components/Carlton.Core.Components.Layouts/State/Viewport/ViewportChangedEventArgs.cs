namespace Carlton.Core.Components.Layouts.State.Viewport;

public class ViewportChangedEventArgs(ViewportModel newViewport) : EventArgs
{
    public ViewportModel NewViewport { get; set; } = newViewport;
}
