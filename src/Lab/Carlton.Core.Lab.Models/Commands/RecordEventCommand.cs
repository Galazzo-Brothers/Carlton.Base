namespace Carlton.Core.Lab.Models.Commands;

public sealed record RecordEventCommand
{
    [Required]
    public required string RecordedEventName { get; init; }

    [Required]
    public required object EventArgs { get; init; }
};

