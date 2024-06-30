namespace Carlton.Core.LayoutServices.Viewport;

/// <summary>
/// Represents the event arguments for viewport changed events.
/// </summary>
/// <param name="viewport">The viewport model representing the changed viewport.</param>
public sealed record ViewportChangedEventArgs(ViewportModel Viewport);


