namespace Carlton.Base.Components.Test;

public class TableComponentTests : TestContext
{
    [Fact]
    public void Table_Markup_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<Table<TableTestHelper.TableTestObject>>(paramaters => paramaters
            .Add(p => p.Headings, TableTestHelper.Headings)
            .Add(p => p.Items, TableTestHelper.Items)
            .Add(p => p.RowsPerPageOpts, TableTestHelper.RowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(TableTestHelper.RowTemplate, item.ID, item.DisplayName, item.CreatedDate))
            .Add(p => p.ShowPaginationRow, true)
            );

        //Assert
        cut.MarkupMatches(TableTestHelper.TableMarkup);
    }

    [Theory]
    [MemberData(nameof(TableTestHelper.GetHeadings), MemberType = typeof(TableTestHelper))]
    public void Table_HeadingsParam_RendersCorrectly(IReadOnlyCollection<TableHeadingItem> headings)
    {
        //Arrange
        var expectedCount = headings.Count;
        var expectedHeaders = headings.Select(_ => _.DisplayName);

        //Act
        var cut = RenderComponent<Table<TableTestHelper.TableTestObject>>(paramaters => paramaters
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

    [Fact]
    public void Table_OnClickOnce_FirstColum_FiltersItemsAsc()
    {
        //Arrange
        var cut = RenderComponent<Table<TableTestHelper.TableTestObject>>(paramaters => paramaters
            .Add(p => p.Headings, TableTestHelper.Headings)
            .Add(p => p.Items, TableTestHelper.Items)
            .Add(p => p.RowsPerPageOpts, TableTestHelper.RowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(TableTestHelper.RowTemplate, item.ID, item.DisplayName, item.CreatedDate))
            .Add(p => p.ShowPaginationRow, true)
            );

        var headerRowItems = cut.FindAll(".header-row-item");

        //Act
        headerRowItems.ElementAt(0).Click();

        //Assert
        Assert.Collection(cut.Instance.Items,
            _ => Assert.Equal(1, _.ID),
            _ => Assert.Equal(2, _.ID),
            _ => Assert.Equal(3, _.ID));
    }

    [Fact]
    public void Table_OnClickTwice_FirstColum_FiltersItemsDesc()
    {
        //Arrange
        var cut = RenderComponent<Table<TableTestHelper.TableTestObject>>(paramaters => paramaters
            .Add(p => p.Headings, TableTestHelper.Headings)
            .Add(p => p.Items, TableTestHelper.Items)
            .Add(p => p.RowsPerPageOpts, TableTestHelper.RowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(TableTestHelper.RowTemplate, item.ID, item.DisplayName, item.CreatedDate))
            .Add(p => p.ShowPaginationRow, true)
            );

        //Act
        var itemToClick = cut.FindAll(".header-row-item").ElementAt(0);
        itemToClick.Click();
        itemToClick = cut.FindAll(".header-row-item").ElementAt(0);
        itemToClick.Click();

        //Assert
        Assert.Collection(cut.Instance.Items,
            _ => Assert.Equal(3, _.ID),
            _ => Assert.Equal(2, _.ID),
            _ => Assert.Equal(1, _.ID));
    }

    [Fact]
    public void Table_OnClickOnce_SecondColumColum_FiltersItemsAsc()
    {
        //Arrange
        var cut = RenderComponent<Table<TableTestHelper.TableTestObject>>(paramaters => paramaters
            .Add(p => p.Headings, TableTestHelper.Headings)
            .Add(p => p.Items, TableTestHelper.Items)
            .Add(p => p.RowsPerPageOpts, TableTestHelper.RowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(TableTestHelper.RowTemplate, item.ID, item.DisplayName, item.CreatedDate))
            .Add(p => p.ShowPaginationRow, true)
            );

        var headerRowItems = cut.FindAll(".header-row-item");

        //Act
        var itemToClick = headerRowItems.ElementAt(1);
        itemToClick.Click();

        //Assert
        Assert.Collection(cut.Instance.Items,
            _ => Assert.Equal("Item A", _.DisplayName),
            _ => Assert.Equal("Item B", _.DisplayName),
            _ => Assert.Equal("Item C", _.DisplayName));
    }

    [Fact]
    public void Table_OnClickTwice_SecondColumColum_FiltersItemsDesc()
    {
        //Arrange
        var cut = RenderComponent<Table<TableTestHelper.TableTestObject>>(paramaters => paramaters
            .Add(p => p.Headings, TableTestHelper.Headings)
            .Add(p => p.Items, TableTestHelper.Items)
            .Add(p => p.RowsPerPageOpts, TableTestHelper.RowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(TableTestHelper.RowTemplate, item.ID, item.DisplayName, item.CreatedDate))
            .Add(p => p.ShowPaginationRow, true)
            );

        //Act
        var itemToClick = cut.FindAll(".header-row-item").ElementAt(1);
        itemToClick.Click();
        itemToClick = cut.FindAll(".header-row-item").ElementAt(1);
        itemToClick.Click();

        //Assert
        Assert.Collection(cut.Instance.Items,
            _ => Assert.Equal("Item C", _.DisplayName),
            _ => Assert.Equal("Item B", _.DisplayName),
            _ => Assert.Equal("Item A", _.DisplayName));
    }

    [Fact]
    public void Table_OnClickOnce_ThirdColumColum_FiltersItemsAsc()
    {
        //Arrange
        var cut = RenderComponent<Table<TableTestHelper.TableTestObject>>(paramaters => paramaters
            .Add(p => p.Headings, TableTestHelper.Headings)
            .Add(p => p.Items, TableTestHelper.Items)
            .Add(p => p.RowsPerPageOpts, TableTestHelper.RowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(TableTestHelper.RowTemplate, item.ID, item.DisplayName, item.CreatedDate))
            .Add(p => p.ShowPaginationRow, true)
            );

        var headerRowItems = cut.FindAll(".header-row-item");

        //Act
        var itemToClick = headerRowItems.ElementAt(2);
        itemToClick.Click();

        //Assert
        Assert.Collection(cut.Instance.Items,
            _ => Assert.Equal(new DateTime(2021, 5, 19), _.CreatedDate),
            _ => Assert.Equal(new DateTime(2022, 2, 3), _.CreatedDate),
            _ => Assert.Equal(new DateTime(2023, 10, 9), _.CreatedDate));
    }

    [Fact]
    public void Table_OnClickTwice_ThirdColumColum_FiltersItemsDesc()
    {
        //Arrange
        var cut = RenderComponent<Table<TableTestHelper.TableTestObject>>(paramaters => paramaters
            .Add(p => p.Headings, TableTestHelper.Headings)
            .Add(p => p.Items, TableTestHelper.Items)
            .Add(p => p.RowsPerPageOpts, TableTestHelper.RowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(TableTestHelper.RowTemplate, item.ID, item.DisplayName, item.CreatedDate))
            .Add(p => p.ShowPaginationRow, true)
            );


        //Act
        var itemToClick = cut.FindAll(".header-row-item").ElementAt(2);
        itemToClick.Click();
        itemToClick = cut.FindAll(".header-row-item").ElementAt(2);
        itemToClick.Click();

        //Assert
        Assert.Collection(cut.Instance.Items,
            _ => Assert.Equal(0, DateTime.Compare(new DateTime(2023, 10, 9).Date, _.CreatedDate.Date)),
            _ => Assert.Equal(0, DateTime.Compare(new DateTime(2022, 2, 3).Date, _.CreatedDate.Date)),
            _ => Assert.Equal(0, DateTime.Compare(new DateTime(2021, 5, 19).Date, _.CreatedDate.Date)));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public void Table_HeaderItemOnClick_SelectedClass_RendersCorreectly(int selectedIndex)
    {
        //Arrange
        var cut = RenderComponent<Table<TableTestHelper.TableTestObject>>(paramaters => paramaters
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

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public void Table_HeaderItemOnClick_AscendingClass_RendersCorreectly(int selectedIndex)
    {
        //Arrange
        var cut = RenderComponent<Table<TableTestHelper.TableTestObject>>(paramaters => paramaters
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

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public void Table_HeaderItemOnClick_DescendingClass_RendersCorreectly(int selectedIndex)
    {
        //Arrange
        var cut = RenderComponent<Table<TableTestHelper.TableTestObject>>(paramaters => paramaters
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

    [Theory]
    [MemberData(nameof(TableTestHelper.GetItems), MemberType = typeof(TableTestHelper))]
    public void Table_ItemsParam_Count_RendersCorrectly(IReadOnlyCollection<TableTestHelper.TableTestObject> items)
    {
        //Arrange
        var expectedCount = items.Count;

        //Act
        var cut = RenderComponent<Table<TableTestHelper.TableTestObject>>(paramaters => paramaters
            .Add(p => p.Headings, TableTestHelper.Headings)
            .Add(p => p.Items, items)
            .Add(p => p.RowsPerPageOpts, TableTestHelper.RowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(TableTestHelper.RowTemplate, item.ID, item.DisplayName, item.CreatedDate))
            .Add(p => p.ShowPaginationRow, true)
            );

        var tableRows = cut.FindAll(".table-row");
        var actualCount = tableRows.Count - 2; //Exclude the header and action rows

        //Assert
        Assert.Equal(expectedCount, actualCount);
    }

    [Fact]
    public void Table_ItemsParam_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<Table<TableTestHelper.TableTestObject>>(paramaters => paramaters
            .Add(p => p.Headings, TableTestHelper.Headings)
            .Add(p => p.Items, TableTestHelper.Items)
            .Add(p => p.RowsPerPageOpts, TableTestHelper.RowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(TableTestHelper.RowTemplate, item.ID, item.DisplayName, item.CreatedDate))
            .Add(p => p.ShowPaginationRow, true)
            );

        var itemSpans = cut.FindAll(".item-row span");

        //Assert
        Assert.Collection(itemSpans,
            item => Assert.Equal("ID:1", item.TextContent),
            item => Assert.Equal("Display Name:Item A", item.TextContent),
            item => Assert.Equal("Date:10/9/2023 12:00:00 AM", item.TextContent),
            item => Assert.Equal("ID:2", item.TextContent),
            item => Assert.Equal("Display Name:Item B", item.TextContent),
            item => Assert.Equal("Date:2/3/2022 12:00:00 AM", item.TextContent),
            item => Assert.Equal("ID:3", item.TextContent),
            item => Assert.Equal("Display Name:Item C", item.TextContent),
            item => Assert.Equal("Date:5/19/2021 12:00:00 AM", item.TextContent));
    }

    [Theory]
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
        var cut = RenderComponent<Table<TableTestHelper.TableTestObject>>(paramaters => paramaters
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

    [Theory]
    [MemberData(nameof(TableTestHelper.GetRowsPerPageOptions), MemberType = typeof(TableTestHelper))]
    public void Table_RowsPerPageOptsParam_Count_RendersCorrectly(IEnumerable<int> rowsPerPageOpts)
    {
        //Arrange
        var expectedCount = rowsPerPageOpts.Count();

        //Act
        var cut = RenderComponent<Table<TableTestHelper.TableTestObject>>(paramaters => paramaters
            .Add(p => p.Headings, TableTestHelper.Headings)
            .Add(p => p.Items, TableTestHelper.Items)
            .Add(p => p.RowsPerPageOpts, rowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(TableTestHelper.RowTemplate, item.ID, item.DisplayName, item.CreatedDate))
            .Add(p => p.ShowPaginationRow, true)
            );

        var options = cut.FindAll(".option");
        var actualCount = options.Count;

        //Assert
        Assert.Equal(expectedCount, actualCount);
    }

    [Fact]
    public void Table_RowsPerPageOptsParam_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<Table<TableTestHelper.TableTestObject>>(paramaters => paramaters
            .Add(p => p.Headings, TableTestHelper.Headings)
            .Add(p => p.Items, TableTestHelper.Items)
            .Add(p => p.RowsPerPageOpts, TableTestHelper.RowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(TableTestHelper.RowTemplate, item.ID, item.DisplayName, item.CreatedDate))
            .Add(p => p.ShowPaginationRow, true)
            );

        var options = cut.FindAll(".option");

        //Assert
        Assert.Collection(options,
            item => Assert.Equal("5", item.TextContent),
            item => Assert.Equal("10", item.TextContent),
            item => Assert.Equal("15", item.TextContent));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Table_ShowPaginationRow_RendersCorrectly(bool showPaginationRow)
    {
        //Act
        var cut = RenderComponent<Table<TableTestHelper.TableTestObject>>(paramaters => paramaters
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

    [Theory]
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
        var cut = RenderComponent<Table<TableTestHelper.TableTestObject>>(paramaters => paramaters
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