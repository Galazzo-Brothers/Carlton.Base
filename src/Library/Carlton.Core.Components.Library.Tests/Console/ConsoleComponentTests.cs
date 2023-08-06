namespace Carlton.Core.Components.Library.Tests;


[Trait("Component", nameof(Console))]
public class ConsoleComponentTests : TestContext
{
    private static readonly string ConsoleMarkup = @"
<div class=""console"" b-26bfns5jah>
    <textarea rows=""15"" class="""" value=""this is some dummy text"" blazor:onchange=""1"" b-26bfns5jah></textarea>
</div>";


    [Fact(DisplayName = "Markup Test")]
    public void Console_Markup_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<Console>(parameters => parameters
            .Add(p => p.IsReadOnly, false)
            .Add(p => p.Text, "this is some dummy text")
            );

        //Assert
        cut.MarkupMatches(ConsoleMarkup);
    }

    [Theory(DisplayName = "ReadOnly Parameter Test")]
    [InlineData(true)]
    [InlineData(false)]
    public void Console_ReadOnlyParam_RendersCorrectly(bool isReadOnly)
    {
        //Act
        var cut = RenderComponent<Console>(parameters => parameters
            .Add(p => p.IsReadOnly, isReadOnly)
            .Add(p => p.Text, "this is some dummy text")
            );

        var consoleElement = cut.Find("textarea");
        var actualIsDisabled = consoleElement.Attributes.Any(_ => _.Name == "disabled");

        //Assert
        Assert.Equal(isReadOnly, actualIsDisabled);
    }

    [Theory(DisplayName = "Text Parameter Test")]
    [InlineData("here is some super special test text")]
    [InlineData("here is some more super special test text")]
    [InlineData("here is event more super special test text")]
    public void Console_TextParam_RendersCorrectly(string expectedText)
    {
        //Act
        var cut = RenderComponent<Console>(parameters => parameters
            .Add(p => p.IsReadOnly, true)
            .Add(p => p.Text, expectedText)
            );

        var consoleElement = cut.Find("textarea");
        var actualText = consoleElement.Attributes.First(_ => _.Name == "value").Value;

        //Assert
        Assert.Equal(expectedText, actualText);
    }


    [Theory(DisplayName = "IsValid Parameter Test")]
    [InlineData(true)]
    [InlineData(false)]
    public void Console_IsValidParam_RendersCorrectly(bool expectedIsValid)
    {
        //Act
        var cut = RenderComponent<Console>(parameters => parameters
            .Add(p => p.IsReadOnly, true)
            .Add(p => p.Text, "Some random text")
            .Add(p => p.IsValid, expectedIsValid)
            );

        var consoleElement = cut.Find("textarea");
        var errorClassExists = consoleElement.ClassList.Contains("error");

        //Assert
        Assert.Equal(expectedIsValid, !errorClassExists);
    }

    [Theory(DisplayName = "OnChangeCallback Parameter Test")]
    [InlineData("new text")]
    [InlineData("some more new text")]
    [InlineData("even more new text")]
    public void Console_OnChangeCallbackParam_FiresCallback(string expectedText)
    {
        //Arrange
        var eventCalled = false;
        var actualText = string.Empty;

        var cut = RenderComponent<Console>(parameters => parameters
            .Add(p => p.IsReadOnly, true)
            .Add(p => p.Text, "some original text")
            .Add(p => p.OnChangeCallback, (str) => { eventCalled = true; actualText = str; })
            );

        var consoleElement = cut.Find("textarea");

        //Act
       consoleElement.Change(new ChangeEventArgs { Value = expectedText });

        //Assert
        Assert.True(eventCalled);
        Assert.Equal(expectedText, actualText);
    }
}
