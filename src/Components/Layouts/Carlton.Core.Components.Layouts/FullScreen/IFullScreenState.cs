namespace Carlton.Core.Components.Layouts.FullScreen;

/// <summary>
/// Represents the interface for managing full screen state.
/// </summary>
public interface IFullScreenState
{
    /// <summary>
    /// Occurs when the full screen state changes.
    /// </summary>
    public event EventHandler<FullScreenStateChangedEventArgs> FullScreenStateChanged;

    /// <summary>
    /// Gets a value indicating whether the application is in full screen mode.
    /// </summary>
    public bool IsFullScreen { get; }

    /// <summary>
    /// Toggles the full screen mode.
    /// </summary>
    public void ToggleFullScreen();
}