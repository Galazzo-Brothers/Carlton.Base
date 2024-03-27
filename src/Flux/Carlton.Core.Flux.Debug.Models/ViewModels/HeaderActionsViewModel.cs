namespace Carlton.Core.Flux.Debug.Models.ViewModels;

/// <summary>
/// Represents the view model for header actions.
/// </summary>
public sealed record HeaderActionsViewModel
{
	/// <summary>
	/// Gets or initializes the user name.
	/// </summary>
	[Required]
	public required string UserName { get; init; }

	/// <summary>
	/// Gets or initializes the URL of the avatar.
	/// </summary>
	[Required]
	public required string AvatarUrl { get; init; }
}
