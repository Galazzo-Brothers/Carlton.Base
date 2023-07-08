namespace Carlton.Base.Components;

public class ThemeState
{
    private readonly Action _stateHasChanged;

    public Themes Theme { get; private set; }

    public string ThemeString
    {
        get => Theme.ToString();
    }

    public ThemeState(Themes theme, Action stateHasChanged) =>
        (Theme, _stateHasChanged) = (theme, stateHasChanged);

    public void SetTheme(Themes theme)
    {
        if(Theme == theme)
            return;

        Theme = theme;
        _stateHasChanged();
    }
}

