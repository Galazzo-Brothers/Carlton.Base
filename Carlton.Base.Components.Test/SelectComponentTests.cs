namespace Carlton.Base.Components.Test;

public class SelectComponentTests : TestContext
{
    private static readonly string SelectMarkup = @"
 <div class=""select"" b-b4t7b28hd7><input readonly placeholder="" "" value=""Option 2"" b-b4t7b28hd7 />
    <div class=""label"" b-b4t7b28hd7>Test Select</div>
    <div class=""options"" b-b4t7b28hd7>
        <div class=""option"" blazor:onclick=""1"" b-b4t7b28hd7>Option 1</div>
        <div class=""option"" blazor:onclick=""2"" b-b4t7b28hd7>Option 2</div>
        <div class=""option"" blazor:onclick=""3"" b-b4t7b28hd7>Option 3</div>
    </div>
</div>";

    private static readonly IReadOnlyDictionary<string, int> options = new Dictionary<string, int>
    {
        { "Option 1", 1 },
        { "Option 2", 2 },
        { "Option 3", 3 }
    };

    public static IEnumerable<object[]> GetOptions()
    {
        yield return new object[]
           {
                new Dictionary<string, int>
                {
                    { "Item 1", 1 }
                }
           };
        yield return new object[]
           {
               new Dictionary<string, int>
                {
                    { "Item 1", 1 },
                    { "Item 2", 2 }
                }
           };
        yield return new object[]
            {
                new Dictionary<string, int>
                {
                    { "Item 1", 1 },
                    { "Item 2", 2 },
                    { "Item 3", 3 }
                }
            };
    }

    [Fact]
    public void Select_Markup_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<Select>(paramaters => paramaters
            .Add(p => p.Options, options)
            .Add(p => p.Label, "Test Select")
            .Add(p => p.IsDisabled, false)
            .Add(p => p.SelectedValue, 2)
            );

        //Assert
        cut.MarkupMatches(SelectMarkup);
    }

    [Theory]
    [InlineData("Test Label")]
    [InlineData("Another Test Label")]
    [InlineData("Yet Another Label")]
    public void Select_LabelParam_RendersCorrectly(string labelText)
    {
        //Act
        var cut = RenderComponent<Select>(paramaters => paramaters
            .Add(p => p.Options, options)
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
            .Add(p => p.Options, options)
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
    [MemberData(nameof(GetOptions))]
    public void Select_OptionsParam_OptsCount_RendersCorrectly(IReadOnlyDictionary<string, int> opts)
    {
        //Arrange
        var expectedCount = opts.Count;

        //Act
        var cut = RenderComponent<Select>(paramaters => paramaters
            .Add(p => p.Options, opts)
            .Add(p => p.Label, "some label text")
            .Add(p => p.IsDisabled, false)
            .Add(p => p.SelectedValue, 2)
            );

        var optionsElements = cut.FindAll(".option");
        var actualCount = optionsElements.Count;

        //Assert
        Assert.Equal(expectedCount, actualCount);
    }

    [Fact]
    public void Select_OptionsParam_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<Select>(paramaters => paramaters
            .Add(p => p.Options, options)
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
            .Add(p => p.Options, options)
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
            .Add(p => p.Options, options)
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

        var expectedKey = options.Keys.ElementAt(index);
        var expectedValue = options.Values.ElementAt(index);

        var cut = RenderComponent<Select>(paramaters => paramaters
            .Add(p => p.Options, options)
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

