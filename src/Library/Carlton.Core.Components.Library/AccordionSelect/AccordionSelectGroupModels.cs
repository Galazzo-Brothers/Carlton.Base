namespace Carlton.Core.Components.Library;

public record ExpandedStateChangedEventArgs(int GroupIndexID, bool IsExpanded);
public record SelectItemChangedEventArgs<TValue>(int GroupIndexID, int ItemIndexID, TValue Item);

public record SelectGroup<TValue>
{
    public string Name { get; internal init; }
    public int Index { get; internal init; }
    public IEnumerable<SelectItem<TValue>> Items { get; internal init; }
    public bool IsExpanded { get; internal init; }

    internal SelectGroup(string name, int index, bool isExpanded, IEnumerable<SelectItem<TValue>> items)
       => (Name, Index, IsExpanded, Items) = (name, index, isExpanded, items);
};


public record SelectItem<TValue>
{
    public string Name { get; internal init; }
    public TValue Value { get; internal init; }
    public int Index { get; internal init; }

    internal SelectItem(string name, TValue value, int index)
        => (Name, Value, Index) = (name, value, index);
}






