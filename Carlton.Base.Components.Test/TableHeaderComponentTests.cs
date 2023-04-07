namespace Carlton.Base.Components.Test;

public class TableHeaderComponentTests : TestContext
{
    private static readonly string TableHeaderMarkup =
       @"<div class=""header-row-item row-item ascending"" blazor:onclick=""1"" b-ydzvi9l03d>
  <span class=""heading-text"" b-ydzvi9l03d>ID</span>
  <div class=""sort-arrows"" b-ydzvi9l03d>
    <span class=""arrow-ascending mdi mdi-arrow-up"" b-ydzvi9l03d></span>
    <span class=""arrow-descending mdi mdi-arrow-down"" b-ydzvi9l03d></span>
  </div>
</div>
<div class=""header-row-item row-item ascending"" blazor:onclick=""2"" b-ydzvi9l03d>
  <span class=""heading-text"" b-ydzvi9l03d>DisplayName</span>
  <div class=""sort-arrows"" b-ydzvi9l03d>
    <span class=""arrow-ascending mdi mdi-arrow-up"" b-ydzvi9l03d></span>
    <span class=""arrow-descending mdi mdi-arrow-down"" b-ydzvi9l03d></span>
  </div>
</div>
<div class=""header-row-item row-item ascending"" blazor:onclick=""3"" b-ydzvi9l03d>
  <span class=""heading-text"" b-ydzvi9l03d>CreatedDate</span>
  <div class=""sort-arrows"" b-ydzvi9l03d>
    <span class=""arrow-ascending mdi mdi-arrow-up"" b-ydzvi9l03d></span>
    <span class=""arrow-descending mdi mdi-arrow-down"" b-ydzvi9l03d></span>
  </div>
</div>";

    public record TableTestObject(int ID, string DisplayName, DateTime CreatedDate);

    private static readonly IEnumerable<TableHeadingItem> Headings = new List<TableHeadingItem>
    {
            new TableHeadingItem(nameof(TableTestObject.ID)),
            new TableHeadingItem(nameof(TableTestObject.DisplayName)),
            new TableHeadingItem(nameof(TableTestObject.CreatedDate))
    };

    public static IEnumerable<object[]> GetHeadings()
    {
        yield return new object[]
        {
            new List<TableHeadingItem>
            {
                new TableHeadingItem(nameof(TableTestObject.ID))
            }
        };
        yield return new object[]
        {
                new List<TableHeadingItem>
                {
                    new TableHeadingItem(nameof(TableTestObject.ID)),
                    new TableHeadingItem(nameof(TableTestObject.DisplayName))
                }
        };
        yield return new object[]
        {
                 new List<TableHeadingItem>
                 {
                    new TableHeadingItem(nameof(TableTestObject.ID)),
                    new TableHeadingItem(nameof(TableTestObject.DisplayName)),
                    new TableHeadingItem(nameof(TableTestObject.CreatedDate))
                 }
        };
    }


    [Fact]
    public void TableHeader_Markup_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<TableHeader<TableTestObject>>(paramaters => paramaters
            .Add(p => p.Headings, Headings)
            .Add(p => p.OrderColumn, string.Empty)
            .Add(p => p.OrderAscending, true)
            );

        //Assert
        cut.MarkupMatches(TableHeaderMarkup);
    }

    [Theory]
    [MemberData(nameof(GetHeadings))]
    public void TableHeader_HeadingsParam_Count_RendersCorrectly(IEnumerable<TableHeadingItem> headings)
    {
        //Arrange
        var expectedCount = headings.Count();

        //Act
        var cut = RenderComponent<TableHeader<TableTestObject>>(paramaters => paramaters
            .Add(p => p.Headings, headings)
            .Add(p => p.OrderColumn, string.Empty)
            .Add(p => p.OrderAscending, true)
            );

        var headerRowItems = cut.FindAll(".header-row-item");
        var actualCount = headerRowItems.Count;

        //Assert
        Assert.Equal(expectedCount, actualCount);
    }

    [Fact]
    public void TableHeader_HeadingsParam_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<TableHeader<TableTestObject>>(paramaters => paramaters
            .Add(p => p.Headings, Headings)
            .Add(p => p.OrderColumn, string.Empty)
            .Add(p => p.OrderAscending, true)
            );

        var headingText = cut.FindAll(".heading-text").Select(_ => _.TextContent);

        //Assert
        Assert.Collection(headingText,
            _ => Assert.Equal("ID", _),
            _ => Assert.Equal("DisplayName", _),
            _ => Assert.Equal("CreatedDate", _));
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
        var cut = RenderComponent<TableHeader<TableTestObject>>(paramaters => paramaters
            .Add(p => p.Headings, Headings)
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
        var cut = RenderComponent<TableHeader<TableTestObject>>(paramaters => paramaters
            .Add(p => p.Headings, Headings)
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
    public void TableHeader_OnClick_SelectedClass_RendersCorreectly(int selectedIndex)
    {
        //Arrange
        var cut = RenderComponent<TableHeader<TableTestObject>>(paramaters => paramaters
            .Add(p => p.Headings, Headings)
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
    public void TableHeader_OnClick_AscendingClass_RendersCorreectly(int selectedIndex)
    {
        //Arrange
        var cut = RenderComponent<TableHeader<TableTestObject>>(paramaters => paramaters
            .Add(p => p.Headings, Headings)
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
    public void TableHeader_OnClick_DescendingClass_RendersCorreectly(int selectedIndex)
    {
        //Arrange
        var cut = RenderComponent<TableHeader<TableTestObject>>(paramaters => paramaters
            .Add(p => p.Headings, Headings)
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

