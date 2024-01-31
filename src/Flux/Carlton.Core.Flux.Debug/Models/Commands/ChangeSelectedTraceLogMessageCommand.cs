using Carlton.Core.Utilities.Validations;

namespace Carlton.Core.Flux.Debug.Models.Commands;

public record ChangeSelectedTraceLogMessageCommand
{
    [Required]
    [NonNegativeInteger]
    public required int SelectedTraceLogMessageIndex { get; init; }
};
