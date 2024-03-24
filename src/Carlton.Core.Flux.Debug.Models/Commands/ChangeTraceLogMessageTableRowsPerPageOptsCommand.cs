namespace Carlton.Core.Flux.Debug.Models.Commands;

/// <summary>
/// Represents a command to change the number of rows per page options for the trace log message table.
/// </summary>
public sealed record ChangeTraceLogMessageTableRowsPerPageOptsCommand
{
	/// <summary>
	/// Gets or initializes the index of the selected rows per page option.
	/// </summary>
	[NonNegativeInteger]
	public required int SelectedRowsPerPageIndex { get; init; }
};
