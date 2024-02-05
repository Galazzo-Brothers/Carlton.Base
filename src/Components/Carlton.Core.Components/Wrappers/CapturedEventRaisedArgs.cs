namespace Carlton.Core.Components.Wrappers;

public record CapturedEventRaisedArgs
{
    public required string EventName { get; init; }
    public object EventArgs { get; init; } = new object();
}
