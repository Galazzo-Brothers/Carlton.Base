namespace Carlton.Core.Flux.Debug.Components.Logging.TraceLogging.LogTable;

public record SelectedTraceLogMessageChangedArgs(int SelectedTraceLogMessageIndex);

public record TraceLogMessageExpansionChangedArgs(int Index, bool IsExpanded);
