namespace Carlton.Core.Flux.Debug.Models.Commands;

public record ChangeEventLogTableOrderingCommand
{
    [Required(AllowEmptyStrings = true)]
    public string OrderByColum { get; init; } = string.Empty;
    public required bool OrderAscending { get; init; } = true;
};
