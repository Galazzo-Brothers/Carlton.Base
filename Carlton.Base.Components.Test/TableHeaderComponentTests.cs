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

    private static readonly IList<TableTestObject> Items = new List<TableTestObject>
    {
        new TableTestObject(1, "Item A", new DateTime(2023, 10, 9)),
        new TableTestObject(2, "Item B", new DateTime(2022, 2, 3)),
        new TableTestObject(3, "Item C", new DateTime(2021, 5, 19))
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
            .Add(p => p.Items, Items)
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
            .Add(p => p.Items, Items)
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
            .Add(p => p.Items, Items)
            );

        var headingText = cut.FindAll(".heading-text").Select(_ => _.TextContent);

        //Assert
        Assert.Collection(headingText,
            _ => Assert.Equal("ID", _),
            _ => Assert.Equal("DisplayName", _),
            _ => Assert.Equal("CreatedDate", _));
    }

    [Fact]
    public void TableHeader_OnClickOnce_FirstColum_EventFires_And_OrdersItemsAsc()
    {
        //Arrange
        var eventFired = false;
        IList<TableTestObject> orderedItems = new List<TableTestObject>();
        var cut = RenderComponent<TableHeader<TableTestObject>>(paramaters => paramaters
            .Add(p => p.Headings, Headings)
            .Add(p => p.Items, Items)
            .Add(p => p.OnItemsOrdered, items => { eventFired = true; orderedItems = items; })
            );

        var headerRowItems = cut.FindAll(".header-row-item");

        //Act
        headerRowItems.ElementAt(0).Click();

        //Assert
        Assert.True(eventFired);
        Assert.Collection(orderedItems,
            _ => Assert.Equal(1, _.ID),
            _ => Assert.Equal(2, _.ID),
            _ => Assert.Equal(3, _.ID));
    }

    [Fact]
    public void TableHeader_OnClickTwice_FirstColum_EventFires_And_OrdersItemsDesc()
    {
        //Arrange
        var eventFired = false;
        IList<TableTestObject> orderedItems = new List<TableTestObject>();
        var cut = RenderComponent<TableHeader<TableTestObject>>(paramaters => paramaters
            .Add(p => p.Headings, Headings)
            .Add(p => p.Items, Items)
            .Add(p => p.OnItemsOrdered, items => { eventFired = true; orderedItems = items; })
            );

        //Act
        var itemToClick = cut.FindAll(".header-row-item").ElementAt(0);
        itemToClick.Click();
        itemToClick = cut.FindAll(".header-row-item").ElementAt(0);
        itemToClick.Click();

        //Assert
        Assert.True(eventFired);
        Assert.Collection(orderedItems,
            _ => Assert.Equal(3, _.ID),
            _ => Assert.Equal(2, _.ID),
            _ => Assert.Equal(1, _.ID));
    }

    [Fact]
    public void TableHeader_OnClickOnce_SecondColumColum_EventFires_And_OrdersItemsAsc()
    {
        //Arrange
        var eventFired = false;
        IList<TableTestObject> orderedItems = new List<TableTestObject>();
        var cut = RenderComponent<TableHeader<TableTestObject>>(paramaters => paramaters
            .Add(p => p.Headings, Headings)
            .Add(p => p.Items, Items)
            .Add(p => p.OnItemsOrdered, items => { eventFired = true; orderedItems = items; })
            );

        var headerRowItems = cut.FindAll(".header-row-item");

        //Act
        var itemToClick = headerRowItems.ElementAt(1);
        itemToClick.Click();

        //Assert
        Assert.True(eventFired);
        Assert.Collection(orderedItems,
            _ => Assert.Equal("Item A", _.DisplayName),
            _ => Assert.Equal("Item B", _.DisplayName),
            _ => Assert.Equal("Item C", _.DisplayName));
    }

    [Fact]
    public void TableHeader_OnClickTwice_SecondColumColum_EventFires_And_OrdersItemsDesc()
    {
        //Arrange
        var eventFired = false;
        IList<TableTestObject> orderedItems = new List<TableTestObject>();
        var cut = RenderComponent<TableHeader<TableTestObject>>(paramaters => paramaters
            .Add(p => p.Headings, Headings)
            .Add(p => p.Items, Items)
            .Add(p => p.OnItemsOrdered, items => { eventFired = true; orderedItems = items; })
            );

        //Act
        var itemToClick = cut.FindAll(".header-row-item").ElementAt(1);
        itemToClick.Click();
        itemToClick = cut.FindAll(".header-row-item").ElementAt(1);
        itemToClick.Click();

        //Assert
        Assert.True(eventFired);
        Assert.Collection(orderedItems,
            _ => Assert.Equal("Item C", _.DisplayName),
            _ => Assert.Equal("Item B", _.DisplayName),
            _ => Assert.Equal("Item A", _.DisplayName));
    }

    [Fact]
    public void TableHeader_OnClickOnce_ThirdColumColum_EventFires_And_OrdersItemsAsc()
    {
        //Arrange
        var eventFired = false;
        IList<TableTestObject> orderedItems = new List<TableTestObject>();
        var cut = RenderComponent<TableHeader<TableTestObject>>(paramaters => paramaters
            .Add(p => p.Headings, Headings)
            .Add(p => p.Items, Items)
            .Add(p => p.OnItemsOrdered, items => { eventFired = true; orderedItems = items; })
            );

        var headerRowItems = cut.FindAll(".header-row-item");

        //Act
        var itemToClick = headerRowItems.ElementAt(2);
        itemToClick.Click();

        //Assert
        Assert.True(eventFired);
        Assert.Collection(orderedItems,
            _ => Assert.Equal(new DateTime(2021, 5, 19), _.CreatedDate),
            _ => Assert.Equal(new DateTime(2022, 2, 3), _.CreatedDate),
            _ => Assert.Equal(new DateTime(2023, 10, 9), _.CreatedDate));
    }

    [Fact]
    public void TableHeader_OnClickTwice_ThirdColumColum_EventFires_And_OrdersItemsDesc()
    {
        //Arrange
        var eventFired = false;
        IList<TableTestObject> orderedItems = new List<TableTestObject>();
        var cut = RenderComponent<TableHeader<TableTestObject>>(paramaters => paramaters
            .Add(p => p.Headings, Headings)
            .Add(p => p.Items, Items)
            .Add(p => p.OnItemsOrdered, items => { eventFired = true; orderedItems = items; })
            );


        //Act
        var itemToClick = cut.FindAll(".header-row-item").ElementAt(2);
        itemToClick.Click();
        itemToClick = cut.FindAll(".header-row-item").ElementAt(2);
        itemToClick.Click();

        //Assert
        Assert.True(eventFired);
        Assert.Collection(orderedItems,
            _ => Assert.Equal(0, DateTime.Compare(new DateTime(2023, 10, 9).Date, _.CreatedDate.Date)),
            _ => Assert.Equal(0, DateTime.Compare(new DateTime(2022, 2, 3).Date, _.CreatedDate.Date)),
            _ => Assert.Equal(0, DateTime.Compare(new DateTime(2021, 5, 19).Date, _.CreatedDate.Date)));
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
            .Add(p => p.Items, Items)
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
            .Add(p => p.Items, Items)
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
            .Add(p => p.Items, Items)
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

