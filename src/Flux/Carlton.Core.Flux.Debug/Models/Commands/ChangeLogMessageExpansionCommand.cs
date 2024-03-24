namespace Carlton.Core.Flux.Debug.Models.Commands;

public record ChangeLogMessageExpansionCommand(int TraceLogMessageIndex, bool IsExpanded);
