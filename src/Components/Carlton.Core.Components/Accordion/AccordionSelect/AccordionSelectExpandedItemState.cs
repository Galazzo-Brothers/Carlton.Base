namespace Carlton.Core.Components.Accordion.AccordionSelect;

/// <summary>
/// Represents the state of expanded items in an accordion select control.
/// </summary>
public record AccordionSelectExpandedItemState
{
	/// <summary>
	/// Gets or initializes the dictionary of expanded items.
	/// </summary>
	/// <remarks>
	/// The dictionary keys represent the items, and the values represent whether the item is expanded or not.
	/// </remarks>
	public Dictionary<object, bool> ExpandedItems { get; init; } = [];
}
