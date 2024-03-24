namespace Carlton.Core.Flux.Debug.Models.Commands;

/// <summary>
/// Represents a command to change the ordering of the event log table.
/// </summary>
public sealed record ChangeEventLogTableOrderingCommand
{
	/// <summary>
	/// Gets or initializes the column by which the event log table is ordered.
	/// </summary>
	[Required(AllowEmptyStrings = true)]
	public string OrderByColum { get; init; } = string.Empty;

	/// <summary>
	/// Gets or initializes a value indicating whether the event log table is ordered in ascending order.
	/// </summary>
	public required bool OrderAscending { get; init; } = true;
};
