namespace Carlton.Core.Flux.Debug.Models.Commands;

internal record ChangeLogMessageExpansionCommand(TraceLogMessage TraceLogMessage, bool IsExpanded);
