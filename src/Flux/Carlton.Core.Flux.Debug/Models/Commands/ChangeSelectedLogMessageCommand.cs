using Carlton.Core.Utilities.Validations;

namespace Carlton.Core.Flux.Debug.Models.Commands;

public record ChangeSelectedLogMessageCommand
{
    [Required]
    [NonNegativeInteger]
    public required int SelectedLogMessageIndex { get; init; }
};
