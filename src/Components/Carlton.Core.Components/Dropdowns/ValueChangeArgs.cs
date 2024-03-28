namespace Carlton.Core.Components.Dropdowns;


/// <summary>
/// Represents the arguments passed when the value of a dropdown component changes.
/// </summary>
/// <typeparam name="T">The type of the selected value.</typeparam>
public sealed record ValueChangeArgs<T>
{
	/// <summary>
	/// Gets the index of the selected item.
	/// </summary>
	public required int SelectedIndex { get; init; }

	/// <summary>
	/// Gets the key of the selected item.
	/// </summary>
	public required string SelectedKey { get; init; }

	/// <summary>
	/// Gets the selected value.
	/// </summary>
	public required T SelectedValue { get; init; }
}