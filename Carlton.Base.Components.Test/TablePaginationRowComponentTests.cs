namespace Carlton.Base.Components.Test;

public class TablePaginationRowComponentTests : TestContext
{
    private static readonly string TablePaginationRowMarkup =
      @"<div class=""pagination-row-item"" b-hp2nj7a13h>
  <div class=""rows-per-page"" b-hp2nj7a13h>
    <span class=""rows-per-page-label"" b-hp2nj7a13h>Rows Per Page</span>
    <div class=""select"" b-b4t7b28hd7>
      <input readonly placeholder="" "" value=""5"" b-b4t7b28hd7 />
      <div class=""label"" b-b4t7b28hd7></div>
      <div class=""options"" b-b4t7b28hd7>
        <div class=""option"" blazor:onclick=""5"" b-b4t7b28hd7>5</div>
        <div class=""option"" blazor:onclick=""6"" b-b4t7b28hd7>10</div>
        <div class=""option"" blazor:onclick=""7"" b-b4t7b28hd7>15</div>
      </div>
    </div>
  </div>
  <div class=""page-number"" b-hp2nj7a13h>
    <span class=""pagination-label"" b-hp2nj7a13h>1-3 of 3</span>
  </div>
  <div class=""page-chevrons"" b-hp2nj7a13h>
    <span class=""mdi mdi-18px mdi-page-first disabled"" blazor:onclick=""1"" b-hp2nj7a13h></span>
    <span class=""mdi mdi-18px mdi-chevron-left disabled"" blazor:onclick=""2"" b-hp2nj7a13h></span>
    <span class=""mdi mdi-18px mdi-chevron-right disabled"" blazor:onclick=""3"" b-hp2nj7a13h></span>
    <span class=""mdi mdi-18px mdi-page-last disabled"" blazor:onclick=""4"" b-hp2nj7a13h></span>
  </div>
</div>";

    public record TableTestObject(int ID, string DisplayName, DateTime CreatedDate);

    private static readonly IList<TableTestObject> Items = new List<TableTestObject>
    {
        new TableTestObject(1, "Item A", new DateTime(2023, 10, 9)),
        new TableTestObject(2, "Item B", new DateTime(2022, 2, 3)),
        new TableTestObject(3, "Item C", new DateTime(2021, 5, 19))
    };

    private static readonly IList<TableTestObject> BigItemsList = new List<TableTestObject>
    {
        new TableTestObject(1, "Item A", new DateTime(2023, 10, 9)),
        new TableTestObject(2, "Item B", new DateTime(2022, 2, 3)),
        new TableTestObject(3, "Item C", new DateTime(2021, 5, 19)),
        new TableTestObject(4, "Item 1", new DateTime(2023, 10, 9)),
        new TableTestObject(5, "Item 2", new DateTime(2022, 2, 3)),
        new TableTestObject(6, "Item 3", new DateTime(2021, 5, 19)),
        new TableTestObject(7, "Additional Item A", new DateTime(2023, 10, 9)),
        new TableTestObject(8, "Additional Item B", new DateTime(2022, 2, 3)),
        new TableTestObject(9, "Additional Item C", new DateTime(2021, 5, 19)),
        new TableTestObject(10, "Additional Item 1", new DateTime(2023, 10, 9)),
        new TableTestObject(11, "Additional Item 2", new DateTime(2022, 2, 3)),
        new TableTestObject(12, "Additional Item 3", new DateTime(2021, 5, 19)),
        new TableTestObject(13, "Some Item", new DateTime(2023, 10, 9)),
        new TableTestObject(14, "Another Item", new DateTime(2022, 2, 3)),
        new TableTestObject(15, "The Final Item", new DateTime(2021, 5, 19))
    };

    private static readonly IList<int> RowsPerPageOpts = new List<int> { 5, 10, 15 };

