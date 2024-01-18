namespace Carlton.Core.Lab.Models.Common;

public record ComponentParameters
{
    [Required]
    public required object ParameterObj { get; init; }
};

