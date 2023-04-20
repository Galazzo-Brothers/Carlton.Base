using Microsoft.AspNetCore.Components.Web.Virtualization;
using System.Text.Json.Serialization;

namespace Carlton.Base.Components.TestBed;

internal static class CardTestStates
{
    public static object DefaultState
    {
        get => new
        {
            CardTitle = "Test"
        };
    }

    public static object DefaultListState
    {
        get => new TestListState

            (
                "Shopping List",
                "Low Items",
                new List<string> { "Eggs", "Milk", "Carrots" },
                listFragment
            );
    }

    public record TestListState
    {
        public string CardTitle { get; init; }
        public string SubTitle { get; init; }
        public List<string> Items { get; init; }
        [JsonIgnore]
        public RenderFragment<string> ItemTemplate { get; init; }

        public TestListState(string cardTitle, string subTitle, List<string> items, RenderFragment<string> itemTemplate) =>
            (CardTitle, SubTitle, Items, ItemTemplate) = (cardTitle, subTitle, items, itemTemplate);
    }

    public static object CountCard1State
    {
        get => new
        {
            Count = 7,
            MessageTemplate = "This is a test",
            Icon = "mdi-camera",
            Theme = CountCardTheme.Blue
        };
    }

    public static object CountCard2State
    {
        get => new
        {
            Count = 7,
            MessageTemplate = "This is a test",
            Icon = "mdi-camera",
            Theme = CountCardTheme.Green
        };
    }

    public static object CountCard3State
    {
        get => new
        {
            Count = 7,
            MessageTemplate = "This is a test",
            Icon = "mdi-camera",
            Theme = CountCardTheme.Red
        };
    }

    public static object CountCard4State
    {
        get => new
        {
            Count = 7,
            MessageTemplate = "This is a test",
            Icon = "mdi-camera",
            Theme = CountCardTheme.Purple
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
