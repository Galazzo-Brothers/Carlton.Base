namespace Carlton.Core.Components.Layouts.Panel;

/// <summary>
/// Represents the state management for controlling the visibility of a panel.
/// </summary>
/// <remarks> Initializes a new instance of the <see cref="PanelState"/> class with the specified initial visibility state. </remarks>
/// <param name="isVisible">The initial visibility state of the panel.</param>
public class PanelState(bool isVisible) : IPanelState
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PanelState"/> class with an initial visibility state of true.
    /// </summary>
    public PanelState() : this(true)
    {
    }

    /// <inheritdoc/>
    public event EventHandler<PanelVisibilityChangedEventArgs> PanelVisibilityChangedChanged;

    /// <inheritdoc/>
    public bool IsPanelVisible { get; private set; } = isVisible;

    /// <inheritdoc/>
    public void TogglePanelVisibility()
        => SetPanelVisibility(!IsPanelVisible);

    /// <inheritdoc/>
    public void SetPanelVisibility(bool isVisible)
    {
        if (IsPanelVisible == isVisible)
            return;

        IsPanelVisible = isVisible;
        var args = new PanelVisibilityChangedEventArgs(isVisible);
        PanelVisibilityChangedChanged?.Invoke(this, args);
    }
}
