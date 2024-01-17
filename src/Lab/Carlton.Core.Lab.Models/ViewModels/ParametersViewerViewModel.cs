namespace Carlton.Core.Lab.Models.ViewModels;
public sealed record ParametersViewerViewModel
{
    [Required]
    public required ComponentParameters ComponentParameters { get; init; }
};
