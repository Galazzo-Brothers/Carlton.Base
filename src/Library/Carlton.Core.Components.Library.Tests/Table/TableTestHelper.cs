using System.Globalization;

namespace Carlton.Core.Components.Library.Tests;

public static class TableTestHelper
{
    public record TableTestObject(int ID, string DisplayName, DateTime CreatedDate);

    public const string RowTemplate =
@"
<div class=""test-row"">
    <span>{0}</span>
    <span>{1}</span>
    <span>{2}</span>
</div>";

    public const string RowTemplate2 = "{0}_{1}_{2}";

    public static string BuildExpectedMarkup(IEnumerable<TableHeadingItem> headings, string rowTemplate, IEnumerable<TableTestObject> items, bool includePaginationRow, IEnumerable<int> rowsPerPageOpts)
    {
        return @$"<div class=""main-container"">
  <div class=""table-container"">
    <div class=""header-row table-row"">
      {BuildExpectedHeaderMarkup(headings)}
    </div>
    {BuildExpectedItemRows(rowTemplate, items)}
    {(includePaginationRow ? 
        @$"<div class=""pagination-row table-row""> 
            {BuildExpectedPaginationRow(rowsPerPageOpts, items.Count())}
          </div>" : string.Empty)}
</div>";
    }

    public static string BuildExpectedHeaderMarkup(IEnumerable<TableHeadingItem> headings)
    {
        var headingsMarkup = string.Join(Environment.NewLine, headings.Select((item, i) =>
        @$"
<div class=""header-row-item row-item ascending heading-{i}"">
    <span class=""heading-text"">{item.DisplayName}</span>
        <div class=""sort-arrows"">
            <span class=""arrow-ascending mdi mdi-arrow-up""></span>
            <span class=""arrow-descending mdi mdi-arrow-down""></span>
        </div>
</div>"));

        return @$"<div class=""table-header-row"">{headingsMarkup}</div>";
    }

    public static string BuildExpectedItemRows(string rowTemplate, IEnumerable<TableTestObject> items)
    {
        return string.Join(Environment.NewLine, items.Select(item => string.Format(@$"<div class=""item-row table-row"">{rowTemplate}</div>", item.ID, item.DisplayName, item.CreatedDate.ToString("d", CultureInfo.InvariantCulture))));
    }

    public static string BuildExpectedPaginationRow(IEnumerable<int> rowsPerPage, int itemTotal)
    {
        var optionsMarkup = string.Join(Environment.NewLine, rowsPerPage.Select(_ => $@"<div class=""option"">{_}</div>"));
        var defaultOption = rowsPerPage.ElementAt(0);
        return
@$"
    <div class=""pagination-row-item"">
        <div class=""rows-per-page"">
            <span class=""rows-per-page-label"">Rows Per Page</span>
            <div class=""select"">
                <input readonly placeholder="" "" value=""{defaultOption}"" />
                <div class=""label""></div>
                <div class=""options"">
                {optionsMarkup}
            </div>
        </div>
    </div>
    <div class=""page-number"">
        <span class=""pagination-label"">1-{Math.Min(defaultOption, itemTotal)} of {itemTotal}</span>
    </div>
        <div class=""page-chevrons"">
          <span class=""mdi mdi-18px mdi-page-first disabled""></span>
          <span class=""mdi mdi-18px mdi-chevron-left disabled""></span>
          <span class=""mdi mdi-18px mdi-chevron-right disabled""></span>
          <span class=""mdi mdi-18px mdi-page-last disabled""></span>
        </div>
    </div>";
    }

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
