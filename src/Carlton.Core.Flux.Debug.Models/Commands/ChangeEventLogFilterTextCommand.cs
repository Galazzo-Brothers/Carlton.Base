namespace Carlton.Core.Flux.Debug.Models.Commands;

/// <summary>
/// Represents a command to change the filter text for event log filtering.
/// </summary>
public sealed record ChangeEventLogFilterTextCommand
{
	/// <summary>
	/// Gets or initializes the filter text for event log filtering.
	/// </summary>
	[Required(AllowEmptyStrings = true)]
	public required string FilterText { get; init; }
};