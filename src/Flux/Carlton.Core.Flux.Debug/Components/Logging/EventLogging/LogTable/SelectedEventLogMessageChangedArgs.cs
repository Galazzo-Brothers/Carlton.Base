namespace Carlton.Core.Flux.Debug.Components.Logging.EventLogging.LogTable;

/// <summary>
/// Represents arguments for when the selected event log message changes.
/// </summary>
/// <param name="SelectedLogMessageIndex">The index of the selected log message.</param>
public sealed record SelectedEventLogMessageChangedArgs(int SelectedLogMessageIndex);