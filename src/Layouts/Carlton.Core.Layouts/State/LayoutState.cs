namespace Carlton.Core.Components.Layouts.State;

public interface ILayoutState
{
    public event EventHandler<LayoutNavCollapsedChangedEventsArgs> LayoutNavCollapsedChanged;

    public bool IsCollapsed { get; }

    public void ToggleCollapsedState();
}

public class LayoutState
{
    public event EventHandler<LayoutNavCollapsedChangedEventsArgs> LayoutNavCollapsedChanged;

    public bool IsCollapsed { get; private set; }

    public LayoutState() : this(false)
    {
    }

    public LayoutState(bool isCollapsed) =>
        IsCollapsed = isCollapsed;

    public void ToggleCollapsedState()
    {
        IsCollapsed = !IsCollapsed;
    }
}

public record LayoutNavCollapsedChangedEventsArgs(bool IsCollapsed);