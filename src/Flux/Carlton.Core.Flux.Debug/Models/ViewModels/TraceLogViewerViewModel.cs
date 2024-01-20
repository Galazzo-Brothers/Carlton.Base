namespace Carlton.Core.Flux.Debug.Models.ViewModels;

public record TraceLogViewerViewModel
{
    public IEnumerable<LogMessage> LogMessages { get; init; } = new List<LogMessage>();
};
