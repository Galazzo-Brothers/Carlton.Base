using AutoFixture;
using Carlton.Core.Components.Table;
using System.Globalization;
using static Carlton.Core.Components.Library.Tests.TableTestHelper;

namespace Carlton.Core.Components.Library.Tests;

[Trait("Component", nameof(Table<int>))]
public class TableComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test"), AutoData]
    public void Table_Markup_RendersCorrectly(
        IEnumerable<TableTestObject> expectedItems,
        IEnumerable<int> expectedRowsPerPageOpts,
        bool expectedShowPaginationRow)
    {
        //Arrange
        var selectedRowsPerPageIndex = RandomUtilities.GetRandomIndex(expectedRowsPerPageOpts.Count());
        var maxPages = expectedItems.Count() % expectedRowsPerPageOpts.ElementAt(selectedRowsPerPageIndex);
        var currentPage = RandomUtilities.GetRandomIndex(maxPages);
        var expectedMarkup = BuildExpectedMarkup(TableTestHeadingItems, RowTemplate, expectedItems, expectedShowPaginationRow, expectedRowsPerPageOpts, selectedRowsPerPageIndex, 1);

        //Act
        var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, TableTestHeadingItems)
            .Add(p => p.Items, expectedItems)
            .Add(p => p.RowsPerPageOpts, expectedRowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString("d", CultureInfo.InvariantCulture)))
            .Add(p => p.ShowPaginationRow, expectedShowPaginationRow));

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

    [Theory(DisplayName = "Header Click Once Test")]
    [InlineAutoData(0)]
    [InlineAutoData(1)]
    [InlineAutoData(2)]
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
    [InlineAutoData(0)]
    [InlineAutoData(1)]
    [InlineAutoData(2)]
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
        cut.Instance.Items.ShouldBe(items);
    }

    [Theory(DisplayName = "Header Click Once, CSS Selected Class Test")]
    [InlineAutoData(0)]
    [InlineAutoData(1)]
    [InlineAutoData(2)]
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
    [InlineAutoData(0)]
    [InlineAutoData(1)]
    [InlineAutoData(2)]
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
    [InlineAutoData(0)]
    [InlineAutoData(1)]
    [InlineAutoData(2)]
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
    [InlineAutoData("<div class=\"test-row\"><span>Template {0}{1}{2}</span></div>")]
    [InlineAutoData("<div class=\"test-row\"><span>{0}Test Template {1}{2}</span></div>")]
    [InlineAutoData("<div class=\"test-row\">{0}<span class=\"test\">{1}More Test Templates {2}</span></div>")]
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

        var itemSpans = cut.FindAll(".item-row");
        var expectedContent = expectedItems.Select(_ => string.Format(expectedRowTemplate, _.ID, _.DisplayName, _.CreatedDate.ToString("d", CultureInfo.InvariantCulture)));
        var actualContent = itemSpans.Select(_ => _.TextContent);

        //Assert
        actualContent.ShouldBe(expectedContent);
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

 
}