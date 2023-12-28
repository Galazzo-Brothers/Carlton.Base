namespace Carlton.Core.Components.Layouts.State.FullScreen;

public interface IFullScreenState
{
    public event EventHandler<FullScreenStateChangedEventArgs> FullScreenStateChanged;

    public bool IsFullScreen { get; }

    public void ToggleFullScreen();
}