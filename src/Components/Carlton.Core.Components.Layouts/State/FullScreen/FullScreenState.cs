namespace Carlton.Core.Components.Layouts.State.FullScreen;


/// <summary>
/// Represents the state of full screen mode.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="FullScreenState"/> class with the specified initial full screen mode.
/// </remarks>
/// <param name="isCollapsed">The initial full screen mode.</param>
public sealed class FullScreenState(bool isCollapsed) : IFullScreenState
{
    /// <summary>
    /// Occurs when the full screen state changes.
    /// </summary>
    public event EventHandler<FullScreenStateChangedEventArgs> FullScreenStateChanged;

    /// <summary>
    /// Gets a value indicating whether the application is in full screen mode.
    /// </summary>
    public bool IsFullScreen { get; private set; } = isCollapsed;

    /// <summary>
    /// Initializes a new instance of the <see cref="FullScreenState"/> class with full screen mode disabled by default.
    /// </summary>
    public FullScreenState() : this(false)
    {
    }

    /// <summary>
    /// Toggles the full screen mode.
    /// </summary>
    public void ToggleFullScreen()
    {
        // Toggle the full screen mode
        IsFullScreen = !IsFullScreen;

        // Raise the FullScreenStateChanged event
        var args = new FullScreenStateChangedEventArgs(IsFullScreen);
        FullScreenStateChanged?.Invoke(this, args);
    }
}