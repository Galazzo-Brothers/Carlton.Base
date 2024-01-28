using Carlton.Core.Flux.Models;
using Microsoft.Extensions.Logging;

namespace Carlton.Core.Flux.Debug.Models.Common;

public record TraceLogMessage
{
    public required DateTime Timestamp { get; init; }
    public required FluxActions FluxAction { get; init; }
    public required EventId EventId { get; init; }
    public required string TypeDisplayName { get; init; }
    public required bool RequestSucceeded { get; init; }
    public required BaseRequestContext RequestContext { get; init; }
}

public class TraceLogMessageGroup
{
    public required TraceLogMessage ParentEntry { get; set; }
    public IEnumerable<TraceLogMessage> ChildEntries { get; set; } = new List<TraceLogMessage>();
}

public enum FluxActions
{
    ViewModelQuery,
    MutationCommand
}
