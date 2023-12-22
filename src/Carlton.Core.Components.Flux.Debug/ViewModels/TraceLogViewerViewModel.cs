using Carlton.Core.Utilities.Logging;

namespace Carlton.Core.Components.Flux.Debug.ViewModels;

public record TraceLogViewerViewModel(IEnumerable<LogMessage> LogMessages);
