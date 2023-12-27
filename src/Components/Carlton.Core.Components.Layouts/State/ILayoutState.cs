namespace Carlton.Core.Components.Layouts.State;

public interface ILayoutState
{
    public event EventHandler<LayoutNavCollapsedChangedEventsArgs> LayoutNavCollapsedChanged;

    public bool IsCollapsed { get; }

    public void ToggleCollapsedState();
}