using Carlton.Core.Components.Dropdowns;
namespace Carlton.Core.Components.Tests;

[Trait("Component", nameof(Dropdown))]
public class DropdownComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test")]
    [InlineAutoData(true)]
    [InlineAutoData(false)]
    public void Dropdown_Markup_RendersCorrectly(
        bool expectedIsDisabled,
        IReadOnlyDictionary<string, int> expectedItems,
        string expectedLabelText)
    {
        //Arrange
        var index = RandomUtilities.GetRandomIndex(expectedItems.Count);
        var selectedItem = expectedItems.ElementAt(index);
        var selectedValue = selectedItem.Value;
        var expectedMarkup = BuildExpectedMarkup(expectedLabelText, selectedItem.Key, expectedIsDisabled, expectedItems);

        //Act
        var cut = RenderComponent<Dropdown>(parameters => parameters
            .Add(p => p.Options, expectedItems)
            .Add(p => p.Label, expectedLabelText)
            .Add(p => p.IsDisabled, expectedIsDisabled)
            .Add(p => p.SelectedValue, selectedValue));

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory(DisplayName = "Label Parameter Test"), AutoData]
    public void Dropdown_LabelParameter_RendersCorrectly(
        IReadOnlyDictionary<string, int> expectedItems,
        string expectedLabelText,
        bool expectedIsDisabled,
        int expectedSelectedValue)
    {
        //Act
        var cut = RenderComponent<Dropdown>(parameters => parameters
            .Add(p => p.Options, expectedItems)
            .Add(p => p.Label, expectedLabelText)
            .Add(p => p.IsDisabled, expectedIsDisabled)
            .Add(p => p.SelectedValue, expectedSelectedValue));

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
        int expectedSelectedValue)
    {
        //Act
        var cut = RenderComponent<Dropdown>(parameters => parameters
            .Add(p => p.Options, expectedItems)
            .Add(p => p.Label, expectedLabelText)
            .Add(p => p.IsDisabled, expectedIsDisabled)
            .Add(p => p.SelectedValue, expectedSelectedValue));

        var optionsElement = cut.Find(".options");
        var containsDisabledAttribute = optionsElement.Attributes.Select(_ => _.Name).Contains("disabled");

        //Assert
        containsDisabledAttribute.ShouldBe(expectedIsDisabled);
    }

    [Theory(DisplayName = "Options Parameter Tests"), AutoData]
    public void Dropdown_OptionsParameterCount_RendersCorrectly(
        IReadOnlyDictionary<string, int> expectedItems,
        string expectedLabelText,
        int expectedSelectedValue)
    {
        //Arrange
        var expectedCount = expectedItems.Count;
        var expectedOptionNames = expectedItems.Keys;

        //Act
        var cut = RenderComponent<Dropdown>(parameters => parameters
            .Add(p => p.Options, expectedItems)
            .Add(p => p.Label, expectedLabelText)
            .Add(p => p.IsDisabled, false)
            .Add(p => p.SelectedValue, expectedSelectedValue));

        var optionsElements = cut.FindAll(".option");
        var actualCount = optionsElements.Count;

        //Assert
        actualCount.ShouldBe(expectedCount);
    }

    [Theory(DisplayName = "Selected Options Parameter Render Test"), AutoData]
    public void Dropdown_OptionsParameter_RendersCorrectly(
        IReadOnlyDictionary<string, int> expectedItems,
        string expectedLabelText,
        int expectedSelectedValue)
    {
        //Act
        var cut = RenderComponent<Dropdown>(parameters => parameters
            .Add(p => p.Options, expectedItems)
            .Add(p => p.Label, expectedLabelText)
            .Add(p => p.IsDisabled, false)
            .Add(p => p.SelectedValue, expectedSelectedValue));

        var optionsElements = cut.FindAll(".option");

        //Assert
        var actualElements = optionsElements.Select(_ => _.TextContent);
        optionsElements.Count.ShouldBe(expectedItems.Count);
        actualElements.ShouldBe(expectedItems.Keys);
    }

    [Theory(DisplayName = "SelectedValue Parameter Test"), AutoData]
    public void Dropdown_SelectedValueParameter_RendersCorrectly(
        IReadOnlyDictionary<string, int> expectedItems,
        string expectedLabelText)
    {
        //Arrange
        var index = RandomUtilities.GetRandomIndex(expectedItems.Count);
        var (selectedKey, selectedValue) = expectedItems.ElementAt(index);

        //Act
        var cut = RenderComponent<Dropdown>(parameters => parameters
            .Add(p => p.Options, expectedItems)
            .Add(p => p.Label, expectedLabelText)
            .Add(p => p.IsDisabled, false)
            .Add(p => p.SelectedValue, selectedValue));

        var inputElement = cut.Find("input");
        var valueDisplay = inputElement?.Attributes["value"]?.TextContent;

        //Assert
        valueDisplay.ShouldBe(selectedKey);
    }

    [Theory(DisplayName = "Default Selected Value Parameter Test"), AutoData]
    public void Dropdown_SelectedValueParameter_InvalidDefaultValue_RendersCorrectly(
        IReadOnlyDictionary<string, int> expectedItems,
        string expectedLabelText)
    {
        //Act
        var cut = RenderComponent<Dropdown>(parameters => parameters
              .Add(p => p.Options, expectedItems)
              .Add(p => p.Label, expectedLabelText)
              .Add(p => p.IsDisabled, false)
              .Add(p => p.SelectedValue, -1));

        var inputElement = cut.Find("input");
        var containsValueAttribute = inputElement.Attributes.Any(_ => _.Name == "value");

        //Assert
        containsValueAttribute.ShouldBeFalse();
    }

    [Theory(DisplayName = "ValueChangedCallback Parameter Test"), AutoData]
    public void Dropdown_ValueChangedCallbackParameter_FiresCallback(
        IReadOnlyDictionary<string, int> expectedItems,
        string expectedLabelText,
        int expectedSelectedValue)
    {
        //Arrange
        var index = RandomUtilities.GetRandomIndex(expectedItems.Count);
        var eventFired = false;
        var eventKey = string.Empty;
        var eventValue = -1;

        var (expectedKey, expectedValue) = expectedItems.ElementAt(index);

        var cut = RenderComponent<Dropdown>(parameters => parameters
           .Add(p => p.Options, expectedItems)
           .Add(p => p.Label, expectedLabelText)
           .Add(p => p.IsDisabled, false)
           .Add(p => p.SelectedValue, expectedSelectedValue)
           .Add(p => p.ValueChangedCallback, (args) =>
                {
                    eventFired = true;
                    eventKey = args.SelectedKey;
                    eventValue = args.SelectedValue;
                }));

        //Act
        cut.FindAll(".option")[index].Click();

        //Assert
        eventFired.ShouldBeTrue();
        eventKey.ShouldBe(expectedKey);
        eventValue.ShouldBe(expectedValue);
    }


    private static string BuildExpectedMarkup(string labelText, string selectedItem, bool isDisabled, IReadOnlyDictionary<string, int> options)
    {
        return
@$"
<div class=""dropdown""><input readonly placeholder="" "" value=""{selectedItem}"" />
    <div class=""label"">{labelText}</div>
    <div {(isDisabled ? "disabled = \"\"" : string.Empty)} class=""options"">
        {BuildExpectedOptionsMarkup(isDisabled, options)}
    </div>
</div>";
    }

    private static string BuildExpectedOptionsMarkup(bool isDisabled, IReadOnlyDictionary<string, int> options)
    {
        return isDisabled ? string.Empty : string.Join(Environment.NewLine, options.Select(_ =>
            $"<div class=\"option\">{_.Key}</div>"
        ));
    }
}

