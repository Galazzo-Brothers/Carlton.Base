using Carlton.Core.Components.Pills;

namespace Carlton.Core.Components.Lab.TestData;

public class PillTestStates
{
    public static Dictionary<string, object> Default
    {
        get => new()
        {
            { nameof(Pill.Text), "This is a pill" }
        };
    }
}
