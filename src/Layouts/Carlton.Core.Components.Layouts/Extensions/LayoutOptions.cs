using Carlton.Core.Components.Layouts.Theme;

namespace Carlton.Core.Components.Layouts.Extensions;

/// <summary>
/// Represents default LayoutOptions
/// </summary>
public class LayoutOptions
{
    /// <summary>
    /// Gets or Sets a value indicating the default theme of the layout.
    /// </summary>
    public Themes Theme { get; set; }

    /// <summary>
    /// Gets or Sets a value indicating whether the layout is in full-screen mode.
    /// </summary>
    public bool IsFullScreen { get; set; }

    /// <summary>
    /// Gets or Sets a value indicating whether the layout panel content is visible.
    /// </summary>
    public bool ShowPanel { get; set; }
}