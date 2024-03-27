using Carlton.Core.Foundation.State;
namespace Carlton.Core.Flux.Debug.Models.ViewModels;

/// <summary>
/// Represents the view model for the trace log viewer component.
/// </summary>
public sealed record TraceLogViewerViewModel
{
	/// <summary>
	/// Gets or initializes the index of the selected trace log message.
	/// </summary>
	[Required]
	public required int SelectedTraceLogMessageIndex { get; init; }

	/// <summary>
	/// Gets or initializes the trace log messages.
	/// </summary>
	[Required]
	public required IEnumerable<TraceLogMessageGroup> TraceLogMessages { get; init; } = new List<TraceLogMessageGroup>();

	/// <summary>
	/// Gets or initializes the indexes of expanded rows.
	/// </summary>
	[Required]
	public required IEnumerable<int> ExpandedRowIndexes { get; init; } = new List<int>();

	/// <summary>
	/// Gets or initializes the state of the trace log table.
	/// </summary>
	[Required]
	public required TableState TraceLogTableState { get; init; } = new();
};
