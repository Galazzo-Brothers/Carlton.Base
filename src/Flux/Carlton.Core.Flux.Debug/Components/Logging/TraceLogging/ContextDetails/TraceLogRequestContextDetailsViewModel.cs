namespace Carlton.Core.Flux.Debug.Components.Logging.TraceLogging.ContextDetails;

public record TraceLogRequestContextDetailsViewModel
{
    public required object? SelectedRequestContext { get; init; }
};
