using AutoFixture.Xunit2;
using static Carlton.Core.Components.Library.Tests.TableTestHelper;

namespace Carlton.Core.Components.Library.Tests;

[Trait("Component", nameof(TablePaginationRow<int>))]
public class TablePaginationRowComponentTests : TestContext
{
    private const int ItemsCount = 3;

    [Theory(DisplayName = "Markup Test"),AutoData]
    public void TablePaginationRow_Markup_RendersCorrectly(int itemsCount, IEnumerable<int> rowsPerPage)
    {
        //Arrange
        var expected = TableTestHelper.BuildExpectedPaginationRow(rowsPerPage, itemsCount);

        //Act
        var cut = RenderComponent<TablePaginationRow<TableTestObject>>(parameters => parameters
            .Add(p => p.TotalItemCount, itemsCount)
            .Add(p => p.RowsPerPageOpts, rowsPerPage)
            .Add(p => p.CurrentPage, 1)
            .Add(p => p.SelectedRowsPerPageCount, rowsPerPage.First()));

        //Assert
        cut.MarkupMatches(expected);
    }

    [Theory(DisplayName = "RowsPerPageOpts Parameter Test")]
    [MemberData(nameof(TableTestHelper.GetRowsPerPageOptions), MemberType = typeof(TableTestHelper))]
    public void TablePaginationRow_RowsPerPageOptsParam_DefaultsToFirstOption_RendersCorrectly(IEnumerable<int> opts)
    {
        //Arrange
        var expectedValue = opts.First();

        //Act
        var cut = RenderComponent<TablePaginationRow<TableTestHelper.TableTestObject>>(parameters => parameters
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

    [Theory(DisplayName = "RowsPerPageOpts Parameter Render Test")]
    [MemberData(nameof(TableTestHelper.GetRowsPerPageOptions), MemberType = typeof(TableTestHelper))]
    public void TablePaginationRow_RowsPerPageOptsParam_RendersCorrectly(IEnumerable<int> opts)
    {
        //Arrange
        var expectedCount = opts.Count();
        var initialRowsPerPageCount = opts.First();
      
        //Act
        var cut = RenderComponent<TablePaginationRow<TableTestHelper.TableTestObject>>(parameters => parameters
            .Add(p => p.TotalItemCount, ItemsCount)
            .Add(p => p.RowsPerPageOpts, opts)
            .Add(p => p.CurrentPage, 1)
            .Add(p => p.SelectedRowsPerPageCount, initialRowsPerPageCount)
            );

        var options = cut.FindAll(".option");
        var actualCount = options.Count;
        var actualOptions = options.Select(_ => int.Parse(_.TextContent));

        //Assert
        Assert.Equal(expectedCount, actualCount);
        Assert.Equal(opts, actualOptions);
    }

    [Theory(DisplayName = "PaginationLabel Parameter Test")]
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
        var cut = RenderComponent<TablePaginationRow<TableTestHelper.TableTestObject>>(parameters => parameters
            .Add(p => p.TotalItemCount, 15)
            .Add(p => p.RowsPerPageOpts, TableTestHelper.RowsPerPageOpts)
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

    [Theory(DisplayName = "PaginationLabel Parameter, Item Count Less Than Page Count Test")]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public void TablePaginationRow_PaginationLabel_ItemCountLessThanItemsPerPage_RendersCorrectly(int optIndex)
    {
        //Arrange
        var expectedText = "1-3 of 3";
        var cut = RenderComponent<TablePaginationRow<TableTestHelper.TableTestObject>>(parameters => parameters
            .Add(p => p.TotalItemCount, ItemsCount)
            .Add(p => p.RowsPerPageOpts, TableTestHelper.RowsPerPageOpts)
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


    [Theory(DisplayName = "Item Count, Right Chevron Disabled Test")]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    public void TablePaginationRow_ItemCountLessThanItemsPerPage_RightChevronsDisabled_RendersCorrectly(int itemCount)
    {
        //Act
        var cut = RenderComponent<TablePaginationRow<TableTestHelper.TableTestObject>>(parameters => parameters
            .Add(p => p.TotalItemCount, itemCount)
            .Add(p => p.RowsPerPageOpts, TableTestHelper.RowsPerPageOpts)
            .Add(p => p.CurrentPage, 1)
            .Add(p => p.SelectedRowsPerPageCount, 5)
            );

        var chevronRightDisabled = cut.Find(".mdi-chevron-right").ClassList.Contains("disabled");
        var chevronLastPageDisabled = cut.Find(".mdi-page-last").ClassList.Contains("disabled");

        //Assert
        Assert.True(chevronRightDisabled);
        Assert.True(chevronLastPageDisabled);
    }

    [Theory(DisplayName = "Item Count, Right Chevron Enabled Test")]
    [InlineData(10)]
    [InlineData(50)]
    [InlineData(100)]
    public void TablePaginationRow_ItemCountGreaterThanItemsPerPage_RightChevronsEnabled_RendersCorrectly(int itemCount)
    {
        //Act
        var cut = RenderComponent<TablePaginationRow<TableTestHelper.TableTestObject>>(parameters => parameters
            .Add(p => p.TotalItemCount, itemCount)
            .Add(p => p.RowsPerPageOpts, TableTestHelper.RowsPerPageOpts)
            .Add(p => p.CurrentPage, 1)
            .Add(p => p.SelectedRowsPerPageCount, 5)
            );

        var chevronRightDisabled = cut.Find(".mdi-chevron-right").ClassList.Contains("disabled");
        var chevronLastPageDisabled = cut.Find(".mdi-page-last").ClassList.Contains("disabled");

        //Assert
        Assert.False(chevronRightDisabled);
        Assert.False(chevronLastPageDisabled);
    }

    [Fact(DisplayName = "Page One, Left Chevron Disabled Test")]
    public void TablePaginationRow_PageOne_LeftChevronsDisabled_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<TablePaginationRow<TableTestHelper.TableTestObject>>(parameters => parameters
            .Add(p => p.TotalItemCount, ItemsCount)
            .Add(p => p.RowsPerPageOpts, TableTestHelper.RowsPerPageOpts)
            .Add(p => p.CurrentPage, 1)
            .Add(p => p.SelectedRowsPerPageCount, 5)
            );

        var chevronLeftDisabled = cut.Find(".mdi-chevron-left").ClassList.Contains("disabled");
        var chevronFirstPageDisabled = cut.Find(".mdi-page-first").ClassList.Contains("disabled");

        //Assert
        Assert.True(chevronLeftDisabled);
        Assert.True(chevronFirstPageDisabled);
    }

    [Theory(DisplayName = "Page Greater Than One, Left Chevron Enabled Test")]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(5)]
    public void TablePaginationRow_PageGreaterThanOne_LeftChevronsEnabled_RendersCorrectly(int currentPage)
    {
        //Arrange
        var cut = RenderComponent<TablePaginationRow<TableTestHelper.TableTestObject>>(parameters => parameters
            .Add(p => p.TotalItemCount, ItemsCount)
            .Add(p => p.RowsPerPageOpts, TableTestHelper.RowsPerPageOpts)
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

    [Theory(DisplayName = "OnPaginationChangedCallback Parameter Test")]
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
        var cut = RenderComponent<TablePaginationRow<TableTestHelper.TableTestObject>>(parameters => parameters
            .Add(p => p.TotalItemCount, 15)
            .Add(p => p.RowsPerPageOpts, TableTestHelper.RowsPerPageOpts)
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

