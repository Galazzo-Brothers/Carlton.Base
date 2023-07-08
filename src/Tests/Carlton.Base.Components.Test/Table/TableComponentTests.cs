namespace Carlton.Base.Components.Test;

[Trait("Component", nameof(Table<int>))]
public class TableComponentTests : TestContext
{
    [Fact(DisplayName = "Markup Test")]
    public void Table_Markup_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<Table<TableTestHelper.TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, TableTestHelper.Headings)
            .Add(p => p.Items, TableTestHelper.Items)
            .Add(p => p.RowsPerPageOpts, TableTestHelper.RowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(TableTestHelper.RowTemplate, item.ID, item.DisplayName, item.CreatedDate))
            .Add(p => p.ShowPaginationRow, true)
            );

        //Assert
        cut.MarkupMatches(TableTestHelper.TableMarkup.Trim());
    }

    [Theory(DisplayName = "Headings Parameter Test")]
    [MemberData(nameof(TableTestHelper.GetHeadings), MemberType = typeof(TableTestHelper))]
    public void Table_HeadingsParam_RendersCorrectly(IReadOnlyCollection<TableHeadingItem> headings)
    {
        //Arrange
        var expectedCount = headings.Count;
        var expectedHeaders = headings.Select(_ => _.DisplayName);

        //Act
        var cut = RenderComponent<Table<TableTestHelper.TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, headings)
            .Add(p => p.Items, TableTestHelper.Items)
            .Add(p => p.RowsPerPageOpts, TableTestHelper.RowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(TableTestHelper.RowTemplate, item.ID, item.DisplayName, item.CreatedDate))
            .Add(p => p.ShowPaginationRow, true)
            );

        var headerRowItems = cut.FindAll(".header-row-item");
        var actualCount = headerRowItems.Count;
        var actualHeaders = cut.FindAll(".heading-text").Select(_ => _.TextContent);

        //Assert
        Assert.Equal(expectedCount, actualCount);
        Assert.Equal(expectedHeaders, actualHeaders);
    }

    [Theory(DisplayName = "Header Click Once Test")]
    [MemberData(nameof(TableTestHelper.GetFilteredItemsAsc), MemberType = typeof(TableTestHelper))]
    public void Table_Header_OnClickOnce_FiltersItemsAsc((int ColumnIndex, ReadOnlyCollection<TableTestHelper.TableTestObject> ExpectedItems) expected)
    {
        //Arrange
        var cut = RenderComponent<Table<TableTestHelper.TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, TableTestHelper.Headings)
            .Add(p => p.Items, TableTestHelper.Items)
            .Add(p => p.RowsPerPageOpts, TableTestHelper.RowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(TableTestHelper.RowTemplate, item.ID, item.DisplayName, item.CreatedDate))
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
    public void Table_Header_OnClickTwice_FiltersItemsDesc((int ColumnIndex, ReadOnlyCollection<TableTestHelper.TableTestObject> ExpectedItems) expected)
    {
        //Arrange
        var cut = RenderComponent<Table<TableTestHelper.TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, TableTestHelper.Headings)
            .Add(p => p.Items, TableTestHelper.Items)
            .Add(p => p.RowsPerPageOpts, TableTestHelper.RowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(TableTestHelper.RowTemplate, item.ID, item.DisplayName, item.CreatedDate))
            .Add(p => p.ShowPaginationRow, true)
            );

        //Act
        var itemToClick = cut.FindAll(".header-row-item").ElementAt(expected.ColumnIndex);
        itemToClick.Click();
        itemToClick = cut.FindAll(".header-row-item").ElementAt(expected.ColumnIndex);
        itemToClick.Click();

        //Assert
        Assert.Equal(expected.ExpectedItems, cut.Instance.Items);
    }

    [Theory(DisplayName = "Header Click Once, CSS Selected Class Test")]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public void Table_HeaderItemOnClick_SelectedClass_RendersCorrectly(int selectedIndex)
    {
        //Arrange
        var cut = RenderComponent<Table<TableTestHelper.TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, TableTestHelper.Headings)
            .Add(p => p.Items, TableTestHelper.Items)
            .Add(p => p.RowsPerPageOpts, TableTestHelper.RowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(TableTestHelper.RowTemplate, item.ID, item.DisplayName, item.CreatedDate))
            .Add(p => p.ShowPaginationRow, true)
            );

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
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public void Table_HeaderItemOnClick_AscendingClass_RendersCorrectly(int selectedIndex)
    {
        //Arrange
        var cut = RenderComponent<Table<TableTestHelper.TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, TableTestHelper.Headings)
            .Add(p => p.Items, TableTestHelper.Items)
            .Add(p => p.RowsPerPageOpts, TableTestHelper.RowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(TableTestHelper.RowTemplate, item.ID, item.DisplayName, item.CreatedDate))
            .Add(p => p.ShowPaginationRow, true)
            );

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
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public void Table_HeaderItemOnClick_DescendingClass_RendersCorrectly(int selectedIndex)
    {
        //Arrange
        var cut = RenderComponent<Table<TableTestHelper.TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, TableTestHelper.Headings)
            .Add(p => p.Items, TableTestHelper.Items)
            .Add(p => p.RowsPerPageOpts, TableTestHelper.RowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(TableTestHelper.RowTemplate, item.ID, item.DisplayName, item.CreatedDate))
            .Add(p => p.ShowPaginationRow, true)
            );


        //Act
        cut.FindAll(".header-row-item").ElementAt(selectedIndex).Click();
        cut.FindAll(".header-row-item").ElementAt(selectedIndex).Click();
        var selectedItem = cut.FindAll(".header-row-item").ElementAt(selectedIndex);
        var containsDescendingClass = selectedItem.ClassList.Contains("descending");


        //Assert
        Assert.True(containsDescendingClass);
    }

    [Theory(DisplayName = "Items Parameter Test")]
    [MemberData(nameof(TableTestHelper.GetItemsWithDisplayedValues), MemberType = typeof(TableTestHelper))]
    public void Table_ItemsParam_RendersCorrectly((ReadOnlyCollection<TableTestHelper.TableTestObject> items, ReadOnlyCollection<string> displayValues) expected)
    {
        //Arrange
        var expectedItemCount = expected.items.Count;

        //Act
        var cut = RenderComponent<Table<TableTestHelper.TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, TableTestHelper.Headings)
            .Add(p => p.Items, expected.items)
            .Add(p => p.RowsPerPageOpts, TableTestHelper.RowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(TableTestHelper.RowTemplate, item.ID, item.DisplayName, item.CreatedDate))
            .Add(p => p.ShowPaginationRow, true)
            );

        var tableRows = cut.FindAll(".table-row");
        var actualItemCount = tableRows.Count - 2; //Exclude the header and action rows
        var actualDisplayValues = cut.FindAll(".item-row span").Select(_ => _.TextContent);

        //Assert
        Assert.Equal(expectedItemCount, actualItemCount);
        Assert.Equal(expected.displayValues, actualDisplayValues);

    }

    [Theory(DisplayName = "RowTemplate Parameter Test")]
    [InlineData("<div class=\"test-row\"><span>Template {0}{1}{2}</span></div>")]
    [InlineData("<div class=\"test-row\"><span>{0}Test Template {1}{2}</span></div>")]
    [InlineData("<div class=\"test-row\">{0}<span class=\"test\">{1}More Test Templates {2}</span></div>")]
    public void Table_RowTemplateParam_RendersCorrectly(string rowTemplate)
    {
        //Arrange
        var expectedRow1 = string.Format(rowTemplate, TableTestHelper.Items.ElementAt(0).ID, TableTestHelper.Items.ElementAt(0).DisplayName, TableTestHelper.Items.ElementAt(0).CreatedDate);
        var expectedRow2 = string.Format(rowTemplate, TableTestHelper.Items.ElementAt(1).ID, TableTestHelper.Items.ElementAt(1).DisplayName, TableTestHelper.Items.ElementAt(1).CreatedDate);
        var expectedRow3 = string.Format(rowTemplate, TableTestHelper.Items.ElementAt(2).ID, TableTestHelper.Items.ElementAt(2).DisplayName, TableTestHelper.Items.ElementAt(2).CreatedDate);

        //Act
        var cut = RenderComponent<Table<TableTestHelper.TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, TableTestHelper.Headings)
            .Add(p => p.Items, TableTestHelper.Items)
            .Add(p => p.RowsPerPageOpts, TableTestHelper.RowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(rowTemplate, item.ID, item.DisplayName, item.CreatedDate))
            .Add(p => p.ShowPaginationRow, true)
            );

        var itemSpans = cut.FindAll(".item-row");

        //Assert
        Assert.Collection(itemSpans,
            item => Assert.Equal(expectedRow1, item.InnerHtml),
            item => Assert.Equal(expectedRow2, item.InnerHtml),
            item => Assert.Equal(expectedRow3, item.InnerHtml));
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
            .Add(p => p.RowTemplate, item => string.Format(TableTestHelper.RowTemplate, item.ID, item.DisplayName, item.CreatedDate))
            .Add(p => p.ShowPaginationRow, true)
            );

        var options = cut.FindAll(".option");
        var actualCount = options.Count;
        var actualValues = options.Select(_ => int.Parse(_.TextContent));

        //Assert
        Assert.Equal(expectedCount, actualCount);
        Assert.Equal(expectedRowsPerPageValues, actualValues);
    }

    [Theory(DisplayName = "ShowPaginationRow Paremter Test")]
    [InlineData(true)]
    [InlineData(false)]
    public void Table_ShowPaginationRow_RendersCorrectly(bool showPaginationRow)
    {
        //Act
        var cut = RenderComponent<Table<TableTestHelper.TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, TableTestHelper.Headings)
            .Add(p => p.Items, TableTestHelper.Items)
            .Add(p => p.RowsPerPageOpts, TableTestHelper.RowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(TableTestHelper.RowTemplate, item.ID, item.DisplayName, item.CreatedDate))
            .Add(p => p.ShowPaginationRow, showPaginationRow)
            );

        var paginationRowExists = cut.HasComponent<TablePaginationRow<TableTestHelper.TableTestObject>>();

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
        var cut = RenderComponent<Table<TableTestHelper.TableTestObject>>(parameters => parameters
           .Add(p => p.Headings, TableTestHelper.Headings)
           .Add(p => p.Items, TableTestHelper.BigItemsList)
           .Add(p => p.RowsPerPageOpts, TableTestHelper.RowsPerPageOpts)
           .Add(p => p.RowTemplate, item => string.Format(TableTestHelper.RowTemplate, item.ID, item.DisplayName, item.CreatedDate))
           .Add(p => p.ShowPaginationRow, true)
           );

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