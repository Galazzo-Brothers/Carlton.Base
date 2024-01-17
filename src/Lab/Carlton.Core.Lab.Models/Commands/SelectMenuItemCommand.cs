using Carlton.Core.Utilities.Validations;

namespace Carlton.Core.Lab.Models.Commands;

public sealed record SelectMenuItemCommand
{
    [NonNegativeInteger]
    public int ComponentIndex { get; init; }

    [NonNegativeInteger]
    public int ComponentStateIndex { get; init; }

    [Required]
    public required ComponentState SelectedComponentState { get; init; }
};
