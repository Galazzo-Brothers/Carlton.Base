namespace Carlton.Core.Flux.Debug.Models.ViewModels;

/// <summary>
/// Represents the view model for the event log viewer.
/// </summary>
public sealed record EventLogViewerViewModel
{
	/// <summary>
	/// Gets or initializes the index of the selected log message.
	/// </summary>
	[NonNegativeInteger]
	public int SelectedLogMessageIndex { get; init; }

	/// <summary>
	/// Gets or initializes the list of log messages to display.
	/// </summary>
	[Required]
	public IEnumerable<LogMessage> LogMessages { get; init; } = new List<LogMessage>();

	/// <summary>
	/// Gets or initializes the state of the event log viewer filter.
	/// </summary>
	[Required]
	public required EventLogViewerFilterState EventLogViewerFilterState { get; init; }
}
