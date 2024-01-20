namespace Carlton.Core.Flux.Debug.Models.ViewModels;

public record EventLogScopesViewModel
{
    public required LogMessage SelectedLogMessage { get; init; }
};

