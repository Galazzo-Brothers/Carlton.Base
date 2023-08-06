namespace Carlton.Core.Components.Library.Tests;

public static class TableTestHelper
{
    public record TableTestObject(int ID, string DisplayName, DateTime CreatedDate);

    public const string TableMarkup =
    @"<div class=""main-container"" b-rbixdumkuw>
  <div class=""table-container"" b-rbixdumkuw>
    <div class=""header-row table-row"" b-rbixdumkuw>
      <div class=""table-header-row"" b-ydzvi9l03d>
        <div class=""header-row-item row-item ascending heading-0"" blazor:onclick=""1"" b-ydzvi9l03d>
          <span class=""heading-text"" b-ydzvi9l03d>ID</span>
          <div class=""sort-arrows"" b-ydzvi9l03d>
            <span class=""arrow-ascending mdi mdi-arrow-up"" b-ydzvi9l03d></span>
            <span class=""arrow-descending mdi mdi-arrow-down"" b-ydzvi9l03d></span>
          </div>
        </div>
        <div class=""header-row-item row-item ascending heading-1"" blazor:onclick=""2"" b-ydzvi9l03d>
          <span class=""heading-text"" b-ydzvi9l03d>DisplayName</span>
          <div class=""sort-arrows"" b-ydzvi9l03d>
            <span class=""arrow-ascending mdi mdi-arrow-up"" b-ydzvi9l03d></span>
            <span class=""arrow-descending mdi mdi-arrow-down"" b-ydzvi9l03d></span>
          </div>
        </div>
        <div class=""header-row-item row-item ascending heading-2"" blazor:onclick=""3"" b-ydzvi9l03d>
          <span class=""heading-text"" b-ydzvi9l03d>CreatedDate</span>
          <div class=""sort-arrows"" b-ydzvi9l03d>
            <span class=""arrow-ascending mdi mdi-arrow-up"" b-ydzvi9l03d></span>
            <span class=""arrow-descending mdi mdi-arrow-down"" b-ydzvi9l03d></span>
          </div>
        </div>
      </div>
    </div>
    <div class=""item-row table-row"" b-rbixdumkuw>
      <div class=""test-row"">
        <span>ID:1</span>
        <span>Display Name:Item A</span>
        <span>Date:10/09/2023</span>
      </div>
    </div>
    <div class=""item-row table-row"" b-rbixdumkuw>
      <div class=""test-row"">
        <span>ID:2</span>
        <span>Display Name:Item B</span>
        <span>Date:02/03/2022</span>
      </div>
    </div>
    <div class=""item-row table-row"" b-rbixdumkuw>
      <div class=""test-row"">
        <span>ID:3</span>
        <span>Display Name:Item C</span>
        <span>Date:05/19/2021</span>
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

    public const string RowTemplate =
    @"
        <div class=""test-row"">
            <span>ID:{0}</span>
            <span>Display Name:{1}</span>
            <span>Date:{2}</span>
        </div>";

    public const string TableHeaderMarkup =
    @"<div class=""table-header-row"" b-ydzvi9l03d>
  <div class=""header-row-item row-item ascending heading-0"" blazor:onclick=""1"" b-ydzvi9l03d>
    <span class=""heading-text"" b-ydzvi9l03d>ID</span>
    <div class=""sort-arrows"" b-ydzvi9l03d>
      <span class=""arrow-ascending mdi mdi-arrow-up"" b-ydzvi9l03d></span>
      <span class=""arrow-descending mdi mdi-arrow-down"" b-ydzvi9l03d></span>
    </div>
  </div>
  <div class=""header-row-item row-item ascending heading-1"" blazor:onclick=""2"" b-ydzvi9l03d>
    <span class=""heading-text"" b-ydzvi9l03d>DisplayName</span>
    <div class=""sort-arrows"" b-ydzvi9l03d>
      <span class=""arrow-ascending mdi mdi-arrow-up"" b-ydzvi9l03d></span>
      <span class=""arrow-descending mdi mdi-arrow-down"" b-ydzvi9l03d></span>
    </div>
  </div>
  <div class=""header-row-item row-item ascending heading-2"" blazor:onclick=""3"" b-ydzvi9l03d>
    <span class=""heading-text"" b-ydzvi9l03d>CreatedDate</span>
    <div class=""sort-arrows"" b-ydzvi9l03d>
      <span class=""arrow-ascending mdi mdi-arrow-up"" b-ydzvi9l03d></span>
      <span class=""arrow-descending mdi mdi-arrow-down"" b-ydzvi9l03d></span>
    </div>
  </div>
</div>";

    public const string TablePaginationRowMarkup =
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


    public static readonly IReadOnlyCollection<TableHeadingItem> Headings = new List<TableHeadingItem>
    {
            new TableHeadingItem(nameof(TableTestObject.ID)),
            new TableHeadingItem(nameof(TableTestObject.DisplayName)),
            new TableHeadingItem(nameof(TableTestObject.CreatedDate))
    };

    public static readonly IReadOnlyCollection<TableTestObject> Items = new List<TableTestObject>
    {
        new TableTestObject(1, "Item A", new DateTime(2023, 10, 9)),
        new TableTestObject(2, "Item B", new DateTime(2022, 2, 3)),
        new TableTestObject(3, "Item C", new DateTime(2021, 5, 19))
    };

    public static readonly IReadOnlyCollection<TableTestObject> BigItemsList = new List<TableTestObject>
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

