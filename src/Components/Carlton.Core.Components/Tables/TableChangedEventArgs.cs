namespace Carlton.Core.Components.Tables;

/// <summary>
/// Represents the event arguments for a rows per page change event.
/// </summary>
public record RowsPerPageChangeEventArgs(int SelectedRowsPerPageIndex);

/// <summary>
/// Represents the event arguments for a page change event.
/// </summary>
public record PageChangeEventArgs(int CurrentPage);

/// <summary>
/// Represents the event arguments for sorting items in a table.
/// </summary>
public record ItemsSortEventArgs(string SortColumn, bool SortAscending);