using Carlton.Core.Flux.Dispatchers;

namespace Carlton.Core.Flux.Debug.Models.ViewModels;

public record TraceLogRequestContextDetailsViewModel
{
	public required BaseRequestContext? SelectedRequestContext { get; init; }
};