    public static readonly IReadOnlyCollection<int> RowsPerPageOpts = new List<int> { 5, 10, 15 };


    public static IEnumerable<object[]> GetFilteredItemsAsc()
    {
        yield return new object[]
        {
           (
                0,
                new List<TableTestObject>
                {
                    new TableTestObject(1, "Item A", new DateTime(2023, 10, 9)),
                    new TableTestObject(2, "Item B", new DateTime(2022, 2, 3)),
                    new TableTestObject(3, "Item C", new DateTime(2021, 5, 19))
                }.AsReadOnly()
            )
        };
        yield return new object[]
        {
          (
                1,
                new List<TableTestObject>
                {
                    new TableTestObject(1, "Item A", new DateTime(2023, 10, 9)),
                    new TableTestObject(2, "Item B", new DateTime(2022, 2, 3)),
                    new TableTestObject(3, "Item C", new DateTime(2021, 5, 19))
                }.AsReadOnly()
            )
        };
        yield return new object[]
        {
         (
                2,
                new List<TableTestObject>
                {
                    new TableTestObject(3, "Item C", new DateTime(2021, 5, 19)),
                    new TableTestObject(2, "Item B", new DateTime(2022, 2, 3)),
                    new TableTestObject(1, "Item A", new DateTime(2023, 10, 9))
                }.AsReadOnly()
            )
        };
    }

    public static IEnumerable<object[]> GetFilteredItemsDesc()
    {
        yield return new object[]
        {
           (
                0,
                new List<TableTestObject>
                {
                    new TableTestObject(3, "Item C", new DateTime(2021, 5, 19)),
                    new TableTestObject(2, "Item B", new DateTime(2022, 2, 3)),
                    new TableTestObject(1, "Item A", new DateTime(2023, 10, 9))
                }.AsReadOnly()
            )
        };
        yield return new object[]
        {
          (
                1,
                new List<TableTestObject>
                {
                    new TableTestObject(3, "Item C", new DateTime(2021, 5, 19)),
                    new TableTestObject(2, "Item B", new DateTime(2022, 2, 3)),
                    new TableTestObject(1, "Item A", new DateTime(2023, 10, 9))
                }.AsReadOnly()
            )
        };
        yield return new object[]
        {
         (
                2,
                new List<TableTestObject>
                {
                    new TableTestObject(1, "Item A", new DateTime(2023, 10, 9)),
                    new TableTestObject(2, "Item B", new DateTime(2022, 2, 3)),
                    new TableTestObject(3, "Item C", new DateTime(2021, 5, 19))
                }.AsReadOnly()
            )
        };
    }

    public static IEnumerable<object[]> GetItemsWithDisplayedValues()
    {
        yield return new object[]
        {
           (
               new List<TableTestObject>
                     {
                          new TableTestObject(1, "Item A", new DateTime(2023, 10, 9))
                     }.AsReadOnly(),
               new List<string> { "ID:1", "Display Name:Item A", "Date:10/09/2023" }.AsReadOnly()
            )
        };
        yield return new object[]
        {
            (
                 new List<TableTestObject>
                 {
                      new TableTestObject(1, "Item A", new DateTime(2023, 10, 9)),
                      new TableTestObject(2, "Item B", new DateTime(2022, 2, 3))
                 }.AsReadOnly(),
                 new List<string> 
                 {
                     "ID:1", "Display Name:Item A", "Date:10/09/2023",
                     "ID:2", "Display Name:Item B", "Date:02/03/2022"
                 }.AsReadOnly()
            )
        };
        yield return new object[]
        {
            (
                 new List<TableTestObject>
                 {
                      new TableTestObject(1, "Item A", new DateTime(2023, 10, 9)),
                      new TableTestObject(2, "Item B", new DateTime(2022, 2, 3)),
                      new TableTestObject(3, "Item C", new DateTime(2021, 5, 19))
                 }.AsReadOnly(),
                 new List<string>
                 {
                     "ID:1", "Display Name:Item A", "Date:10/09/2023",
                     "ID:2", "Display Name:Item B", "Date:02/03/2022",
                     "ID:3", "Display Name:Item C", "Date:05/19/2021" 
                }.AsReadOnly()
            )
        };
    }

    public static IEnumerable<object[]> GetHeadings()
    {
        yield return new object[]
        {
            new List<TableHeadingItem>
            {
                new TableHeadingItem(nameof(TableTestObject.ID))
            }.AsReadOnly()
        };
        yield return new object[]
        {
                new List<TableHeadingItem>
                {
                    new TableHeadingItem(nameof(TableTestObject.ID)),
                    new TableHeadingItem(nameof(TableTestObject.DisplayName))
                }.AsReadOnly()
        };
        yield return new object[]
        {
                 new List<TableHeadingItem>
                 {
                    new TableHeadingItem(nameof(TableTestObject.ID)),
                    new TableHeadingItem(nameof(TableTestObject.DisplayName)),
                    new TableHeadingItem(nameof(TableTestObject.CreatedDate))
                 }.AsReadOnly()
        };
    }

    public static IEnumerable<object[]> GetRowsPerPageOptions()
    {
        yield return new object[]
        {
           new List<int> { 1, 3, 5 }.AsReadOnly()
        };
        yield return new object[]
        {
            new List<int> { 2 }.AsReadOnly()
        };
        yield return new object[]
        {
            new List<int> { 5, 10, 15, 20, 25 }.AsReadOnly()
        };
    }
}
