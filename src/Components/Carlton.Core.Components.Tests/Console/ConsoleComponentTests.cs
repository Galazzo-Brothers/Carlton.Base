using Console = Carlton.Core.Components.Consoles.Console;
namespace Carlton.Core.Components.Tests;


[Trait("Component", nameof(Console))]
public class ConsoleComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test")]
    [InlineAutoData(true)]
    [InlineAutoData(false)]
    public void Console_Markup_RendersCorrectly(bool expectedIsReadOnly, string expectedText)
    {
        //Arrange
        var expectedMarkup = @$"
<div class=""console"">
    <textarea rows=""15"" {(expectedIsReadOnly ? "disabled=\"\"" : string.Empty)} class="""" value=""{expectedText}""></textarea>
</div>";


        //Act
        var cut = RenderComponent<Console>(parameters => parameters
                .Add(p => p.IsReadOnly, expectedIsReadOnly)
                .Add(p => p.Text, expectedText));

        //Assert
        cut.MarkupMatches(string.Format(expectedMarkup, expectedText));
    }

    [Theory(DisplayName = "ReadOnly Parameter Test")]
    [InlineAutoData(true)]
    [InlineAutoData(false)]
    public void Console_ReadOnlyParam_RendersCorrectly(bool expectedIsReadOnly, string expectedText)
    {
        //Act
        var cut = RenderComponent<Console>(parameters => parameters
            .Add(p => p.IsReadOnly, expectedIsReadOnly)
            .Add(p => p.Text, expectedText));

        var consoleElement = cut.Find("textarea");
        var actualIsDisabled = consoleElement.Attributes.Any(_ => _.Name == "disabled");

        //Assert
        actualIsDisabled.ShouldBe(expectedIsReadOnly);
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
        actualText.ShouldBe(expectedText);
    }


    [Theory(DisplayName = "IsValid Parameter Test")]
    [InlineAutoData(true)]
    [InlineAutoData(false)]
    public void Console_IsValidParam_RendersCorrectly(bool expectedIsValid, string expectedText)
    {
        //Act
        var cut = RenderComponent<Console>(parameters => parameters
            .Add(p => p.IsReadOnly, true)
            .Add(p => p.Text, expectedText)
            .Add(p => p.IsValid, expectedIsValid));

        var consoleElement = cut.Find("textarea");
        var errorClassExists = consoleElement.ClassList.Contains("error");

        //Assert
        errorClassExists.ShouldNotBe(expectedIsValid);
    }

    [Theory(DisplayName = "OnChangeCallback Parameter Test"), AutoData]
    public void Console_OnChangeCallbackParam_FiresCallback(string expectedText)
    {
        //Arrange
        var eventCalled = false;
        var actualText = string.Empty;

        var cut = RenderComponent<Console>(parameters => parameters
            .Add(p => p.IsReadOnly, true)
            .Add(p => p.Text, expectedText)
            .Add(p => p.OnChangeCallback, (str) => { eventCalled = true; actualText = str; }));

        var consoleElement = cut.Find("textarea");

        //Act
        consoleElement.Change(new ChangeEventArgs { Value = expectedText });

        //Assert
        eventCalled.ShouldBeTrue();
        actualText.ShouldBe(expectedText);
    }
}
