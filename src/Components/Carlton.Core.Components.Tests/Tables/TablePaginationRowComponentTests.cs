using Carlton.Core.Components.Dropdowns;
using Carlton.Core.Components.Tables;
using static Carlton.Core.Components.Tests.Tables.TableTestHelper;
namespace Carlton.Core.Components.Tests.Tables;

[Trait("Component", nameof(TablePaginationRow<int>))]
public class TablePaginationRowComponentTests : TestContext
{
	[Theory(DisplayName = "Markup Test")]
	[InlineData(1, 0)] //15 Items, Page 1, 5 Rows PerPage 
	[InlineData(2, 0)] //15 Items, Page 2, 5 Rows PerPage 
	[InlineData(3, 0)] //15 Items, Page 3, 5 Rows PerPage 
	[InlineData(1, 1)] //15 Items, Page 1, 10 Rows PerPage 
	[InlineData(2, 1)] //15 Items, Page 2, 10 Rows PerPage 
	[InlineData(1, 2)] //15 Items, Page 1, 15 Rows PerPage 
	public void TablePaginationRow_Markup_RendersCorrectly(
		int expectedCurrentPage,
		int expectedSelectedRowsPerPageIndex)
	{
		//Arrange
		var expected = BuildExpectedPaginationRow(
			ItemCount,
			expectedCurrentPage,
			RowsPerPageOpts,
			expectedSelectedRowsPerPageIndex);

		//Act
		var cut = RenderComponent<TablePaginationRow<TableTestObject>>(parameters => parameters
			.Add(p => p.TotalItemCount, ItemCount)
			.Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
			.Add(p => p.CurrentPage, expectedCurrentPage)
			.Add(p => p.SelectedRowsPerPageIndex, expectedSelectedRowsPerPageIndex));

		//Assert
		cut.MarkupMatches(expected);
	}

	[Theory(DisplayName = "SelectedRowsPerPageIndex Parameter Test")]
	[InlineData(1, 0)] //15 Items, Page 1, 5 Rows PerPage 
	[InlineData(2, 0)] //15 Items, Page 2, 5 Rows PerPage 
	[InlineData(3, 0)] //15 Items, Page 3, 5 Rows PerPage 
	[InlineData(1, 1)] //15 Items, Page 1, 10 Rows PerPage 
	[InlineData(2, 1)] //15 Items, Page 2, 10 Rows PerPage 
	[InlineData(1, 2)] //15 Items, Page 1, 15 Rows PerPage 
	public void TablePaginationRow_SelectedRowsPerPageIndexParameter_RendersCorrectly(
		int expectedCurrentPage,
		int expectedSelectedRowsPerPageIndex)
	{
		//Act
		var cut = RenderComponent<TablePaginationRow<TableTestObject>>(parameters => parameters
			.Add(p => p.TotalItemCount, ItemCount)
			.Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
			.Add(p => p.CurrentPage, expectedCurrentPage)
			.Add(p => p.SelectedRowsPerPageIndex, expectedSelectedRowsPerPageIndex));

		var dropdown = cut.FindComponent<Dropdown<int>>();
		var actualValue = dropdown.Instance.SelectedIndex;

		//Assert
		actualValue.ShouldBe(expectedSelectedRowsPerPageIndex);
	}

	[Theory(DisplayName = "RowsPerPageOpts Parameter Render Test")]
	[InlineData(1, 0)] //15 Items, Page 1, 5 Rows PerPage 
	[InlineData(2, 0)] //15 Items, Page 2, 5 Rows PerPage 
	[InlineData(3, 0)] //15 Items, Page 3, 5 Rows PerPage 
	[InlineData(1, 1)] //15 Items, Page 1, 10 Rows PerPage 
	[InlineData(2, 1)] //15 Items, Page 2, 10 Rows PerPage 
	[InlineData(1, 2)] //15 Items, Page 1, 15 Rows PerPage 
	public void TablePaginationRow_RowsPerPageOptsParameter_RendersCorrectly(
		int expectedCurrentPage,
		int expectedSelectedRowsPerPageIndex)
	{
		//Arrange
		var expectedCount = RowsPerPageOpts.Length;

		//Act
		var cut = RenderComponent<TablePaginationRow<TableTestObject>>(parameters => parameters
			.Add(p => p.TotalItemCount, ItemCount)
			.Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
			.Add(p => p.CurrentPage, expectedCurrentPage)
			.Add(p => p.SelectedRowsPerPageIndex, expectedSelectedRowsPerPageIndex));

		var options = cut.FindAll(".option");
		var actualCount = options.Count;
		var actualOptions = options.Select(_ => int.Parse(_.TextContent));

		//Assert
		actualCount.ShouldBe(expectedCount);
		actualOptions.ShouldBe(RowsPerPageOpts);
	}

