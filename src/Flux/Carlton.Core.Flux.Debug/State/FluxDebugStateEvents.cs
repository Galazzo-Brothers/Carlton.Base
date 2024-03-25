namespace Carlton.Core.Flux.Debug.State;

internal enum FluxDebugStateEvents
{
	LoadLogMessages,
	LogsCleared,
	EventLogLevelFiltersChanged,
	EventLogFilterTextChanged,
	EventLogPageChanged,
	EventLogRowsPerPageChanged,
	EventLogTableOrderingChanged,
	TraceLogTablePageChanged,
	TraceLogTableRowsPerPageChanged,
	SelectedLogMessageChanged,
	SelectedTraceLogMessageChanged,
	TraceLogMessageExpandedChanged
}
