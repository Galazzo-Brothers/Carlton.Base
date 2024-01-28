namespace Carlton.Core.Flux.Debug.Models.ViewModels;

public record TraceLogRequestContextDetailsViewModel
{
    public required TraceLogMessage SelectedTraceLogMessage { get; init; }
};
