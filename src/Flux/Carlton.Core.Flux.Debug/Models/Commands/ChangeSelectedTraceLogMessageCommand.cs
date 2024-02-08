using Carlton.Core.Utilities.Validation;
namespace Carlton.Core.Flux.Debug.Models.Commands;

public record ChangeSelectedTraceLogMessageCommand
{
    [Required]
    [NonNegativeInteger]
    public required int SelectedTraceLogMessageIndex { get; init; }
};
