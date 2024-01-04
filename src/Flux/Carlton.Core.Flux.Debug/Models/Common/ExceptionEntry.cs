namespace Carlton.Core.Flux.Debug.Models.Common;

public record ExceptionEntry
{
    public string ExceptionType { get; init; }
    public string Message { get; init; }
    public string StackTrace { get; init; }
}