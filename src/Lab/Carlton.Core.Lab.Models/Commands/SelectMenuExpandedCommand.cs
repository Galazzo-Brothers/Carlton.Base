using Carlton.Core.Utilities.Validation;
namespace Carlton.Core.Lab.Models.Commands;

public sealed record SelectMenuExpandedCommand
{
    [NonNegativeInteger]
    public int SelectedComponentIndex { get; init; }

    public bool IsExpanded { get; init; }
};