	[Theory(DisplayName = " SelectedRowsPerPageValue Property Test")]
	[InlineData(1, 0)] //15 Items, Page 1, 5 Rows PerPage 
	[InlineData(2, 0)] //15 Items, Page 2, 5 Rows PerPage 
	[InlineData(3, 0)] //15 Items, Page 3, 5 Rows PerPage 
	[InlineData(1, 1)] //15 Items, Page 1, 10 Rows PerPage 
	[InlineData(2, 1)] //15 Items, Page 2, 10 Rows PerPage 
	[InlineData(1, 2)] //15 Items, Page 1, 15 Rows PerPage 
	public void TablePaginationRow_SelectedRowsPerPageValue_GetsCorrectly(
	  int expectedCurrentPage,
	  int expectedSelectedRowsPerPageIndex)
	{
		//Act
		var cut = RenderComponent<TablePaginationRow<TableTestObject>>(parameters => parameters
			.Add(p => p.TotalItemCount, ItemCount)
			.Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
			.Add(p => p.CurrentPage, expectedCurrentPage)
			.Add(p => p.SelectedRowsPerPageIndex, expectedSelectedRowsPerPageIndex));

		var actualSelectedRowsPerPageIndex = cut.Instance.SelectedRowsPerPageIndex;
		var actualSelectedRowsPerPageValue = cut.Instance.SelectedRowsPerPageValue;
		var expectedSelectedRowsPerPageValue = RowsPerPageOpts.ElementAt(expectedSelectedRowsPerPageIndex);

		//Assert
		actualSelectedRowsPerPageIndex.ShouldBe(expectedSelectedRowsPerPageIndex);
		actualSelectedRowsPerPageValue.ShouldBe(expectedSelectedRowsPerPageValue);
	}

	[Theory(DisplayName = "PaginationLabel Parameter Test")]
	[InlineAutoData(1, 0, "1-5 of 15")] //15 Items, Page 1, 5 Rows PerPage 
	[InlineAutoData(2, 0, "6-10 of 15")] //15 Items, Page 2, 5 Rows PerPage 
	[InlineAutoData(3, 0, "11-15 of 15")] //15 Items, Page 3, 5 Rows PerPage 
	[InlineAutoData(1, 1, "1-10 of 15")] //15 Items, Page 1, 10 Rows PerPage 
	[InlineAutoData(2, 1, "11-15 of 15")] //15 Items, Page 2, 10 Rows PerPage 
	[InlineAutoData(1, 2, "1-15 of 15")] //15 Items, Page 1, 15 Rows PerPage 
	public void TablePaginationRow_PaginationLabel_RendersCorrectly(
		int expectedCurrentPage,
		int expectedSelectedRowsPerPageIndex,
		string expectedText)
	{
		//Act
		var selectedRowsPerPage = RowsPerPageOpts.ElementAt(expectedSelectedRowsPerPageIndex);
		var cut = RenderComponent<TablePaginationRow<TableTestObject>>(parameters => parameters
			.Add(p => p.TotalItemCount, 15)
			.Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
			.Add(p => p.CurrentPage, expectedCurrentPage)
			.Add(p => p.SelectedRowsPerPageIndex, expectedSelectedRowsPerPageIndex));

		var options = cut.FindAll(".option");
		var optToClick = options.First(_ => int.Parse(_.TextContent) == selectedRowsPerPage);
		var paginationLabel = cut.Find(".pagination-label");
		var rightChevron = cut.Find(".mdi-chevron-right");

		//Assert
		paginationLabel.TextContent.ShouldBe(expectedText);
	}

	[Theory(DisplayName = "Right Chevrons Disabled Test")]
	[InlineAutoData(1, 0, false)] //15 Items, Page 1, 5 Rows PerPage 
	[InlineAutoData(2, 0, false)] //15 Items, Page 2, 5 Rows PerPage 
	[InlineAutoData(3, 0, true)] //15 Items, Page 3, 5 Rows PerPage 
	[InlineAutoData(1, 1, false)] //15 Items, Page 1, 10 Rows PerPage 
	[InlineAutoData(2, 1, true)] //15 Items, Page 1, 10 Rows PerPage 
	[InlineAutoData(1, 2, true)] //15 Items, Page 1, 15 Rows PerPage 
	public void TablePaginationRow_RightChevrons_RendersCorrectly(
		int expectedCurrentPage,
		int expectedSelectedRowsPerPageIndex,
		bool expectedChevronDisabled)
	{
		//Act
		var cut = RenderComponent<TablePaginationRow<TableTestObject>>(parameters => parameters
			.Add(p => p.TotalItemCount, ItemCount)
			.Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
			.Add(p => p.CurrentPage, expectedCurrentPage)
			.Add(p => p.SelectedRowsPerPageIndex, expectedSelectedRowsPerPageIndex));

		var chevronRightDisabled = cut.Find(".mdi-chevron-right").ClassList.Contains("disabled");
		var chevronLastPageDisabled = cut.Find(".mdi-page-last").ClassList.Contains("disabled");

		//Assert
		chevronRightDisabled.ShouldBe(expectedChevronDisabled);
		chevronLastPageDisabled.ShouldBe(expectedChevronDisabled);
	}

