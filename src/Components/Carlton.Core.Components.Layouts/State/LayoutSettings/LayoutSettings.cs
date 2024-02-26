namespace Carlton.Core.Components.Layouts.State.LayoutSettings;

/// <summary>
/// Represents a concrete implementation of layout settings.
/// </summary>
public sealed class LayoutSettings : ILayoutSettings
{
    /// <summary>
    /// Gets the read-only dictionary containing the layout settings.
    /// </summary>
    public IReadOnlyDictionary<string, object> Settings { get; init; } = new Dictionary<string, object>();
}
