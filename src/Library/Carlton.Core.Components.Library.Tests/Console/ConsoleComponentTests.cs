using AutoFixture.Xunit2;

namespace Carlton.Core.Components.Library.Tests;


[Trait("Component", nameof(Console))]
public class ConsoleComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test")]
    [InlineAutoData(false)]
    public void Console_Markup_RendersCorrectly(bool isReadOnly, string text)
    {
        //Arrange
        var expectedMarkup = @$"
<div class=""console"">
    <textarea rows=""15"" class="""" value=""{text}""></textarea>
</div>";


        //Act
        var cut = RenderComponent<Console>(parameters => parameters
                .Add(p => p.IsReadOnly, isReadOnly)
                .Add(p => p.Text, text));

        //Assert
        cut.MarkupMatches(string.Format(expectedMarkup, text));
    }

    [Theory(DisplayName = "Markup Test")]
    [InlineAutoData(true)]
    public void Console_Disabled_Markup_RendersCorrectly(bool isReadOnly, string text)
    {
        //Arrange
        var expectedMarkup = @$"
<div class=""console"">
    <textarea rows=""15"" {(isReadOnly ? "disabled=\"\"" : string.Empty)} class="""" value=""{text}""></textarea>
</div>";

        //Act
        var cut = RenderComponent<Console>(parameters => parameters
                .Add(p => p.IsReadOnly, isReadOnly)
                .Add(p => p.Text, text));

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory(DisplayName = "ReadOnly Parameter Test")]
    [InlineAutoData(true)]
    [InlineAutoData(false)]
    public void Console_ReadOnlyParam_RendersCorrectly(bool isReadOnly, string text)
    {
        //Act
        var cut = RenderComponent<Console>(parameters => parameters
            .Add(p => p.IsReadOnly, isReadOnly)
            .Add(p => p.Text, text));

        var consoleElement = cut.Find("textarea");
        var actualIsDisabled = consoleElement.Attributes.Any(_ => _.Name == "disabled");

        //Assert
        Assert.Equal(isReadOnly, actualIsDisabled);
    }

    [Theory(DisplayName = "Text Parameter Test"), AutoData]
    public void Console_TextParam_RendersCorrectly(string expectedText)
    {
        //Act
        var cut = RenderComponent<Console>(parameters => parameters
            .Add(p => p.IsReadOnly, true)
            .Add(p => p.Text, expectedText));

        var consoleElement = cut.Find("textarea");
        var actualText = consoleElement.Attributes.First(_ => _.Name == "value").Value;

        //Assert
        Assert.Equal(expectedText, actualText);
    }


    [Theory(DisplayName = "IsValid Parameter Test")]
    [InlineAutoData(true)]
    [InlineAutoData(false)]
    public void Console_IsValidParam_RendersCorrectly(bool expectedIsValid, string text)
    {
        //Act
        var cut = RenderComponent<Console>(parameters => parameters
            .Add(p => p.IsReadOnly, true)
            .Add(p => p.Text, text)
            .Add(p => p.IsValid, expectedIsValid));

        var consoleElement = cut.Find("textarea");
        var errorClassExists = consoleElement.ClassList.Contains("error");

        //Assert
        Assert.Equal(expectedIsValid, !errorClassExists);
    }

    [Theory(DisplayName = "OnChangeCallback Parameter Test"), AutoData]
    public void Console_OnChangeCallbackParam_FiresCallback(string expectedText, string text)
    {
        //Arrange
        var eventCalled = false;
        var actualText = string.Empty;

        var cut = RenderComponent<Console>(parameters => parameters
            .Add(p => p.IsReadOnly, true)
            .Add(p => p.Text, text)
            .Add(p => p.OnChangeCallback, (str) => { eventCalled = true; actualText = str; }));

        var consoleElement = cut.Find("textarea");

        //Act
        consoleElement.Change(new ChangeEventArgs { Value = expectedText });

        //Assert
        Assert.True(eventCalled);
        Assert.Equal(expectedText, actualText);
    }
}
