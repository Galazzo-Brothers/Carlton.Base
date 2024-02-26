namespace Carlton.Core.Components.Layouts.State.Theme;

/// <summary>
/// Represents the interface for managing application themes.
/// </summary>
public interface IThemeState
{
    /// <summary>
    /// Occurs when the application theme changes.
    /// </summary>
    public event EventHandler<ThemeChangedEventArgs> ThemeChanged;

    /// <summary>
    /// Gets the current application theme.
    /// </summary>
    public Themes Theme { get; }

    /// <summary>
    /// Toggles between light and dark themes.
    /// </summary>
    public void ToggleTheme();

    /// <summary>
    /// Sets the application theme to the specified theme.
    /// </summary>
    /// <param name="theme">The theme to set.</param>
    public void SetTheme(Themes theme);
}
