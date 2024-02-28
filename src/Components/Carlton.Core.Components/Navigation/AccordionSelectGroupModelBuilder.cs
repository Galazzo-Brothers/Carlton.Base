namespace Carlton.Core.Components.Navigation;

/// <summary>
/// Builder class for creating groups of accordion select items.
/// </summary>
/// <typeparam name="TValue">The type of value associated with each item in the select groups.</typeparam>
public sealed class AccordionSelectGroupModelBuilder<TValue>
{
    private readonly List<AccordionSelectGroupModel<TValue>> _groups;
    private int _groupIndex;

    /// <summary>
    /// Initializes a new instance of the <see cref="AccordionSelectGroupModelBuilder{TValue}"/> class.
    /// </summary>
    public AccordionSelectGroupModelBuilder()
    {
        _groups = [];
        _groupIndex = 0;
    }

    /// <summary>
    /// Adds a group of accordion select items with individual names and values.
    /// </summary>
    public AccordionSelectGroupModelBuilder<TValue> AddGroup(string name, bool isExpanded, Dictionary<string, TValue> items)
    {
        var selectItems = items.WithIndex().Select((kvp, i) => new AccordionSelectModel<TValue>
        {
            Index = i,
            Name = kvp.item.Key,
            Value = kvp.item.Value
        });
        _groups.Add(new AccordionSelectGroupModel<TValue>
        {
            Index = _groupIndex,
            Name = name,
            IsExpanded = isExpanded,
            Items = selectItems
        });
        _groupIndex++;
        return this;
    }

    /// <summary>
    /// Adds a group of accordion select items using a builder pattern.
    /// </summary>
    public AccordionSelectGroupModelBuilder<TValue> AddGroup(string name, bool isExpanded, Action<AccordionSelectModelBuilder<TValue>> itemBuilder)
    {
        var sib = new AccordionSelectModelBuilder<TValue>();
        itemBuilder(sib);
        _groups.Add(new AccordionSelectGroupModel<TValue>
        {
            Index = _groupIndex,
            Name = name,
            IsExpanded = isExpanded,
            Items = sib.Build()
        });
        _groupIndex++;
        return this;
    }

    /// <summary>
    /// Builds and returns the list of select groups.
    /// </summary>
    public IEnumerable<AccordionSelectGroupModel<TValue>> Build()
    {
        return _groups;
    }
}


