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

	public static TableStateParameters VeryLargeItemList
	{
		get => new()
		{
			Headings = Headings,
			RowTemplate = RowTemplate,
			Items = VeryBigList,
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

	private static readonly List<TableTestObject> VeryBigList =
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
			new TableTestObject { Id = 15, DisplayName = "The Final Item", CreatedDate = new DateTime(2021, 5, 19) },
			new TableTestObject { Id = 16, DisplayName = "Additional Item 4", CreatedDate = new DateTime(2023, 10, 9) },
			new TableTestObject { Id = 17, DisplayName = "Additional Item 5", CreatedDate = new DateTime(2022, 2, 3) },
			new TableTestObject { Id = 18, DisplayName = "Additional Item 6", CreatedDate = new DateTime(2021, 5, 19) },
			new TableTestObject { Id = 19, DisplayName = "Additional Item 7", CreatedDate = new DateTime(2023, 10, 9) },
			new TableTestObject { Id = 20, DisplayName = "Additional Item 8", CreatedDate = new DateTime(2022, 2, 3) },
			new TableTestObject { Id = 21, DisplayName = "Additional Item 9", CreatedDate = new DateTime(2021, 5, 19) },
			new TableTestObject { Id = 22, DisplayName = "Additional Item 10", CreatedDate = new DateTime(2023, 10, 9) },
			new TableTestObject { Id = 23, DisplayName = "Additional Item 11", CreatedDate = new DateTime(2022, 2, 3) },
			new TableTestObject { Id = 24, DisplayName = "Additional Item 12", CreatedDate = new DateTime(2021, 5, 19) },
			new TableTestObject { Id = 25, DisplayName = "Additional Item 13", CreatedDate = new DateTime(2023, 10, 9) },
			new TableTestObject { Id = 26, DisplayName = "Additional Item 14", CreatedDate = new DateTime(2022, 2, 3) },
			new TableTestObject { Id = 27, DisplayName = "Additional Item 15", CreatedDate = new DateTime(2021, 5, 19) },
			new TableTestObject { Id = 28, DisplayName = "Additional Item 16", CreatedDate = new DateTime(2023, 10, 9) },
			new TableTestObject { Id = 29, DisplayName = "Additional Item 17", CreatedDate = new DateTime(2022, 2, 3) },
			new TableTestObject { Id = 30, DisplayName = "Additional Item 18", CreatedDate = new DateTime(2021, 5, 19) },
			new TableTestObject { Id = 31, DisplayName = "Additional Item 19", CreatedDate = new DateTime(2023, 10, 9) },
			new TableTestObject { Id = 32, DisplayName = "Additional Item 20", CreatedDate = new DateTime(2022, 2, 3) },
			new TableTestObject { Id = 33, DisplayName = "Additional Item 21", CreatedDate = new DateTime(2021, 5, 19) },
			new TableTestObject { Id = 34, DisplayName = "Additional Item 22", CreatedDate = new DateTime(2023, 10, 9) },
			new TableTestObject { Id = 35, DisplayName = "Additional Item 23", CreatedDate = new DateTime(2022, 2, 3) },
			new TableTestObject { Id = 36, DisplayName = "Additional Item 24", CreatedDate = new DateTime(2021, 5, 19) },
			new TableTestObject { Id = 37, DisplayName = "Additional Item 25", CreatedDate = new DateTime(2023, 10, 9) },
			new TableTestObject { Id = 38, DisplayName = "Additional Item 26", CreatedDate = new DateTime(2022, 2, 3) },
			new TableTestObject { Id = 39, DisplayName = "Additional Item 27", CreatedDate = new DateTime(2021, 5, 19) },
			new TableTestObject { Id = 40, DisplayName = "Additional Item 28", CreatedDate = new DateTime(2023, 10, 9) },
			new TableTestObject { Id = 41, DisplayName = "Additional Item 29", CreatedDate = new DateTime(2022, 2, 3) },
			new TableTestObject { Id = 42, DisplayName = "Additional Item 30", CreatedDate = new DateTime(2021, 5, 19) },
			new TableTestObject { Id = 43, DisplayName = "Additional Item 31", CreatedDate = new DateTime(2023, 10, 9) },
			new TableTestObject { Id = 44, DisplayName = "Additional Item 32", CreatedDate = new DateTime(2022, 2, 3) },
			new TableTestObject { Id = 45, DisplayName = "Additional Item 33", CreatedDate = new DateTime(2021, 5, 19) },
			new TableTestObject { Id = 46, DisplayName = "Additional Item 34", CreatedDate = new DateTime(2023, 10, 9) },
			new TableTestObject { Id = 47, DisplayName = "Additional Item 35", CreatedDate = new DateTime(2022, 2, 3) },
			new TableTestObject { Id = 48, DisplayName = "Additional Item 36", CreatedDate = new DateTime(2021, 5, 19) },
			new TableTestObject { Id = 49, DisplayName = "Additional Item 37", CreatedDate = new DateTime(2023, 10, 9) },
			new TableTestObject { Id = 50, DisplayName = "Additional Item 38", CreatedDate = new DateTime(2022, 2, 3) },
			new TableTestObject { Id = 51, DisplayName = "Additional Item 39", CreatedDate = new DateTime(2021, 5, 19) },
			new TableTestObject { Id = 52, DisplayName = "Additional Item 40", CreatedDate = new DateTime(2023, 10, 9) },
			new TableTestObject { Id = 53, DisplayName = "Additional Item 41", CreatedDate = new DateTime(2022, 2, 3) },
			new TableTestObject { Id = 54, DisplayName = "Additional Item 42", CreatedDate = new DateTime(2021, 5, 19) },
			new TableTestObject { Id = 55, DisplayName = "Additional Item 43", CreatedDate = new DateTime(2023, 10, 9) },
			new TableTestObject { Id = 56, DisplayName = "Additional Item 44", CreatedDate = new DateTime(2022, 2, 3) },
			new TableTestObject { Id = 57, DisplayName = "Additional Item 45", CreatedDate = new DateTime(2021, 5, 19) },
			new TableTestObject { Id = 58, DisplayName = "Additional Item 46", CreatedDate = new DateTime(2023, 10, 9) },
			new TableTestObject { Id = 59, DisplayName = "Additional Item 47", CreatedDate = new DateTime(2022, 2, 3) },
			new TableTestObject { Id = 60, DisplayName = "Additional Item 48", CreatedDate = new DateTime(2021, 5, 19) },
			new TableTestObject { Id = 61, DisplayName = "Additional Item 49", CreatedDate = new DateTime(2023, 10, 9) },
			new TableTestObject { Id = 62, DisplayName = "Additional Item 50", CreatedDate = new DateTime(2022, 2, 3) },
			new TableTestObject { Id = 63, DisplayName = "Additional Item 51", CreatedDate = new DateTime(2021, 5, 19) },
			new TableTestObject { Id = 64, DisplayName = "Additional Item 52", CreatedDate = new DateTime(2023, 10, 9) },
			new TableTestObject { Id = 65, DisplayName = "Additional Item 53", CreatedDate = new DateTime(2022, 2, 3) },
			new TableTestObject { Id = 66, DisplayName = "Additional Item 54", CreatedDate = new DateTime(2021, 5, 19) },
			new TableTestObject { Id = 67, DisplayName = "Additional Item 55", CreatedDate = new DateTime(2023, 10, 9) },
			new TableTestObject { Id = 68, DisplayName = "Additional Item 56", CreatedDate = new DateTime(2022, 2, 3) },
			new TableTestObject { Id = 69, DisplayName = "Additional Item 57", CreatedDate = new DateTime(2021, 5, 19) },
			new TableTestObject { Id = 70, DisplayName = "Additional Item 58", CreatedDate = new DateTime(2023, 10, 9) },
			new TableTestObject { Id = 71, DisplayName = "Additional Item 59", CreatedDate = new DateTime(2022, 2, 3) },
			new TableTestObject { Id = 72, DisplayName = "Additional Item 60", CreatedDate = new DateTime(2021, 5, 19) },
			new TableTestObject { Id = 73, DisplayName = "Additional Item 61", CreatedDate = new DateTime(2023, 10, 9) },
			new TableTestObject { Id = 74, DisplayName = "Additional Item 62", CreatedDate = new DateTime(2022, 2, 3) },
			new TableTestObject { Id = 75, DisplayName = "Additional Item 63", CreatedDate = new DateTime(2021, 5, 19) },
			new TableTestObject { Id = 76, DisplayName = "Additional Item 64", CreatedDate = new DateTime(2023, 10, 9) },
			new TableTestObject { Id = 77, DisplayName = "Additional Item 65", CreatedDate = new DateTime(2022, 2, 3) },
			new TableTestObject { Id = 78, DisplayName = "Additional Item 66", CreatedDate = new DateTime(2021, 5, 19) },
			new TableTestObject { Id = 79, DisplayName = "Additional Item 67", CreatedDate = new DateTime(2023, 10, 9) },
			new TableTestObject { Id = 80, DisplayName = "Additional Item 68", CreatedDate = new DateTime(2022, 2, 3) },
			new TableTestObject { Id = 81, DisplayName = "Additional Item 69", CreatedDate = new DateTime(2021, 5, 19) },
			new TableTestObject { Id = 82, DisplayName = "Additional Item 70", CreatedDate = new DateTime(2023, 10, 9) },
			new TableTestObject { Id = 83, DisplayName = "Additional Item 71", CreatedDate = new DateTime(2022, 2, 3) },
			new TableTestObject { Id = 84, DisplayName = "Additional Item 72", CreatedDate = new DateTime(2021, 5, 19) },
			new TableTestObject { Id = 85, DisplayName = "Additional Item 73", CreatedDate = new DateTime(2023, 10, 9) },
			new TableTestObject { Id = 86, DisplayName = "Additional Item 74", CreatedDate = new DateTime(2022, 2, 3) },
			new TableTestObject { Id = 87, DisplayName = "Additional Item 75", CreatedDate = new DateTime(2021, 5, 19) },
			new TableTestObject { Id = 88, DisplayName = "Additional Item 76", CreatedDate = new DateTime(2023, 10, 9) },
			new TableTestObject { Id = 89, DisplayName = "Additional Item 77", CreatedDate = new DateTime(2022, 2, 3) },
			new TableTestObject { Id = 90, DisplayName = "Additional Item 78", CreatedDate = new DateTime(2021, 5, 19) },
			new TableTestObject { Id = 91, DisplayName = "Additional Item 79", CreatedDate = new DateTime(2023, 10, 9) },
			new TableTestObject { Id = 92, DisplayName = "Additional Item 80", CreatedDate = new DateTime(2022, 2, 3) },
			new TableTestObject { Id = 93, DisplayName = "Additional Item 81", CreatedDate = new DateTime(2021, 5, 19) },
			new TableTestObject { Id = 94, DisplayName = "Additional Item 82", CreatedDate = new DateTime(2023, 10, 9) },
			new TableTestObject { Id = 95, DisplayName = "Additional Item 83", CreatedDate = new DateTime(2022, 2, 3) },
			new TableTestObject { Id = 96, DisplayName = "Additional Item 84", CreatedDate = new DateTime(2021, 5, 19) },
			new TableTestObject { Id = 97, DisplayName = "Additional Item 85", CreatedDate = new DateTime(2023, 10, 9) },
			new TableTestObject { Id = 98, DisplayName = "Additional Item 86", CreatedDate = new DateTime(2022, 2, 3) },
			new TableTestObject { Id = 99, DisplayName = "Additional Item 87", CreatedDate = new DateTime(2021, 5, 19) },
			new TableTestObject { Id = 100, DisplayName = "Additional Item 88", CreatedDate = new DateTime(2023, 10, 9) }
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

