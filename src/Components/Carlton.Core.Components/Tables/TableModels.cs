namespace Carlton.Core.Components.Tables;

/// <summary>
/// Represents a table heading item with display name and ordering name.
/// </summary>
public sealed record TableHeadingItem
{
    /// <summary>
    /// Gets the display name of the table heading item.
    /// </summary>
    public string DisplayName { get; }

    /// <summary>
    /// Gets the ordering name of the table heading item.
    /// </summary>
    public string OrderingName { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TableHeadingItem"/> record with the specified display and ordering names.
    /// </summary>
    /// <param name="displayName">The display name of the table heading item.</param>
    /// <param name="orderingName">The ordering name of the table heading item.</param>
    public TableHeadingItem(string displayName, string orderingName)
    {
        DisplayName = displayName;
        OrderingName = orderingName;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TableHeadingItem"/> record with the same display and ordering names.
    /// </summary>
    /// <param name="columnName">The name of the column used for both display and ordering.</param>
    public TableHeadingItem(string columnName) : this(columnName, columnName)
    {
    }
}

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