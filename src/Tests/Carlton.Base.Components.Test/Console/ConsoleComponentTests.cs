namespace Carlton.Base.Components.Test;

public class ConsoleComponentTests : TestContext
{
    private static readonly string ConsoleMarkup = @"
<div class=""console"" b-26bfns5jah>
    <textarea rows=""15"" class="""" blazor:onchange=""1"" b-26bfns5jah>this is some dummt text</textarea>
</div>";


    [Fact]
    public void Console_Markup_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<Console>(parameters => parameters
            .Add(p => p.IsReadOnly, false)
            .Add(p => p.Text, "this is some dummt text")
            );

        //Assert
        cut.MarkupMatches(ConsoleMarkup);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Console_ReadOnlyParam_RendersCorrectly(bool isReadOnly)
    {
        //Act
        var cut = RenderComponent<Console>(parameters => parameters
            .Add(p => p.IsReadOnly, isReadOnly)
            .Add(p => p.Text, "this is some dummt text")
            );

        var consoleElement = cut.Find("textarea");
        var actualIsDisabled = consoleElement.Attributes.Any(_ => _.Name == "disabled");

        //Assert
        Assert.Equal(isReadOnly, actualIsDisabled);
    }

    [Theory]
    [InlineData("here is some super special test text")]
    [InlineData("here is some more super special test text")]
    [InlineData("here is event more super special test text")]
    public void Console_TextParam_RendersCorrectly(string expectedParam)
    {
        //Act
        var cut = RenderComponent<Console>(parameters => parameters
            .Add(p => p.IsReadOnly, true)
            .Add(p => p.Text, expectedParam)
            );

        var consoleElement = cut.Find("textarea");

        //Assert
        Assert.Equal(expectedParam, consoleElement.TextContent);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void Console_ValidateFuncParam_RendersCorrectly(bool expectedValidationResult)
    {
        //Act
        var cut = RenderComponent<Console>(parameters => parameters
            .Add(p => p.IsReadOnly, true)
            .Add(p => p.Text, "here is some super special test text")
            .Add(p => p.ValidateFunc, (_) => Task.FromResult(expectedValidationResult))
            );

        var consoleElement = cut.Find("textarea");
        var errorClassExists = consoleElement.ClassList.Contains("error");
        var expectedResult = !expectedValidationResult;

        //Assert
        Assert.Equal(expectedResult, errorClassExists);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void Console_OnChangeCallbackParam_FiresCallback(bool expectedValidation)
    {
        //Arrange
        var eventCalled = false;
        ChangeEventArgs changeEventArgs;

        var cut = RenderComponent<Console>(parameters => parameters
            .Add(p => p.IsReadOnly, true)
            .Add(p => p.Text, "here is some super special test text")
            .Add(p => p.ValidateFunc, (_) => Task.FromResult(expectedValidation))
            .Add(p => p.OnChangeCallback, (_) => { eventCalled = true; changeEventArgs = _; })
            );

        var consoleElement = cut.Find("textarea");
        var expectedResult = expectedValidation;

        //Act
       consoleElement.Change(new ChangeEventArgs { Value = "New Value" });

        //Assert
        Assert.Equal(expectedResult, eventCalled);
    }
}
