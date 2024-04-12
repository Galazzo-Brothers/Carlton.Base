using Carlton.Core.Components.Accordion.AccordionSelect;
namespace Carlton.Core.Components.Tests.Navigation;

[Trait("Component", nameof(AccordionSelect<int>))]
public class AccordionSelectComponentTests : TestContext
{
	[Theory(DisplayName = "Markup Test")]
	[InlineAutoData(true)]
	[InlineAutoData(false)]
	public void AccordionSelect_Markup_RendersCorrectly(
		bool expectedIsExpanded,
		string expectedTitle,
		IDictionary<string, int> expectedItems)
	{
		//Arrange
		var selectedIndex = RandomUtilities.GetRandomIndex(expectedItems.Count);
		var items = new AccordionSelectModelBuilder<int>()
						.AddItems(expectedItems)
						.Build();

		//Act
		var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
			.Add(p => p.Title, expectedTitle)
			.Add(p => p.IsExpanded, expectedIsExpanded)
			.Add(p => p.SelectedIndex, selectedIndex)
			.Add(p => p.Items, items));

		//Assert
		cut.MarkupMatches(GenerateExpectedMarkup(expectedTitle, expectedIsExpanded, selectedIndex, items));
	}

	[Theory(DisplayName = "Markup Test No Selection")]
	[InlineAutoData(true)]
	[InlineAutoData(false)]
	public void AccordionSelect_Markup_NoSelection_RendersCorrectly(
	  bool expectedIsExpanded,
	  string expectedTitle,
	  IDictionary<string, int> expectedItems)
	{
		//Arrange
		var selectedIndex = -1;
		var items = new AccordionSelectModelBuilder<int>()
						.AddItems(expectedItems)
						.Build();

		//Act
		var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
			.Add(p => p.Title, expectedTitle)
			.Add(p => p.IsExpanded, expectedIsExpanded)
			.Add(p => p.SelectedIndex, selectedIndex)
			.Add(p => p.Items, items));

		//Assert
		cut.MarkupMatches(GenerateExpectedMarkup(expectedTitle, expectedIsExpanded, selectedIndex, items));
	}

	[Theory(DisplayName = "Title Parameter Test"), AutoData]
	public void AccordionSelect_TitleParameter_RendersCorrectly(string expectedTitle)
	{
		//Act
		var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
			.Add(p => p.Title, expectedTitle));

		var titleElm = cut.Find(".item-group-name");

		//Assert
		titleElm.TextContent.ShouldBe(expectedTitle);
	}

	[Theory(DisplayName = "IsExpanded Parameter Test")]
	[InlineAutoData(false, "mdi-plus-box-outline")]
	[InlineAutoData(true, "mdi-minus-box-outline")]
	public void AccordionSelect_IsExpandedParameter_RendersCorrectly(
		bool expectedIsExpanded,
		string expectedClass,
		string expectedTitle)
	{
		//Act
		var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
			.Add(p => p.Title, expectedTitle)
			.Add(p => p.IsExpanded, expectedIsExpanded));

		var itemContainerElement = cut.Find(".item-container");
		var accordionContainerElement = cut.Find(".accordion-icon-btn");
		var expectedAccordionValue = !expectedIsExpanded;

		var isCollapsedClassPresent = itemContainerElement.ClassList.Contains("collapsed");

		//Assert
		isCollapsedClassPresent.ShouldBe(expectedAccordionValue);
		accordionContainerElement.ClassList.ShouldContain(expectedClass);
	}

	[Theory(DisplayName = "Items Parameter Test"), AutoData]
	public void AccordionSelect_ItemsParameter_RendersCorrectly(
		IDictionary<string, int> expectedItems,
		string expectedTitle,
		bool expectedIsExapnded)
	{
		//Arrange
		var items = new AccordionSelectModelBuilder<int>()
						.AddItems(expectedItems)
						.Build();

		var expectedCount = items.Count();
		var expectedItemNames = items.Select(_ => _.Name);

		//Act
		var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
				.Add(p => p.Title, expectedTitle)
				.Add(p => p.IsExpanded, expectedIsExapnded)
				.Add(p => p.Items, items));

		var actualCount = cut.FindAll(".item").Count;
		var actualItemNames = cut.FindAll(".item-name").Select(_ => _.TextContent);


		//Assert
		actualCount.ShouldBe(expectedCount);
		actualItemNames.ShouldBe(expectedItemNames);
	}

	[Theory(DisplayName = "SelectedIndex Parameter Test"), AutoData]
	public void AccordionSelect_SelectedIndexParameter_RendersCorrectly(
		IDictionary<string, int> expectedItems,
		string expectedTitle)
	{
		//Arrange
		var selectedIndex = RandomUtilities.GetRandomIndex(expectedItems.Count);
		var items = new AccordionSelectModelBuilder<int>()
						.AddItems(expectedItems)
						.Build();

		//Act
		var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
			.Add(p => p.Title, expectedTitle)
			.Add(p => p.IsExpanded, true)
			.Add(p => p.Items, items)
			.Add(p => p.SelectedIndex, selectedIndex));

		var itemElms = cut.FindAll(".item");
		var selectedItem = itemElms[selectedIndex];

		//Assert
		selectedItem.ClassList.ShouldContain("selected");
	}

	[Theory(DisplayName = "SelectedValue Property Test"), AutoData]
	public void AccordionSelect_SelectedValueProperty_RendersCorrectly(
		IDictionary<string, int> expectedItems,
		string expectedTitle)
	{
		//Arrange
		var selectedIndex = RandomUtilities.GetRandomIndex(expectedItems.Count);
		var items = new AccordionSelectModelBuilder<int>()
						.AddItems(expectedItems)
						.Build();

		var expectedValue = items.ElementAt(selectedIndex).Value;

		//Act
		var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
			.Add(p => p.Title, expectedTitle)
			.Add(p => p.IsExpanded, true)
			.Add(p => p.Items, items)
			.Add(p => p.SelectedIndex, selectedIndex));

		//Assert
		cut.Instance.SelectedValue.ShouldBe(expectedValue);
	}

	[Theory(DisplayName = "SelectedIndex Parameter Not Provided Render Test"), AutoData]
	public void AccordionSelect_SelectedIndexParameterNotProvided_RendersCorrectly(
		IDictionary<string, int> expectedItems,
		string expectedTitle,
		bool expectedIsExpanded)
	{
		//Arrange
		var items = new AccordionSelectModelBuilder<int>()
						.AddItems(expectedItems)
						.Build();

		//Act
		var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
			.Add(p => p.Title, expectedTitle)
			.Add(p => p.IsExpanded, expectedIsExpanded)
			.Add(p => p.Items, items));

		var itemElms = cut.FindAll(".accordion-header");
		var classList = itemElms.SelectMany(_ => _.ClassList);

		//Assert
		classList.ShouldNotContain("selected");
	}


	[Theory(DisplayName = "SelectedIndex Parameter Not Provided Value Test"), AutoData]
	public void AccordionSelect_InvalidSelectedIndexParameter_ShouldThrowArgumentException(
		IDictionary<string, int> expectedItems,
		string expectedTitle,
		bool expectedIsExpanded)
	{
		//Arrange
		var selectedIndex = expectedItems.Count + 10; //Index out of range
		var items = new AccordionSelectModelBuilder<int>()
						.AddItems(expectedItems)
						.Build();

		//Act
		IRenderedComponent<AccordionSelect<int>> act() => RenderComponent<AccordionSelect<int>>(parameters => parameters
			.Add(p => p.Title, expectedTitle)
			.Add(p => p.IsExpanded, expectedIsExpanded)
			.Add(p => p.SelectedIndex, selectedIndex)
			.Add(p => p.Items, items));

		//Assert
		Should.Throw<ArgumentException>((Func<IRenderedComponent<AccordionSelect<int>>>)act, "The SelectedIndex parameter value is invalid.");
	}

	[Theory(DisplayName = "SelectedItemChanged Callback Parameter Test"), AutoData]
	public void AccordionSelect_SelectedItemChangedParameter_FiresCallback(
		IDictionary<string, int> expectedItems,
		string expectedTitle)
	{
		//Arrange
		var selectedIndex = RandomUtilities.GetRandomIndex(expectedItems.Count);
		var items = new AccordionSelectModelBuilder<int>()
						.AddItems(expectedItems)
						.Build();
		var eventCalled = false;
		AccordionSelectModel<int>? selectedItem = null;
		var expectedValue = items.ElementAt(selectedIndex).Value;

		var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
			.Add(p => p.Title, expectedTitle)
			.Add(p => p.IsExpanded, true)
			.Add(p => p.Items, items)
			.Add(p => p.OnItemSelected, (selected) => { eventCalled = true; selectedItem = selected; }));

		var itemElms = cut.FindAll(".item");
		var selectedElement = itemElms[selectedIndex];

		//Act
		selectedElement.Click();

		//Assert
		eventCalled.ShouldBeTrue();
		selectedItem.ShouldNotBeNull();
		selectedItem.Value.ShouldBe(expectedValue);
	}


	[Theory(DisplayName = "Item Click Value Test"), AutoData]
	public void AccordionSelect_ItemClick_RendersCorrectly(
		IDictionary<string, int> expectedItems,
		string expectedTitle)
	{
		//Arrange
		var selectedIndex = RandomUtilities.GetRandomIndex(expectedItems.Count);
		var items = new AccordionSelectModelBuilder<int>()
						.AddItems(expectedItems)
						.Build();

		var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
			.Add(p => p.Title, expectedTitle)
			.Add(p => p.IsExpanded, true)
			.Add(p => p.Items, items));
		var itemElms = cut.FindAll(".item");
		var selectedItem = itemElms[selectedIndex];

		//Act
		selectedItem.Click();

		//Assert
		itemElms = cut.FindAll(".item");
		selectedItem = itemElms[selectedIndex];
		selectedItem.ClassList.ShouldContain("selected");
	}

	[Theory(DisplayName = "Item Click Value Test"), AutoData]
	public void AccordionSelect_ItemClick_SetsValueCorrectly(
		IDictionary<string, int> expectedItems,
		string expectedTitle)
	{
		//Arrange
		var selectedIndex = RandomUtilities.GetRandomIndex(expectedItems.Count);
		var items = new AccordionSelectModelBuilder<int>()
						.AddItems(expectedItems)
						.Build();
		var selectedValue = items.ElementAt(selectedIndex).Value;

		var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
			.Add(p => p.Title, expectedTitle)
			.Add(p => p.IsExpanded, true)
			.Add(p => p.Items, items));


		var itemElms = cut.FindAll(".item");
		var selectedItem = itemElms[selectedIndex];

		//Act
		selectedItem.Click();

		//Assert
		cut.Instance.SelectedValue.ShouldBe(selectedValue);
	}


	private static string GenerateExpectedMarkup(string title, bool isExpanded, int selectedIndex, IEnumerable<AccordionSelectModel<int>> items)
	{
		var iconType = isExpanded ? "minus" : "plus";
		var itemMarkup = string.Join(Environment.NewLine, items
			.Select(item => $@"
                    <div class=""item {(item.Index == selectedIndex ? "selected" : string.Empty)}"">
                        <span class=""icon mdi mdi-icon mdi-12px mdi-bookmark""></span>
                        <span class=""item-name"">{item.Name}</span>
                    </div>
                "));

		return $@"
                <div class=""accordion-select"">
                    <div class=""content"">
                        <div class=""accordion-header {(selectedIndex != -1 ? "selected" : string.Empty)}"">
                            <span class=""accordion-icon-btn mdi mdi-icon mdi-24px mdi-{iconType}-box-outline""></span>
                            <span class=""item-group-name"">{title}</span>
                        </div>
                        <div class=""item-container {(isExpanded ? string.Empty : "collapsed")}"">
                            {itemMarkup}
                        </div>
                    </div>
                </div>
            ";
	}
}






