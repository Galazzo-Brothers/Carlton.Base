namespace Carlton.Core.Components.Layouts.State.FullScreen;

/// <summary>
/// Represents the event arguments for full screen state change events.
/// </summary>
public sealed record FullScreenStateChangedEventArgs(bool IsCollapsed);