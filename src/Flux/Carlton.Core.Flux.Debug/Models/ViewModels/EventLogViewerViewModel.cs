using Carlton.Core.Utilities.Validation;
namespace Carlton.Core.Flux.Debug.Models.ViewModels;

public record EventLogViewerViewModel
{
    [NonNegativeInteger]
    public int SelectedLogMessageIndex { get; init; }
    [Required]
    public IEnumerable<LogMessage> LogMessages { get; init; } = new List<LogMessage>();
    [Required]
    public required EventLogViewerFilterState EventLogViewerFilterState { get; init; }
    [Required]
    public required TableState EventLogTableState { get; init; }
}
