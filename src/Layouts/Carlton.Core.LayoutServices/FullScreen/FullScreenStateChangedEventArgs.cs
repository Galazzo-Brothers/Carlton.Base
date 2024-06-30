namespace Carlton.Core.LayoutServices.FullScreen;

/// <summary>
/// Represents the event arguments for full screen state change events.
/// </summary>
/// <param name="IsCollapsed">Whether the layout is collapsed.</param>
public sealed record FullScreenStateChangedEventArgs(bool IsCollapsed);