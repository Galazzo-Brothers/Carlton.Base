namespace Carlton.Core.Components.Layouts.State.FullScreen;

public class FullScreenState(bool isCollapsed) : IFullScreenState
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

        if(FullScreenStateChanged != null)
            FullScreenStateChanged(this, args);
    }
}

