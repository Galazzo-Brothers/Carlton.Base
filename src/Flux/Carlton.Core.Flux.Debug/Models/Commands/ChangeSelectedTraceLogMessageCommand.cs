namespace Carlton.Core.Flux.Debug.Models.Commands;

public record ChangeSelectedTraceLogMessageCommand
{
    [Required]
    public required TraceLogMessage SelectedTraceLogMessage { get; init; }
};
