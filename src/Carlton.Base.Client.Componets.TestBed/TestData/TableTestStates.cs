namespace Carlton.Base.Components.TestBed;

public static class TableTestStates
{
    public static Dictionary<string, object> DefaultState
    {
        get => new()
        {
            { "Headings", new List<TableHeadingItem>
                {
                    new TableHeadingItem(nameof(TableTestObject.ID)),
                    new TableHeadingItem(nameof(TableTestObject.DisplayName)),
                    new TableHeadingItem(nameof(TableTestObject.CreatedDate))
                }
            }
        };
    }

    public record TableTestObject(int ID, string DisplayName, DateTime CreatedDate);
}
