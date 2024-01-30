namespace Carlton.Core.Lab.Models.Commands;

public sealed record RecordEventCommand
{
    [Required]
    public required string RecordedEventName { get; init; }

    public object EventArgs { get; init; } = new object();
};

