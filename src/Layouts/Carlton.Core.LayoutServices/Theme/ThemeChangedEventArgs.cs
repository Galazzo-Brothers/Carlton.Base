namespace Carlton.Core.LayoutServices.Theme;

/// <summary>
/// Represents the event arguments for theme change events.
/// </summary>
/// <param name="theme">The theme associated with the event.</param>
public record ThemeChangedEventArgs(Themes Theme);