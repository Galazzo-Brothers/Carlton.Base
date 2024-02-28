namespace Carlton.Core.Components.Navigation;


/// <summary>
/// Represents a selectable accordion item.
/// </summary>
/// <typeparam name="TValue">The type of the item's value.</typeparam>
public sealed record AccordionSelectModel<TValue>
{
    /// <summary>
    /// Gets or initializes  the name of the item.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Gets or initializes  the value of the item.
    /// </summary>
    public required TValue Value { get; init; }

    /// <summary>
    /// Gets or initializes  the index of the item.
    /// </summary>
    public required int Index { get; init; }

    internal AccordionSelectModel()
    {
    }
}