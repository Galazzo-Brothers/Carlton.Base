using System.Globalization;
using System.Reflection;
using Bunit.TestDoubles;
using Carlton.Core.Components.Tables;
using Carlton.Core.Foundation.State;
using static Carlton.Core.Components.Tests.Tables.TableTestHelper;
namespace Carlton.Core.Components.Tests.Tables;

[Trait("Component", nameof(Table<int>))]
public class TableComponentTests_Stubs : TestContext
{
	[Theory(DisplayName = "Markup Test")]
	[InlineAutoData(false, false)]
	[InlineAutoData(true, false)]
	[InlineAutoData(false, true)]
	[InlineAutoData(true, true)]
	public void Table_Markup_RendersCorrectly(
		bool expectedIsZebraStriped,
		bool expectedIsHoverable,
		IEnumerable<TableTestObject> expectedItems)
	{
		//Arrange 
		var expectedMarkup = BuildExpectedMarkup(
			TableTestHeadingItems,
			RowTemplate,
			expectedItems,
			expectedIsZebraStriped,
			expectedIsHoverable,
			false,
			RowsPerPageOpts,
			0,
			0);

		ComponentFactories.AddStub<TableHeader<TableTestObject>>(parameters =>
		{
			var headings = parameters.Get(p => p.Headings);
			var headingsText = headings.Select(_ => _.DisplayName);
			return BuildExpectedHeaderMarkup(headings, true);
		});

		//Act
		var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
			.Add(p => p.Headings, TableTestHeadingItems)
			.Add(p => p.Items, expectedItems)
			.Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString("d", CultureInfo.InvariantCulture)))
			.Add(p => p.ZebraStripped, expectedIsZebraStriped)
			.Add(p => p.Hoverable, expectedIsHoverable)
			.Add(p => p.ShowPaginationRow, false));

		//Assert
		cut.MarkupMatches(expectedMarkup);
	}

	[Theory(DisplayName = "Markup with Pagination Test")]
	[InlineAutoData(1, 0, false, false)] //15 Items, Page 1, 5 Rows PerPage 
	[InlineAutoData(2, 0, false, false)] //15 Items, Page 2, 5 Rows PerPage 
	[InlineAutoData(3, 0, false, false)] //15 Items, Page 3, 5 Rows PerPage 
	[InlineAutoData(1, 1, false, false)] //15 Items, Page 1, 10 Rows PerPage 
	[InlineAutoData(2, 1, false, false)] //15 Items, Page 2, 10 Rows PerPage 
	[InlineAutoData(1, 2, false, false)] //15 Items, Page 1, 15 Rows PerPage 
	[InlineAutoData(1, 0, true, false)] //15 Items, Page 1, 5 Rows PerPage 
	[InlineAutoData(2, 0, true, false)] //15 Items, Page 2, 5 Rows PerPage 
	[InlineAutoData(3, 0, true, false)] //15 Items, Page 3, 5 Rows PerPage 
	[InlineAutoData(1, 1, true, false)] //15 Items, Page 1, 10 Rows PerPage 
	[InlineAutoData(2, 1, true, false)] //15 Items, Page 2, 10 Rows PerPage 
	[InlineAutoData(1, 2, true, false)] //15 Items, Page 1, 15 Rows PerPage
	[InlineAutoData(1, 0, false, true)] //15 Items, Page 1, 5 Rows PerPage 
	[InlineAutoData(2, 0, false, true)] //15 Items, Page 2, 5 Rows PerPage 
	[InlineAutoData(3, 0, false, true)] //15 Items, Page 3, 5 Rows PerPage 
	[InlineAutoData(1, 1, false, true)] //15 Items, Page 1, 10 Rows PerPage 
	[InlineAutoData(2, 1, false, true)] //15 Items, Page 2, 10 Rows PerPage 
	[InlineAutoData(1, 2, false, true)] //15 Items, Page 1, 15 Rows PerPage 
	[InlineAutoData(1, 0, true, true)] //15 Items, Page 1, 5 Rows PerPage 
	[InlineAutoData(2, 0, true, true)] //15 Items, Page 2, 5 Rows PerPage 
	[InlineAutoData(3, 0, true, true)] //15 Items, Page 3, 5 Rows PerPage 
	[InlineAutoData(1, 1, true, true)] //15 Items, Page 1, 10 Rows PerPage 
	[InlineAutoData(2, 1, true, true)] //15 Items, Page 2, 10 Rows PerPage 
	[InlineAutoData(1, 2, true, true)] //15 Items, Page 1, 15 Rows PerPage 
	public void Table_Markup_WithPagination_RendersCorrectly(
	   int expectedCurrentPage,
	   int expectedRowsPerPageIndex,
	   bool expectedIsZebraStriped,
	   bool expectedIsHoverable)
	{
		//Arrange 
		var rowsPerPage = RowsPerPageOpts.ElementAt(expectedRowsPerPageIndex);
		var skip = expectedCurrentPage == 1 ? 0 : rowsPerPage * (expectedCurrentPage - 1);
		var items = GetItems().ToList();
		var expectedItems = items.Skip(skip).Take(rowsPerPage);
		var expectedMarkup = BuildExpectedMarkup(
			TableTestHeadingItems,
			RowTemplate,
			items,
			expectedIsZebraStriped,
			expectedIsHoverable,
			true,
			RowsPerPageOpts,
			expectedCurrentPage,
			expectedRowsPerPageIndex);

		ComponentFactories.AddStub<TableHeader<TableTestObject>>(parameters =>
		{
			var headings = parameters.Get(p => p.Headings);
			var headingsText = headings.Select(_ => _.DisplayName);
			return BuildExpectedHeaderMarkup(headings, true);
		});

		ComponentFactories.AddStub<TablePaginationRow<TableTestObject>>(parameters =>
		{
			return BuildExpectedPaginationRow(items.Count, expectedCurrentPage, RowsPerPageOpts, expectedRowsPerPageIndex);
		});

		var tableInteractionState = new TableInteractionState
		{
			CurrentPage = expectedCurrentPage,
			SelectedRowsPerPageIndex = expectedRowsPerPageIndex
		};

		//Act
		var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
			.Add(p => p.Headings, TableTestHeadingItems)
			.Add(p => p.Items, items)
			.Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString("d", CultureInfo.InvariantCulture)))
			.Add(p => p.ZebraStripped, expectedIsZebraStriped)
			.Add(p => p.Hoverable, expectedIsHoverable)
			.Add(p => p.RowsPerPageOptions, RowsPerPageOpts)
			.Add(p => p.ShowPaginationRow, true)
			.Add(p => p.TableInteractionState, tableInteractionState));

		//Assert
		cut.MarkupMatches(expectedMarkup);
	}

	[Theory(DisplayName = "Ordered Headers Markup Test")]
	[InlineAutoData("ID", 0, true)]
	[InlineAutoData("ID", 0, false)]
	[InlineAutoData("DisplayName", 1, true)]
	[InlineAutoData("DisplayName", 1, false)]
	[InlineAutoData("CreatedDate", 2, true)]
	[InlineAutoData("CreatedDate", 2, false)]
	public void Table_Markup_OrderedHeadersSelected_RendersCorrectly(
	   string expectedOrderColumn,
	   int expectedOrderColumnIndex,
	   bool expectedOrderAscending,
	   IEnumerable<TableTestObject> expectedItems,
	   bool expectedIsZebraStriped,
	   bool expectedIsHoverable,
	   bool expectedShowPaginationRow,
	   IEnumerable<int> expectedRowsPerPageOpts)
	{
		//Arrange
		var expectedOrderedItems = expectedOrderAscending ? OrderCollection(expectedItems, expectedOrderColumnIndex) : OrderCollectionDesc(expectedItems, expectedOrderColumnIndex);
		var selectedRowsPerPageIndex = RandomUtilities.GetRandomIndex(expectedRowsPerPageOpts.Count());
		var maxPages = Math.Max(1, (int)Math.Ceiling((double)expectedItems.Count() / expectedRowsPerPageOpts.ElementAt(selectedRowsPerPageIndex)));
		var currentPage = RandomUtilities.GetRandomNonZeroIndex(maxPages);
		var expectedMarkup = BuildExpectedMarkup(
			TableTestHeadingItems,
			RowTemplate,
			expectedOrderedItems,
			expectedIsZebraStriped,
			expectedIsHoverable,
			false,
			expectedRowsPerPageOpts,
			currentPage,
			selectedRowsPerPageIndex,
			expectedOrderColumnIndex,
			expectedOrderAscending);

		ComponentFactories.AddStub<TableHeader<TableTestObject>>(parameters =>
		{
			var headings = parameters.Get(p => p.Headings);
			var headingsText = headings.Select(_ => _.DisplayName);
			return BuildExpectedHeaderMarkup(headings, expectedOrderAscending, expectedOrderColumnIndex);
		});

		ComponentFactories.AddStub<TablePaginationRow<TableTestObject>>(parameters =>
		{
			var itemTotal = expectedItems.Count();
			return BuildExpectedPaginationRow(itemTotal, currentPage, expectedRowsPerPageOpts, selectedRowsPerPageIndex);
		});

		var tableInteractionState = new TableInteractionState
		{
			OrderByColumn = expectedOrderColumn,
			IsAscending = expectedOrderAscending,
			CurrentPage = currentPage,
			SelectedRowsPerPageIndex = selectedRowsPerPageIndex
		};

		//Act
		var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
			.Add(p => p.Headings, TableTestHeadingItems)
			.Add(p => p.Items, expectedOrderedItems)
			.Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString("d", CultureInfo.InvariantCulture)))
			.Add(p => p.ZebraStripped, expectedIsZebraStriped)
			.Add(p => p.ShowPaginationRow, expectedShowPaginationRow)
			.Add(p => p.RowsPerPageOptions, expectedRowsPerPageOpts)
			.Add(p => p.Hoverable, expectedIsHoverable)
			.Add(p => p.TableInteractionState, tableInteractionState));

		//Assert
		cut.MarkupMatches(expectedMarkup);
	}

	[Theory(DisplayName = "Headings Parameter Test"), AutoData]
	public void Table_HeadingsParameter_RendersCorrectly(
	 IEnumerable<TableHeadingItem> expectedHeadings,
	 IEnumerable<TableTestObject> expectedItems,
	 IEnumerable<int> rowsPerPageOpts)
	{
		//Arrange
		var expectedCount = expectedHeadings.Count();
		var expectedHeaders = expectedHeadings.Select(_ => _.DisplayName);

		ComponentFactories.AddStub<TableHeader<TableTestObject>>(parameters =>
		{
			var headings = parameters.Get(p => p.Headings);
			var headingsText = headings.Select(_ => _.DisplayName);
			return BuildExpectedHeaderMarkup(headings, true);
		});

		ComponentFactories.AddStub<TablePaginationRow<TableTestObject>>(parameters =>
		{
			var itemTotal = expectedItems.Count();
			return BuildExpectedPaginationRow(itemTotal, 0, RowsPerPageOpts, 0);
		});

		//Act
		var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
			.Add(p => p.Headings, expectedHeadings)
			.Add(p => p.Items, expectedItems)
			.Add(p => p.RowsPerPageOptions, rowsPerPageOpts)
			.Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString("d", CultureInfo.InvariantCulture)))
			.Add(p => p.ShowPaginationRow, true));

		var headerRowItems = cut.FindAll(".header-cell");
		var actualCount = headerRowItems.Count;
		var actualHeaders = cut.FindAll(".heading-text").Select(_ => _.TextContent);

		//Assert
		actualCount.ShouldBe(expectedCount);
		actualHeaders.ShouldBe(expectedHeaders);
	}

	[Theory(DisplayName = "Items Parameter Test"), AutoData]
	public void Table_ItemsParameter_RendersCorrectly(
	   IEnumerable<TableHeadingItem> expectedHeadings,
	   IEnumerable<TableTestObject> expectedItems,
	   IEnumerable<int> expectedRowsPerPageOpts)
	{
		//Arrange
		ComponentFactories.AddStub<TableHeader<TableTestObject>>();
		ComponentFactories.AddStub<TablePaginationRow<TableTestObject>>();
		var expectedItemCount = expectedItems.Count();
		var expectedDisplayValues = expectedItems.SelectMany(_ => new[] { _.ID.ToString(), _.DisplayName, _.CreatedDate.ToString("d", CultureInfo.InvariantCulture) });

		//Act
		var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
			.Add(p => p.Headings, expectedHeadings)
			.Add(p => p.Items, expectedItems)
			.Add(p => p.RowsPerPageOptions, expectedRowsPerPageOpts)
			.Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString("d", CultureInfo.InvariantCulture)))
			.Add(p => p.ShowPaginationRow, false));

		var tableRows = cut.FindAll(".table-row");
		var actualItemCount = tableRows.Count;

		var actualDisplayValues = cut.FindAll("span.table-cell").Select(_ => _.TextContent);

		//Assert
		actualItemCount.ShouldBe(expectedItemCount);
		actualDisplayValues.ShouldBe(expectedDisplayValues);
	}

	[Theory(DisplayName = "Items Parameter with Pagination Test")]
	[InlineAutoData(1, 0)] //15 Items, Page 1, 5 Rows PerPage 
	[InlineAutoData(2, 0)] //15 Items, Page 2, 5 Rows PerPage 
	[InlineAutoData(3, 0)] //15 Items, Page 3, 5 Rows PerPage 
	[InlineAutoData(1, 1)] //15 Items, Page 1, 10 Rows PerPage 
	[InlineAutoData(2, 1)] //15 Items, Page 2, 10 Rows PerPage 
	[InlineAutoData(1, 2)] //15 Items, Page 1, 15 Rows PerPage 
	public void Table_ItemsParameter_WithPagination_RendersCorrectly(
		int expectedCurrentPage,
		int expectedRowsPerPageIndex)
	{
		//Arrange
		ComponentFactories.AddStub<TableHeader<TableTestObject>>();
		ComponentFactories.AddStub<TablePaginationRow<TableTestObject>>();
		var rowsPerPage = RowsPerPageOpts.ElementAt(expectedRowsPerPageIndex);
		var skip = expectedCurrentPage == 1 ? 0 : rowsPerPage * (expectedCurrentPage - 1);
		var items = GetItems().ToList();
		var expectedItems = items.Skip(skip).Take(rowsPerPage);
		var expectedItemCount = expectedItems.Count();
		var expectedDisplayValues = expectedItems.SelectMany(_ => new[] { _.ID.ToString(), _.DisplayName, _.CreatedDate.ToString("d", CultureInfo.InvariantCulture) });

		var tableInteractionState = new TableInteractionState
		{
			CurrentPage = expectedCurrentPage,
			SelectedRowsPerPageIndex = expectedRowsPerPageIndex
		};

		//Act
		var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
			.Add(p => p.Headings, Headings)
			.Add(p => p.Items, items)
			.Add(p => p.RowsPerPageOptions, RowsPerPageOpts)
			.Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString("d", CultureInfo.InvariantCulture)))
			.Add(p => p.ShowPaginationRow, true)
			.Add(p => p.TableInteractionState, tableInteractionState));

		var tableRows = cut.FindAll(".table-row");
		var actualItemCount = tableRows.Count;
		var actualDisplayValues = cut.FindAll("span.table-cell").Select(_ => _.TextContent);

		//Assert
		actualItemCount.ShouldBe(expectedItemCount);
		actualDisplayValues.ShouldBe(expectedDisplayValues);
	}

	[Theory(DisplayName = "RowTemplate Parameter Test")]
	[InlineAutoData(@"<div class=""table-row""><span>Template {0}{1}{2}</span></div>")]
	[InlineAutoData(@"<div class=""table-row""><span>{0}Test Template {1}{2}</span></div>")]
	[InlineAutoData(@"<div class=""table-row"">{0}<span>{1}More Test Templates {2}</span></div>")]
	public void Table_RowTemplateParameter_RendersCorrectly(
	   string expectedRowTemplate,
	   List<TableTestObject> expectedItems,
	   IEnumerable<int> expectedRowsPerPageOpts)
	{
		//Arrange
		ComponentFactories.AddStub<TableHeader<TableTestObject>>();
		ComponentFactories.AddStub<TablePaginationRow<TableTestObject>>();
		var expectedContent = expectedItems.Select(_ => string.Format(expectedRowTemplate, _.ID, _.DisplayName, _.CreatedDate.ToString("d", CultureInfo.InvariantCulture)));

		//Act
		var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
			.Add(p => p.Headings, Headings)
			.Add(p => p.Items, expectedItems)
			.Add(p => p.RowsPerPageOptions, expectedRowsPerPageOpts)
			.Add(p => p.RowTemplate, item => string.Format(expectedRowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString("d", CultureInfo.InvariantCulture)))
			.Add(p => p.ShowPaginationRow, false));

		var rowElements = cut.FindAll(".table-row");
		var actualContent = rowElements.Select(_ => _.OuterHtml);

		//Assert
		actualContent.ShouldBe(expectedContent);
	}

	[Theory(DisplayName = "RowTemplate Parameter with Pagination Test")]
	[InlineAutoData(1, 0, @"<div class=""table-row""><span>Template {0}{1}{2}</span></div>")] //15 Items, Page 1, 5 Rows PerPage 
	[InlineAutoData(2, 0, @"<div class=""table-row""><span>Template {0}{1}{2}</span></div>")] //15 Items, Page 2, 5 Rows PerPage 
	[InlineAutoData(3, 0, @"<div class=""table-row""><span>Template {0}{1}{2}</span></div>")] //15 Items, Page 3, 5 Rows PerPage 
	[InlineAutoData(1, 1, @"<div class=""table-row""><span>Template {0}{1}{2}</span></div>")] //15 Items, Page 1, 10 Rows PerPage 
	[InlineAutoData(2, 1, @"<div class=""table-row""><span>Template {0}{1}{2}</span></div>")] //15 Items, Page 2, 10 Rows PerPage 
	[InlineAutoData(1, 2, @"<div class=""table-row""><span>Template {0}{1}{2}</span></div>")] //15 Items, Page 1, 15 Rows PerPage 
	[InlineAutoData(1, 0, @"<div class=""table-row""><span>{0}Test Template {1}{2}</span></div>")] //15 Items, Page 1, 5 Rows PerPage 
	[InlineAutoData(2, 0, @"<div class=""table-row""><span>{0}Test Template {1}{2}</span></div>")] //15 Items, Page 2, 5 Rows PerPage 
	[InlineAutoData(3, 0, @"<div class=""table-row""><span>{0}Test Template {1}{2}</span></div>")] //15 Items, Page 3, 5 Rows PerPage 
	[InlineAutoData(1, 1, @"<div class=""table-row""><span>{0}Test Template {1}{2}</span></div>")] //15 Items, Page 1, 10 Rows PerPage 
	[InlineAutoData(2, 1, @"<div class=""table-row""><span>{0}Test Template {1}{2}</span></div>")] //15 Items, Page 2, 10 Rows PerPage 
	[InlineAutoData(1, 2, @"<div class=""table-row""><span>{0}Test Template {1}{2}</span></div>")] //15 Items, Page 1, 15 Rows PerPage 
	[InlineAutoData(1, 0, @"<div class=""table-row"">{0}<span>{1}More Test Templates {2}</span></div>")] //15 Items, Page 1, 5 Rows PerPage 
	[InlineAutoData(2, 0, @"<div class=""table-row"">{0}<span>{1}More Test Templates {2}</span></div>")] //15 Items, Page 2, 5 Rows PerPage 
	[InlineAutoData(3, 0, @"<div class=""table-row"">{0}<span>{1}More Test Templates {2}</span></div>")] //15 Items, Page 3, 5 Rows PerPage 
	[InlineAutoData(1, 1, @"<div class=""table-row"">{0}<span>{1}More Test Templates {2}</span></div>")] //15 Items, Page 1, 10 Rows PerPage 
	[InlineAutoData(2, 1, @"<div class=""table-row"">{0}<span>{1}More Test Templates {2}</span></div>")] //15 Items, Page 2, 10 Rows PerPage 
	[InlineAutoData(1, 2, @"<div class=""table-row"">{0}<span>{1}More Test Templates {2}</span></div>")] //15 Items, Page 1, 15 Rows PerPage 
	public void Table_RowTemplateParameter_WithPagination_RendersCorrectly(
	  int expectedCurrentPage,
	  int expectedRowsPerPageIndex,
	  string expectedRowTemplate)
	{
		//Arrange
		ComponentFactories.AddStub<TableHeader<TableTestObject>>();
		ComponentFactories.AddStub<TablePaginationRow<TableTestObject>>();
		var rowsPerPage = RowsPerPageOpts.ElementAt(expectedRowsPerPageIndex);
		var skip = expectedCurrentPage == 1 ? 0 : rowsPerPage * (expectedCurrentPage - 1);
		var items = GetItems().ToList();
		var expectedItems = items.Skip(skip).Take(rowsPerPage);
		var expectedContent = expectedItems.Select(_ => string.Format(expectedRowTemplate, _.ID, _.DisplayName, _.CreatedDate.ToString("d", CultureInfo.InvariantCulture)));

		var tableInteractionState = new TableInteractionState
		{
			CurrentPage = expectedCurrentPage,
			SelectedRowsPerPageIndex = expectedRowsPerPageIndex
		};

		//Act
		var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
			.Add(p => p.Headings, Headings)
			.Add(p => p.Items, items)
			.Add(p => p.RowsPerPageOptions, RowsPerPageOpts)
			.Add(p => p.RowTemplate, item => string.Format(expectedRowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString("d", CultureInfo.InvariantCulture)))
			.Add(p => p.ShowPaginationRow, true)
			.Add(p => p.TableInteractionState, tableInteractionState));

		var rowElements = cut.FindAll(".table-row");
		var itemRowElements = rowElements.Take(rowElements.Count);
		var actualContent = itemRowElements.Select(_ => _.OuterHtml);

		//Assert
		actualContent.ShouldBe(expectedContent);
	}

	[Theory(DisplayName = "ZebraStriped Parameter Test")]
	[InlineAutoData(true)]
	[InlineAutoData(false)]
	public void Table_ZebraStripedParameter_RendersCorrectly(
	  bool expectedZebraStriped,
	  string expectedRowTemplate,
	  List<TableTestObject> expectedItems,
	  IEnumerable<TableHeadingItem> expectedHeadings,
	  IEnumerable<int> expectedRowsPerPage,
	  bool expectedShowPaginationRow)
	{
		//Arrange
		ComponentFactories.AddStub<TableHeader<TableTestObject>>();
		ComponentFactories.AddStub<TablePaginationRow<TableTestObject>>();

		//Act
		var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
			.Add(p => p.Headings, expectedHeadings)
			.Add(p => p.Items, expectedItems)
			.Add(p => p.ZebraStripped, expectedZebraStriped)
			.Add(p => p.RowsPerPageOptions, expectedRowsPerPage)
			.Add(p => p.RowTemplate, item => string.Format(expectedRowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString("d", CultureInfo.InvariantCulture)))
			.Add(p => p.ShowPaginationRow, expectedShowPaginationRow));

		var tableElement = cut.Find(".table-container");
		var zebraClassPresent = tableElement.ClassList.Contains("zebra");

		//Assert
		zebraClassPresent.ShouldBe(expectedZebraStriped);
	}

	[Theory(DisplayName = "Hoverable Parameter Test")]
	[InlineAutoData(true)]
	[InlineAutoData(false)]
	public void Table_HoverableParameter_RendersCorrectly(
	  bool expectedHoverable,
	  bool expectedZebraStriped,
	  string expectedRowTemplate,
	  List<TableTestObject> expectedItems,
	  IEnumerable<TableHeadingItem> expectedHeadings,
	  IEnumerable<int> expectedRowsPerPage,
	  bool expectedShowPaginationRow)
	{
		//Arrange
		ComponentFactories.AddStub<TableHeader<TableTestObject>>();
		ComponentFactories.AddStub<TablePaginationRow<TableTestObject>>();

		//Act
		var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
			.Add(p => p.Headings, expectedHeadings)
			.Add(p => p.Items, expectedItems)
			.Add(p => p.ZebraStripped, expectedZebraStriped)
			.Add(p => p.Hoverable, expectedHoverable)
			.Add(p => p.RowsPerPageOptions, expectedRowsPerPage)
			.Add(p => p.RowTemplate, item => string.Format(expectedRowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString("d", CultureInfo.InvariantCulture)))
			.Add(p => p.ShowPaginationRow, expectedShowPaginationRow));

		var tableElement = cut.Find(".table-container");
		var hoverableClassPresent = tableElement.ClassList.Contains("hoverable");

		//Assert
		hoverableClassPresent.ShouldBe(expectedHoverable);
	}

	[Theory(DisplayName = "ShowPaginationRow Parameter Test")]
	[InlineAutoData(true)]
	[InlineAutoData(false)]
	public void Table_ShowPaginationRowParameter_RendersCorrectly(
	  bool expectedShowPaginationRow,
	  IEnumerable<TableHeadingItem> expectedHeadings,
	  IEnumerable<TableTestObject> expectedItems,
	  IEnumerable<int> expectedRowsPerPage)
	{
		//Arrange
		ComponentFactories.AddStub<TableHeader<TableTestObject>>();
		if (expectedShowPaginationRow)
			ComponentFactories.AddStub<TablePaginationRow<TableTestObject>>();

		//Act
		var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
			.Add(p => p.Headings, expectedHeadings)
			.Add(p => p.Items, expectedItems)
			.Add(p => p.RowsPerPageOptions, expectedRowsPerPage)
			.Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString(CultureInfo.InvariantCulture)))
			.Add(p => p.ShowPaginationRow, expectedShowPaginationRow));

		var paginationRowExists = cut.HasComponent<Stub<TablePaginationRow<TableTestObject>>>();

		//Assert
		paginationRowExists.ShouldBe(expectedShowPaginationRow);
	}

	[Theory(DisplayName = "CurrentPage Parameter Test"), AutoData]
	public void Table_CurrentPageParameter_RendersCorrectly(int expectedCurrentPage)
	{
		//Arrange
		ComponentFactories.AddStub<TableHeader<TableTestObject>>();
		ComponentFactories.AddStub<TablePaginationRow<TableTestObject>>();

		var tableInteractionState = new TableInteractionState
		{
			CurrentPage = expectedCurrentPage
		};

		//Act
		var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
			.Add(p => p.Headings, Headings)
			.Add(p => p.Items, GetItems())
			.Add(p => p.RowsPerPageOptions, RowsPerPageOpts)
			.Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString(CultureInfo.InvariantCulture)))
			.Add(p => p.ShowPaginationRow, true)
			.Add(p => p.TableInteractionState, tableInteractionState));

		var paginationRow = cut.FindComponent<Stub<TablePaginationRow<TableTestObject>>>();
		var actualPage = paginationRow.Instance.Parameters.Get(_ => _.CurrentPage);

		//Assert
		actualPage.ShouldBe(expectedCurrentPage);
	}

	[Theory(DisplayName = "RowsPerPageOpts Parameter Test"), AutoData]
	public void Table_RowsPerPageOptsParameter_RendersCorrectly(IEnumerable<int> expectedRowsPerPageOpts)
	{
		//Arrange
		ComponentFactories.AddStub<TableHeader<TableTestObject>>();
		ComponentFactories.AddStub<TablePaginationRow<TableTestObject>>();
		var expectedCount = expectedRowsPerPageOpts.Count();
		var expectedRowsPerPageValues = expectedRowsPerPageOpts.Select(_ => _);

		//Act
		var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
			.Add(p => p.Headings, Headings)
			.Add(p => p.Items, GetItems())
			.Add(p => p.RowsPerPageOptions, expectedRowsPerPageOpts)
			.Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString(CultureInfo.InvariantCulture)))
			.Add(p => p.ShowPaginationRow, true));

		var paginationRow = cut.FindComponent<Stub<TablePaginationRow<TableTestObject>>>();
		var actualRowsPerPage = paginationRow.Instance.Parameters.Get(_ => _.RowsPerPageOpts); ;

		//Assert
		actualRowsPerPage.Count().ShouldBe(expectedCount);
		actualRowsPerPage.ShouldBe(expectedRowsPerPageValues);
	}


	[Theory(DisplayName = "SelectedRowsPerPageIndex Parameter Test"), AutoData]
	public void Table_SelectedRowsPageIndexParameter_RendersCorrectly(IEnumerable<int> expectedRowsPerPageOpts)
	{
		//Arrange
		ComponentFactories.AddStub<TableHeader<TableTestObject>>();
		ComponentFactories.AddStub<TablePaginationRow<TableTestObject>>();
		var expectedRowsPerPageIndex = RandomUtilities.GetRandomIndex(expectedRowsPerPageOpts.Count());

		var tableInteractionState = new TableInteractionState
		{
			SelectedRowsPerPageIndex = expectedRowsPerPageIndex
		};

		//Act
		var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
			.Add(p => p.Headings, Headings)
			.Add(p => p.Items, GetItems())
			.Add(p => p.RowsPerPageOptions, expectedRowsPerPageOpts)
			.Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString(CultureInfo.InvariantCulture)))
			.Add(p => p.ShowPaginationRow, true)
			.Add(p => p.TableInteractionState, tableInteractionState));

		var paginationRow = cut.FindComponent<Stub<TablePaginationRow<TableTestObject>>>();
		var actualSelectedIndex = paginationRow.Instance.Parameters.Get(_ => _.SelectedRowsPerPageIndex);

		//Assert
		actualSelectedIndex.ShouldBe(expectedRowsPerPageIndex);
	}

	[Theory(DisplayName = "OrderColumn Parameter Test")]
	[InlineData("ID")]
	[InlineData("DisplayName")]
	[InlineData("CreatedDate")]
	public void Table_OrderColumnParameter_RendersCorrectly(string expectedOrderColumn)
	{
		//Arrange
		ComponentFactories.AddStub<TableHeader<TableTestObject>>();
		ComponentFactories.AddStub<TablePaginationRow<TableTestObject>>();

		var tableInteractionState = new TableInteractionState
		{
			OrderByColumn = expectedOrderColumn,
			IsAscending = true
		};

		//Act
		var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
			.Add(p => p.Headings, Headings)
			.Add(p => p.Items, GetItems())
			.Add(p => p.RowsPerPageOptions, RowsPerPageOpts)
			.Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString(CultureInfo.InvariantCulture)))
			.Add(p => p.ShowPaginationRow, true)
			.Add(p => p.TableInteractionState, tableInteractionState));

		var tableHeader = cut.FindComponent<Stub<TableHeader<TableTestObject>>>();
		var actualSelectedOrderColumn = tableHeader.Instance.Parameters.Get(_ => _.SelectedOrderColumn);

		//Assert
		actualSelectedOrderColumn.ShouldBe(expectedOrderColumn);
	}

	[Theory(DisplayName = "OrderAscending Parameter Test")]
	[InlineData(true)]
	[InlineData(false)]
	public void Table_OrderAscendingParameter_RendersCorrectly(bool expectedOrderAscending)
	{
		//Arrange
		ComponentFactories.AddStub<TableHeader<TableTestObject>>();
		ComponentFactories.AddStub<TablePaginationRow<TableTestObject>>();

		var tableInteractionState = new TableInteractionState
		{
			IsAscending = expectedOrderAscending
		};

		//Act
		var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
			.Add(p => p.Headings, Headings)
			.Add(p => p.Items, GetItems())
			.Add(p => p.RowsPerPageOptions, RowsPerPageOpts)
			.Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString(CultureInfo.InvariantCulture)))
			.Add(p => p.ShowPaginationRow, true)
			.Add(p => p.TableInteractionState, tableInteractionState));

		var tableHeader = cut.FindComponent<Stub<TableHeader<TableTestObject>>>();
		var actualOrderAscending = tableHeader.Instance.Parameters.Get(_ => _.IsAscending);

		//Assert
		actualOrderAscending.ShouldBe(expectedOrderAscending);
	}

	[Theory(DisplayName = "PageChangedCallback Parameter Test"), AutoData]
	public void Table_PageChangedCallbackParameter_FiresEvent(int expectedPage)
	{
		//Arrange
		var eventFired = false;
		var actualPage = 1;
		ComponentFactories.AddStub<TableHeader<TableTestObject>>();
		ComponentFactories.AddStub<TablePaginationRow<TableTestObject>>();
		var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
			.Add(p => p.Headings, Headings)
			.Add(p => p.Items, GetItems())
			.Add(p => p.RowsPerPageOptions, RowsPerPageOpts)
			.Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString(CultureInfo.InvariantCulture)))
			.Add(p => p.SortableHeadersEnabled, false)
			.Add(p => p.ShowPaginationRow, true)
			.Add(p => p.OnTableInteraction, args =>
			{
				eventFired = true;
				actualPage = args.CurrentPage;
			}));

		var paginationRow = cut.FindComponent<Stub<TablePaginationRow<TableTestObject>>>();
		var callback = paginationRow.Instance.Parameters.Get(_ => _.OnPageChange);

		//Act
		paginationRow.InvokeAsync(() => callback.InvokeAsync(new PageChangeEventArgs(expectedPage)));

		//Assert
		eventFired.ShouldBeTrue();
		actualPage.ShouldBe(expectedPage);
	}

	[Fact(DisplayName = "RowsPerPageChangedCallback Parameter Test")]
	public void Table_RowsPerPageChangedCallbackParameter_FiresEvent()
	{
		//Arrange
		var eventFired = false;
		var actualSelectedRowsPerPageIndex = -1;
		var expectedRowsPerPageIndex = RandomUtilities.GetRandomIndex(RowsPerPageOpts.Count());
		ComponentFactories.AddStub<TableHeader<TableTestObject>>();
		ComponentFactories.AddStub<TablePaginationRow<TableTestObject>>();
		var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
			.Add(p => p.Headings, Headings)
			.Add(p => p.Items, GetItems())
			.Add(p => p.RowsPerPageOptions, RowsPerPageOpts)
			.Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString(CultureInfo.InvariantCulture)))
			.Add(p => p.ShowPaginationRow, true)
			.Add(p => p.OnTableInteraction, args =>
			{
				eventFired = true;
				actualSelectedRowsPerPageIndex = args.SelectedRowsPerPageIndex;
			}));

		var paginationRow = cut.FindComponent<Stub<TablePaginationRow<TableTestObject>>>();
		var callback = paginationRow.Instance.Parameters.Get(_ => _.OnRowsPerPageChange);

		//Act
		paginationRow.InvokeAsync(() => callback.InvokeAsync(new RowsPerPageChangeEventArgs(expectedRowsPerPageIndex)));

		//Assert
		eventFired.ShouldBeTrue();
		actualSelectedRowsPerPageIndex.ShouldBe(expectedRowsPerPageIndex);
	}

	[Theory(DisplayName = "ItemsOrderedChangedCallback Parameter Test")]
	[InlineAutoData(0, true)] //1st Header Ascending 
	[InlineAutoData(1, true)] //2nd Header Ascending
	[InlineAutoData(2, true)] //3rd Header Ascending
	[InlineAutoData(0, false)] //1st Header Descending 
	[InlineAutoData(1, false)] //2nd Header Descending
	[InlineAutoData(2, false)] //3rd Header DescendingAscending
	public void Table_ItemsOrderedCallbackParameter_FiresEvent(int expectedOrderColumnIndex, bool expectedIsAscending)
	{
		//Arrange
		var eventFired = false;
		var actualOrderColumn = string.Empty;
		var actualOrderAscending = false;
		var expectedOrderColumn = Headings.ElementAt(expectedOrderColumnIndex).OrderingName;
		ComponentFactories.AddStub<TableHeader<TableTestObject>>();
		ComponentFactories.AddStub<TablePaginationRow<TableTestObject>>();
		var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
			.Add(p => p.Headings, Headings)
			.Add(p => p.Items, GetItems())
			.Add(p => p.RowsPerPageOptions, RowsPerPageOpts)
			.Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString(CultureInfo.InvariantCulture)))
			.Add(p => p.SortableHeadersEnabled, true)
			.Add(p => p.ShowPaginationRow, true)
			.Add(p => p.OnTableInteraction, args =>
			{
				eventFired = true;
				actualOrderColumn = args.OrderByColumn;
				actualOrderAscending = args.IsAscending;
			}));

		var header = cut.FindComponent<Stub<TableHeader<TableTestObject>>>();
		var callback = header.Instance.Parameters.Get(_ => _.OnItemsSort);

		//Act
		header.InvokeAsync(() => callback.InvokeAsync(new ItemsSortEventArgs(expectedOrderColumn, expectedIsAscending)));

		//Assert
		eventFired.ShouldBeTrue();
		actualOrderColumn.ShouldBe(expectedOrderColumn);
		actualOrderAscending.ShouldBe(expectedIsAscending);
	}

	[Theory(DisplayName = "ItemsOrderedChangedCallback Parameter Test")]
	[InlineAutoData(0, true)] //1st Header Ascending 
	[InlineAutoData(1, true)] //2nd Header Ascending
	[InlineAutoData(2, true)] //3rd Header Ascending
	[InlineAutoData(0, false)] //1st Header Descending 
	[InlineAutoData(1, false)] //2nd Header Descending
	[InlineAutoData(2, false)] //3rd Header DescendingAscending
	public void Table_ItemsOrderedCallback_SortableHeadersEnabledFalse_DoesNotFireEvent(int expectedOrderColumnIndex, bool expectedIsAscending)
	{
		//Arrange
		var eventFired = false;
		var expectedOrderColumn = Headings.ElementAt(expectedOrderColumnIndex).OrderingName;
		ComponentFactories.AddStub<TableHeader<TableTestObject>>();
		ComponentFactories.AddStub<TablePaginationRow<TableTestObject>>();
		var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
			.Add(p => p.Headings, Headings)
			.Add(p => p.Items, GetItems())
			.Add(p => p.RowsPerPageOptions, RowsPerPageOpts)
			.Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString(CultureInfo.InvariantCulture)))
			.Add(p => p.SortableHeadersEnabled, false)
			.Add(p => p.ShowPaginationRow, false)
			.Add(p => p.OnTableInteraction, args => eventFired = true));

		var header = cut.FindComponent<Stub<TableHeader<TableTestObject>>>();
		var callback = header.Instance.Parameters.Get(_ => _.OnItemsSort);

		//Act
		header.InvokeAsync(() => callback.InvokeAsync(new ItemsSortEventArgs(expectedOrderColumn, expectedIsAscending)));

		//Assert
		eventFired.ShouldBeFalse();
	}

	[Theory(DisplayName = "ItemsOrderedChangedCallback Parameter Test")]
	[InlineAutoData(0, true)] //1st Header Ascending 
	[InlineAutoData(1, true)] //2nd Header Ascending
	[InlineAutoData(2, true)] //3rd Header Ascending
	[InlineAutoData(0, false)] //1st Header Descending 
	[InlineAutoData(1, false)] //2nd Header Descending
	[InlineAutoData(2, false)] //3rd Header DescendingAscending
	public void Table_ItemsOrderedCallback_OrdersItems(int expectedOrderColumnIndex, bool expectedIsAscending)
	{
		//Arrange
		var filteredItemsProp = typeof(Table<TableTestObject>).GetProperty("FilteredItems", BindingFlags.NonPublic | BindingFlags.Instance);
		var items = GetItems().ToList();
		var expectedOrderColumn = Headings.ElementAt(expectedOrderColumnIndex).OrderingName;
		var expectedOrderedItems = expectedIsAscending ? OrderCollection(items, expectedOrderColumnIndex)
			: OrderCollectionDesc(items, expectedOrderColumnIndex);
		ComponentFactories.AddStub<TableHeader<TableTestObject>>();
		ComponentFactories.AddStub<TablePaginationRow<TableTestObject>>();
		var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
			.Add(p => p.Headings, Headings)
			.Add(p => p.Items, items)
			.Add(p => p.RowsPerPageOptions, RowsPerPageOpts)
			.Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString(CultureInfo.InvariantCulture)))
			.Add(p => p.SortableHeadersEnabled, true)
			.Add(p => p.ShowPaginationRow, true));

		var header = cut.FindComponent<Stub<TableHeader<TableTestObject>>>();
		var callback = header.Instance.Parameters.Get(_ => _.OnItemsSort);

		//Act
		header.InvokeAsync(() => callback.InvokeAsync(new ItemsSortEventArgs(expectedOrderColumn, expectedIsAscending)));

		//Assert
		filteredItemsProp.GetValue(cut).ShouldBe(expectedOrderedItems);
	}
}
