namespace Carlton.Core.Components.Layouts.State;

public class LayoutState(bool isCollapsed) : ILayoutState
{
    public event EventHandler<LayoutNavCollapsedChangedEventsArgs> LayoutNavCollapsedChanged;

    public bool IsCollapsed { get; private set; } = isCollapsed;

    public LayoutState() : this(false)
    {
    }

    public void ToggleCollapsedState()
    {
        IsCollapsed = !IsCollapsed;
        var args = new LayoutNavCollapsedChangedEventsArgs(IsCollapsed);
        LayoutNavCollapsedChanged?.DynamicInvoke(this, args);
    }
}

