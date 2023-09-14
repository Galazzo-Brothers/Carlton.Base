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
        var expectedMarkup = BuildExpectedMarkup(headings, rowTemplate, items, showPaginationRow, rowsPerPageOpts);

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
            int numOfColumns)
    {
        //Arrange
        var fixture = new Fixture();
        var headings = fixture.CreateMany<TableHeadingItem>(numOfColumns);
        var expectedCount = headings.Count();
        var expectedHeaders = headings.Select(_ => _.DisplayName);

        //Act
        var cut = RenderComponent<Table<TableTestHelper.TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, headings)
            .Add(p => p.Items, TableTestHelper.Items)
            .Add(p => p.RowsPerPageOpts, TableTestHelper.RowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString("d", CultureInfo.InvariantCulture)))
            .Add(p => p.ShowPaginationRow, true));

        var headerRowItems = cut.FindAll(".header-row-item");
        var actualCount = headerRowItems.Count;
        var actualHeaders = cut.FindAll(".heading-text").Select(_ => _.TextContent);

        //Assert
        Assert.Equal(expectedCount, actualCount);
        Assert.Equal(expectedHeaders, actualHeaders);
    }

    [Theory(DisplayName = "Header Click Once Test")]
    [MemberData(nameof(TableTestHelper.GetFilteredItemsAsc), MemberType = typeof(TableTestHelper))]
    public void Table_Header_OnClickOnce_FiltersItemsAsc((int ColumnIndex, ReadOnlyCollection<TableTestObject> ExpectedItems) expected)
    {
        //Arrange
        var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, TableTestHelper.Headings)
            .Add(p => p.Items, TableTestHelper.Items)
            .Add(p => p.RowsPerPageOpts, TableTestHelper.RowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString("d", CultureInfo.InvariantCulture)))
            .Add(p => p.ShowPaginationRow, true)
            );

        var headerRowItems = cut.FindAll(".header-row-item");

        //Act
        headerRowItems.ElementAt(expected.ColumnIndex).Click();

        //Assert
        Assert.Equal(expected.ExpectedItems, cut.Instance.Items);
    }

    [Theory(DisplayName = "Header Click Twice Test")]
    [MemberData(nameof(TableTestHelper.GetFilteredItemsDesc), MemberType = typeof(TableTestHelper))]
    public void Table_Header_OnClickTwice_FiltersItemsDesc((int ColumnIndex, ReadOnlyCollection<TableTestObject> ExpectedItems) expected)
    {
        //Arrange
        var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, TableTestHelper.Headings)
            .Add(p => p.Items, TableTestHelper.Items)
            .Add(p => p.RowsPerPageOpts, TableTestHelper.RowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString("d", CultureInfo.InvariantCulture)))
            .Add(p => p.ShowPaginationRow, true));

        //Act
        var itemToClick = cut.FindAll(".header-row-item").ElementAt(expected.ColumnIndex);
        itemToClick.Click();
        itemToClick = cut.FindAll(".header-row-item").ElementAt(expected.ColumnIndex);
        itemToClick.Click();

        //Assert
        Assert.Equal(expected.ExpectedItems, cut.Instance.Items);
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
            .Add(p => p.Headings, TableTestHelper.Headings)
            .Add(p => p.Items, items)
            .Add(p => p.RowsPerPageOpts, rowsPerPage)
            .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString("d", CultureInfo.InvariantCulture)))
            .Add(p => p.ShowPaginationRow, showPaginationRow));

        var headerRowItems = cut.FindAll(".header-row-item", true);
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
            .Add(p => p.Headings, TableTestHelper.Headings)
            .Add(p => p.Items, items)
            .Add(p => p.RowsPerPageOpts, rowsPerPage)
            .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString("d", CultureInfo.InvariantCulture)))
            .Add(p => p.ShowPaginationRow, showPaginationRow));

        var headerRowItems = cut.FindAll(".header-row-item", true);
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
            .Add(p => p.Headings, TableTestHelper.Headings)
            .Add(p => p.Items, items)
            .Add(p => p.RowsPerPageOpts, rowsPerPage)
            .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString("d", CultureInfo.InvariantCulture)))
            .Add(p => p.ShowPaginationRow, showPaginationRow));


        //Act
        cut.FindAll(".header-row-item").ElementAt(selectedIndex).Click();
        cut.FindAll(".header-row-item").ElementAt(selectedIndex).Click();
        var selectedItem = cut.FindAll(".header-row-item").ElementAt(selectedIndex);
        var containsDescendingClass = selectedItem.ClassList.Contains("descending");


        //Assert
        Assert.True(containsDescendingClass);
    }

    [Theory(DisplayName = "Items Parameter Test"), AutoData]
    public void Table_ItemsParam_RendersCorrectly(IEnumerable<TableHeadingItem> headings, IEnumerable<TableTestObject> items)
    {
        //Arrange
        var expectedItemCount = items.Count();

        //Act
        var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, headings)
            .Add(p => p.Items, items)
            .Add(p => p.RowsPerPageOpts, TableTestHelper.RowsPerPageOpts)
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
        var actualDisplayValues = cut.FindAll(".item-row span").Select(_ => _.TextContent);

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


    [Theory(DisplayName = "RowsPerPageOpts Parameter Test")]
    [MemberData(nameof(TableTestHelper.GetRowsPerPageOptions), MemberType = typeof(TableTestHelper))]
    public void Table_RowsPerPageOptsParam_RendersCorrectly(IEnumerable<int> rowsPerPageOpts)
    {
        //Arrange
        var expectedCount = rowsPerPageOpts.Count();
        var expectedRowsPerPageValues = rowsPerPageOpts.Select(_ => _);

        //Act
        var cut = RenderComponent<Table<TableTestHelper.TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, TableTestHelper.Headings)
            .Add(p => p.Items, TableTestHelper.Items)
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
        IEnumerable<TableTestHelper.TableTestObject> items,
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

    [Theory(DisplayName = "ItemsChangedCallback Parameter Test")]
    [InlineData(1, 0, 0, 5)]
    [InlineData(1, 1, 0, 10)]
    [InlineData(1, 2, 0, 15)]
    [InlineData(2, 0, 5, 5)]
    [InlineData(2, 1, 10, 10)]
    [InlineData(2, 2, 0, 15)]
    [InlineData(3, 0, 10, 5)]
    [InlineData(3, 1, 10, 5)]
    [InlineData(3, 2, 0, 15)]
    public void Table_ItemsChangedCallback_FiresEvent(int currentPage, int optIndex, int expectedSkip, int expectedTake)
    {
        //Arrange
        var expectedItems = TableTestHelper.BigItemsList.Skip(expectedSkip).Take(expectedTake);
        var expectedIDs = expectedItems.Select(_ => _.ID);
        var cut = RenderComponent<Table<TableTestObject>>(parameters => parameters
           .Add(p => p.Headings, TableTestHelper.Headings)
           .Add(p => p.Items, TableTestHelper.BigItemsList)
           .Add(p => p.RowsPerPageOpts, TableTestHelper.RowsPerPageOpts)
           .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate.ToString(CultureInfo.InvariantCulture)))
           .Add(p => p.ShowPaginationRow, true));

        var options = cut.FindAll(".option");
        var optToClick = options.ElementAt(optIndex);
        var rightChevron = cut.Find(".mdi-chevron-right");

        //Act
        optToClick.Click();

        for(var i = 0; i < currentPage - 1; i++)
            rightChevron.Click();

        var actualIDs = cut.FindAll(".test-row").Select(_ => int.Parse(_.Children.First().TextContent.Replace("ID:", string.Empty)));

        //Assert
        Assert.Equal(expectedIDs, actualIDs);
    }


}