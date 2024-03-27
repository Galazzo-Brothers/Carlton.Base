namespace Carlton.Core.LayoutServices.Viewport;

/// <summary>
/// Represents the event arguments for viewport changed events.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ViewportChangedEventArgs"/> record with the specified viewport.
/// </remarks>
/// <param name="viewport">The viewport model representing the changed viewport.</param>
/// <remarks>
public sealed record ViewportChangedEventArgs(ViewportModel Viewport);
