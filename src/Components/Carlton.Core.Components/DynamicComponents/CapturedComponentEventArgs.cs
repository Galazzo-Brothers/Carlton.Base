namespace Carlton.Core.Components.DynamicComponents;

/// <summary>
/// Represents the event arguments captured from a component.
/// </summary>
public sealed record CapturedComponentEventArgs
{
    /// <summary>
    /// Gets the name of the captured event.
    /// </summary>
    public string EventName { get; init; }

    /// <summary>
    /// Gets or sets the arguments associated with the captured event.
    /// </summary>
    public object EventArgs { get; init; } = new object();
}