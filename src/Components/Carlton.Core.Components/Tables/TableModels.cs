namespace Carlton.Core.Components.Tables;

public record TableHeadingItem(string DisplayName, string OrderingName)
{
    public TableHeadingItem(string columnName) : this(columnName, columnName)
    {
    }
};

public record RowsPerPageChangeEventArgs(int SelectedRowsPerPageIndex);

public record PageChangeEventArgs(int CurrentPage);

public record ItemsSortEventArgs(string SortColumn, bool SortAscending);