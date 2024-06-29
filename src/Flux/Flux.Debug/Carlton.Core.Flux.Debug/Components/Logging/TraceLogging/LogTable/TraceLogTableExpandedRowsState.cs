namespace Carlton.Core.Flux.Debug.Components.Logging.TraceLogging.LogTable;

internal record TraceLogTableExpandedRowsState
{
	public List<int> ExpandedRows { get; init; } = [];
}
