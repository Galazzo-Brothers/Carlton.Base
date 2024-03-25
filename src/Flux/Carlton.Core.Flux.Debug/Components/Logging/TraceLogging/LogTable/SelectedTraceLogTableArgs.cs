namespace Carlton.Core.Flux.Debug.Components.Logging.TraceLogging.LogTable;

/// <summary>
/// Represents the arguments for the event raised when the selected trace log message changes.
/// </summary>
/// <param name="SelectedTraceLogMessageIndex">The index of the selected trace log message.</param>
public sealed record SelectedTraceLogMessageChangedArgs(int SelectedTraceLogMessageIndex);

/// <summary>
/// Represents the arguments for the event raised when the expansion state of a trace log message changes.
/// </summary>
/// <param name="Index">The index of the trace log message whose expansion state changed.</param>
/// <param name="IsExpanded">Indicates whether the trace log message is expanded or collapsed.</param>
public record TraceLogMessageExpansionChangedArgs(int Index, bool IsExpanded);
