namespace Carlton.Core.Flux.Debug.Models.ViewModels;

public record TraceLogViewerViewModel
{
    public IEnumerable<TraceLogMessageGroup> TraceLogMessages { get; init; } = new List<TraceLogMessageGroup>();
};
