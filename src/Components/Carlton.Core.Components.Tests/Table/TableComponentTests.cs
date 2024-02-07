using System.Globalization;
using Carlton.Core.Components.Dropdowns;
using Carlton.Core.Components.Table;
using static Carlton.Core.Components.Tests.TableTestHelper;
namespace Carlton.Core.Components.Tests;

[Trait("Component", nameof(Table<int>))]
public class TableComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test"), AutoData]
    public void Table_Markup_RendersCorrectly(
        IEnumerable<TableTestObject> expectedItems,
        bool expectedIsZebraStriped,
        bool expectedIsHoverable,
        bool expectedShowPaginationRow,
        IEnumerable<int> expectedRowsPerPageOpts)
    {
        //Arrange
        var selectedRowsPerPageIndex = RandomUtilities.GetRandomIndex(expectedRowsPerPageOpts.Count());
        var maxPages = Math.Max(1, (int)Math.Ceiling((double)expectedItems.Count() / expectedRowsPerPageOpts.ElementAt(selectedRowsPerPageIndex)));
        var currentPage = RandomUtilities.GetRandomNonZeroIndex(maxPages);
        var expectedMarkup = BuildExpectedMarkup(
            TableTestHeadingItems,
            RowTemplate,
            expectedItems,
            expectedIsZebraStriped,
            expectedIsHoverable,
            expectedShowPaginationRow,
            expectedRowsPerPageOpts,
            currentPage,
            selectedRowsPerPageIndex);

        //Act
        var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, TableTestHeadingItems)
            .Add(p => p.Items, expectedItems)
            .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString("d", CultureInfo.InvariantCulture)))
            .Add(p => p.ZebraStripped, expectedIsZebraStriped)
            .Add(p => p.Hoverable, expectedIsHoverable)
            .Add(p => p.ShowPaginationRow, expectedShowPaginationRow)
            .Add(p => p.RowsPerPageOpts, expectedRowsPerPageOpts)
            .Add(p => p.CurrentPage, currentPage)
            .Add(p => p.SelectedRowsPerPageIndex, selectedRowsPerPageIndex));

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

        //Act
        var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, TableTestHeadingItems)
            .Add(p => p.Items, expectedOrderedItems)
            .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString("d", CultureInfo.InvariantCulture)))
            .Add(p => p.ZebraStripped, expectedIsZebraStriped)
            .Add(p => p.Hoverable, expectedIsHoverable)
            .Add(p => p.OrderColumn, expectedOrderColumn)
            .Add(p => p.OrderAscending, expectedOrderAscending)
            .Add(p => p.ShowPaginationRow, expectedShowPaginationRow)
            .Add(p => p.RowsPerPageOpts, expectedRowsPerPageOpts)
            .Add(p => p.CurrentPage, currentPage)
            .Add(p => p.SelectedRowsPerPageIndex, selectedRowsPerPageIndex));

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory(DisplayName = "Headings Parameter Test"), AutoData]
    public void Table_HeadingsParameter_RendersCorrectly(
        IEnumerable<TableHeadingItem> expectedHeadings,
        IEnumerable<TableTestObject> items,
        IEnumerable<int> rowsPerPageOpts)
    {
        //Arrange
        var expectedCount = expectedHeadings.Count();
        var expectedHeaders = expectedHeadings.Select(_ => _.DisplayName);

        //Act
        var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, expectedHeadings)
            .Add(p => p.Items, items)
            .Add(p => p.RowsPerPageOpts, rowsPerPageOpts)
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
        IEnumerable<int> rowsPerPageOpts)
    {
        //Arrange
        var expectedItemCount = expectedItems.Count();

        //Act
        var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, expectedHeadings)
            .Add(p => p.Items, expectedItems)
            .Add(p => p.RowsPerPageOpts, rowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString("d", CultureInfo.InvariantCulture)))
            .Add(p => p.ShowPaginationRow, true));

        var tableRows = cut.FindAll(".table-row");
        var actualItemCount = tableRows.Count - 2; //Exclude the header and action rows
        var expectedDisplayValues = expectedItems.Select(item => new TableTestObject(item.ID, item.DisplayName, item.CreatedDate))
            .SelectMany(_ => new[] { _.ID.ToString(), _.DisplayName, _.CreatedDate.ToString("d", CultureInfo.InvariantCulture) });
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
        IEnumerable<TableHeadingItem> expectedHeadings,
        IEnumerable<int> expectedRowsPerPage,
        bool expectedShowPaginationRow)
    {
        //Act
        var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, expectedHeadings)
            .Add(p => p.Items, expectedItems)
            .Add(p => p.RowsPerPageOpts, expectedRowsPerPage)
            .Add(p => p.RowTemplate, item => string.Format(expectedRowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString("d", CultureInfo.InvariantCulture)))
            .Add(p => p.ShowPaginationRow, expectedShowPaginationRow));

        var rowElements = cut.FindAll(".table-row");
        var itemRowElements = rowElements.Skip(1).Take(rowElements.Count - 2);
        var expectedContent = expectedItems.Select(_ => string.Format(expectedRowTemplate, _.ID, _.DisplayName, _.CreatedDate.ToString("d", CultureInfo.InvariantCulture)));
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
        //Act
        var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, expectedHeadings)
            .Add(p => p.Items, expectedItems)
            .Add(p => p.ZebraStripped, expectedZebraStriped)
            .Add(p => p.RowsPerPageOpts, expectedRowsPerPage)
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
        //Act
        var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, expectedHeadings)
            .Add(p => p.Items, expectedItems)
            .Add(p => p.ZebraStripped, expectedZebraStriped)
            .Add(p => p.Hoverable, expectedHoverable)
            .Add(p => p.RowsPerPageOpts, expectedRowsPerPage)
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
    public void Table_ShowPaginationRowParamter_RendersCorrectly(
        bool expectedShowPaginationRow,
        IEnumerable<TableHeadingItem> expectedHeadings,
        IEnumerable<TableTestObject> expectedItems,
        IEnumerable<int> expectedRowsPerPage)
        {
            //Act
            var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
                .Add(p => p.Headings, expectedHeadings)
                .Add(p => p.Items, expectedItems)
                .Add(p => p.RowsPerPageOpts, expectedRowsPerPage)
                .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString(CultureInfo.InvariantCulture)))
                .Add(p => p.ShowPaginationRow, expectedShowPaginationRow));

            var paginationRowExists = cut.HasComponent<TablePaginationRow<TableTestObject>>();

            //Assert
            paginationRowExists.ShouldBe(expectedShowPaginationRow);
        }

    [Theory(DisplayName = "CurrentPage Parameter Test")]
    [InlineAutoData(1, 0, "1-5 of 15")] //15 Items, Page 1, 5 Rows PerPage 
    [InlineAutoData(2, 0, "6-10 of 15")] //15 Items, Page 2, 5 Rows PerPage 
    [InlineAutoData(3, 0, "11-15 of 15")] //15 Items, Page 3, 5 Rows PerPage 
    [InlineAutoData(1, 1, "1-10 of 15")] //15 Items, Page 1, 10 Rows PerPage 
    [InlineAutoData(2, 1, "11-15 of 15")] //15 Items, Page 2, 10 Rows PerPage 
    [InlineAutoData(1, 2, "1-15 of 15")] //15 Items, Page 1, 15 Rows PerPage 
    public void Table_CurrentPageParamter_RendersCorrectly(
      int expectedCurrentPage,
      int expectedSelectedRowsPerPageIndex,
      string expectedLabel)
    {
        //Act
        var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, Headings)
            .Add(p => p.Items, GetItems())
            .Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString(CultureInfo.InvariantCulture)))
            .Add(p => p.CurrentPage, expectedCurrentPage)
            .Add(p => p.SelectedRowsPerPageIndex, expectedSelectedRowsPerPageIndex)
            .Add(p => p.ShowPaginationRow, true));

        var paginationLabelElement = cut.Find(".pagination-label");

        //Assert
        paginationLabelElement.TextContent.ShouldBe(expectedLabel);
    }

    [Theory(DisplayName = "RowsPerPageOpts Parameter Test"), AutoData]
    public void Table_RowsPerPageOptsParameter_RendersCorrectly(
        IEnumerable<TableTestObject> expectedItems,
        IEnumerable<int> expectedRowsPerPageOpts)
    {
        //Arrange
        var expectedCount = expectedRowsPerPageOpts.Count();
        var expectedRowsPerPageValues = expectedRowsPerPageOpts.Select(_ => _);

        //Act
        var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, Headings)
            .Add(p => p.Items, expectedItems)
            .Add(p => p.RowsPerPageOpts, expectedRowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString(CultureInfo.InvariantCulture)))
            .Add(p => p.ShowPaginationRow, true));

        var options = cut.FindAll(".option");
        var actualCount = options.Count;
        var actualValues = options.Select(_ => int.Parse(_.TextContent));

        //Assert
        actualCount.ShouldBe(expectedCount);
        actualValues.ShouldBe(expectedRowsPerPageValues);
    }


    [Theory(DisplayName = "SelectedRowsPerPageIndex Parameter Test")]
    [InlineAutoData(1, 0)] //15 Items, Page 1, 5 Rows PerPage 
    [InlineAutoData(2, 0)] //15 Items, Page 2, 5 Rows PerPage 
    [InlineAutoData(3, 0)] //15 Items, Page 3, 5 Rows PerPage 
    [InlineAutoData(1, 1)] //15 Items, Page 1, 10 Rows PerPage 
    [InlineAutoData(2, 1)] //15 Items, Page 2, 10 Rows PerPage 
    [InlineAutoData(1, 2)] //15 Items, Page 1, 15 Rows PerPage 
    public void Table_SelectedRowsPageIndexParamter_RendersCorrectly(
       int expectedCurrentPage,
       int expectedSelectedRowsPerPageIndex)
    {
        //Act
        var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, Headings)
            .Add(p => p.Items, GetItems())
            .Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString(CultureInfo.InvariantCulture)))
            .Add(p => p.CurrentPage, expectedCurrentPage)
            .Add(p => p.SelectedRowsPerPageIndex, expectedSelectedRowsPerPageIndex)
            .Add(p => p.ShowPaginationRow, true));

        var dropdownComponent = cut.FindComponent<Dropdown<int>>();

        //Assert
        dropdownComponent.Instance.SelectedIndex.ShouldBe(expectedSelectedRowsPerPageIndex);
        dropdownComponent.Instance.SelectedValue.ShouldBe(RowsPerPageOpts.ElementAt(expectedSelectedRowsPerPageIndex));
    }

    [Theory(DisplayName = "SortableHeaders Enabled Parameter Test")]
    [InlineData(true)]
    [InlineData(false)]
    public void Table_Header_SortableHeadersEnabledParameter_RendersCorrectly(
        bool expectedSortableHeadersEnabled)
    {
        //Arrange
        var headerIndex = RandomUtilities.GetRandomIndex(Headings.Count);
        var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, Headings)
            .Add(p => p.Items, GetItems())
            .Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString("d", CultureInfo.InvariantCulture)))
            .Add(p => p.ShowPaginationRow, true)
            .Add(p => p.SortableHeadersEnabled, expectedSortableHeadersEnabled));

        var headerRowItems = cut.FindAll(".header-cell");
        var selectedHeaders = cut.FindAll(".header-cell.selected", true);

        //Act
        headerRowItems.ElementAt(headerIndex).Click();


        //Assert
        selectedHeaders.Any().ShouldBe(expectedSortableHeadersEnabled);
    }

    [Theory(DisplayName = "OrderColumn Parameter Test")]
    [InlineData("ID", true)]
    [InlineData("ID", false)]
    [InlineData("DisplayName", true)]
    [InlineData("DisplayName", false)]
    [InlineData("CreatedDate", true)]
    [InlineData("CreatedDate", false)]
    public void Table_OrderColumnParamter_RendersCorrectly(
        string expectedOrderColumn,
        bool expectedOrderAscending)
    {
        //Act
        var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, Headings)
            .Add(p => p.Items, GetItems())
            .Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString(CultureInfo.InvariantCulture)))
            .Add(p => p.CurrentPage, 1)
            .Add(p => p.SelectedRowsPerPageIndex, 0)
            .Add(p => p.ShowPaginationRow, true)
            .Add(p => p.OrderColumn, expectedOrderColumn)
            .Add(p => p.OrderAscending, expectedOrderAscending));

        var tableHeader = cut.FindComponent<TableHeader<TableTestObject>>();

        //Assert
        tableHeader.Instance.SelectedOrderColumn.ShouldBe(expectedOrderColumn);
    }

    [Theory(DisplayName = "OrderAscending Parameter Test")]
    [InlineData("ID", true)]
    [InlineData("ID", false)]
    [InlineData("DisplayName", true)]
    [InlineData("DisplayName", false)]
    [InlineData("CreatedDate", true)]
    [InlineData("CreatedDate", false)]
    public void Table_OrderAscendingParamter_RendersCorrectly(
       string expectedOrderColumn,
       bool expectedOrderAscending)
    {
        //Act
        var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, Headings)
            .Add(p => p.Items, GetItems())
            .Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString(CultureInfo.InvariantCulture)))
            .Add(p => p.CurrentPage, 1)
            .Add(p => p.SelectedRowsPerPageIndex, 0)
            .Add(p => p.ShowPaginationRow, true)
            .Add(p => p.OrderColumn, expectedOrderColumn)
            .Add(p => p.OrderAscending, expectedOrderAscending));

        var tableHeader = cut.FindComponent<TableHeader<TableTestObject>>();

        //Assert
        tableHeader.Instance.OrderAscending.ShouldBe(expectedOrderAscending);
    }

    [Theory(DisplayName = "PageChangedCallback Parameter Test")]
    [InlineAutoData(1)] //15 Rows, Page 1, 5 Rows PerPage
    [InlineAutoData(2)] //15 Rows, Page 2, 5 Rows PerPage
    [InlineAutoData(3)] //15 Rows, Page 3, 5 Rows PerPage
    public void Table_PageChangedCallback_FiresEvent(
       int expectedCurrentPage)
    {
        //Arrange
        var eventFired = false;
        var actualPage = 1;
        var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, Headings)
            .Add(p => p.Items, GetItems())
            .Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString(CultureInfo.InvariantCulture)))
            .Add(p => p.CurrentPage, expectedCurrentPage)
            .Add(p => p.SelectedRowsPerPageIndex, 0)
            .Add(p => p.ShowPaginationRow, true)
            .Add(p => p.PageChanged, args =>
            {
                eventFired = true;
                actualPage = args.CurrentPage;
            }));

        var rightChevron = cut.Find(".mdi-chevron-right");

        //Act
        rightChevron.Click();

        //Assert
        eventFired.ShouldBeTrue();
        actualPage.ShouldBe(expectedCurrentPage + 1);
    }

    [Theory(DisplayName = "RowsPerPageChangedCallback Parameter Test")]
    [InlineAutoData(0)] //5 Rows PerPage 
    [InlineAutoData(1)] //10 Rows PerPage 
    [InlineAutoData(2)] //15 Rows PerPage 
    public void Table_RowsPerPageChangedCallback_FiresEvent(int expectedIndexToSelect)
    {
        //Arrange
        var eventFired = false;
        var actualSelectedRowsPerPageIndex = -1;
        var selectedRowsPerPageCount = RowsPerPageOpts.ElementAt(expectedIndexToSelect);
        var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, Headings)
            .Add(p => p.Items, GetItems())
            .Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString(CultureInfo.InvariantCulture)))
            .Add(p => p.CurrentPage, 1)
            .Add(p => p.SelectedRowsPerPageIndex, expectedIndexToSelect)
            .Add(p => p.ShowPaginationRow, true)
            .Add(p => p.RowsPerPageChanged, args =>
            {
                eventFired = true;
                actualSelectedRowsPerPageIndex = args.SelectedRowsPerPageIndex;
            }));

        var options = cut.FindAll(".option");
        var optToClick = options.First(_ => int.Parse(_.TextContent) == selectedRowsPerPageCount);

        //Act
        optToClick.Click();

        //Assert
        eventFired.ShouldBeTrue();
        actualSelectedRowsPerPageIndex.ShouldBe(expectedIndexToSelect);
    }

    [Theory(DisplayName = "ItemsOrderedChangedCallback Parameter Test")]
    [InlineAutoData(0)] //1st Header 
    [InlineAutoData(1)] //2nd Header 
    [InlineAutoData(2)] //3rd Header 
    public void Table_ItemsOrderedCallback_FiresEvent(
       int expectedColumnIndex)
    {
        //Arrange
        var eventFired = false;
        var actualOrderColumn = string.Empty;
        var actualOrderAscending = false;

        var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, Headings)
            .Add(p => p.Items, GetItems())
            .Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString(CultureInfo.InvariantCulture)))
            .Add(p => p.CurrentPage, 1)
            .Add(p => p.SelectedRowsPerPageIndex, 0)
            .Add(p => p.ShowPaginationRow, true)
            .Add(p => p.ItemsOrdered, args =>
            {
                eventFired = true;
                actualOrderColumn = args.OrderColumn;
                actualOrderAscending = args.OrderAscending;
            }));

        var headerRowItems = cut.FindAll(".header-cell");
        var expectedOrderColumn = Headings.ElementAt(expectedColumnIndex).OrderingName;

        //Act
        headerRowItems.ElementAt(expectedColumnIndex).Click();

        //Assert
        eventFired.ShouldBeTrue();
        actualOrderColumn.ShouldBe(expectedOrderColumn);
        actualOrderAscending.ShouldBe(true);
    }

    [Theory(DisplayName = "ItemsOrderedChangedCallback Descending Parameter Test")]
    [InlineAutoData(0)] //1st Header 
    [InlineAutoData(1)] //2nd Header 
    [InlineAutoData(2)] //3rd Header 
    public void Table_ItemsOrderedCallback_Desc_FiresEvent(
      int expectedColumnIndex)
    {
        //Arrange
        var eventFired = false;
        var actualOrderColumn = string.Empty;
        var actualOrderAscending = true;

        var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, Headings)
            .Add(p => p.Items, GetItems())
            .Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString(CultureInfo.InvariantCulture)))
            .Add(p => p.CurrentPage, 1)
            .Add(p => p.SelectedRowsPerPageIndex, 0)
            .Add(p => p.ShowPaginationRow, true)
            .Add(p => p.ItemsOrdered, args =>
            {
                eventFired = true;
                actualOrderColumn = args.OrderColumn;
                actualOrderAscending = args.OrderAscending;
            }));

        var headerRowItems = cut.FindAll(".header-cell");
        var expectedOrderColumn = Headings.ElementAt(expectedColumnIndex).OrderingName;

        //Act
        var heading = headerRowItems.ElementAt(expectedColumnIndex);
        heading.Click();

        heading = cut.FindAll(".header-cell").ElementAt(expectedColumnIndex);
        heading.Click();

        //Assert
        eventFired.ShouldBeTrue();
        actualOrderColumn.ShouldBe(expectedOrderColumn);
        actualOrderAscending.ShouldBe(false);
    }

    [Theory(DisplayName = "Header Click Once Test")]
    [InlineAutoData(0)] //1st Header 
    [InlineAutoData(1)] //2nd Header
    [InlineAutoData(2)] //3rd Header
    public void Table_Header_OnClickOnce_FiltersItemsAsc(
       int expectedColumnIndex,
       IEnumerable<TableTestObject> items,
       IEnumerable<int> expectedRowsPerPage)
    {
        //Arrange
        var expectedItems = OrderCollection(items, expectedColumnIndex);
        var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, Headings)
            .Add(p => p.Items, items)
            .Add(p => p.RowsPerPageOpts, expectedRowsPerPage)
            .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString("d", CultureInfo.InvariantCulture)))
            .Add(p => p.ShowPaginationRow, true));

        var headerRowItems = cut.FindAll(".header-cell");

        //Act
        headerRowItems.ElementAt(expectedColumnIndex).Click();

        //Assert
        cut.Instance.Items.ShouldBe(expectedItems);
    }

    [Theory(DisplayName = "Header Click Twice Test")]
    [InlineAutoData(0)] //1st Header 
    [InlineAutoData(1)] //2nd Header 
    [InlineAutoData(2)] //3rd Header 
    public void Table_Header_OnClickTwice_FiltersItemsDesc(
        int expectedColumnIndex,
        IEnumerable<TableTestObject> items,
        IEnumerable<int> expectedRowsPerPages)
    {
        //Arrange
        var expectedItems = OrderCollectionDesc(items, expectedColumnIndex);
        var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, Headings)
            .Add(p => p.Items, items)
            .Add(p => p.RowsPerPageOpts, expectedRowsPerPages)
            .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString("d", CultureInfo.InvariantCulture)))
            .Add(p => p.ShowPaginationRow, true));

        //Act
        var itemToClick = cut.FindAll(".header-cell").ElementAt(expectedColumnIndex);
        itemToClick.Click();
        itemToClick = cut.FindAll(".header-cell").ElementAt(expectedColumnIndex);
        itemToClick.Click();

        //Assert
        cut.Instance.Items.ShouldBe(expectedItems);
    }

    [Theory(DisplayName = "Header Click Once, CSS Selected Class Test")]
    [InlineAutoData(0)] //1st Header 
    [InlineAutoData(1)] //2nd Header 
    [InlineAutoData(2)] //3rd Header 
    public void Table_HeaderItemOnClick_SelectedClass_RendersCorrectly(
        int selectedIndex,
        IEnumerable<TableTestObject> items,
        IEnumerable<int> rowsPerPage,
        bool showPaginationRow)
    {
        //Arrange
        var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, Headings)
            .Add(p => p.Items, items)
            .Add(p => p.RowsPerPageOpts, rowsPerPage)
            .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString("d", CultureInfo.InvariantCulture)))
            .Add(p => p.ShowPaginationRow, showPaginationRow));

        var headerRowItems = cut.FindAll(".header-cell", true);
        var selectedItem = headerRowItems.ElementAt(selectedIndex);

        //Act
        selectedItem.Click();
        selectedItem = headerRowItems.ElementAt(selectedIndex);

        //Assert
        selectedItem.ClassList.ShouldContain("selected");
    }

    [Theory(DisplayName = "Header Click Once, CSS Ascending Class Test")]
    [InlineAutoData(0)] //1st Header 
    [InlineAutoData(1)] //2nd Header 
    [InlineAutoData(2)] //3rd Header 
    public void Table_HeaderItemOnClick_AscendingClass_RendersCorrectly(
        int selectedIndex,
        IEnumerable<TableTestObject> items,
        IEnumerable<int> rowsPerPage,
        bool showPaginationRow)
    {
        //Arrange
        var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, Headings)
            .Add(p => p.Items, items)
            .Add(p => p.RowsPerPageOpts, rowsPerPage)
            .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString("d", CultureInfo.InvariantCulture)))
            .Add(p => p.ShowPaginationRow, showPaginationRow));

        var headerRowItems = cut.FindAll(".header-cell", true);
        var selectedItem = headerRowItems.ElementAt(selectedIndex);

        //Act
        selectedItem.Click();
        selectedItem = headerRowItems.ElementAt(selectedIndex);

        //Assert
        selectedItem.ClassList.ShouldContain("ascending");
    }

    [Theory(DisplayName = "Header Click Once, CSS Descending Class Test")]
    [InlineAutoData(0)] //1st Header 
    [InlineAutoData(1)] //2nd Header 
    [InlineAutoData(2)] //3rd Header 
    public void Table_HeaderItemOnClick_DescendingClass_RendersCorrectly(
        int selectedIndex,
        IEnumerable<TableTestObject> items,
        IEnumerable<int> rowsPerPage,
        bool showPaginationRow)
    {
        //Arrange
        var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, Headings)
            .Add(p => p.Items, items)
            .Add(p => p.RowsPerPageOpts, rowsPerPage)
            .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString("d", CultureInfo.InvariantCulture)))
            .Add(p => p.ShowPaginationRow, showPaginationRow));


        //Act
        cut.FindAll(".header-cell").ElementAt(selectedIndex).Click();
        cut.FindAll(".header-cell").ElementAt(selectedIndex).Click();
        var selectedItem = cut.FindAll(".header-cell").ElementAt(selectedIndex);

        //Assert
        selectedItem.ClassList.ShouldContain("descending");
    }
}