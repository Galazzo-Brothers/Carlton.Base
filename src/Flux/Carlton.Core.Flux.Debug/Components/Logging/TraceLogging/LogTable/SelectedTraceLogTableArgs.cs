namespace Carlton.Core.Flux.Debug.Components.Logging.TraceLogging.LogTable;

/// <summary>
/// Represents the arguments for the event raised when the selected trace log message changes.
/// </summary>
/// <param name="SelectedTraceLogMessage">The selected trace log message.</param>
public sealed record SelectedTraceLogMessageChangedArgs(TraceLogMessage SelectedTraceLogMessage);

/// <summary>
/// Represents the arguments for the event raised when the expansion state of a trace log message changes.
/// </summary>
/// <param name="Index">The index of the trace log message whose expansion state changed.</param>
/// <param name="IsExpanded">Indicates whether the trace log message is expanded or collapsed.</param>
public record TraceLogMessageExpansionChangedArgs(TraceLogMessage TraceLogMessage, bool IsExpanded);