	[Theory(DisplayName = "Left Chevrons Disabled Test")]
	[InlineAutoData(1, 0, true)] //15 Items, Page 1, 5 Rows PerPage 
	[InlineAutoData(2, 0, false)] //15 Items, Page 2, 5 Rows PerPage 
	[InlineAutoData(3, 0, false)] //15 Items, Page 3, 5 Rows PerPage 
	[InlineAutoData(1, 1, true)] //15 Items, Page 1, 10 Rows PerPage 
	[InlineAutoData(2, 1, false)] //15 Items, Page 2, 10 Rows PerPage 
	[InlineAutoData(1, 2, true)] //15 Items, Page 1, 15 Rows PerPage 
	public void TablePaginationRow_LeftChevronsDisabled_RendersCorrectly(
		int expectedCurrentPage,
		int expectedSelectedRowsPerPageIndex,
		bool expectedChevronDisabled)
	{
		//Act
		var cut = RenderComponent<TablePaginationRow<TableTestObject>>(parameters => parameters
			.Add(p => p.TotalItemCount, ItemCount)
			.Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
			.Add(p => p.CurrentPage, expectedCurrentPage)
			.Add(p => p.SelectedRowsPerPageIndex, expectedSelectedRowsPerPageIndex));

		var chevronLeftDisabled = cut.Find(".mdi-chevron-left").ClassList.Contains("disabled");
		var chevronFirstPageDisabled = cut.Find(".mdi-page-first").ClassList.Contains("disabled");

		//Assert
		chevronLeftDisabled.ShouldBe(expectedChevronDisabled);
		chevronFirstPageDisabled.ShouldBe(expectedChevronDisabled);
	}

	[Theory(DisplayName = "OnRowsPerPageChangedCallback Parameter Test")]
	[InlineAutoData(0)] //5 Rows PerPage 
	[InlineAutoData(1)] //10 Rows PerPage 
	[InlineAutoData(2)] //15 Rows PerPage 
	public void TablePaginationRow_OnRowsPerPageChangedCallback_FiresEvent(int expectedIndexToSelect)
	{
		//Arrange
		var eventFired = false;
		var actualSelectedRowsPerPageIndex = -1;
		var selectedRowsPerPageCount = RowsPerPageOpts.ElementAt(expectedIndexToSelect);
		var cut = RenderComponent<TablePaginationRow<TableTestObject>>(parameters => parameters
			.Add(p => p.TotalItemCount, ItemCount)
			.Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
			.Add(p => p.CurrentPage, 1)
			.Add(p => p.SelectedRowsPerPageIndex, 0)
			.Add(p => p.OnRowsPerPageChange, args =>
			{
				eventFired = true;
				actualSelectedRowsPerPageIndex = args.SelectedRowsPerPageIndex;
			}));

		var options = cut.FindAll(".option");
		var optToClick = options.First(_ => int.Parse(_.TextContent) == selectedRowsPerPageCount);

		//Act
		optToClick.Click();

		//Assert
		eventFired.ShouldBeTrue();
		actualSelectedRowsPerPageIndex.ShouldBe(expectedIndexToSelect);
	}

	[Theory(DisplayName = "OnPageChangedCallback Parameter Test")]
	[InlineAutoData(1)] //15 Rows, Page 1, 5 Rows PerPage
	[InlineAutoData(2)] //15 Rows, Page 2, 5 Rows PerPage
	public void TablePaginationRow_OnPageChangedCallback_FiresEvent(
		int expectedCurrentPage)
	{
		//Arrange
		var eventFired = false;
		var actualPage = 1;
		var cut = RenderComponent<TablePaginationRow<TableTestObject>>(parameters => parameters
			.Add(p => p.TotalItemCount, ItemCount)
			.Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
			.Add(p => p.CurrentPage, expectedCurrentPage)
			.Add(p => p.SelectedRowsPerPageIndex, 0)
			.Add(p => p.OnPageChange, args =>
			{
				eventFired = true;
				actualPage = args.CurrentPage;
			}));

		var rightChevron = cut.Find(".mdi-chevron-right");

		//Act
		rightChevron.Click();

		//Assert
		eventFired.ShouldBeTrue();
		actualPage.ShouldBe(expectedCurrentPage + 1);
	}

