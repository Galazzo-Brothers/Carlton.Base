namespace Carlton.Core.Components.Accordion.AccordionSelectGroup;

/// <summary>
/// Represents the event arguments for an group expansion change.
/// </summary>
/// <param name="GroupIndexId">The index Id of the group.</param>
/// <param name="IsExpanded">A value indicating whether the group is expanded.</param>
public sealed record AccordionSelectGroupExpansionChangeEventArgs(int GroupIndexId, bool IsExpanded);

/// <summary>
/// Represents the event arguments for an item selection.
/// </summary>
/// <typeparam name="TValue">The type of the selected item's value.</typeparam>
/// <param name="GroupIndexId">The index Id of the group.</param>
/// <param name="ItemIndexId">The index Id of the item.</param>
/// <param name="Item">The selected item.</param>
public sealed record AccordionSelectGroupItemChangedEventArgs<TValue>(int GroupIndexId, int ItemIndexId, TValue Item);
