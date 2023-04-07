namespace Carlton.Base.Components.Test;

public class TableComponentTests : TestContext
{
    private const string TableMarkup =
    @"<div class=""main-container"" b-rbixdumkuw>
  <div class=""table-container"" b-rbixdumkuw>
    <div class=""header-row table-row"" b-rbixdumkuw>
      <div class=""header-row-item row-item ascending"" blazor:onclick=""1"" b-ydzvi9l03d>
        <span class=""heading-text"" b-ydzvi9l03d>ID</span>
        <div class=""filter-arrows"" b-ydzvi9l03d>
          <span class=""arrow-ascending mdi mdi-arrow-up"" b-ydzvi9l03d></span>
          <span class=""arrow-descending mdi mdi-arrow-down"" b-ydzvi9l03d></span>
        </div>
      </div>
      <div class=""header-row-item row-item ascending"" blazor:onclick=""2"" b-ydzvi9l03d>
        <span class=""heading-text"" b-ydzvi9l03d>DisplayName</span>
        <div class=""filter-arrows"" b-ydzvi9l03d>
          <span class=""arrow-ascending mdi mdi-arrow-up"" b-ydzvi9l03d></span>
          <span class=""arrow-descending mdi mdi-arrow-down"" b-ydzvi9l03d></span>
        </div>
      </div>
      <div class=""header-row-item row-item ascending"" blazor:onclick=""3"" b-ydzvi9l03d>
        <span class=""heading-text"" b-ydzvi9l03d>CreatedDate</span>
        <div class=""filter-arrows"" b-ydzvi9l03d>
          <span class=""arrow-ascending mdi mdi-arrow-up"" b-ydzvi9l03d></span>
          <span class=""arrow-descending mdi mdi-arrow-down"" b-ydzvi9l03d></span>
        </div>
      </div>
    </div>
    <div class=""item-row table-row"" b-rbixdumkuw>
      <div class=""test-row"">
        <span>ID:1</span>
        <span>Display Name:Item A</span>
        <span>Date:10/9/2023 12:00:00 AM</span>
      </div>
    </div>
    <div class=""item-row table-row"" b-rbixdumkuw>
      <div class=""test-row"">
        <span>ID:2</span>
        <span>Display Name:Item B</span>
        <span>Date:2/3/2022 12:00:00 AM</span>
      </div>
    </div>
    <div class=""item-row table-row"" b-rbixdumkuw>
      <div class=""test-row"">
        <span>ID:3</span>
        <span>Display Name:Item C</span>
        <span>Date:5/19/2021 12:00:00 AM</span>
      </div>
    </div>
    <div class=""pagination-row table-row"" b-rbixdumkuw>
      <div class=""pagination-row-item"" b-hp2nj7a13h>
        <div class=""rows-per-page"" b-hp2nj7a13h>
          <span class=""rows-per-page-label"" b-hp2nj7a13h>Rows Per Page</span>
          <div class=""select"" b-b4t7b28hd7>
            <input readonly placeholder="" "" value=""5"" b-b4t7b28hd7 />
            <div class=""label"" b-b4t7b28hd7></div>
            <div class=""options"" b-b4t7b28hd7>
              <div class=""option"" blazor:onclick=""8"" b-b4t7b28hd7>5</div>
              <div class=""option"" blazor:onclick=""9"" b-b4t7b28hd7>10</div>
              <div class=""option"" blazor:onclick=""10"" b-b4t7b28hd7>15</div>
            </div>
          </div>
        </div>
        <div class=""page-number"" b-hp2nj7a13h>
          <span class=""pagination-label"" b-hp2nj7a13h>1-3 of 3</span>
        </div>
        <div class=""page-chevrons"" b-hp2nj7a13h>
          <span class=""mdi mdi-18px mdi-page-first disabled"" blazor:onclick=""4"" b-hp2nj7a13h></span>
          <span class=""mdi mdi-18px mdi-chevron-left disabled"" blazor:onclick=""5"" b-hp2nj7a13h></span>
          <span class=""mdi mdi-18px mdi-chevron-right disabled"" blazor:onclick=""6"" b-hp2nj7a13h></span>
          <span class=""mdi mdi-18px mdi-page-last disabled"" blazor:onclick=""7"" b-hp2nj7a13h></span>
        </div>
      </div>
    </div>
  </div>
</div>";

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

