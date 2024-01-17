namespace Carlton.Core.Lab.Models.Common;

public record ComponentRecordedEvent
{
    [Required(AllowEmptyStrings = false)]
    public required string Name { get; init; }

    [Required]
    public required object EventObj { get; init; }
}
