namespace Carlton.Core.Flux.Debug.Models.Commands;

internal record ChangeLogMessageExpansionCommand(int TraceLogMessageIndex, bool IsExpanded);
