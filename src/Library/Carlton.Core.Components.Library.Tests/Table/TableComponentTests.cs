using AutoFixture;
using AutoFixture.Xunit2;
using System.Globalization;
using static Carlton.Core.Components.Library.Tests.TableTestHelper;

namespace Carlton.Core.Components.Library.Tests;

[Trait("Component", nameof(Table<int>))]
public class TableComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test")]
    [InlineAutoData(10, 2, RowTemplate, 5, false)]
    [InlineAutoData(5, 7, RowTemplate, 3, false)]
    [InlineAutoData(3, 1, RowTemplate, 3, true)]
    [InlineAutoData(10, 3, RowTemplate, 4, true)]
    [InlineAutoData(10, 2, RowTemplate2, 5, false)]
    [InlineAutoData(5, 7, RowTemplate2, 3, false)]
    [InlineAutoData(3, 1, RowTemplate2, 3, true)]
    [InlineAutoData(10, 3, RowTemplate2, 4, true)]
    public void Table_Markup_RendersCorrectly(
        int numOfColumns,
        int numOfRows,
        string rowTemplate,
        int numRowsPerPage,
        bool showPaginationRow)
    {
        //Arrange
        var fixture = new Fixture();
        var headings = fixture.CreateMany<TableHeadingItem>(numOfColumns);
        var items = fixture.CreateMany<TableTestObject>(numOfRows);
        var rowsPerPageOpts = fixture.CreateMany<int>(numRowsPerPage);
        var expectedMarkup = BuildExpectedMarkup(headings, rowTemplate, items, showPaginationRow, rowsPerPageOpts, 0, 1);

        //Act
        var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, headings)
            .Add(p => p.Items, items)
            .Add(p => p.RowsPerPageOpts, rowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(rowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString("d", CultureInfo.InvariantCulture)))
            .Add(p => p.ShowPaginationRow, showPaginationRow));

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory(DisplayName = "Headings Parameter Test")]
    [InlineAutoData(3)]
    [InlineAutoData(10)]
    public void Table_HeadingsParam_RendersCorrectly(
            int numOfColumns,
            IEnumerable<TableTestObject> items,
            IEnumerable<int> rowsPerPageOpts)
    {
        //Arrange
        var fixture = new Fixture();
        var headings = fixture.CreateMany<TableHeadingItem>(numOfColumns);
        var expectedCount = headings.Count();
        var expectedHeaders = headings.Select(_ => _.DisplayName);

        //Act
        var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, headings)
            .Add(p => p.Items, items)
            .Add(p => p.RowsPerPageOpts, rowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString("d", CultureInfo.InvariantCulture)))
            .Add(p => p.ShowPaginationRow, true));

        var headerRowItems = cut.FindAll(".header-cell");
        var actualCount = headerRowItems.Count;
        var actualHeaders = cut.FindAll(".heading-text").Select(_ => _.TextContent);

        //Assert
        Assert.Equal(expectedCount, actualCount);
        Assert.Equal(expectedHeaders, actualHeaders);
    }

    [Theory(DisplayName = "Header Click Once Test")]
    [InlineAutoData(0)]
    [InlineAutoData(1)]
    [InlineAutoData(2)]
    public void Table_Header_OnClickOnce_FiltersItemsAsc(int columnIndex, IEnumerable<int> rowsPerPage)
    {
        //Arrange
        var unorderedItems = new Fixture().CreateMany<TableTestObject>();
        var items = OrderCollection(unorderedItems, columnIndex);

        var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, Headings)
            .Add(p => p.Items, items)
            .Add(p => p.RowsPerPageOpts, rowsPerPage)
            .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString("d", CultureInfo.InvariantCulture)))
            .Add(p => p.ShowPaginationRow, true));

        var headerRowItems = cut.FindAll(".header-cell");

        //Act
        headerRowItems.ElementAt(columnIndex).Click();

        //Assert
        Assert.Equal(items, cut.Instance.Items);
    }

    [Theory(DisplayName = "Header Click Twice Test")]
    [InlineAutoData(0)]
    [InlineAutoData(1)]
    [InlineAutoData(2)]
    public void Table_Header_OnClickTwice_FiltersItemsDesc(int columnIndex, IEnumerable<int> rowsPerPages)
    {
        //Arrange
        var unorderedItems = new Fixture().CreateMany<TableTestObject>();
        var items = OrderCollectionDesc(unorderedItems, columnIndex);
        var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, Headings)
            .Add(p => p.Items, items)
            .Add(p => p.RowsPerPageOpts, rowsPerPages)
            .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString("d", CultureInfo.InvariantCulture)))
            .Add(p => p.ShowPaginationRow, true));

        //Act
        var itemToClick = cut.FindAll(".header-cell").ElementAt(columnIndex);
        itemToClick.Click();
        itemToClick = cut.FindAll(".header-cell").ElementAt(columnIndex);
        itemToClick.Click();

        //Assert
        Assert.Equal(items, cut.Instance.Items);
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
        var containsSelectedClass = selectedItem.ClassList.Contains("selected");


        //Assert
        Assert.True(containsSelectedClass);
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
        var containsAscendingClass = selectedItem.ClassList.Contains("ascending");


        //Assert
        Assert.True(containsAscendingClass);
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
        var containsDescendingClass = selectedItem.ClassList.Contains("descending");


        //Assert
        Assert.True(containsDescendingClass);
    }

    [Theory(DisplayName = "Items Parameter Test"), AutoData]
    public void Table_ItemsParam_RendersCorrectly(
        IEnumerable<TableHeadingItem> headings,
        IEnumerable<TableTestObject> items,
        IEnumerable<int> rowsPerPageOpts)
    {
        //Arrange
        var expectedItemCount = items.Count();

        //Act
        var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, headings)
            .Add(p => p.Items, items)
            .Add(p => p.RowsPerPageOpts, rowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString("d", CultureInfo.InvariantCulture)))
            .Add(p => p.ShowPaginationRow, true));

        var tableRows = cut.FindAll(".table-row");
        var actualItemCount = tableRows.Count - 2; //Exclude the header and action rows
        var expectedDisplayValues = new List<string>();
        items.ToList().ForEach(item =>
        {
            expectedDisplayValues.Add(item.ID.ToString());
            expectedDisplayValues.Add(item.DisplayName);
            expectedDisplayValues.Add(item.CreatedDate.ToString("d", CultureInfo.InvariantCulture));
        });
        var actualDisplayValues = cut.FindAll("span.table-cell").Select(_ => _.TextContent);

        //Assert
        Assert.Equal(expectedItemCount, actualItemCount);
        Assert.Equal(expectedDisplayValues, actualDisplayValues);
    }

    [Theory(DisplayName = "RowTemplate Parameter Test")]
    [InlineAutoData("<div class=\"test-row\"><span>Template {0}{1}{2}</span></div>")]
    [InlineAutoData("<div class=\"test-row\"><span>{0}Test Template {1}{2}</span></div>")]
    [InlineAutoData("<div class=\"test-row\">{0}<span class=\"test\">{1}More Test Templates {2}</span></div>")]
    public void Table_RowTemplateParam_RendersCorrectly(string rowTemplate,
        List<TableTestObject> items,
        IEnumerable<TableHeadingItem> headings,
        IEnumerable<int> rowsPerPage,
        bool showPaginationRow)
    {
        //Act
        var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, headings)
            .Add(p => p.Items, items)
            .Add(p => p.RowsPerPageOpts, rowsPerPage)
            .Add(p => p.RowTemplate, item => string.Format(rowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString("d", CultureInfo.InvariantCulture)))
            .Add(p => p.ShowPaginationRow, showPaginationRow));

        var itemSpans = cut.FindAll(".item-row");

        //Assert
        Assert.All(itemSpans, (itemElement, i) =>
            {
                var expected = string.Format(rowTemplate, items[i].ID, items[i].DisplayName, items[i].CreatedDate.ToString("d", CultureInfo.InvariantCulture));
                Assert.Equal(expected, itemElement.InnerHtml);
            });
    }


    [Theory(DisplayName = "RowsPerPageOpts Parameter Test"), AutoData]
    public void Table_RowsPerPageOptsParam_RendersCorrectly(
        IEnumerable<TableTestObject> items,
        IEnumerable<int> rowsPerPageOpts)
    {
        //Arrange
        var expectedCount = rowsPerPageOpts.Count();
        var expectedRowsPerPageValues = rowsPerPageOpts.Select(_ => _);

        //Act
        var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, Headings)
            .Add(p => p.Items, items)
            .Add(p => p.RowsPerPageOpts, rowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString(CultureInfo.InvariantCulture)))
            .Add(p => p.ShowPaginationRow, true));

        var options = cut.FindAll(".option");
        var actualCount = options.Count;
        var actualValues = options.Select(_ => int.Parse(_.TextContent));

        //Assert
        Assert.Equal(expectedCount, actualCount);
        Assert.Equal(expectedRowsPerPageValues, actualValues);
    }

    [Theory(DisplayName = "ShowPaginationRow Parameter Test")]
    [InlineAutoData(true)]
    [InlineAutoData(false)]
    public void Table_ShowPaginationRow_RendersCorrectly(
        bool showPaginationRow,
        IEnumerable<TableHeadingItem> headings,
        IEnumerable<TableTestObject> items,
        IEnumerable<int> rowsPerPage)
    {
        //Act
        var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, headings)
            .Add(p => p.Items, items)
            .Add(p => p.RowsPerPageOpts, rowsPerPage)
            .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString(CultureInfo.InvariantCulture)))
            .Add(p => p.ShowPaginationRow, showPaginationRow));

        var paginationRowExists = cut.HasComponent<TablePaginationRow<TableTestObject>>();

        //Assert
        Assert.Equal(showPaginationRow, paginationRowExists);
    }

 
}