namespace Carlton.Core.Components.Accordion.AccordionSelect;

/// <summary>
/// Builder class for creating individual accordion select items.
/// </summary>
/// <typeparam name="TValue">The type of value associated with each item.</typeparam>
public sealed class AccordionSelectModelBuilder<TValue>
{
	private readonly List<AccordionSelectModel<TValue>> _items;
	private int _itemIndex;

	/// <summary>
	/// Initializes a new instance of the <see cref="AccordionSelectModelBuilder{TValue}"/> class.
	/// </summary>
	public AccordionSelectModelBuilder()
	{
		_items = [];
		_itemIndex = 0;
	}

	/// <summary>
	/// Adds an individual accordion select item with a name and value.
	/// </summary>
	public AccordionSelectModelBuilder<TValue> AddItem(string name, TValue value)
	{
		_items.Add(new AccordionSelectModel<TValue>
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
	public AccordionSelectModelBuilder<TValue> AddItems(IDictionary<string, TValue> items)
	{
		foreach (var item in items)
		{
			_items.Add(new AccordionSelectModel<TValue>
			{
				Index = _itemIndex,
				Name = item.Key,
				Value = item.Value
			});
			_itemIndex++;
		}

		return this;
	}

	/// <summary>
	/// Builds and returns the list of select items.
	/// </summary>
	public IEnumerable<AccordionSelectModel<TValue>> Build()
	{
		return _items;
	}
}
