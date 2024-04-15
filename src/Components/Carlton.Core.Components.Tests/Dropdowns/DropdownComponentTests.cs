using Carlton.Core.Components.Dropdowns;
namespace Carlton.Core.Components.Tests.Dropdowns;

[Trait("Component", nameof(Dropdown<int>))]
public class DropdownComponentTests : TestContext
{
	[Theory(DisplayName = "Markup Test")]
	[InlineAutoData(true)]
	[InlineAutoData(false)]
	public void Dropdown_Markup_RendersCorrectly(
		bool expectedIsDisabled,
		IReadOnlyDictionary<string, int> expectedItems,
		string expectedLabelText,
		bool expectedIsPristineEnabled)
	{
		//Arrange
		var expectedIndex = RandomUtilities.GetRandomIndex(expectedItems.Count);
		var (selectedKey, selectedValue) = expectedItems.ElementAt(expectedIndex);
		var expectedMarkup = BuildExpectedMarkup(expectedLabelText, selectedKey, expectedIsDisabled, expectedIsPristineEnabled, expectedItems);

		//Act
		var cut = RenderComponent<Dropdown<int>>(parameters => parameters
			.Add(p => p.Options, expectedItems)
			.Add(p => p.Label, expectedLabelText)
			.Add(p => p.IsDisabled, expectedIsDisabled)
			.Add(p => p.InitialSelectedIndex, expectedIndex)
			.Add(p => p.IsPristineEnabled, expectedIsPristineEnabled));

		//Assert
		cut.MarkupMatches(expectedMarkup);
	}

	[Theory(DisplayName = "Label Parameter Test"), AutoData]
	public void Dropdown_LabelParameter_RendersCorrectly(
		IReadOnlyDictionary<string, int> expectedItems,
		string expectedLabelText,
		bool expectedIsDisabled,
		bool expectedIsPristineEnabled)
	{
		//Arrange
		var expectedIndex = RandomUtilities.GetRandomIndex(expectedItems.Count);

		//Act
		var cut = RenderComponent<Dropdown<int>>(parameters => parameters
			.Add(p => p.Options, expectedItems)
			.Add(p => p.Label, expectedLabelText)
			.Add(p => p.IsDisabled, expectedIsDisabled)
			.Add(p => p.InitialSelectedIndex, expectedIndex)
			.Add(p => p.IsPristineEnabled, expectedIsPristineEnabled));

		var labelContent = cut.Find(".label").TextContent;

		//Assert
		labelContent.ShouldBe(expectedLabelText);
	}

	[Theory(DisplayName = "Disabled Parameter Test")]
	[InlineAutoData(true)]
	[InlineAutoData(false)]
	public void Dropdown_DisabledParameter_RendersCorrectly(
		bool expectedIsDisabled,
		IReadOnlyDictionary<string, int> expectedItems,
		string expectedLabelText,
		bool expectedIsPristineEnabled)
	{
		//Arrange
		var expectedIndex = RandomUtilities.GetRandomIndex(expectedItems.Count);

		//Act
		var cut = RenderComponent<Dropdown<int>>(parameters => parameters
			.Add(p => p.Options, expectedItems)
			.Add(p => p.Label, expectedLabelText)
			.Add(p => p.IsDisabled, expectedIsDisabled)
			.Add(p => p.InitialSelectedIndex, expectedIndex)
			.Add(p => p.IsPristineEnabled, expectedIsPristineEnabled));

		var optionsElement = cut.Find(".options");
		var optionsElementAttributes = optionsElement.Attributes.Select(_ => _.Name);
		var containsDisabledClass = optionsElementAttributes.Contains("disabled");

		//Assert
		containsDisabledClass.ShouldBe(expectedIsDisabled);
	}

	[Theory(DisplayName = "Options Parameter Count Tests"), AutoData]
	public void Dropdown_OptionsParameterCount_RendersCorrectly(
		IReadOnlyDictionary<string, int> expectedItems,
		string expectedLabelText)
	{
		//Arrange
		var expectedIndex = RandomUtilities.GetRandomIndex(expectedItems.Count);
		var expectedCount = expectedItems.Count;
		var expectedOptionNames = expectedItems.Keys;

		//Act
		var cut = RenderComponent<Dropdown<int>>(parameters => parameters
			.Add(p => p.Options, expectedItems)
			.Add(p => p.Label, expectedLabelText)
			.Add(p => p.IsDisabled, false)
			.Add(p => p.InitialSelectedIndex, expectedIndex));

		var optionsElements = cut.FindAll(".option");
		var actualCount = optionsElements.Count;

		//Assert
		actualCount.ShouldBe(expectedCount);
	}

	[Theory(DisplayName = "Selected Options Parameter Render Test"), AutoData]
	public void Dropdown_OptionsParameter_RendersCorrectly(
		IReadOnlyDictionary<string, int> expectedItems,
		string expectedLabelText)
	{
		//Arrange
		var expectedIndex = RandomUtilities.GetRandomIndex(expectedItems.Count);

		//Act
		var cut = RenderComponent<Dropdown<int>>(parameters => parameters
			.Add(p => p.Options, expectedItems)
			.Add(p => p.Label, expectedLabelText)
			.Add(p => p.IsDisabled, false)
			.Add(p => p.InitialSelectedIndex, expectedIndex));

		var optionsElements = cut.FindAll(".option");
		var actualContent = optionsElements.Select(_ => _.TextContent);

		//Assert
		optionsElements.Count.ShouldBe(expectedItems.Count);
		actualContent.ShouldBe(expectedItems.Keys);
	}

