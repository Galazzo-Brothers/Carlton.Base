using Carlton.Core.Components.Tables;
namespace Carlton.Core.Components.Lab.TestData;
internal static class TableTestStates
{
	public record TableStateParameters
	{
		public List<TableHeadingItem> Headings { get; init; }
		[JsonIgnore]
		public RenderFragment<TableTestObject> RowTemplate { get; init; } = TableTestStates.RowTemplate;
		public List<TableTestObject> Items { get; init; }
		public bool ShowPaginationRow { get; init; }
		public List<int> RowsPerPageOpts { get; init; }
	}

	public static TableStateParameters LargeItemList
	{
		get => new()
		{
			Headings = Headings,
			RowTemplate = RowTemplate,
			Items = BigList,
			ShowPaginationRow = true,
			RowsPerPageOpts = RowsPerPageOptions
		};
	}

	public static TableStateParameters SmallItemList
	{
		get => new()
		{
			Headings = Headings,
			RowTemplate = RowTemplate,
			Items = SmallList,
			ShowPaginationRow = true,
			RowsPerPageOpts = RowsPerPageOptions
		};
	}

	public static TableStateParameters WithOutPaginationRow
	{
		get => new()
		{
			Headings = Headings,
			RowTemplate = RowTemplate,
			Items = SmallList,
			ShowPaginationRow = false,
			RowsPerPageOpts = RowsPerPageOptions
		};
	}

	private static readonly List<TableHeadingItem> Headings =
			[
				new TableHeadingItem(nameof(TableTestObject.Id)),
				new TableHeadingItem(nameof(TableTestObject.DisplayName)),
				new TableHeadingItem(nameof(TableTestObject.CreatedDate))
			];

	private static readonly List<TableTestObject> BigList =
		[
			new TableTestObject { Id = 1, DisplayName = "Item A", CreatedDate = new DateTime(2023, 10, 9) },
			new TableTestObject { Id = 2, DisplayName = "Item B", CreatedDate = new DateTime(2022, 2, 3) },
			new TableTestObject { Id = 3, DisplayName = "Item C", CreatedDate = new DateTime(2021, 5, 19) },
			new TableTestObject { Id = 4, DisplayName = "Item 1", CreatedDate = new DateTime(2023, 10, 9) },
			new TableTestObject { Id = 5, DisplayName = "Item 2", CreatedDate = new DateTime(2022, 2, 3) },
			new TableTestObject { Id = 6, DisplayName = "Item 3", CreatedDate = new DateTime(2021, 5, 19) },
			new TableTestObject { Id = 7, DisplayName = "Additional Item A", CreatedDate = new DateTime(2023, 10, 9) },
			new TableTestObject { Id = 8, DisplayName = "Additional Item B", CreatedDate = new DateTime(2022, 2, 3) },
			new TableTestObject { Id = 9, DisplayName = "Additional Item C", CreatedDate = new DateTime(2021, 5, 19) },
			new TableTestObject { Id = 10, DisplayName = "Additional Item 1", CreatedDate = new DateTime(2023, 10, 9) },
			new TableTestObject { Id = 11, DisplayName = "Additional Item 2", CreatedDate = new DateTime(2022, 2, 3) },
			new TableTestObject { Id = 12, DisplayName = "Additional Item 3", CreatedDate = new DateTime(2021, 5, 19) },
			new TableTestObject { Id = 13, DisplayName = "Some Item", CreatedDate = new DateTime(2023, 10, 9) },
			new TableTestObject { Id = 14, DisplayName = "Another Item", CreatedDate = new DateTime(2022, 2, 3) },
			new TableTestObject { Id = 15, DisplayName = "The Final Item", CreatedDate = new DateTime(2021, 5, 19) }
		];

	private static readonly List<TableTestObject> SmallList =
		[
			new TableTestObject { Id = 1, DisplayName = "Item A", CreatedDate = new DateTime(2023, 10, 9) },
			new TableTestObject { Id = 2, DisplayName = "Item B", CreatedDate = new DateTime(2022, 2, 3) },
			new TableTestObject { Id = 3, DisplayName = "Item C", CreatedDate = new DateTime(2021, 5, 19) }
		];

	private static readonly RenderFragment<TableTestObject> RowTemplate = item =>
			builder =>
			{
				builder.AddMarkupContent(0,
				 $@"
                 <div class=""table-row"">
                     <div class=""table-cell"">
                        <span>{item.Id}</span>
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

	public record TableTestObject
	{
		public int Id { get; init; }
		public string DisplayName { get; init; }
		public DateTime CreatedDate { get; init; }
	}
}

