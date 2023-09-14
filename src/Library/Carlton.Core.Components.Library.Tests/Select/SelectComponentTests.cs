using AutoFixture;
using AutoFixture.Xunit2;
using Carlton.Core.Utilities.UnitTesting;

namespace Carlton.Core.Components.Library.Tests;

[Trait("Component", nameof(Select))]
public class SelectComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test")]
    [InlineAutoData(true, 10, 2)]
    [InlineAutoData(false, 10, 2)]
    public void Select_Markup_RendersCorrectly(
        bool isDisabled,
        int numOfItems,
        int selectedIndex,
        string labelText)
    {
        //Arrange
        var kvp = new Fixture().CreateMany<KeyValuePair<string, int>>(numOfItems);
        var items = new Dictionary<string, int>(kvp).AsReadOnly();
        var selectedItem = kvp.ElementAt(selectedIndex).Key;
        var selectedValue = kvp.ElementAt(selectedIndex).Value;
        var expectedMarkup = BuildExpectedMarkup(labelText, selectedItem, isDisabled, items);

        //Act
        var cut = RenderComponent<Select>(parameters => parameters
            .Add(p => p.Options, items)
            .Add(p => p.Label, labelText)
            .Add(p => p.IsDisabled, isDisabled)
            .Add(p => p.SelectedValue, selectedValue));

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory(DisplayName = "Label Parameter Test"), AutoData]
    public void Select_LabelParam_RendersCorrectly(
        IDictionary<string, int> items,
        string labelText,
        bool isDisabled,
        int selectedValue)
    {
        //Act
        var cut = RenderComponent<Select>(parameters => parameters
            .Add(p => p.Options, items.AsReadOnly())
            .Add(p => p.Label, labelText)
            .Add(p => p.IsDisabled, isDisabled)
            .Add(p => p.SelectedValue, selectedValue));

        var labelContent = cut.Find(".label").TextContent;

        //Assert
        Assert.Equal(labelText, labelContent);
    }

    [Theory(DisplayName = "Disabled Parameter Test")]
    [InlineAutoData(true)]
    [InlineAutoData(false)]
    public void Select_DisabledParam_RendersCorrectly(
        bool isDisabled,
        IDictionary<string, int> items,
        string labelText,
        int selectedValue)
    {
        //Act
        var cut = RenderComponent<Select>(parameters => parameters
            .Add(p => p.Options, items.AsReadOnly())
            .Add(p => p.Label, labelText)
            .Add(p => p.IsDisabled, isDisabled)
            .Add(p => p.SelectedValue, selectedValue));

        var optionsElement = cut.Find(".options");
        var containsDisabledAttribute = optionsElement.Attributes.Select(_ => _.Name).Contains("disabled");

        //Assert
        Assert.Equal(isDisabled, containsDisabledAttribute);
    }

    [Theory(DisplayName = "Options Parameter Tests"), AutoData]
    public void Select_OptionsParam_OptsCount_RendersCorrectly(
        IDictionary<string, int> items,
        string labelText,
        int selectedValue)
    {
        //Arrange
        var expectedCount = items.Count;
        var expectedOptionNames = items.Keys;

        //Act
        var cut = RenderComponent<Select>(parameters => parameters
            .Add(p => p.Options, items.AsReadOnly())
            .Add(p => p.Label, labelText)
            .Add(p => p.IsDisabled, false)
            .Add(p => p.SelectedValue, selectedValue));

        var optionsElements = cut.FindAll(".option");
        var actualCount = optionsElements.Count;

        //Assert
        Assert.Equal(expectedCount, actualCount);
    }

    [Theory(DisplayName = "Selected Options Parameter Render Test"), AutoData]
    public void Select_OptionsParam_RendersCorrectly(
        IDictionary<string, int> items,
        string labelText,
        int selectedValue)
    {
        //Act
        var cut = RenderComponent<Select>(parameters => parameters
            .Add(p => p.Options, items.AsReadOnly())
            .Add(p => p.Label, labelText)
            .Add(p => p.IsDisabled, false)
            .Add(p => p.SelectedValue, selectedValue));

        var optionsElements = cut.FindAll(".option");

        //Assert
        Assert.Equal(items.Count, optionsElements.Count);
        Assert.All(optionsElements, (item, i) =>
        {
            Assert.Equal(items.ElementAt(i).Key, item.TextContent);
        });
    }

    [Theory(DisplayName = "SelectedValue Parameter Test")]
    [InlineAutoData(10, 2)]
    [InlineAutoData(2, 0)]
    public void Select_SelectedValueParam_RendersCorrectly(
        int numOfItems,
        int selectedIndex,
        string labelText)
    {
        //Arrange
        var kvp = new Fixture().CreateMany<KeyValuePair<string, int>>(numOfItems);
        var items = new Dictionary<string, int>(kvp);
        var selectedKey = items.Keys.ElementAt(selectedIndex);
        var selectedValue = items.Values.ElementAt(selectedIndex);

        //Act
        var cut = RenderComponent<Select>(parameters => parameters
            .Add(p => p.Options, items.AsReadOnly())
            .Add(p => p.Label, labelText)
            .Add(p => p.IsDisabled, false)
            .Add(p => p.SelectedValue, selectedValue));

        var inputElement = cut.Find("input");
        var valueDisplay = inputElement?.Attributes["value"]?.TextContent;

        //Assert
        Assert.Equal(selectedKey, valueDisplay);
    }

    [Theory(DisplayName = "Default Selected Value Parameter Test"), AutoData]
    public void Select_SelectedValueParam_InvalidDefaultValue_RendersCorrectly(
        IDictionary<string, int> items,
        string labelText)
    {
        //Act
        var cut = RenderComponent<Select>(parameters => parameters
              .Add(p => p.Options, items.AsReadOnly())
              .Add(p => p.Label, labelText)
              .Add(p => p.IsDisabled, false)
              .Add(p => p.SelectedValue, -1));

        var inputElement = cut.Find("input");
        var containsValueAttribute = inputElement.Attributes.Any(_ => _.Name == "value");

        //Assert
        Assert.False(containsValueAttribute);
    }

    [Theory(DisplayName = "ValueChangedCallback Parameter Test"), AutoData]
    public void Select_ValueChangedCallbackParam_FiresCallback(
        IDictionary<string, int> items,
        string labelText,
        int selectedValue)
    {
        //Arrange
        var index = TestingRndUtilities.GetRandomActiveIndex(items.Count);
        var eventFired = false;
        var eventKey = string.Empty;
        var eventValue = -1;

        var expectedKey = items.Keys.ElementAt(index);
        var expectedValue = items.Values.ElementAt(index);

        var cut = RenderComponent<Select>(parameters => parameters
           .Add(p => p.Options, items.AsReadOnly())
           .Add(p => p.Label, labelText)
           .Add(p => p.IsDisabled, false)
           .Add(p => p.SelectedValue, selectedValue)
           .Add(p => p.ValueChangedCallback, (kvp) =>
                {
                    eventFired = true;
                    eventKey = kvp.Key;
                    eventValue = kvp.Value;
                }));

        //Act
        cut.FindAll(".option")[index].Click();

        //Assert
        Assert.True(eventFired);
        Assert.Equal(expectedKey, eventKey);
        Assert.Equal(expectedValue, eventValue);
    }


    private static string BuildExpectedMarkup(string labelText, string selectedItem, bool isDisabled, IDictionary<string, int> options)
    {
        return
@$"
<div class=""select""><input readonly placeholder="" "" value=""{selectedItem}"" />
    <div class=""label"">{labelText}</div>
    <div {(isDisabled ? "disabled = \"\"" : string.Empty)} class=""options"">
        {BuildExpectedOptionsMarkup(isDisabled, options)}
    </div>
</div>";
    }

    private static string BuildExpectedOptionsMarkup(bool isDisabled, IDictionary<string, int> options)
    {
        return isDisabled ? string.Empty : string.Join(Environment.NewLine, options.Select(_ =>
            $"<div class=\"option\">{_.Key}</div>"
        ));
    }
}