	[Theory(DisplayName = "SelectedIndex Parameter Test"), AutoData]
	public void Dropdown_SelectedIndexParameter_Pristine_RendersCorrectly(
		IReadOnlyDictionary<string, int> expectedItems,
		string expectedLabelText)
	{
		//Arrange
		var expectedIndex = RandomUtilities.GetRandomIndex(expectedItems.Count);
		var (selectedKey, selectedValue) = expectedItems.ElementAt(expectedIndex);

		//Act
		var cut = RenderComponent<Dropdown<int>>(parameters => parameters
			.Add(p => p.Options, expectedItems)
			.Add(p => p.Label, expectedLabelText)
			.Add(p => p.IsDisabled, false)
			.Add(p => p.InitialSelectedIndex, expectedIndex)
			.Add(p => p.IsPristineEnabled, true));

		var inputElement = cut.Find("input");
		var valueDisplay = inputElement?.Attributes["value"]?.TextContent;

		//Assert
		valueDisplay.ShouldBeNull();
	}

	[Theory(DisplayName = "SelectedIndex Parameter Test"), AutoData]
	public void Dropdown_SelectedIndexParameter_NotPristine_RendersCorrectly(
	   IReadOnlyDictionary<string, int> expectedItems,
	   string expectedLabelText)
	{
		//Arrange
		var expectedIndex = RandomUtilities.GetRandomIndex(expectedItems.Count);
		var (selectedKey, selectedValue) = expectedItems.ElementAt(expectedIndex);

		//Act
		var cut = RenderComponent<Dropdown<int>>(parameters => parameters
			.Add(p => p.Options, expectedItems)
			.Add(p => p.Label, expectedLabelText)
			.Add(p => p.IsDisabled, false)
			.Add(p => p.InitialSelectedIndex, expectedIndex)
			.Add(p => p.IsPristineEnabled, false));

		var inputElement = cut.Find("input");
		var valueDisplay = inputElement?.Attributes["value"]?.TextContent;

		//Assert
		valueDisplay.ShouldBe(selectedKey);
	}

	[Theory(DisplayName = "ValueChangedCallback Parameter Test"), AutoData]
	public void Dropdown_ValueChangedCallbackParameter_FiresCallback(
		IReadOnlyDictionary<string, int> expectedItems,
		string expectedLabelText)
	{
		//Arrange
		var expectedIndex = RandomUtilities.GetRandomIndex(expectedItems.Count);
		var eventFired = false;
		var eventKey = string.Empty;
		var eventValue = -1;
		var (expectedKey, expectedValue) = expectedItems.ElementAt(expectedIndex);

		var cut = RenderComponent<Dropdown<int>>(parameters => parameters
		   .Add(p => p.Options, expectedItems)
		   .Add(p => p.Label, expectedLabelText)
		   .Add(p => p.IsDisabled, false)
		   .Add(p => p.InitialSelectedIndex, expectedIndex)
		   .Add(p => p.OnValueChange, (args) =>
				{
					eventFired = true;
					eventKey = args.SelectedKey;
					eventValue = args.SelectedValue;
				}));

		//Act
		cut.FindAll(".option")[expectedIndex].Click();

		//Assert
		eventFired.ShouldBeTrue();
		eventKey.ShouldBe(expectedKey);
		eventValue.ShouldBe(expectedValue);
	}


	[Theory(DisplayName = "Default Selected Value Parameter Test"), AutoData]
	public void Dropdown_SelectedValueParameter_InvalidDefaultValue_ThrowsArgumentException(
	   IReadOnlyDictionary<string, int> expectedItems,
	   string expectedLabelText,
	   bool expectedIsPristineEnabled)
	{
		//Arrange
		var expectedIndex = RandomUtilities.GetRandomIndex(expectedItems.Count);
		IRenderedComponent<Dropdown<int>> act() => RenderComponent<Dropdown<int>>(parameters => parameters
			  .Add(p => p.Options, expectedItems)
			  .Add(p => p.Label, expectedLabelText)
			  .Add(p => p.IsDisabled, false)
			  .Add(p => p.InitialSelectedIndex, -1)
			  .Add(p => p.IsPristineEnabled, expectedIsPristineEnabled));

		//Assert
		Should.Throw<ArgumentException>(act, "SelctedIndex must be greater than 0 and less than the options count");
	}


	private static string BuildExpectedMarkup(string labelText, string selectedItem, bool isDisabled, bool isPristineEnabled, IReadOnlyDictionary<string, int> options)
	{
		var optionsMarkup = isDisabled ? string.Empty : string.Join(Environment.NewLine, options.Select(_ =>
			$"<div class=\"option\">{_.Key}</div>"
		));

		return
		@$"
        <div class=""dropdown""><input readonly class=""dropdown-input {(isDisabled ? "disabled" : string.Empty)}"" placeholder="" "" value=""{(isPristineEnabled ? null : selectedItem)}"" />
            <div class=""label"">{labelText}</div>
            <div {(isDisabled ? "disabled = \"\"" : string.Empty)} class=""options"">
                {optionsMarkup}
            </div>
        </div>";
	}
}

