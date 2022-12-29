namespace Carlton.Base.Components;

public class LayoutCollapsedState
{
    public bool IsCollapsed { get; private set; } = false;
    public void ToggleCollapsedStatus() => IsCollapsed = !IsCollapsed;
}
