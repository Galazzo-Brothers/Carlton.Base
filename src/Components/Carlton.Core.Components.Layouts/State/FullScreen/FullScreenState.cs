namespace Carlton.Core.Components.Layouts.State.FullScreen;

public sealed class FullScreenState(bool isCollapsed) : IFullScreenState
{
    public event EventHandler<FullScreenStateChangedEventArgs> FullScreenStateChanged;

    public bool IsFullScreen { get; private set; } = isCollapsed;

    public FullScreenState() : this(false)
    {
    }

    public void ToggleFullScreen()
    {
        IsFullScreen = !IsFullScreen;
        var args = new FullScreenStateChangedEventArgs(IsFullScreen);
        FullScreenStateChanged?.Invoke(this, args);
    }
}

