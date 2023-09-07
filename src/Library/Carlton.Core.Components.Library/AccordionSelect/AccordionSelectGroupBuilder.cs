namespace Carlton.Core.Components.Library.AccordionSelect;

public class AccordionSelectGroupBuilder<TValue>
{
    private readonly List<SelectGroup<TValue>> _groups;
    private int _groupIndex;

    public AccordionSelectGroupBuilder()
    {
        _groups = new List<SelectGroup<TValue>>();
        _groupIndex = 0;
    }

    public AccordionSelectGroupBuilder<TValue> AddGroup(string name, bool isExpanded, Dictionary<string, TValue> items)
    {
        var selectItems = items.WithIndex().Select((kvp, i) => new SelectItem<TValue>(kvp.item.Key, kvp.item.Value, i));
        _groups.Add(new SelectGroup<TValue>(name, _groupIndex, isExpanded, selectItems));
        _groupIndex++;
        return this;
    }

    public AccordionSelectGroupBuilder<TValue> AddGroup(string name, bool isExpanded, Action<AccordionSelectItemBuilder<TValue>> itemBuilder)
    {
        var sib = new AccordionSelectItemBuilder<TValue>();
        itemBuilder(sib);
        _groups.Add(new SelectGroup<TValue>(name, _groupIndex, isExpanded, sib.Build()));
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
        _items = new List<SelectItem<TValue>>();
        _itemIndex = 0;
    }

    public AccordionSelectItemBuilder<TValue> AddItem(string name, TValue value)
    {
        _items.Add(new SelectItem<TValue>(name, value, _itemIndex));
        _itemIndex++;
        return this;
    }

    public AccordionSelectItemBuilder<TValue> AddItems(Dictionary<string, TValue> items)
    {
        foreach (var item in items)
        {
            _items.Add(new SelectItem<TValue>(item.Key, item.Value, _itemIndex));
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

