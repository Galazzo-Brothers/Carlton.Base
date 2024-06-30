namespace Carlton.Core.Components.Tables;

/// <summary>
/// Represents the event arguments for a rows per page change event.
/// </summary>
/// <param name="SelectedRowsPerPageIndex">The index of the selected rows per page.</param>
public record RowsPerPageChangeEventArgs(int SelectedRowsPerPageIndex);

/// <summary>
/// Represents the event arguments for a page change event.
/// </summary>
/// <param name="CurrentPage">The index of the current page.</param>
public record PageChangeEventArgs(int CurrentPage);

/// <summary>
/// Represents the event arguments for sorting items in a table.
/// </summary>
/// <param name="SortColumn">The name of the column to sort by.</param>
/// <param name="SortAscending">Whether the column should be sorted in ascending order.</param>
public record ItemsSortEventArgs(string SortColumn, bool SortAscending);