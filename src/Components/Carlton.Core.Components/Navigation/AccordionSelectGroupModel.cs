namespace Carlton.Core.Components.Navigation;


/// <summary>
/// Represents a selectable group of accordion items.
/// </summary>
/// <typeparam name="TValue">The type of the items' values.</typeparam>
public sealed record AccordionSelectGroupModel<TValue>
{
    /// <summary>
    /// Gets or initializes  the name of the group.
    /// </summary>
    public required string Name { get; init; }

    // <summary>
    /// Gets or initializes  ts the index of the group.
    /// </summary>
    public required int Index { get; init; }

    /// <summary>
    /// Gets or initializes  the collection of items in the group.
    /// </summary>
    public IEnumerable<AccordionSelectModel<TValue>> Items { get; init; }

    /// <summary>
    /// Gets or initializes  a value indicating whether the group is expanded.
    /// </summary>
    public bool IsExpanded { get; init; }

    internal AccordionSelectGroupModel()
    {
    }
};








