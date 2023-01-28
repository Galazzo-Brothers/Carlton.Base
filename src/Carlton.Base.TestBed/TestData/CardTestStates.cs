using Microsoft.AspNetCore.Components;

namespace Carlton.Base.Components.TestStates;

public static class CardTestStates
{
    public static Dictionary<string, object> DefaultState()
    {
        return new Dictionary<string, object>
        {
            { "CardTitle", "Test"}
        };
    }

    public static Dictionary<string, object> DefaultListState()
    {
        return new Dictionary<string, object>
        {
            { "CardTitle", "Shopping List"},
            { "SubTitle", "Low Items" },
            { "Items", new List<string> {"Eggs", "Milk", "Carrots"} },
            { "ItemTemplate", listFragment }
        };
    }

    public static Dictionary<string, object> CountCard1State()
    {
        return new Dictionary<string, object>
        {
            { "Count", 7 },
            { "MessageTemplate", "This is a test" },
            { "Icon", "mdi-camera" },
            { "AccentColorClass", "accent-color-1" }
        };
    }

    public static Dictionary<string, object> CountCard2State()
    {
        return new Dictionary<string, object>
        {
            { "Count", 7 },
            { "MessageTemplate", "This is a test" },
            { "Icon", "mdi-camera" },
            { "AccentColorClass", "accent-color-2" }
        };
    }

    public static Dictionary<string, object> CountCard3State()
    {
        return new Dictionary<string, object>
        {
            { "Count", 7 },
            { "MessageTemplate", "This is a test" },
            { "Icon", "mdi-camera" },
            { "AccentColorClass", "accent-color-3" }
        };
    }

    public static Dictionary<string, object> CountCard4State()
    {
        return new Dictionary<string, object>
        {
            { "Count", 7 },
            { "MessageTemplate", "This is a test" },
            { "Icon", "mdi-camera" },
            { "AccentColorClass", "accent-color-4" }
        };
    }

    private static readonly RenderFragment<string> listFragment = (str) =>
        (builder) =>
    {
        builder.OpenElement(1, "div");
        builder.AddContent(2, str);
        builder.CloseElement();
    };
}
