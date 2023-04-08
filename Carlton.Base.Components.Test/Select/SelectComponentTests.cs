using System.Linq;

namespace Carlton.Base.Components.Test;

public class SelectComponentTests : TestContext
{
    [Fact]
    public void Select_Markup_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<Select>(paramaters => paramaters
            .Add(p => p.Options, SelectTestHelper.Options)
            .Add(p => p.Label, "Test Select")
            .Add(p => p.IsDisabled, false)
            .Add(p => p.SelectedValue, 2)
            );

        //Assert
        cut.MarkupMatches(SelectTestHelper.SelectMarkup);
    }

    [Theory]
    [InlineData("Test Label")]
    [InlineData("Another Test Label")]
    [InlineData("Yet Another Label")]
    public void Select_LabelParam_RendersCorrectly(string labelText)
    {
        //Act
        var cut = RenderComponent<Select>(paramaters => paramaters
            .Add(p => p.Options, SelectTestHelper.Options)
            .Add(p => p.Label, labelText)
            .Add(p => p.IsDisabled, false)
            .Add(p => p.SelectedValue, 2)
            );

        var labelContent = cut.Find(".label").TextContent;

        //Assert
        Assert.Equal(labelText, labelContent);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Select_DisabledParam_RendersCorrectly(bool isDisabled)
    {
        //Act
        var cut = RenderComponent<Select>(paramaters => paramaters
            .Add(p => p.Options, SelectTestHelper.Options)
            .Add(p => p.Label, "Test Label")
            .Add(p => p.IsDisabled, isDisabled)
            .Add(p => p.SelectedValue, 2)
            );

        var optionsElement = cut.Find(".options");
        var containsDisabledAttribute = optionsElement.Attributes.Select(_ => _.Name).Contains("disabled");

        //Assert
        Assert.Equal(isDisabled, containsDisabledAttribute);
    }

    [Theory]
    [MemberData(nameof(SelectTestHelper.GetOptions), MemberType = typeof(SelectTestHelper))]
    public void Select_OptionsParam_OptsCount_RendersCorrectly(IReadOnlyDictionary<string, int> opts)
    {
        //Arrange
        var expectedCount = opts.Count;
        var expectedOptionNames = opts.Keys;
        var expectedValues = opts.Values;

        //Act
        var cut = RenderComponent<Select>(paramaters => paramaters
            .Add(p => p.Options, opts)
            .Add(p => p.Label, "some label text")
            .Add(p => p.IsDisabled, false)
            .Add(p => p.SelectedValue, 2)
            );

        var optionsElements = cut.FindAll(".option");
        var actualCount = optionsElements.Count;
        var actualOptionNames = optionsElements.Select(_ => _.TextContent);
        var actualValues = optionsElements.Select(_ => int.Parse(_.Attributes.First(attri => attri.Name == "blazor:onclick").Value));

        //Assert
        Assert.Equal(expectedCount, actualCount);
        Assert.Equal(expectedOptionNames, actualOptionNames);
        Assert.Equal(expectedValues, actualValues);
    }

    [Fact]
    public void Select_OptionsParam_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<Select>(paramaters => paramaters
            .Add(p => p.Options, SelectTestHelper.Options)
            .Add(p => p.Label, "some label text")
            .Add(p => p.IsDisabled, false)
            .Add(p => p.SelectedValue, 2)
            );

        var optionsElements = cut.FindAll(".option");

        //Assert
        Assert.Collection(optionsElements,
                   item => Assert.Equal("Option 1", item.TextContent),
                   item => Assert.Equal("Option 2", item.TextContent),
                   item => Assert.Equal("Option 3", item.TextContent));
    }

    [Theory]
    [InlineData(1, "Option 1")]
    [InlineData(2, "Option 2")]
    [InlineData(3, "Option 3")]
    public void Select_SelectedValueParam_RendersCorrectly(int selectedValue, string expectedLabel)
    {
        //Act
        var cut = RenderComponent<Select>(paramaters => paramaters
            .Add(p => p.Options, SelectTestHelper.Options)
            .Add(p => p.Label, "some label text")
            .Add(p => p.IsDisabled, false)
            .Add(p => p.SelectedValue, selectedValue)
            );

        var inputElement = cut.Find("input");
        var valueDisplay = inputElement?.Attributes["value"]?.TextContent;

        //Assert
        Assert.Equal(expectedLabel, valueDisplay);
    }

    [Fact]
    public void Select_SelectedValueParam_InvalidDefaultValue_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<Select>(paramaters => paramaters
            .Add(p => p.Options, SelectTestHelper.Options)
            .Add(p => p.Label, "some label text")
            .Add(p => p.IsDisabled, false)
            .Add(p => p.SelectedValue, -1)
            );

        var inputElement = cut.Find("input");
        var containsValueAttribtue = inputElement.Attributes.Any(_ => _.Name == "value");

        //Assert
        Assert.False(containsValueAttribtue);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public void Select_ValueChangedCallbackParam_FiresCallback(int index)
    {
        //Arrange
        var eventFired = false;
        var eventKey = string.Empty;
        var eventValue = -1;

        var expectedKey = SelectTestHelper.Options.Keys.ElementAt(index);
        var expectedValue = SelectTestHelper.Options.Values.ElementAt(index);

        var cut = RenderComponent<Select>(paramaters => paramaters
            .Add(p => p.Options, SelectTestHelper.Options)
            .Add(p => p.Label, "some label text")
            .Add(p => p.IsDisabled, false)
            .Add(p => p.SelectedValue, -1)
            .Add(p => p.ValueChangedCallback, (kvp) => 
                {
                    eventFired = true;
                    eventKey = kvp.Key;
                    eventValue = kvp.Value;
                })
            );

        //Act
        cut.FindAll(".option")[index].Click();



        //Assert
        Assert.True(eventFired);
        Assert.Equal(expectedKey, eventKey);
        Assert.Equal(expectedValue, eventValue);
    }
}

