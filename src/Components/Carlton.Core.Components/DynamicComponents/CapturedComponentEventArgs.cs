namespace Carlton.Core.Components.DynamicComponents;

public record CapturedComponentEventArgs
{
    public required string EventName { get; init; }
    public object EventArgs { get; init; } = new object();
}
