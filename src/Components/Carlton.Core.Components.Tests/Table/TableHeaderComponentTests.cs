using Carlton.Core.Components.Table;
using static Carlton.Core.Components.Tests.TableTestHelper;
namespace Carlton.Core.Components.Tests;

[Trait("Component", nameof(TableHeader<int>))]
public class TableHeaderComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test")]
    [InlineAutoData(true)]
    [InlineAutoData(false)]
    public void TableHeader_Markup_RendersCorrectly(
        bool expectedIsAscending,
        IEnumerable<TableHeadingItem> headings)
    {
        //Arrange
        var expected = BuildExpectedHeaderMarkup(headings, expectedIsAscending);

        //Act
        var cut = RenderComponent<TableHeader<TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, headings)
            .Add(p => p.SelectedOrderColumn, string.Empty)
            .Add(p => p.OrderAscending, expectedIsAscending));

        //Assert
        cut.MarkupMatches(expected);
    }

    [Theory(DisplayName = "Headings Parameter Test"), AutoData]
    public void TableHeader_HeadingsParameter_RendersCorrectly(IEnumerable<TableHeadingItem> expectedHeadingItems)
    {
        //Arrange
        var expectedCount = expectedHeadingItems.Count();
        var expectedHeadings = expectedHeadingItems.Select(_ => _.DisplayName);

        //Act
        var cut = RenderComponent<TableHeader<TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, expectedHeadingItems)
            .Add(p => p.SelectedOrderColumn, string.Empty)
            .Add(p => p.OrderAscending, true));

        var headerRowItems = cut.FindAll(".header-cell");
        var actualCount = headerRowItems.Count;
        var actualHeadings = cut.FindAll(".heading-text").Select(_ => _.TextContent);

        //Assert
        actualCount.ShouldBe(expectedCount);
        actualHeadings.ShouldBe(expectedHeadings);
    }

    [Theory(DisplayName = "OrderColumn and OrderDirection Parameter Test")]
    [InlineData("ID", 0, true)]
    [InlineData("ID", 0, false)]
    [InlineData("DisplayName", 1, true)]
    [InlineData("DisplayName", 1, false)]
    [InlineData("CreatedDate", 2, true)]
    [InlineData("CreatedDate", 2, false)]
    public void TableHeader_OrderColumnParam_And_OrderDirectionParameter_RendersCorrectly(
        string expectedColumnName,
        int expectedColumnIndex,
        bool expectedOrderAscending)
    {
        //Act
        var cut = RenderComponent<TableHeader<TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, Headings)
            .Add(p => p.SelectedOrderColumn, expectedColumnName)
            .Add(p => p.OrderAscending, expectedOrderAscending));

        var headerRowItems = cut.FindAll(".header-cell");
        var selectedItem = headerRowItems.ElementAt(expectedColumnIndex);
 
        var hasAscendingClass = selectedItem.ClassList.Contains("ascending");
        var hasDescendingClass = selectedItem.ClassList.Contains("descending");

        //Assert
        selectedItem.ClassList.ShouldContain("selected");
        hasAscendingClass.ShouldBe(expectedOrderAscending);
        hasDescendingClass.ShouldNotBe(expectedOrderAscending);
    }

    [Theory(DisplayName = "Invalid OrderColumn Parameter Test")]
    [InlineData("Wrong")]
    [InlineData("Also Wrong")]
    [InlineData("Still Wrong")]
    public void TableHeader_InvalidOrderColumnParameter_RendersCorrectly(string expectedColumnName)
    {
        //Arrange
        IRenderedComponent<TableHeader<TableTestObject>> act() => RenderComponent<TableHeader<TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, Headings)
            .Add(p => p.SelectedOrderColumn, expectedColumnName)
            .Add(p => p.OrderAscending, true));

        //Act
        Should.Throw<ArgumentException>((Func<IRenderedComponent<TableHeader<TableTestObject>>>)act, $"Attempting to order the table by a non-existent column: {expectedColumnName}");
    }

    [Theory(DisplayName = "Header Click Once Test")]
    [InlineData(0, "ID")]
    [InlineData(1, "DisplayName")]
    [InlineData(2, "CreatedDate")]
    public void TableHeader_ClickHeadersOnce_EventFires(
        int expectedColumnIndex,
        string expectedColumnName)
    {
        //Arrange
        var eventFired = false;
        var actualOrderAscending = false;
        var actualOrderColumn = string.Empty;
        var cut = RenderComponent<TableHeader<TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, Headings)
            .Add(p => p.SelectedOrderColumn, string.Empty)
            .Add(p => p.OrderAscending, true)
            .Add(p => p.OnItemsOrdered, args => { eventFired = true; actualOrderAscending = args.OrderAscending; actualOrderColumn = args.OrderColumn; }));

        var headerRowItems = cut.FindAll(".header-cell");

        //Act
        headerRowItems.ElementAt(expectedColumnIndex).Click();

        //Assert
        eventFired.ShouldBeTrue();
        actualOrderAscending.ShouldBeTrue();
        actualOrderColumn.ShouldBe(expectedColumnName);
    }

    [Theory(DisplayName = "Header Click Twice Test")]
    [InlineData(0, "ID")]
    [InlineData(1, "DisplayName")]
    [InlineData(2, "CreatedDate")]
    public void TableHeader_ClickHeadersTwice_EventFires(
        int exepectedColumnIndex,
        string expectedColumnName)
    {
        //Arrange
        var eventFired = false;
        var actualOrderAscending = true;
        var actualOrderColumn = string.Empty;
        var cut = RenderComponent<TableHeader<TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, Headings)
            .Add(p => p.SelectedOrderColumn, string.Empty)
            .Add(p => p.OrderAscending, true)
            .Add(p => p.OnItemsOrdered, args => { eventFired = true; actualOrderAscending = args.OrderAscending; actualOrderColumn = args.OrderColumn; }));

        //Act
        var itemToClick = cut.FindAll(".header-cell").ElementAt(exepectedColumnIndex);
        itemToClick.Click();
        itemToClick = cut.FindAll(".header-cell").ElementAt(exepectedColumnIndex);
        itemToClick.Click();

        //Assert
        eventFired.ShouldBeTrue();
        actualOrderAscending.ShouldBeFalse();
        actualOrderColumn.ShouldBe(expectedColumnName);
    }

    [Theory(DisplayName = "Header Click, CSS Selected Class Test")]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public void TableHeader_OnClick_SelectedClass_RendersCorrectly(int expectedSelectedIndex)
    {
        //Arrange
        var cut = RenderComponent<TableHeader<TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, Headings)
            .Add(p => p.SelectedOrderColumn, string.Empty)
            .Add(p => p.OrderAscending, true));

        var headerRowItems = cut.FindAll(".header-cell", true);
        var selectedItem = headerRowItems.ElementAt(expectedSelectedIndex);

        //Act
        selectedItem.Click();
        selectedItem = headerRowItems.ElementAt(expectedSelectedIndex);

        //Assert
        selectedItem.ClassList.ShouldContain("selected");
    }

    [Theory(DisplayName = "Header Click, CSS Ascending Class Test")]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public void TableHeader_OnClick_AscendingClass_RendersCorrectly(int expectedSelectedIndex)
    {
        //Arrange
        var cut = RenderComponent<TableHeader<TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, Headings)
            .Add(p => p.SelectedOrderColumn, string.Empty)
            .Add(p => p.OrderAscending, true));

        var headerRowItems = cut.FindAll(".header-cell", true);
        var selectedItem = headerRowItems.ElementAt(expectedSelectedIndex);

        //Act
        selectedItem.Click();
        selectedItem = headerRowItems.ElementAt(expectedSelectedIndex);

        //Assert
        selectedItem.ClassList.ShouldContain("ascending");
    }

    [Theory(DisplayName = "Header Click, CSS Descending Class Test")]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public void TableHeader_OnClick_DescendingClass_RendersCorrectly(int expectedSelectedIndex)
    {
        //Arrange
        var cut = RenderComponent<TableHeader<TableTestObject>>(parameters => parameters
            .Add(p => p.Headings, Headings)
            .Add(p => p.SelectedOrderColumn, string.Empty)
            .Add(p => p.OrderAscending, true));

        //Act
        cut.FindAll(".header-cell").ElementAt(expectedSelectedIndex).Click();
        cut.FindAll(".header-cell").ElementAt(expectedSelectedIndex).Click();
        var selectedItem = cut.FindAll(".header-cell").ElementAt(expectedSelectedIndex);

        //Assert
        selectedItem.ClassList.ShouldContain("descending");
    }
}
