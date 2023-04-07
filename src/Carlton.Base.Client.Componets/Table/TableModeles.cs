namespace Carlton.Base.Components;

public record TableHeadingItem(string DisplayName, string OrderColumn)
{
    public TableHeadingItem(string columnName) : this(columnName, columnName)
    {
    }
};

public record TablePaginationChangedArgs(int CurrentPage, int SelectedRowsPerPageCount);

public record TableOrderingChangedArgs(string OrderColumn, bool OrderAscending);