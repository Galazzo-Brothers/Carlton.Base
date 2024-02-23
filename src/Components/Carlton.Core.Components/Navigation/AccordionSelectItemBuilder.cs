namespace Carlton.Core.Components.Navigation;

/// <summary>
/// Builder class for creating individual accordion select items.
/// </summary>
/// <typeparam name="TValue">The type of value associated with each item.</typeparam>
public sealed class AccordionSelectItemBuilder<TValue>
{
    private readonly List<SelectItem<TValue>> _items;
    private int _itemIndex;

    /// <summary>
    /// Initializes a new instance of the <see cref="AccordionSelectItemBuilder{TValue}"/> class.
    /// </summary>
    public AccordionSelectItemBuilder()
    {
        _items = [];
        _itemIndex = 0;
    }

    /// <summary>
    /// Adds an individual accordion select item with a name and value.
    /// </summary>
    public AccordionSelectItemBuilder<TValue> AddItem(string name, TValue value)
    {
        _items.Add(new SelectItem<TValue>
        {
            Index = _itemIndex,
            Name = name,
            Value = value
        });
        _itemIndex++;
        return this;
    }

    /// <summary>
    /// Adds multiple accordion select items from a dictionary of names and values.
    /// </summary>
    public AccordionSelectItemBuilder<TValue> AddItems(IDictionary<string, TValue> items)
    {
        foreach (var item in items)
        {
            _items.Add(new SelectItem<TValue>
            {
                Index = _itemIndex,
                Name = item.Key,
                Value = item.Value
            });
            _itemIndex++;
        }
        ;
        return this;
    }

    /// <summary>
    /// Builds and returns the list of select items.
    /// </summary>
    public IEnumerable<SelectItem<TValue>> Build()
    {
        return _items;
    }
}
