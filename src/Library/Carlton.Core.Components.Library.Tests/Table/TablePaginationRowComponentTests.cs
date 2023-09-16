using AutoFixture.Xunit2;
using static Carlton.Core.Components.Library.Tests.TableTestHelper;

namespace Carlton.Core.Components.Library.Tests;

[Trait("Component", nameof(TablePaginationRow<int>))]
public class TablePaginationRowComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test")]
    [InlineData(1, new int[] { 5, 10, 15 }, 0, 1)]
    [InlineData(50, new int[] { 5, 10, 15 }, 0, 2)]
    [InlineData(100, new int[] { 5, 10, 15 }, 0, 3)]
    [InlineData(1, new int[] { 5, 10, 15 }, 1, 1)]
    [InlineData(50, new int[] { 5, 10, 15 }, 1, 2)]
    [InlineData(100, new int[] { 5, 10, 15 }, 1, 3)]
    [InlineData(1, new int[] { 5, 10, 15 }, 2, 1)]
    [InlineData(50, new int[] { 5, 10, 15 }, 2, 2)]
    [InlineData(100, new int[] { 5, 10, 15 }, 2, 3)]
    public void TablePaginationRow_Markup_RendersCorrectly(
        int itemsCount,
        IEnumerable<int> rowsPerPageOpts,
        int selectedRowsPerPageIndex,
        int currentPage)
    {
        //Arrange
        var expected = BuildExpectedPaginationRow(itemsCount, rowsPerPageOpts, selectedRowsPerPageIndex, currentPage);

        //Act
        var cut = RenderComponent<TablePaginationRow<TableTestObject>>(parameters => parameters
            .Add(p => p.TotalItemCount, itemsCount)
            .Add(p => p.RowsPerPageOpts, rowsPerPageOpts)
            .Add(p => p.CurrentPage, currentPage)
            .Add(p => p.SelectedRowsPerPageIndex, selectedRowsPerPageIndex));

        //Assert
        cut.MarkupMatches(expected);
    }

    [Theory(DisplayName = "RowsPerPageOpts Parameter Test"), AutoData]
    public void TablePaginationRow_RowsPerPageOptsParam_DefaultsToFirstOption(int itemCount, IEnumerable<int> opts)
    {
        //Act
        var cut = RenderComponent<TablePaginationRow<TableTestObject>>(parameters => parameters
            .Add(p => p.TotalItemCount, itemCount)
            .Add(p => p.RowsPerPageOpts, opts)
            .Add(p => p.CurrentPage, 1)
            .Add(p => p.SelectedRowsPerPageIndex, 0));

        var select = cut.FindComponent<Select>();
        var actualValue = select.Instance.SelectedValue;
        var expectedValue = opts.First();

        //Assert
        Assert.Equal(expectedValue, actualValue);
    }

    [Theory(DisplayName = "RowsPerPageOpts Parameter Render Test")]
    [InlineAutoData(15, 1, 0, new int[] { 5, 10, 15 })]
    [InlineAutoData(15, 2, 0, new int[] { 5, 10, 15 })]
    [InlineAutoData(15, 2, 0, new int[] { 5, 10, 15 })]
    [InlineAutoData(15, 1, 1, new int[] { 5, 10, 15 })]
    [InlineAutoData(15, 2, 1, new int[] { 5, 10, 15 })]
    [InlineAutoData(15, 1, 2, new int[] { 5, 10, 15 })]
    public void TablePaginationRow_RowsPerPageOptsParam_RendersCorrectly(
        int itemCount,
        int currentPage,
        int selectedRowsPerPageIndex,
        IEnumerable<int> rowsPerPageOpts)
    {
        //Arrange
        var expectedCount = rowsPerPageOpts.Count();

        //Act
        var cut = RenderComponent<TablePaginationRow<TableTestObject>>(parameters => parameters
            .Add(p => p.TotalItemCount, itemCount)
            .Add(p => p.RowsPerPageOpts, rowsPerPageOpts)
            .Add(p => p.CurrentPage, currentPage)
            .Add(p => p.SelectedRowsPerPageIndex, selectedRowsPerPageIndex));

        var options = cut.FindAll(".option");
        var actualCount = options.Count;
        var actualOptions = options.Select(_ => int.Parse(_.TextContent));

        //Assert
        Assert.Equal(expectedCount, actualCount);
        Assert.Equal(rowsPerPageOpts, actualOptions);
    }

    [Theory(DisplayName = "PaginationLabel Parameter Test")]
    [InlineAutoData(1, 0, new int[] { 5, 10, 15 }, "1-5 of 15")]
    [InlineAutoData(2, 0, new int[] { 5, 10, 15 }, "6-10 of 15")]
    [InlineAutoData(3, 0, new int[] { 5, 10, 15 }, "11-15 of 15")]
    [InlineAutoData(1, 1, new int[] { 5, 10, 15 }, "1-10 of 15")]
    [InlineAutoData(2, 1, new int[] { 5, 10, 15 }, "11-15 of 15")]
    [InlineAutoData(1, 2, new int[] { 5, 10, 15 }, "1-15 of 15")]
    public void TablePaginationRow_PaginationLabel_RendersCorrectly(
        int currentPage,
        int selectedRowsPerPageIndex,
        IEnumerable<int> rowsPerPageOpts,
        string expectedText)
    {
        //Arrange
        var selectedRowsPerPage = rowsPerPageOpts.ElementAt(selectedRowsPerPageIndex);
        var cut = RenderComponent<TablePaginationRow<TableTestObject>>(parameters => parameters
            .Add(p => p.TotalItemCount, 15)
            .Add(p => p.RowsPerPageOpts, rowsPerPageOpts)
            .Add(p => p.CurrentPage, currentPage)
            .Add(p => p.SelectedRowsPerPageIndex, selectedRowsPerPageIndex));

        var options = cut.FindAll(".option");
        var optToClick = options.First(_ => int.Parse(_.TextContent) == selectedRowsPerPage);
        var paginationLabel = cut.Find(".pagination-label");
        var rightChevron = cut.Find(".mdi-chevron-right");

        //Act
        optToClick.Click();

        for (var i = 0; i < currentPage - 1; i++)
            rightChevron.Click();

        //Assert
        Assert.Equal(expectedText, paginationLabel.TextContent);
    }

    [Theory(DisplayName = "PaginationLabel Parameter, Item Count Less Than Page Count Test")]
    [InlineAutoData(15, new int[] { 5, 10, 15 }, 0, 1, "1-5 of 15")]
    [InlineAutoData(15, new int[] { 5, 10, 15 }, 0, 2, "6-10 of 15")]
    [InlineAutoData(15, new int[] { 5, 10, 15 }, 0, 3, "11-15 of 15")]
    [InlineAutoData(15, new int[] { 5, 10, 15 }, 1, 1, "1-10 of 15")]
    [InlineAutoData(15, new int[] { 5, 10, 15 }, 1, 2, "11-15 of 15")]
    [InlineAutoData(15, new int[] { 5, 10, 15 }, 2, 1, "1-15 of 15")]
    public void TablePaginationRow_PaginationLabel_ItemCountLessThanItemsPerPage_RendersCorrectly(
        int itemCount,
        IEnumerable<int> rowsPerPageOpts,
        int optIndex,
        int currentPage,
        string expectedText)
    {
        //Act
        var cut = RenderComponent<TablePaginationRow<TableTestObject>>(parameters => parameters
            .Add(p => p.TotalItemCount, itemCount)
            .Add(p => p.RowsPerPageOpts, rowsPerPageOpts)
            .Add(p => p.CurrentPage, currentPage)
            .Add(p => p.SelectedRowsPerPageIndex, optIndex));

        var options = cut.FindAll(".option");
        var optToClick = options.ElementAt(optIndex);
        var paginationLabel = cut.Find(".pagination-label");

        //Assert
        Assert.Equal(expectedText, paginationLabel.TextContent);
    }


    [Theory(DisplayName = "Right Chevron Disabled Test")]
    [InlineAutoData(15, new int[] { 5, 10, 15 }, 0, 1, false)]
    [InlineAutoData(15, new int[] { 5, 10, 15 }, 0, 2, false)]
    [InlineAutoData(15, new int[] { 5, 10, 15 }, 0, 3, true)]
    [InlineAutoData(15, new int[] { 5, 10, 15 }, 1, 1, false)]
    [InlineAutoData(15, new int[] { 5, 10, 15 }, 1, 2, true)]
    [InlineAutoData(15, new int[] { 5, 10, 15 }, 2, 1, true)]
    public void TablePaginationRow_RightChevrons_RendersCorrectly(
        int itemCount,
        IEnumerable<int> rowsPerPageOpts,
        int selectedRowsPerPageIndex,
        int currentPage,
        bool expectedChevronDisabled)
    {
        //Act
        var cut = RenderComponent<TablePaginationRow<TableTestObject>>(parameters => parameters
            .Add(p => p.TotalItemCount, itemCount)
            .Add(p => p.RowsPerPageOpts, rowsPerPageOpts)
            .Add(p => p.CurrentPage, currentPage)
            .Add(p => p.SelectedRowsPerPageIndex, selectedRowsPerPageIndex));

        var chevronRightDisabled = cut.Find(".mdi-chevron-right").ClassList.Contains("disabled");
        var chevronLastPageDisabled = cut.Find(".mdi-page-last").ClassList.Contains("disabled");

        //Assert
        Assert.Equal(expectedChevronDisabled, chevronRightDisabled);
        Assert.Equal(expectedChevronDisabled, chevronLastPageDisabled);
    }

    [Theory(DisplayName = "Left Chevron Disabled Test")]
    [InlineAutoData(15, new int[] { 5, 10, 15 }, 0, 1, true)]
    [InlineAutoData(15, new int[] { 5, 10, 15 }, 0, 2, false)]
    [InlineAutoData(15, new int[] { 5, 10, 15 }, 0, 3, false)]
    [InlineAutoData(15, new int[] { 5, 10, 15 }, 1, 1, true)]
    [InlineAutoData(15, new int[] { 5, 10, 15 }, 1, 2, false)]
    [InlineAutoData(15, new int[] { 5, 10, 15 }, 2, 1, true)]
    public void TablePaginationRow_LeftChevronsDisabled_RendersCorrectly(
        int itemCount,
        IEnumerable<int> rowsPerPageOpts,
        int selectedRowsPerPageIndex,
        int currentPage,
        bool expectedChevronDisabled)
    {
        //Act
        var cut = RenderComponent<TablePaginationRow<TableTestObject>>(parameters => parameters
            .Add(p => p.TotalItemCount, itemCount)
            .Add(p => p.RowsPerPageOpts, rowsPerPageOpts)
            .Add(p => p.CurrentPage, currentPage)
            .Add(p => p.SelectedRowsPerPageIndex, selectedRowsPerPageIndex));

        var chevronLeftDisabled = cut.Find(".mdi-chevron-left").ClassList.Contains("disabled");
        var chevronFirstPageDisabled = cut.Find(".mdi-page-first").ClassList.Contains("disabled");

        //Assert
        Assert.Equal(expectedChevronDisabled, chevronLeftDisabled);
        Assert.Equal(expectedChevronDisabled, chevronFirstPageDisabled);
    }

    [Theory(DisplayName = "OnRowsPerPageChangedCallback Parameter Test")]
    [InlineAutoData(15, new int[] { 5, 10, 15 }, 0)]
    [InlineAutoData(15, new int[] { 5, 10, 15 }, 1)]
    [InlineAutoData(15, new int[] { 5, 10, 15 }, 2)]
    public void TablePaginationRow_OnRowsPerPageChangedCallback_FiresEvent(
        int numOfItems,
        IEnumerable<int> rowsPerPageOpts,
        int indexToSelect)
    {
        //Arrange
        var eventFired = false;
        var actualSelectedRowsPerPageIndex = -1;
        var selectedRowsPerPageCount = rowsPerPageOpts.ElementAt(indexToSelect);
        var cut = RenderComponent<TablePaginationRow<TableTestObject>>(parameters => parameters
            .Add(p => p.TotalItemCount, numOfItems)
            .Add(p => p.RowsPerPageOpts, rowsPerPageOpts)
            .Add(p => p.CurrentPage, 1)
            .Add(p => p.SelectedRowsPerPageIndex, 0)
            .Add(p => p.OnRowsPerPageChanged, args =>
            {
                eventFired = true;
                actualSelectedRowsPerPageIndex = args.SelectedRowsPerPageIndex;
            }));

        var options = cut.FindAll(".option");
        var optToClick = options.First(_ => int.Parse(_.TextContent) == selectedRowsPerPageCount);

        //Act
        optToClick.Click();

        //Assert
        Assert.True(eventFired);
        Assert.Equal(indexToSelect, actualSelectedRowsPerPageIndex);
    }

    [Theory(DisplayName = "OnPageChangedCallback Parameter Test")]
    [InlineAutoData(15, new int[] { 5, 10, 15 }, 0, 2)]
    [InlineAutoData(15, new int[] { 5, 10, 15 }, 0, 3)]
    public void TablePaginationRow_OnPageChangedCallback_FiresEvent(
     int numOfItems,
     IEnumerable<int> rowsPerPageOpts,
     int selectedIndex,
     int pageNumberToSelect)
    {
        //Arrange
        var eventFired = false;
        var actualPage = 1;
        var cut = RenderComponent<TablePaginationRow<TableTestObject>>(parameters => parameters
            .Add(p => p.TotalItemCount, numOfItems)
            .Add(p => p.RowsPerPageOpts, rowsPerPageOpts)
            .Add(p => p.CurrentPage, 1)
            .Add(p => p.SelectedRowsPerPageIndex, selectedIndex)
            .Add(p => p.OnPageChanged, args =>
            {
                eventFired = true;
                actualPage = args.CurrentPage;
            }));

        var rightChevron = cut.Find(".mdi-chevron-right");

        //Act
        for (var i = 0; i < pageNumberToSelect - 1; i++)
            rightChevron.Click();

        //Assert
        Assert.True(eventFired);
        Assert.Equal(pageNumberToSelect, actualPage);
    }

    [Theory(DisplayName = "Invalid SelectedRowsPerPageIndexParam Test")]
    [InlineAutoData(-1, new int[] { 5, 10, 15 }, "Selected index cannot be less than 0.")]
    [InlineAutoData(5, new int[] { 5, 10, 15 }, "Selected index cannot be greater than RowsPerPageOpts count.")]
    public void TablePaginationRow_Invalid_SelectedRowsPerPageIndexParam_ShouldThrowArgumentException(
        int selectedIndex,
        IEnumerable<int> rowsPerPageOpts,
        string expectedMessage,
        int numOfItems)
    {
        //Arrange
        var ex = Assert.Throws<ArgumentException>(() =>
            RenderComponent<TablePaginationRow<TableTestObject>>(parameters => parameters
                .Add(p => p.TotalItemCount, numOfItems)
                .Add(p => p.RowsPerPageOpts, rowsPerPageOpts)
                .Add(p => p.CurrentPage, 1)
                .Add(p => p.SelectedRowsPerPageIndex, selectedIndex)));


        //Assert
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Theory(DisplayName = "Invalid CurrentPageParam Test")]
    [InlineAutoData(15, new int[] { 5, 10, 15 }, 0, 10, "Current page cannot be greater than the max page.")]
    public void TablePaginationRow_Invalid_CurrentPageParam_ShouldThrowArgumentException(
      int numOfItems,
      IEnumerable<int> rowsPerPageOpts,
      int rowsPerPageIndex,
      int currentPage, 
      string expectedMessage)
    {
        //Arrange
        var ex = Assert.Throws<ArgumentException>(() =>
            RenderComponent<TablePaginationRow<TableTestObject>>(parameters => parameters
                .Add(p => p.TotalItemCount, numOfItems)
                .Add(p => p.RowsPerPageOpts, rowsPerPageOpts)
                .Add(p => p.CurrentPage, currentPage)
                .Add(p => p.SelectedRowsPerPageIndex, rowsPerPageIndex)));


        //Assert
        Assert.Equal(expectedMessage, ex.Message);
    }
}

