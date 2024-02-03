namespace Carlton.Core.Components.Navigation;

public class AccordionSelectGroupBuilder<TValue>
{
    private readonly List<SelectGroup<TValue>> _groups;
    private int _groupIndex;

    public AccordionSelectGroupBuilder()
    {
        _groups = [];
        _groupIndex = 0;
    }

    public AccordionSelectGroupBuilder<TValue> AddGroup(string name, bool isExpanded, Dictionary<string, TValue> items)
    {
        var selectItems = items.WithIndex().Select((kvp, i) => new SelectItem<TValue>
        {
            Index = i,
            Name = kvp.item.Key,
            Value = kvp.item.Value
        });
        _groups.Add(new SelectGroup<TValue>
        {
            Index = _groupIndex,
            Name = name,
            IsExpanded = isExpanded,
            Items = selectItems
        });
        _groupIndex++;
        return this;
    }

    public AccordionSelectGroupBuilder<TValue> AddGroup(string name, bool isExpanded, Action<AccordionSelectItemBuilder<TValue>> itemBuilder)
    {
        var sib = new AccordionSelectItemBuilder<TValue>();
        itemBuilder(sib);
        _groups.Add(new SelectGroup<TValue>
        {
            Index = _groupIndex,
            Name = name,
            IsExpanded = isExpanded,
            Items = sib.Build()
        });
        _groupIndex++;
        return this;
    }

    public IEnumerable<SelectGroup<TValue>> Build()
    {
        return _groups;
    }
}

public class AccordionSelectItemBuilder<TValue>
{
    private readonly List<SelectItem<TValue>> _items;
    private int _itemIndex;

    public AccordionSelectItemBuilder()
    {
        _items = [];
        _itemIndex = 0;
    }

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

    public IEnumerable<SelectItem<TValue>> Build()
    {
        return _items;
    }
}

