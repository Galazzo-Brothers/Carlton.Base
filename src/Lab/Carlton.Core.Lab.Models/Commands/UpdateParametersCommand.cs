namespace Carlton.Core.Lab.Models.Commands;

/// <summary>
/// Represents a command to update parameters.
/// </summary>
public sealed record UpdateParametersCommand
{
	/// <summary>
	/// Gets or initializes the parameters to be updated.
	/// </summary>
	[Required]
	public required object Parameters { get; init; }
};

