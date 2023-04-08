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

    private const int ItemsCount = 3;

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
            .Add(p => p.TotalItemCount, ItemsCount)
            .Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
            .Add(p => p.CurrentPage, 1)
            .Add(p => p.SelectedRowsPerPageCount, 5)
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
            .Add(p => p.TotalItemCount, ItemsCount)
            .Add(p => p.RowsPerPageOpts, opts)
            .Add(p => p.CurrentPage, 1)
            .Add(p => p.SelectedRowsPerPageCount, expectedValue)
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
        var initialRowsPerPageCount = opts.First();

        //Act
        var cut = RenderComponent<TablePaginationRow<TableTestObject>>(paramaters => paramaters
            .Add(p => p.TotalItemCount, ItemsCount)
            .Add(p => p.RowsPerPageOpts, opts)
            .Add(p => p.CurrentPage, 1)
            .Add(p => p.SelectedRowsPerPageCount, initialRowsPerPageCount)
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
            .Add(p => p.TotalItemCount, ItemsCount)
            .Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
            .Add(p => p.CurrentPage, 1)
            .Add(p => p.SelectedRowsPerPageCount, 5)
            );

        var options = cut.FindAll(".option");

        //Assert
        Assert.Collection(options,
            item => Assert.Equal("5", item.TextContent),
            item => Assert.Equal("10", item.TextContent),
            item => Assert.Equal("15", item.TextContent));
    }

    [Theory]
    [InlineData(1, 5, "1-5 of 15")]
    [InlineData(1, 10, "1-10 of 15")]
    [InlineData(1, 15, "1-15 of 15")]
    [InlineData(2, 5, "6-10 of 15")]
    [InlineData(2, 10, "11-15 of 15")]
    [InlineData(2, 15, "1-15 of 15")]
    [InlineData(3, 5, "11-15 of 15")]
    [InlineData(3, 10, "11-15 of 15")]
    [InlineData(3, 15, "1-15 of 15")]
    public void TablePaginationRow_PaginationLabel_RendersCorrectly(int currentPage, int currentRowsPerPage, string expectedText)
    {
        //Arrange
        var cut = RenderComponent<TablePaginationRow<TableTestObject>>(paramaters => paramaters
            .Add(p => p.TotalItemCount, 15)
            .Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
            .Add(p => p.CurrentPage, currentPage)
            .Add(p => p.SelectedRowsPerPageCount, currentRowsPerPage)
            );

        var options = cut.FindAll(".option");
        var optToClick = options.First(_ => int.Parse(_.TextContent) == currentRowsPerPage);
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
            .Add(p => p.TotalItemCount, ItemsCount)
            .Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
            .Add(p => p.CurrentPage, 1)
            .Add(p => p.SelectedRowsPerPageCount, 5)
            );

        var options = cut.FindAll(".option");
        var optToClick = options.ElementAt(optIndex);
        var paginationLabel = cut.Find(".pagination-label");

        //Act
        optToClick.Click();

        //Assert
        Assert.Equal(expectedText, paginationLabel.TextContent);
    }

    [Theory]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    public void TablePaginationRow_ItemCountLessThanItemsPerPage_RightChevronsDisabled_RendersCorrectly(int itemCount)
    {
        //Act
        var cut = RenderComponent<TablePaginationRow<TableTestObject>>(paramaters => paramaters
            .Add(p => p.TotalItemCount, itemCount)
            .Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
            .Add(p => p.CurrentPage, 1)
            .Add(p => p.SelectedRowsPerPageCount, 5)
            );

        var chevronRightDisabled = cut.Find(".mdi-chevron-right").ClassList.Contains("disabled");
        var chevronLastPageDisabled = cut.Find(".mdi-page-last").ClassList.Contains("disabled");

        //Assert
        Assert.True(chevronRightDisabled);
        Assert.True(chevronLastPageDisabled);
    }

    [Theory]
    [InlineData(10)]
    [InlineData(50)]
    [InlineData(100)]
    public void TablePaginationRow_ItemCountGreaterThanItemsPerPage_RightChevronsEnabled_RendersCorrectly(int itemCount)
    {
        //Act
        var cut = RenderComponent<TablePaginationRow<TableTestObject>>(paramaters => paramaters
            .Add(p => p.TotalItemCount, itemCount)
            .Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
            .Add(p => p.CurrentPage, 1)
            .Add(p => p.SelectedRowsPerPageCount, 5)
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
            .Add(p => p.TotalItemCount, ItemsCount)
            .Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
            .Add(p => p.CurrentPage, 1)
            .Add(p => p.SelectedRowsPerPageCount, 5)
            );

        var chevronLeftDisabled = cut.Find(".mdi-chevron-left").ClassList.Contains("disabled");
        var chevronFirstPageDisabled = cut.Find(".mdi-page-first").ClassList.Contains("disabled");

        //Assert
        Assert.True(chevronLeftDisabled);
        Assert.True(chevronFirstPageDisabled);
    }

    [Theory]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(5)]
    public void TablePaginationRow_PageGreaterThanOne_LeftChevronsEnabled_RendersCorrectly(int currentPage)
    {
        //Arrange
        var cut = RenderComponent<TablePaginationRow<TableTestObject>>(paramaters => paramaters
            .Add(p => p.TotalItemCount, ItemsCount)
            .Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
            .Add(p => p.CurrentPage, currentPage)
            .Add(p => p.SelectedRowsPerPageCount, 5)
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
    [InlineData(1, 5, 1)]
    [InlineData(1, 10, 1)]
    [InlineData(1, 15, 1)]
    [InlineData(2, 5, 2)]
    [InlineData(2, 10, 2)]
    [InlineData(2, 15, 1)]
    [InlineData(3, 5, 3)]
    [InlineData(3, 10, 2)]
    [InlineData(3, 15, 1)]
    public void TablePaginationRow_OnPaginationChangedCallback_FiresEvent(int selectedPage, int selectedRowsPerPage, int expectedPage)
    {
        //Arrange
        var eventFired = false;
        var actualCurrentPage = -1;
        var actualSelectedRowsPerPageCount = -1;
        var cut = RenderComponent<TablePaginationRow<TableTestObject>>(paramaters => paramaters
            .Add(p => p.TotalItemCount, 15)
            .Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
            .Add(p => p.CurrentPage, selectedPage)
            .Add(p => p.SelectedRowsPerPageCount, selectedRowsPerPage)
            .Add(p => p.OnPaginationChanged, args =>
            {
                eventFired = true;
                actualCurrentPage = args.CurrentPage;
                actualSelectedRowsPerPageCount = args.SelectedRowsPerPageCount;
            }));

        var options = cut.FindAll(".option");
        var optToClick = options.First(_ => int.Parse(_.TextContent) == selectedRowsPerPage);
        var rightChevron = cut.Find(".mdi-chevron-right");

        //Act
        optToClick.Click();

        for(var i = 0; i < selectedPage - 1; i++)
            rightChevron.Click();

        //Assert
        Assert.True(eventFired);
        Assert.Equal(expectedPage, actualCurrentPage);
        Assert.Equal(selectedRowsPerPage, actualSelectedRowsPerPageCount);
    }
}

