using Carlton.Core.Utilities.Validations;

namespace Carlton.Core.Flux.Debug.Models.ViewModels;

public record EventLogViewerViewModel
{
    [NonNegativeInteger]
    public int SelectedLogMessageIndex { get; init; }
    [Required]
    public IEnumerable<LogMessage> LogMessages { get; init; } = new List<LogMessage>();
    [Required]
    public required EventLogViewerFilterState EventLogViewerFilterState { get; init; }
}
