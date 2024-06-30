namespace Carlton.Core.LayoutServices.Theme;

/// <summary>
/// Represents the state management for application themes.
/// </summary>
/// <remark>
/// Initializes a new instance of the <see cref="ThemeState"/> class with the specified theme.
/// </remark>
/// <param name="theme">The initial theme of the application.</param>
internal class ThemeState(Themes theme) : IThemeState
{
	/// <summary>
	/// Occurs when the application theme changes.
	/// </summary>
	public event EventHandler<ThemeChangedEventArgs> ThemeChanged;

	/// <summary>
	/// Gets or sets the current application theme.
	/// </summary>
	public Themes Theme { get; private set; } = theme;

	/// <summary>
	/// Initializes a new instance of the <see cref="ThemeState"/> class with the light theme as the initial theme.
	/// </summary>
	public ThemeState() : this(Themes.light)
	{
	}

	/// <inheritdoc/>
	public void ToggleTheme()
	{
		switch (Theme)
		{
			case Themes.light:
				SetTheme(Themes.dark);
				break;
			case Themes.dark:
				SetTheme(Themes.light);
				break;
			default:
				return;
		}
	}

	/// <inheritdoc/>
	public void SetTheme(Themes theme)
	{
		if (Theme == theme)
			return;

		Theme = theme;
		var args = new ThemeChangedEventArgs(theme);
		ThemeChanged?.Invoke(this, args);
	}
}

