using Carlton.Core.Flux.Models;
namespace Carlton.Core.Flux.Debug.Models.ViewModels;

public record TraceLogRequestContextDetailsViewModel
{
    public required BaseRequestContext? SelectedRequestContext{ get; init; }
};
