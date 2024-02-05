using Carlton.Core.Components.Table;

namespace Carlton.Core.Components.Library.Lab.TestData;
public static class TableTestStates
{
    public record TableTestDate
    {
        public List<TableHeadingItem> Headings { get; init; }
        public List<TableTestObject> Items { get; init; }
        [JsonIgnore]
        public RenderFragment<TableTestObject> RowTemplate { get; init; }
        public bool ShowPaginationRow { get; init; }
        public List<int> RowsPerPageOpts { get; init; }

        public TableTestDate(List<TableHeadingItem> headings,
            List<TableTestObject> items,
            RenderFragment<TableTestObject> rowTemplate,
            bool showPaginationRow,
            List<int> rowsPerPageOpts
            )
        {
            Headings = headings;
            Items = items;
            RowTemplate = rowTemplate;
            ShowPaginationRow = showPaginationRow;
            RowsPerPageOpts = rowsPerPageOpts;
        }

    }

    public static Dictionary<string, object> LargeItemList
    {
        get => new()
        {
            { nameof(Table<int>.Headings), Headings },
            { nameof(Table<int>.Items), BigList },
            { nameof(Table<int>.RowTemplate), RowTemplate },
            { nameof(Table<int>.ShowPaginationRow), true },
            { nameof(Table<int>.RowsPerPageOpts), RowsPerPageOptions }
        };
    }

    public static Dictionary<string, object> SmallItemList
    {
        get => new()
        {
            { nameof(Table<int>.Headings), Headings },
            { nameof(Table<int>.Items), SmallItemList },
            { nameof(Table<int>.RowTemplate), RowTemplate },
            { nameof(Table<int>.ShowPaginationRow), true },
            { nameof(Table<int>.RowsPerPageOpts), RowsPerPageOptions }
        };
    }

    public static Dictionary<string, object> WithOutPaginationRow
    {
        get => new()
        {
            { nameof(Table<int>.Headings), Headings },
            { nameof(Table<int>.Items), SmallItemList },
            { nameof(Table<int>.RowTemplate), RowTemplate },
            { nameof(Table<int>.ShowPaginationRow), false },
            { nameof(Table<int>.RowsPerPageOpts), RowsPerPageOptions }
        };
    }

    private static readonly List<TableHeadingItem> Headings =
            [
                    new TableHeadingItem(nameof(TableTestObject.ID)),
                    new TableHeadingItem(nameof(TableTestObject.DisplayName)),
                    new TableHeadingItem(nameof(TableTestObject.CreatedDate))
            ];

    private static readonly List<TableTestObject> BigList =
        [
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
        ];

    private static readonly List<TableTestObject> SmallList =
        [
                new TableTestObject(1, "Item A", new DateTime(2023, 10, 9)),
                new TableTestObject(2, "Item B", new DateTime(2022, 2, 3)),
                new TableTestObject(3, "Item C", new DateTime(2021, 5, 19))
        ];

    private static readonly RenderFragment<TableTestObject> RowTemplate = item =>
            builder =>
            {
                builder.AddMarkupContent(0,
                 $@"
                 <div class=""table-row"">
                     <div class=""table-cell"">
                        <span>{item.ID}</span>
                     </div>
                     <div class= ""table-cell"">
                        <span>{item.DisplayName}</span>
                     </div>
                     <div class=""table-cell"">
                        <span>{item.CreatedDate}</span>
                     </div>
                 </div>");
            };

    private static readonly List<int> RowsPerPageOptions = [5, 10, 15];

    public record TableTestObject(int ID, string DisplayName, DateTime CreatedDate);
}