    public static IEnumerable<object[]> GetRowsPerPageOptions()
    {
        yield return new object[]
        {
           new List<int> { 1, 3, 5 }
        };
        yield return new object[]
        {
            new List<int> { 2 }
        };
        yield return new object[]
        {
            new List<int> { 5, 10, 15, 20, 25 }
        };
    }

    [Fact]
    public void TablePaginationRow_Markup_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<TablePaginationRow<TableTestObject>>(paramaters => paramaters
            .Add(p => p.CurrentItems, Items)
            .Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
            );

        //Assert
        cut.MarkupMatches(TablePaginationRowMarkup);
    }

    [Theory]
    [MemberData(nameof(GetRowsPerPageOptions))]
    public void TablePaginationRow_RowsPerPageOptsParam_DefaultsToFirstOption_RendersCorrectly(IEnumerable<int> opts)
    {
        //Arrange
        var expectedValue = opts.First();

        //Act
        var cut = RenderComponent<TablePaginationRow<TableTestObject>>(paramaters => paramaters
            .Add(p => p.CurrentItems, Items)
            .Add(p => p.RowsPerPageOpts, opts)
            );

        var select = cut.FindComponent<Select>();
        var actualValue = select.Instance.SelectedValue;

        //Assert
        Assert.Equal(expectedValue, actualValue);
    }

    [Theory]
    [MemberData(nameof(GetRowsPerPageOptions))]
    public void TablePaginationRow_RowsPerPageOptsParam_Count_RendersCorrectly(IEnumerable<int> opts)
    {
        //Arrange
        var expectedCount = opts.Count();

        //Act
        var cut = RenderComponent<TablePaginationRow<TableTestObject>>(paramaters => paramaters
            .Add(p => p.CurrentItems, Items)
            .Add(p => p.RowsPerPageOpts, opts)
            );

        var options = cut.FindAll(".option");
        var actualCount = options.Count;

        //Assert
        Assert.Equal(expectedCount, actualCount);
    }

    [Fact]
    public void TablePaginationRow_RowsPerPageOptsParam_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<TablePaginationRow<TableTestObject>>(paramaters => paramaters
            .Add(p => p.CurrentItems, Items)
            .Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
            );

        var options = cut.FindAll(".option");

        //Assert
        Assert.Collection(options,
            item => Assert.Equal("5", item.TextContent),
            item => Assert.Equal("10", item.TextContent),
            item => Assert.Equal("15", item.TextContent));
    }


    [Theory]
    [InlineData(1, 0, "1-5 of 15")]
    [InlineData(1, 1, "1-10 of 15")]
    [InlineData(1, 2, "1-15 of 15")]
    [InlineData(2, 0, "6-10 of 15")]
    [InlineData(2, 1, "11-15 of 15")]
    [InlineData(2, 2, "1-15 of 15")]
    [InlineData(3, 0, "11-15 of 15")]
    [InlineData(3, 1, "11-15 of 15")]
    [InlineData(3, 2, "1-15 of 15")]
    public void TablePaginationRow_PaginationLabel_RendersCorrectly(int currentPage, int optIndex, string expectedText)
    {
        //Arrange
        var cut = RenderComponent<TablePaginationRow<TableTestObject>>(paramaters => paramaters
            .Add(p => p.CurrentItems, BigItemsList)
            .Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
            );

        var options = cut.FindAll(".option");
        var optToClick = options.ElementAt(optIndex);
        var paginationLabel = cut.Find(".pagination-label");
        var rightChevron = cut.Find(".mdi-chevron-right");

        //Act
        optToClick.Click();

        for(var i = 0; i < currentPage - 1; i++)
            rightChevron.Click();

        //Assert
        Assert.Equal(expectedText, paginationLabel.TextContent);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public void TablePaginationRow_PaginationLabel_ItemCountLessThanItemsPerPage_RendersCorrectly(int optIndex)
    {
        //Arrange
        var expectedText = "1-3 of 3";
        var cut = RenderComponent<TablePaginationRow<TableTestObject>>(paramaters => paramaters
            .Add(p => p.CurrentItems, Items)
            .Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
            );

        var options = cut.FindAll(".option");
        var optToClick = options.ElementAt(optIndex);
        var paginationLabel = cut.Find(".pagination-label");

        //Act
        optToClick.Click();

        //Assert
        Assert.Equal(expectedText, paginationLabel.TextContent);
    }

    [Fact]
    public void TablePaginationRow_ItemCountLessThanItemsPerPage_RightChevronsDisabled_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<TablePaginationRow<TableTestObject>>(paramaters => paramaters
            .Add(p => p.CurrentItems, Items)
            .Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
            );

        var chevronRightDisabled = cut.Find(".mdi-chevron-right").ClassList.Contains("disabled");
        var chevronLastPageDisabled = cut.Find(".mdi-page-last").ClassList.Contains("disabled");

        //Assert
        Assert.True(chevronRightDisabled);
        Assert.True(chevronLastPageDisabled);
    }

    [Fact]
    public void TablePaginationRow_ItemCountGreaterThanItemsPerPage_RightChevronsEnabled_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<TablePaginationRow<TableTestObject>>(paramaters => paramaters
            .Add(p => p.CurrentItems, BigItemsList)
            .Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
            );

        var chevronRightDisabled = cut.Find(".mdi-chevron-right").ClassList.Contains("disabled");
        var chevronLastPageDisabled = cut.Find(".mdi-page-last").ClassList.Contains("disabled");

        //Assert
        Assert.False(chevronRightDisabled);
        Assert.False(chevronLastPageDisabled);
    }

    [Fact]
    public void TablePaginationRow_PageOne_LeftChevronsDisabled_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<TablePaginationRow<TableTestObject>>(paramaters => paramaters
            .Add(p => p.CurrentItems, Items)
            .Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
            );

        var chevronLeftDisabled = cut.Find(".mdi-chevron-left").ClassList.Contains("disabled");
        var chevronFirstPageDisabled = cut.Find(".mdi-page-first").ClassList.Contains("disabled");

        //Assert
        Assert.True(chevronLeftDisabled);
        Assert.True(chevronFirstPageDisabled);
    }

    [Fact]
    public void TablePaginationRow_PageGreaterThanOne_LeftChevronsEnabled_RendersCorrectly()
    {
        //Arrange
        var cut = RenderComponent<TablePaginationRow<TableTestObject>>(paramaters => paramaters
            .Add(p => p.CurrentItems, BigItemsList)
            .Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
            );

        var chevronRight = cut.Find(".mdi-chevron-right");

        //Act
        chevronRight.Click();
        var chevronLeftDisabled = cut.Find(".mdi-chevron-left").ClassList.Contains("disabled");
        var chevronFirstPageDisabled = cut.Find(".mdi-page-first").ClassList.Contains("disabled");

        //Assert
        Assert.False(chevronLeftDisabled);
        Assert.False(chevronFirstPageDisabled);
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
    public void TablePaginationRow_ItemsChangedCallback_FiresEvent(int currentPage, int optIndex, int expectedSkip, int expectedTake)
    {
        //Arrange
        var eventFired = false;
        var expectedItems = BigItemsList.Skip(expectedSkip).Take(expectedTake);
        IEnumerable<TableTestObject> actualItems = new List<TableTestObject>();
        var cut = RenderComponent<TablePaginationRow<TableTestObject>>(paramaters => paramaters
            .Add(p => p.CurrentItems, BigItemsList)
            .Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
            .Add(p => p.ItemsChangedCallback, items => 
            { 
                eventFired = true;
                actualItems = items;
            })
            );

        var options = cut.FindAll(".option");
        var optToClick = options.ElementAt(optIndex);
        var rightChevron = cut.Find(".mdi-chevron-right");

        //Act
        optToClick.Click();

        for(var i = 0; i < currentPage - 1; i++)
            rightChevron.Click();

        //Assert
        Assert.True(eventFired);
        Assert.Equal(expectedItems, actualItems);
    }
}

