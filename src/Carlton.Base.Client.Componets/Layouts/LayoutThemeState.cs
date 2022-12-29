namespace Carlton.Base.Components;

public class LayoutThemeState
{
    public LayoutThemeState(Themes theme)
    {
        Theme = theme;
    }

    public Themes Theme { get; private set; }

    public void SetTheme(Themes theme)
    {
        Theme = theme;
    }
}
