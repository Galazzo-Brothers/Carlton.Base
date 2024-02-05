using Carlton.Core.Components.Cards;

namespace Carlton.Core.Components.Library.Lab.TestData;
internal static class CardTestStates
{
    public static Dictionary<string, object> DefaultState
    {
        get => new()
        {
            { nameof(Card.CardTitle), "Test" }
        };
    }

    internal static readonly string[] listCardItems = ["Eggs", "Milk", "Carrots"];
    public static Dictionary<string, object> DefaultListState
    {
        get => new()
        {
            { nameof(ListCard<int>.CardTitle), "Shopping List" },
            { nameof(ListCard<int>.SubTitle),  "Low Items" },
            { nameof(ListCard<int>.Items), listCardItems },
            { nameof(ListCard<int>.ItemTemplate), listFragment }
        };
    }

    public static Dictionary<string, object> CountCard1State
    {
        get => new()
        {
            { nameof(CountCard.Count),  7 },
            { nameof(CountCard.MessageTemplate), "This is a test" },
            { nameof(CountCard.Icon), "mdi-camera" },
            { nameof(CountCard.Theme),  CountCardTheme.Blue }
        };
    }

    public static Dictionary<string, object> CountCard2State
    {
        get => new()
        {
            { nameof(CountCard.Count), 7 },
            { nameof(CountCard.MessageTemplate), "This is a test" },
            { nameof(CountCard.Icon), "mdi-camera" },
            { nameof(CountCard.Theme), CountCardTheme.Green }
        };
    }

    public static Dictionary<string, object> CountCard3State
    {
        get => new()
        {
            { nameof(CountCard.Count), 7 },
            { nameof(CountCard.MessageTemplate), "This is a test" },
            { nameof(CountCard.Icon), "mdi-camera" },
            { nameof(CountCard.Theme), CountCardTheme.Red }
        };
    }

    public static Dictionary<string, object> CountCard4State
    {
        get => new()
        {
            { nameof(CountCard.Count), 7 },
            { nameof(CountCard.MessageTemplate), "This is a test" },
            { nameof(CountCard.Icon), "mdi-camera" },
            { nameof(CountCard.Theme), CountCardTheme.Purple }
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
