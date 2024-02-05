namespace Carlton.Core.Lab.Models.Commands;

public sealed record UpdateParametersCommand
{
    [Required]
    public required ComponentParameters Parameters { get; init; }
};

