namespace Carlton.Core.Lab.Models.ViewModels;

public sealed record ComponentViewerViewModel
{
    [Required]
    public required Type ComponentType { get; init; }

    [Required]
    public required ComponentParameters ComponentParameters { get; init; }
};