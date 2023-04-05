namespace Carlton.Base.Components;

public record TableHeadingItem(string DisplayName, string OrderColumn)
{
    public TableHeadingItem(string columnName) : this(columnName, columnName)
    {
    }
};
