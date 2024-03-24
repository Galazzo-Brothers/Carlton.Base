namespace Carlton.Core.Flux.Debug.Models.ViewModels;

public record EventLogDetailsViewModel
{
    public required LogMessage SelectedLogMessage { get; init; }
};
