namespace Carlton.Core.Components.Layouts.State.Theme;

public interface IThemeState
{
    public event EventHandler<ThemeChangedEventArgs> ThemeChanged;
    public Themes Theme { get; }
    public void ToggleTheme();
    public void SetTheme(Themes theme);
}
