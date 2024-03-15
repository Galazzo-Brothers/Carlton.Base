namespace Carlton.Core.Components.Navigation;

/// <summary>
/// Represents the event arguments for an group expansion change.
/// </summary>
/// <param name="GroupIndexID">The index ID of the group.</param>
/// <param name="IsExpanded">A value indicating whether the group is expanded.</param>
public sealed record GroupExpansionChangeEventArgs(int GroupIndexID, bool IsExpanded);

/// <summary>
/// Represents the event arguments for an item selection.
/// </summary>
/// <typeparam name="TValue">The type of the selected item's value.</typeparam>
/// <param name="GroupIndexID">The index ID of the group.</param>
/// <param name="ItemIndexID">The index ID of the item.</param>
/// <param name="Item">The selected item.</param>
public sealed record ItemSelectedEventArgs<TValue>(int GroupIndexID, int ItemIndexID, TValue Item);
