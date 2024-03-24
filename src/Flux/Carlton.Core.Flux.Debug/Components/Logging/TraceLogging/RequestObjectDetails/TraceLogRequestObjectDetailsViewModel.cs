namespace Carlton.Core.Flux.Debug.Components.Logging.TraceLogging.RequestObjectDetails;

public record TraceLogRequestObjectDetailsViewModel
{
    public required object SelectedRequestObject { get; init; }
};
