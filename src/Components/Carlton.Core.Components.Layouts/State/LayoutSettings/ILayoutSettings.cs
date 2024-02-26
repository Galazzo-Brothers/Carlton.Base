namespace Carlton.Core.Components.Layouts.State.LayoutSettings;

/// <summary>
/// Represents an interface for managing layout settings.
/// </summary>
public interface ILayoutSettings
{
    /// <summary>
    /// Gets the read-only dictionary containing the layout settings.
    /// </summary>
    public IReadOnlyDictionary<string, object> Settings { get; }
}
