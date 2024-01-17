namespace Carlton.Core.Lab.Models.ViewModels;

public record BreadCrumbsViewModel
{
    [Required]
    public required string SelectedComponent { get; init; }

    [Required]
    public required string SelectedComponentState { get; init; }
};