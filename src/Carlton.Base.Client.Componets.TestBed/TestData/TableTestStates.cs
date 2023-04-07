namespace Carlton.Base.Components.TestBed;

public static class TableTestStates
{
    public static Dictionary<string, object> LargeItemList
    {
        get => new()
        {
            { "Headings", Headings },
            { "Items", BigList },
            { "RowTemplate", RowTemplate },
            { "ShowPaginationRow", true },
            { "RowsPerPageOpts", new List<int> {5, 10, 15} }
        };
    }

    public static Dictionary<string, object> SmallItemList
    {
        get => new()
        {
            { "Headings", Headings },
            { "Items", SmallList },
            { "RowTemplate", RowTemplate },
            { "ShowPaginationRow", true },
            { "RowsPerPageOpts", new List<int> {5, 10, 15} }
        };
    }

    public static Dictionary<string, object> WithOutPaginationRow
    {
        get => new()
        {
            { "Headings", Headings },
            { "Items", SmallList },
            { "RowTemplate", RowTemplate },
            { "ShowPaginationRow", false },
            { "RowsPerPageOpts", new List<int> {5, 10, 15} }
        };
    }

    private static readonly List<TableHeadingItem> Headings =
        new()
                {
                    new TableHeadingItem(nameof(TableTestObject.ID)),
                    new TableHeadingItem(nameof(TableTestObject.DisplayName)),
                    new TableHeadingItem(nameof(TableTestObject.CreatedDate))
                };

    private static readonly List<TableTestObject> BigList =
        new()
        {
                new TableTestObject(1, "Item A", new DateTime(2023, 10, 9)),
                new TableTestObject(2, "Item B", new DateTime(2022, 2, 3)),
                new TableTestObject(3, "Item C", new DateTime(2021, 5, 19)),
                new TableTestObject(4, "Item 1", new DateTime(2023, 10, 9)),
                new TableTestObject(5, "Item 2", new DateTime(2022, 2, 3)),
                new TableTestObject(6, "Item 3", new DateTime(2021, 5, 19)),
                new TableTestObject(7, "Additional Item A", new DateTime(2023, 10, 9)),
                new TableTestObject(8, "Additional Item B", new DateTime(2022, 2, 3)),
                new TableTestObject(9, "Additional Item C", new DateTime(2021, 5, 19)),
                new TableTestObject(10, "Additional Item 1", new DateTime(2023, 10, 9)),
                new TableTestObject(11, "Additional Item 2", new DateTime(2022, 2, 3)),
                new TableTestObject(12, "Additional Item 3", new DateTime(2021, 5, 19)),
                new TableTestObject(13, "Some Item", new DateTime(2023, 10, 9)),
                new TableTestObject(14, "Another Item", new DateTime(2022, 2, 3)),
                new TableTestObject(15, "The Final Item", new DateTime(2021, 5, 19))
                };

    private static readonly List<TableTestObject> SmallList =
        new()
        {
                new TableTestObject(1, "Item A", new DateTime(2023, 10, 9)),
                new TableTestObject(2, "Item B", new DateTime(2022, 2, 3)),
                new TableTestObject(3, "Item C", new DateTime(2021, 5, 19))
        };

    private static readonly RenderFragment<TableTestObject> RowTemplate = item =>
            builder =>
            {
                builder.AddMarkupContent(0,
                 $@"<div class=""row-item"">
                    <span>{item.ID}</span>
              </div>
             <div class= ""row-item"">
                <span>{item.DisplayName}</span>
            </div>
            <div class=""row-item"">
                <span>{item.CreatedDate}</span>
            </div>");
            };

    public record TableTestObject(int ID, string DisplayName, DateTime CreatedDate);
}

