namespace Carlton.Base.Components;


public record SelectGroup<TValue>(string Name, int Index, IEnumerable<SelectItem<TValue>> Items);
public record SelectItem<TValue>(string Name, int Index, TValue Value);

public record SelectItemChangedEvent<TValue>(int GroupIndexID, int ItemIndexID, TValue Item);





