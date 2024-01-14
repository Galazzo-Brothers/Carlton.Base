namespace Carlton.Core.Flux.Debug.Models.ViewModels;

public record EventLogViewerViewModel(IEnumerable<LogMessage> LogMessages);
