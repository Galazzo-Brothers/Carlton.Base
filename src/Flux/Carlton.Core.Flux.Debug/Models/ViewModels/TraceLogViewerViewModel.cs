namespace Carlton.Core.Flux.Debug.Models.ViewModels;

public record TraceLogViewerViewModel
{
    [Required]
    public required int SelectedTraceLogMessageIndex { get; init; }
    [Required]
    public required IEnumerable<TraceLogMessageGroup> TraceLogMessages { get; init; } = new List<TraceLogMessageGroup>();
    [Required]
    public required IEnumerable<int> ExpandedRowIndexes { get; init; } = new List<int>();
    [Required]
    public required TableState TraceLogTableState { get; init; } = new();
};
