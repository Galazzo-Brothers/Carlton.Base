namespace Carlton.Base.Components;

public record AccordionSelectGroupModel<TItem>(string GroupName, Dictionary<int, TItem> GroupItems);
