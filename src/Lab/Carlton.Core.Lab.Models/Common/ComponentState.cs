namespace Carlton.Core.Lab.Models.Common;

/// <summary>
/// Represents the test lab state of a component.
/// </summary>
public record ComponentState
{
	/// <summary>
	/// Gets or initializes the display name of the component state.
	/// </summary>
	[Required]
	public required string DisplayName { get; init; }

	/// <summary>
	/// Gets or initializes the parameters of the component state.
	/// </summary>
	[Required]
	public required object ComponentParameters { get; init; }
};

