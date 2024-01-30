namespace Carlton.Core.Components.ComponentViewer;

public record ComponentEventRaisedArgs
{
    public required string EventName { get; init; }
    public object EventArgs { get; init; } = new object();
}
