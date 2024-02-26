
namespace Carlton.Core.Components.Layouts;

/// <summary>
/// Represents the cascading state of the layout manager.
/// </summary>
public record class LayoutManagerCascadingState
{
    /// <summary>
    /// Gets or initializes a value indicating whether the layout is in full-screen mode.
    /// </summary>
    public bool IsFullScreen { get; init; }

    /// <summary>
    /// Gets or initializes the layout settings as a dictionary of key-value pairs.
    /// </summary>
    public IReadOnlyDictionary<string, object> LayoutSettings { get; init; }
}