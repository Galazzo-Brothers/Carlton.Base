namespace Carlton.Core.Components.Navigation;

public record ItemExpansionChangeEventArgs(int GroupIndexID, bool IsExpanded);
public record ItemSelectedEventArgs<TValue>(int GroupIndexID, int ItemIndexID, TValue Item);

public record SelectGroup<TValue>
{
    public required string Name { get; init; }
    public required int Index { get; init; }
    public IEnumerable<SelectItem<TValue>> Items { get; init; }
    public bool IsExpanded { get; init; }

    internal SelectGroup()
    {
    }
};


public record SelectItem<TValue>
{
    public required string Name { get; init; }
    public required TValue Value { get; init; }
    public required int Index { get; init; }

    internal SelectItem()
    {
    }
}






