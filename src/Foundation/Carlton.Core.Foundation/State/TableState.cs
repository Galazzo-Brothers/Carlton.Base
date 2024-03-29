namespace Carlton.Core.Foundation.State;

/// <summary>
/// Represents the state of a table component.
/// </summary>
public sealed record TableState
{
	/// <summary>
	/// Gets or initializes the current page number.
	/// </summary>
	public int CurrentPage { get; init; } = 1;

	/// <summary>
	/// Gets or initializes the index of the selected rows per page option.
	/// </summary>
	public int SelectedRowsPerPageOptsIndex { get; init; } = 0;

	/// <summary>
	/// Gets or initializes the options for rows per page.
	/// </summary>
	public IEnumerable<int> RowsPerPageOpts { get; init; } = new List<int> { 5, 10, 25 };

	/// <summary>
	/// Gets or initializes the column by which the table is ordered.
	/// </summary>
	public string OrderByColum { get; init; } = string.Empty;

	/// <summary>
	/// Gets or initializes a value indicating whether the table is ordered in ascending order.
	/// </summary>
	public bool IsAscending { get; init; } = true;
}