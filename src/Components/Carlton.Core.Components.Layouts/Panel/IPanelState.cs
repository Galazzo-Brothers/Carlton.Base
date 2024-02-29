namespace Carlton.Core.Components.Layouts.Panel;

/// <summary>
/// Represents the interface for managing the visibility state of a panel.
/// </summary>
public interface IPanelState
{
    /// <summary>
    /// Event that is raised when the visibility of the panel changes.
    /// </summary>
    public event EventHandler<PanelVisibilityChangedEventArgs> PanelVisibilityChangedChanged;

    /// <summary>
    /// Gets a value indicating whether the panel is currently visible.
    /// </summary>
    public bool IsPanelVisible { get; }

    /// <summary>
    /// Sets the visibility state of the panel.
    /// </summary>
    /// <param name="isVisible">True to make the panel visible, false to hide it.</param>
    public void SetPanelVisibility(bool isVisible);

    /// <summary>
    /// Toggles the visibility state of the panel.
    /// </summary>
    public void TogglePanelVisibility();
}
