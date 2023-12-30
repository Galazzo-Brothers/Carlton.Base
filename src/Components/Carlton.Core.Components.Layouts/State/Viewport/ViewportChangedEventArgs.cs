namespace Carlton.Core.Components.Layouts.State.Viewport;

public class ViewportChangedEventArgs(ViewportModel viewportModel) : EventArgs
{
    public ViewportModel ViewportModel { get; set; } = viewportModel;
}
