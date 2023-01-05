namespace Carlton.Base.Components;

public class LayoutState
{
    private readonly Action _stateHasChanged;

    public bool IsCollapsed { get; private set; }
    public Themes Theme { get; private set; }
    public bool IsFooterFixed { get; private set; }


    public LayoutState(bool isCollapsed, Themes theme, bool isFooterFixed, Action stateHasChanged) =>
        (IsCollapsed, Theme, IsFooterFixed, _stateHasChanged) = (isCollapsed, theme, isFooterFixed, stateHasChanged);

    public void ToggleCollapsedState()
    {
        IsCollapsed = !IsCollapsed;
        _stateHasChanged();
    }

    public void SetTheme(Themes theme)
    {
        if(Theme == theme)
            return;

        Theme = theme;
        _stateHasChanged();
    }
}
