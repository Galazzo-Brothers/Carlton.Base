namespace Carlton.Core.Flux.Debug.Models.ViewModels;

public record HeaderActionsViewModel
{
    [Required]
    public required string UserName { get; init; }
    [Required]
    public required string AvatarUrl { get; init; }
}
