namespace Carlton.Core.Components.Navigation;

public sealed record ItemExpansionChangeEventArgs(int GroupIndexID, bool IsExpanded);
public sealed record ItemSelectedEventArgs<TValue>(int GroupIndexID, int ItemIndexID, TValue Item);

public sealed record SelectGroup<TValue>
{
    public required string Name { get; init; }
    public required int Index { get; init; }
    public IEnumerable<SelectItem<TValue>> Items { get; init; }
    public bool IsExpanded { get; init; }

    internal SelectGroup()
    {
    }
};


public sealed record SelectItem<TValue>
{
    public required string Name { get; init; }
    public required TValue Value { get; init; }
    public required int Index { get; init; }

    internal SelectItem()
    {
    }
}






