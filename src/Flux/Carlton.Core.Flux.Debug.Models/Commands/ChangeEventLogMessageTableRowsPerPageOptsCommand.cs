namespace Carlton.Core.Flux.Debug.Models.Commands;

/// <summary>
/// Represents a command to change the number of rows per page options for the event log message table.
/// </summary>
public sealed record ChangeEventLogMessageTableRowsPerPageOptsCommand
{
	/// <summary>
	/// Gets the index of the selected rows per page option.
	/// </summary>
	[NonNegativeInteger]
	public required int SelectedRowsPerPageIndex { get; init; }
};
