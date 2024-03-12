namespace Carlton.Core.Flux.Debug.Models.ViewModels;

public record TraceLogRequestContextDetailsViewModel
{
	public required object? SelectedRequestContext { get; init; }
};
