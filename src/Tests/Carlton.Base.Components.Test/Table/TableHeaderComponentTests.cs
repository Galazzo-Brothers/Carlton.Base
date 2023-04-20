namespace Carlton.Base.Components.Test;

public class TableHeaderComponentTests : TestContext
{
    [Fact]
    public void TableHeader_Markup_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<TableHeader<TableTestHelper.TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, TableTestHelper.Headings)
            .Add(p => p.OrderColumn, string.Empty)
            .Add(p => p.OrderAscending, true)
            );

        //Assert
        cut.MarkupMatches(TableTestHelper.TableHeaderMarkup);
    }

    [Theory]
    [MemberData(nameof(TableTestHelper.GetHeadings), MemberType = typeof(TableTestHelper))]
    public void TableHeader_HeadingsParam_RendersCorrectly(IEnumerable<TableHeadingItem> headings)
    {
        //Arrange
        var expectedCount = headings.Count();
        var expectedHeadings = headings.Select(_ => _.DisplayName);

        //Act
        var cut = RenderComponent<TableHeader<TableTestHelper.TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, headings)
            .Add(p => p.OrderColumn, string.Empty)
            .Add(p => p.OrderAscending, true)
            );

        var headerRowItems = cut.FindAll(".header-row-item");
        var actualCount = headerRowItems.Count;
        var actualHeadings = cut.FindAll(".heading-text").Select(_ => _.TextContent);

        //Assert
        Assert.Equal(expectedCount, actualCount);
        Assert.Equal(expectedHeadings, actualHeadings);
    }

    [Theory]
    [InlineData("ID", 0, true)]
    [InlineData("ID", 0, false)]
    [InlineData("DisplayName", 1, true)]
    [InlineData("DisplayName", 1, false)]
    [InlineData("CreatedDate", 2, true)]
    [InlineData("CreatedDate", 2, false)]
    public void TableHeader_OrderColumnParam_And_OrderDirectionParam_RendersCorrectly(string columnName, int columnIndex, bool orderAscending)
    {
        //Act
        var cut = RenderComponent<TableHeader<TableTestHelper.TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, TableTestHelper.Headings)
            .Add(p => p.OrderColumn, columnName)
            .Add(p => p.OrderAscending, orderAscending)
            );

        var headerRowItems = cut.FindAll(".header-row-item");
        var selectedItem = headerRowItems.ElementAt(columnIndex);
        var hasSelectedClass = selectedItem.ClassList.Contains("selected");
        var hasAscendingClass = selectedItem.ClassList.Contains("ascending");
        var hasDescendingClass = selectedItem.ClassList.Contains("descending");

        //Assert
        Assert.True(hasSelectedClass);
        Assert.Equal(orderAscending, hasAscendingClass);
        Assert.Equal(orderAscending, !hasDescendingClass);
    }

    [Theory]
    [InlineData("Wrong")]
    [InlineData("Also Wrong")]
    [InlineData("Still Wrong")]
    public void TableHeader_InvalidOrderColumnParam_RendersCorrectly(string columnName)
    {
        //Arrange
        var cut = RenderComponent<TableHeader<TableTestHelper.TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, TableTestHelper.Headings)
            .Add(p => p.OrderColumn, columnName)
            .Add(p => p.OrderAscending, true)
            );

        //Act
        Assert.Throws<ElementNotFoundException>(() => cut.Find(".selected"));
    }

    [Theory]
    [InlineData(0, "ID")]
    [InlineData(1, "DisplayName")]
    [InlineData(2, "CreatedDate")]
    public void TableHeader_ClickHeadersOnce_EventFires(int columnIndex, string expectedColumnName)
    {
        //Arrange
        var eventFired = false;
        var actualOrderAscending = false;
        var actualOrderColumn = string.Empty;
        var cut = RenderComponent<TableHeader<TableTestHelper.TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, TableTestHelper.Headings)
            .Add(p => p.OrderColumn, string.Empty)
            .Add(p => p.OrderAscending, true)
            .Add(p => p.OnItemsOrdered, args => { eventFired = true; actualOrderAscending = args.OrderAscending; actualOrderColumn = args.OrderColumn; })
            );

        var headerRowItems = cut.FindAll(".header-row-item");

        //Act
        headerRowItems.ElementAt(columnIndex).Click();

        //Assert
        Assert.True(eventFired);
        Assert.True(actualOrderAscending);
        Assert.Equal(expectedColumnName, actualOrderColumn);
    }

    [Theory]
    [InlineData(0, "ID")]
    [InlineData(1, "DisplayName")]
    [InlineData(2, "CreatedDate")]
    public void TableHeader_ClickHeadersOnceTwice_EventFires(int columnIndex, string expectedColumnName)
    {
        //Arrange
        var eventFired = false;
        var actualOrderAscending = true;
        var actualOrderColumn = string.Empty;
        var cut = RenderComponent<TableHeader<TableTestHelper.TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, TableTestHelper.Headings)
            .Add(p => p.OrderColumn, string.Empty)
            .Add(p => p.OrderAscending, true)
            .Add(p => p.OnItemsOrdered, args => { eventFired = true; actualOrderAscending = args.OrderAscending; actualOrderColumn = args.OrderColumn; })
            );

        //Act
        var itemToClick = cut.FindAll(".header-row-item").ElementAt(columnIndex);
        itemToClick.Click();
        itemToClick = cut.FindAll(".header-row-item").ElementAt(columnIndex);
        itemToClick.Click();

        //Assert
        Assert.True(eventFired);
        Assert.False(actualOrderAscending);
        Assert.Equal(expectedColumnName, actualOrderColumn);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public void TableHeader_OnClick_SelectedClass_RendersCorrectly(int selectedIndex)
    {
        //Arrange
        var cut = RenderComponent<TableHeader<TableTestHelper.TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, TableTestHelper.Headings)
            .Add(p => p.OrderColumn, string.Empty)
            .Add(p => p.OrderAscending, true)
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
    public void TableHeader_OnClick_AscendingClass_RendersCorrectly(int selectedIndex)
    {
        //Arrange
        var cut = RenderComponent<TableHeader<TableTestHelper.TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, TableTestHelper.Headings)
            .Add(p => p.OrderColumn, string.Empty)
            .Add(p => p.OrderAscending, true)
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
    public void TableHeader_OnClick_DescendingClass_RendersCorrectly(int selectedIndex)
    {
        //Arrange
        var cut = RenderComponent<TableHeader<TableTestHelper.TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, TableTestHelper.Headings)
            .Add(p => p.OrderColumn, string.Empty)
            .Add(p => p.OrderAscending, true)
            );


        //Act
        cut.FindAll(".header-row-item").ElementAt(selectedIndex).Click();
        cut.FindAll(".header-row-item").ElementAt(selectedIndex).Click();
        var selectedItem = cut.FindAll(".header-row-item").ElementAt(selectedIndex);
        var containsDescendingClass = selectedItem.ClassList.Contains("descending");


        //Assert
        Assert.True(containsDescendingClass);
    }
}
