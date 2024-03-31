using Carlton.Core.Components.Cards;
namespace Carlton.Core.Components.Lab.TestData;
internal static class CardTestStates
{
	public static readonly string[] listCardItems = ["Eggs", "Milk", "Carrots"];

	public static object DefaultState
	{
		get => new
		{
			CardTitle = "Test"
		};
	}

    public class ListCardState
    {
        public string CardTitle { get; set; }
        public string SubTitle { get; set; }
        public IEnumerable<string> Items { get; set; }
		[JsonIgnore]
		public RenderFragment<string> ItemTemplate { get; set; }
    }



    public static ListCardState DefaultListState
	{
		get => new()
		{
			CardTitle = "Shopping List",
			SubTitle = "Low Items",
			Items = listCardItems,
			ItemTemplate = listFragment
		};
	}

	public static object CountCard1State
	{
		get => new
		{
			Count = 7,
			MessageTemplate = "This is a test",
			Icon = "mdi-camera",
			Theme = CountCardThemes.Blue
		};
	}

	public static object CountCard2State
	{
		get => new
		{
			Count = 7,
			MessageTemplate = "This is a test",
			Icon = "mdi-camera",
			Theme = CountCardThemes.Green
		};
	}

	public static object CountCard3State
	{
		get => new
		{
			Count = 7,
			MessageTemplate = "This is a test",
			Icon = "mdi-camera",
			Theme = CountCardThemes.Red
		};
	}

	public static object CountCard4State
	{
		get => new
		{
			Count = 7,
			MessageTemplate = "This is a test",
			Icon = "mdi-camera",
			Theme = CountCardThemes.Purple
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
