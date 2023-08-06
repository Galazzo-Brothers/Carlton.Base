namespace Carlton.Core.Components.Library;

public class LayoutState
{
    private readonly Action _stateHasChanged;

    public bool IsCollapsed { get; private set; }

    public LayoutState(bool isCollapsed, Action stateHasChanged) =>
        (IsCollapsed, _stateHasChanged) = (isCollapsed, stateHasChanged);

    public void ToggleCollapsedState()
    {
        IsCollapsed = !IsCollapsed;
        _stateHasChanged();
    }
}
