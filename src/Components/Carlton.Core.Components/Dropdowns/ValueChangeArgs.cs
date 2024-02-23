namespace Carlton.Core.Components.Dropdowns;


/// <summary>
/// Represents the arguments passed when the value of a dropdown component changes.
/// </summary>
/// <typeparam name="T">The type of the selected value.</typeparam>
public sealed record ValueChangeArgs<T>(
    /// <summary>
    /// Gets the index of the selected item.
    /// </summary>
    int SelectedIndex,

    /// <summary>
    /// Gets the key of the selected item.
    /// </summary>
    string SelectedKey,

    /// <summary>
    /// Gets the selected value.
    /// </summary>
    T SelectedValue
);