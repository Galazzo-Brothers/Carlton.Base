using Carlton.Core.Components.Table;
using System.ComponentModel;
using System.Globalization;

namespace Carlton.Core.Components.Library.Tests;

public static class TableTestHelper
{
    public const int ItemCount = 15;
    public static readonly int[] RowsPerPageOpts = [5, 10, 15];

    public static readonly IEnumerable<TableHeadingItem> TableTestHeadingItems =
        new List<TableHeadingItem>
        {
            new("ID"),
            new("DisplayName"),
            new("Created Date")
        };

    public record TableTestObject(int ID, string DisplayName, DateTime CreatedDate);

    public const string RowTemplate =
@"
<span class=""table-cell"">{0}</span>
<span class=""table-cell"">{1}</span>
<span class=""table-cell"">{2}</span>";

    public const string RowTemplate2 = "{0}_{1}_{2}";

    public static string BuildExpectedMarkup(
        IEnumerable<TableHeadingItem> headings,
        string rowTemplate,
        IEnumerable<TableTestObject> items,
        bool includePaginationRow,
        IEnumerable<int> rowsPerPageOpts,
        int selectedRowsPerPageIndex,
        int currentPage)
    {
        return @$"
  <div class=""table-container"">
    <div class=""table-row"">
      {BuildExpectedHeaderMarkup(headings)}
    </div>
    {BuildExpectedItemRows(rowTemplate, items)}
    {(includePaginationRow ? 
        @$"<div class=""table-row""> 
            {BuildExpectedPaginationRow(items.Count(), rowsPerPageOpts, selectedRowsPerPageIndex, currentPage)}
          </div>" : string.Empty)}";
    }

    public static string BuildExpectedHeaderMarkup(IEnumerable<TableHeadingItem> headings)
    {
        return string.Join(Environment.NewLine, headings.Select((item, i) =>
        @$"
<div class=""header-cell table-cell ascending heading-{i}"">
    <div class=""heading-container"">
        <span class=""heading-text"">{item.DisplayName}</span>
            <div class=""sort-arrows"">
                <span class=""arrow-ascending mdi mdi-arrow-up""></span>
                <span class=""arrow-descending mdi mdi-arrow-down""></span>
            </div>
    </div>
</div>"));
    }

    public static string BuildExpectedItemRows(string rowTemplate, IEnumerable<TableTestObject> items)
    {
        return string.Join(Environment.NewLine, items.Select(item => string.Format(@$"<div class=""table-row"">{rowTemplate}</div>", item.ID, item.DisplayName, item.CreatedDate.ToString("d", CultureInfo.InvariantCulture))));
    }

    public static string BuildExpectedPaginationRow(int itemTotal, IEnumerable<int> rowsPerPage, int currentPage, int selectedRowsPerPageIndex)
    {
        var selectedRowsPerPage = rowsPerPage.ElementAt(selectedRowsPerPageIndex);
        var numOfPages = Math.Ceiling((decimal)itemTotal / selectedRowsPerPage);
        var leftDisabled = currentPage == 1;
        var rightDisabled = currentPage == numOfPages;

        var optionsMarkup = string.Join(Environment.NewLine, rowsPerPage.Select(_ => $@"<div class=""option"">{_}</div>"));

        var endPageCount = Math.Min((selectedRowsPerPage * currentPage), itemTotal);
        var startPageCount = Math.Max(((selectedRowsPerPage * currentPage) - selectedRowsPerPage) + 1, 1);

        return
@$"
    <div class=""pagination-row"">
        <div class=""rows-per-page"">
            <span class=""rows-per-page-label"">Rows Per Page</span>
            <div class=""select"">
                <input readonly placeholder="" "" value=""{selectedRowsPerPage}"" />
                <div class=""label""></div>
                <div class=""options"">
                    {optionsMarkup}
                </div>
            </div>
        </div>
        <div class=""page-number"">
            <span class=""pagination-label"">{startPageCount}-{endPageCount} of {itemTotal}</span>
        </div>
        <div class=""page-chevrons"">
          <span class=""mdi mdi-18px mdi-page-first {(leftDisabled ? "disabled" : string.Empty)}""></span>
          <span class=""mdi mdi-18px mdi-chevron-left {(leftDisabled ? "disabled" : string.Empty)}""></span>
          <span class=""mdi mdi-18px mdi-chevron-right {(rightDisabled ? "disabled" : string.Empty)}""></span>
          <span class=""mdi mdi-18px mdi-page-last {(rightDisabled ? "disabled" : string.Empty)}""></span>
        </div>
    </div>
    ";
    }

    public static readonly IReadOnlyCollection<TableHeadingItem> Headings = new List<TableHeadingItem>
    {
            new TableHeadingItem(nameof(TableTestObject.ID)),
            new TableHeadingItem(nameof(TableTestObject.DisplayName)),
            new TableHeadingItem(nameof(TableTestObject.CreatedDate))
    };

    public static IEnumerable<TableTestObject> OrderCollection(IEnumerable<TableTestObject> items, int columnIndex)
    {
        return columnIndex switch
        {
            0 => items.OrderBy(p => p.ID),
            1 => items.OrderBy(p => p.DisplayName),
            2 => items.OrderBy(p => p.CreatedDate),
            _ => throw new Exception(),
        };
    }

    public static IEnumerable<TableTestObject> OrderCollectionDesc(IEnumerable<TableTestObject> items, int columnIndex)
    {
        return columnIndex switch
        {
            0 => items.OrderByDescending(p => p.ID),
            1 => items.OrderByDescending(p => p.DisplayName),
            2 => items.OrderByDescending(p => p.CreatedDate),
            _ => throw new Exception(),
        };
    }

    public static readonly IReadOnlyCollection<TableTestObject> ItemListForPagingTests = new List<TableTestObject>
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
}