    private static readonly IList<int> RowsPerPageOpts = new List<int> { 5, 10, 15 };

    private static readonly string RowTemplate =
    @"
        <div class=""test-row"">
            <span>ID:{0}</span>
            <span>Display Name:{1}</span>
            <span>Date:{2}</span>
        </div>";

    public static IEnumerable<object[]> GetItems()
    {
        yield return new object[]
        {
           new List<TableTestObject>
                 {
                      new TableTestObject(1, "Item A", new DateTime(2023, 10, 9))
                 }
        };
        yield return new object[]
        {
                 new List<TableTestObject>
                 {
                      new TableTestObject(1, "Item A", new DateTime(2023, 10, 9)),
                      new TableTestObject(2, "Item B", new DateTime(2022, 2, 3))
                 }
        };
        yield return new object[]
        {
                 new List<TableTestObject>
                 {
                      new TableTestObject(1, "Item A", new DateTime(2023, 10, 9)),
                      new TableTestObject(2, "Item B", new DateTime(2022, 2, 3)),
                      new TableTestObject(3, "Item C", new DateTime(2021, 5, 19))
                 }
        };
    }

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

    public record TableTestObject(int ID, string DisplayName, DateTime CreatedDate);

    [Fact]
    public void Table_Markup_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<Table<TableTestObject>>(paramaters => paramaters
            .Add(p => p.Headings, Headings)
            .Add(p => p.Items, Items)
            .Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate))
            .Add(p => p.ShowPaginationRow, true)
            );

        //Assert
        cut.MarkupMatches(TableMarkup);
    }

    [Theory]
    [MemberData(nameof(GetHeadings))]
    public void Table_HeadingsParam_Count_RendersCorrectly(IEnumerable<TableHeadingItem> headings)
    {
        //Arrange
        var expectedCount = headings.Count();

        //Act
        var cut = RenderComponent<Table<TableTestObject>>(paramaters => paramaters
            .Add(p => p.Headings, headings)
            .Add(p => p.Items, Items)
            .Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate))
            .Add(p => p.ShowPaginationRow, true)
            );

        var headerRowItems = cut.FindAll(".header-row-item");
        var actualCount = headerRowItems.Count;

        //Assert
        Assert.Equal(expectedCount, actualCount);
    }

    [Fact]
    public void Table_HeadingsParam_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<Table<TableTestObject>>(paramaters => paramaters
            .Add(p => p.Headings, Headings)
            .Add(p => p.Items, Items)
            .Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate))
            .Add(p => p.ShowPaginationRow, true)
            );

        var headingText = cut.FindAll(".heading-text").Select(_ => _.TextContent);

        //Assert
        Assert.Collection(headingText,
            _ => Assert.Equal("ID", _),
            _ => Assert.Equal("DisplayName", _),
            _ => Assert.Equal("CreatedDate", _));
    }

    [Fact]
    public void Table_OnClickOnce_FirstColum_FiltersItemsAsc()
    {
        //Arrange
        var cut = RenderComponent<Table<TableTestObject>>(paramaters => paramaters
            .Add(p => p.Headings, Headings)
            .Add(p => p.Items, Items)
            .Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate))
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
        var cut = RenderComponent<Table<TableTestObject>>(paramaters => paramaters
            .Add(p => p.Headings, Headings)
            .Add(p => p.Items, Items)
            .Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate))
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
        var cut = RenderComponent<Table<TableTestObject>>(paramaters => paramaters
            .Add(p => p.Headings, Headings)
            .Add(p => p.Items, Items)
            .Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate))
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
        var cut = RenderComponent<Table<TableTestObject>>(paramaters => paramaters
            .Add(p => p.Headings, Headings)
            .Add(p => p.Items, Items)
            .Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate))
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
        var cut = RenderComponent<Table<TableTestObject>>(paramaters => paramaters
            .Add(p => p.Headings, Headings)
            .Add(p => p.Items, Items)
            .Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate))
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
        var cut = RenderComponent<Table<TableTestObject>>(paramaters => paramaters
            .Add(p => p.Headings, Headings)
            .Add(p => p.Items, Items)
            .Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate))
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
        var cut = RenderComponent<Table<TableTestObject>>(paramaters => paramaters
            .Add(p => p.Headings, Headings)
            .Add(p => p.Items, Items)
            .Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate))
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
        var cut = RenderComponent<Table<TableTestObject>>(paramaters => paramaters
            .Add(p => p.Headings, Headings)
            .Add(p => p.Items, Items)
            .Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate))
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
        var cut = RenderComponent<Table<TableTestObject>>(paramaters => paramaters
            .Add(p => p.Headings, Headings)
            .Add(p => p.Items, Items)
            .Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate))
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
    [MemberData(nameof(GetItems))]
    public void Table_ItemsParam_Count_RendersCorrectly(IEnumerable<TableTestObject> items)
    {
        //Arrange
        var expectedCount = items.Count();

        //Act
        var cut = RenderComponent<Table<TableTestObject>>(paramaters => paramaters
            .Add(p => p.Headings, Headings)
            .Add(p => p.Items, items)
            .Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate))
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
        var cut = RenderComponent<Table<TableTestObject>>(paramaters => paramaters
            .Add(p => p.Headings, Headings)
            .Add(p => p.Items, Items)
            .Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate))
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
        var expectedRow1 = string.Format(rowTemplate, Items[0].ID, Items[0].DisplayName, Items[0].CreatedDate);
        var expectedRow2 = string.Format(rowTemplate, Items[1].ID, Items[1].DisplayName, Items[1].CreatedDate);
        var expectedRow3 = string.Format(rowTemplate, Items[2].ID, Items[2].DisplayName, Items[2].CreatedDate);

        //Act
        var cut = RenderComponent<Table<TableTestObject>>(paramaters => paramaters
            .Add(p => p.Headings, Headings)
            .Add(p => p.Items, Items)
            .Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
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
    [MemberData(nameof(GetRowsPerPageOptions))]
    public void Table_RowsPerPageOptsParam_Count_RendersCorrectly(IEnumerable<int> rowsPerPageOpts)
    {
        //Arrange
        var expectedCount = rowsPerPageOpts.Count();

        //Act
        var cut = RenderComponent<Table<TableTestObject>>(paramaters => paramaters
            .Add(p => p.Headings, Headings)
            .Add(p => p.Items, Items)
            .Add(p => p.RowsPerPageOpts, rowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate))
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
        var cut = RenderComponent<Table<TableTestObject>>(paramaters => paramaters
            .Add(p => p.Headings, Headings)
            .Add(p => p.Items, Items)
            .Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate))
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
        var cut = RenderComponent<Table<TableTestObject>>(paramaters => paramaters
            .Add(p => p.Headings, Headings)
            .Add(p => p.Items, Items)
            .Add(p => p.RowsPerPageOpts, RowsPerPageOpts)
            .Add(p => p.RowTemplate, item => string.Format(RowTemplate, item.ID, item.DisplayName, item.CreatedDate))
            .Add(p => p.ShowPaginationRow, showPaginationRow)
            );

        var paginationRowExists = cut.HasComponent<TablePaginationRow<TableTestObject>>();

        //Assert
        Assert.Equal(showPaginationRow, paginationRowExists);
    }
}