	[Theory(DisplayName = "OnPageChangedCallback Parameter Test")]
	[InlineAutoData(1)] //15 Rows, Page 1, 5 Rows PerPage
	public void TablePaginationRow_FirstPage_OnPageChangedCallback_DoesNotFireEvent(
	int expectedCurrentPage)
	{
		//Arrange
		var eventFired = false;
		var cut = RenderComponent<TablePaginationRow<TableTestObject>>(parameters => parameters
			.Add(p => p.TotalItemCount, ItemCount)
			.Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
			.Add(p => p.CurrentPage, expectedCurrentPage)
			.Add(p => p.SelectedRowsPerPageIndex, 0)
			.Add(p => p.OnPageChange, args =>
			{
				eventFired = true;
			}));

		var rightChevron = cut.Find(".mdi-chevron-left");

		//Act
		rightChevron.Click();

		//Assert
		eventFired.ShouldBeFalse();
	}

	[Theory(DisplayName = "OnPageChangedCallback Parameter Test")]
	[InlineAutoData(3)] //15 Rows, Page 3, 5 Rows PerPage
	public void TablePaginationRow_LastPage_OnPageChangedCallback_DoesNotFireEvent(
	int expectedCurrentPage)
	{
		//Arrange
		var eventFired = false;
		var cut = RenderComponent<TablePaginationRow<TableTestObject>>(parameters => parameters
			.Add(p => p.TotalItemCount, ItemCount)
			.Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
			.Add(p => p.CurrentPage, expectedCurrentPage)
			.Add(p => p.SelectedRowsPerPageIndex, 0)
			.Add(p => p.OnPageChange, args =>
			{
				eventFired = true;
			}));

		var rightChevron = cut.Find(".mdi-chevron-right");

		//Act
		rightChevron.Click();

		//Assert
		eventFired.ShouldBeFalse();
	}

	[Theory(DisplayName = "Invalid ItemCountParameter Test")]
	[InlineAutoData(-1)]
	[InlineAutoData(-5)]
	[InlineAutoData(-15)]
	public void TablePaginationRow_Invalid_ItemCountParameter_ShouldThrowArgumentException(int expectedItemCount)
	{
		//Arrange
		IRenderedComponent<TablePaginationRow<TableTestObject>> act() =>
			RenderComponent<TablePaginationRow<TableTestObject>>(parameters => parameters
				.Add(p => p.TotalItemCount, expectedItemCount)
				.Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
				.Add(p => p.CurrentPage, 1)
				.Add(p => p.SelectedRowsPerPageIndex, 0));


		//Act
		var ex = Should.Throw<ArgumentException>((Func<IRenderedComponent<TablePaginationRow<TableTestObject>>>)act, "TotalItemCount must be a non-negative value.");
	}

	[Theory(DisplayName = "Invalid SelectedRowsPerPageIndexParameter Test")]
	[InlineAutoData(-1, "Selected index cannot be less than 0.")]
	[InlineAutoData(5, "Selected index cannot be greater than RowsPerPageOpts count.")]
	public void TablePaginationRow_Invalid_SelectedRowsPerPageIndexParameter_ShouldThrowArgumentException(
		int expectedSelectedIndex,
		string expectedMessage)
	{
		//Arrange
		IRenderedComponent<TablePaginationRow<TableTestObject>> act() =>
			RenderComponent<TablePaginationRow<TableTestObject>>(parameters => parameters
				.Add(p => p.TotalItemCount, ItemCount)
				.Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
				.Add(p => p.CurrentPage, 1)
				.Add(p => p.SelectedRowsPerPageIndex, expectedSelectedIndex));


		//Act
		var ex = Should.Throw<ArgumentException>((Func<IRenderedComponent<TablePaginationRow<TableTestObject>>>)act, expectedMessage);
	}

	[Theory(DisplayName = "Invalid CurrentPageParam Test")]
	[InlineAutoData(5, 0, "Current page cannot be greater than the max page.")]
	public void TablePaginationRow_Invalid_CurrentPageParam_ShouldThrowArgumentException(
		int expectedCurrentPage,
		int expectedRowsPerPageIndex,
		string expectedMessage)
	{
		//Arrange
		IRenderedComponent<TablePaginationRow<TableTestObject>> act() =>
			RenderComponent<TablePaginationRow<TableTestObject>>(parameters => parameters
				.Add(p => p.TotalItemCount, ItemCount)
				.Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
				.Add(p => p.CurrentPage, expectedCurrentPage)
				.Add(p => p.SelectedRowsPerPageIndex, expectedRowsPerPageIndex));

		//Act
		Should.Throw<ArgumentException>(act, expectedMessage);
	}
}

