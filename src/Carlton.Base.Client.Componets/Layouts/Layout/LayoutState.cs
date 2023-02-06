namespace Carlton.Base.Components;

public class LayoutState
{
    private readonly Action _stateHasChanged;

    public bool IsCollapsed { get; private set; }

    public LayoutState(bool isCollapsed, Action stateHasChanged) =>
        (IsCollapsed, _stateHasChanged) = (IsCollapsed, stateHasChanged);

    public void ToggleCollapsedState()
    {
        IsCollapsed = !IsCollapsed;
        _stateHasChanged();
    }
}
