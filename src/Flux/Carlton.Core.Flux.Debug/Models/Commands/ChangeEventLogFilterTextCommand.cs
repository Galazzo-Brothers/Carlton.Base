namespace Carlton.Core.Flux.Debug.Models.Commands;

public record ChangeEventLogFilterTextCommand
{
    [Required(AllowEmptyStrings = true)]
    public required string FilterText { get; init; }
};
