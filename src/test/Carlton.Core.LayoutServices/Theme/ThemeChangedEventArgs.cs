namespace Carlton.Core.LayoutServices.Theme;

/// <summary>
/// Represents the event arguments for theme change events.
/// </summary>
/// <remarks>
/// The properties of this record are initialized upon creation and cannot be modified after instantiation.
/// </remarks>
/// <param name="theme">The theme associated with the event.</param>
public record ThemeChangedEventArgs(Themes Theme);