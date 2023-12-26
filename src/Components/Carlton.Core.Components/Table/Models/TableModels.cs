namespace Carlton.Core.Components.Table;

public record TableHeadingItem(string DisplayName, string OrderColumn)
{
    public TableHeadingItem(string columnName) : this(columnName, columnName)
    {
    }
};

public record TableRowsPerPageChangedArgs(int SelectedRowsPerPageIndex);

public record TablePageChangedArgs(int CurrentPage);

public record TableOrderingChangedArgs(string OrderColumn, bool OrderAscending);