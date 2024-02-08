using Carlton.Core.Utilities.Validation;
namespace Carlton.Core.Flux.Debug.Models.Commands;

public record ChangeSelectedLogMessageCommand
{
    [Required]
    [NonNegativeInteger]
    public required int SelectedLogMessageIndex { get; init; }
};
