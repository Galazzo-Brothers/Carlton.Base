namespace Carlton.Core.Flux.Debug.Models.ViewModels;

public record EventLogViewerViewModel
{
    public IEnumerable<LogMessage> LogMessages { get; init; } = new List<LogMessage>();
}
