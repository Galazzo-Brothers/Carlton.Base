namespace Carlton.Core.Flux.Debug.Components.Logging.EventLogging.LogTable;

/// <summary>
/// Represents arguments for when the selected event log message changes.
/// </summary>
/// <param name="SelectedLogMessage">The selected log message.</param>
public sealed record SelectedEventLogMessageChangedArgs(LogMessage SelectedLogMessage);