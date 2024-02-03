using Carlton.Core.Components.Buttons;
namespace Carlton.Core.Components.Tests.Buttons;

[Trait("Component", nameof(ActionButton))]
public class ActionButtonComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test"), AutoData]
    public void ActionButton_Markup_RendersCorrectly(string expectedText)
    {
        //Arrange
        var expectedMarkup = @$"<div class=""action-btn""><span>{expectedText}</span></div>";

        //Act
        var cut = RenderComponent<ActionButton>(parameters => parameters
           .Add(p => p.Text, expectedText));

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory(DisplayName = "Text Parameter Test"), AutoData]
    public void ActionButton_TextParameter_RendersCorrectly(string expectedText)
    {
        //Arrange
        var cut = RenderComponent<ActionButton>(parameters => parameters
            .Add(p => p.Text, expectedText));

        //Act
        var btn = cut.Find(".action-btn");

        //Assert
        btn.TextContent.ShouldBe(expectedText);
    }

    [Theory(DisplayName = "Button Click Test"), AutoData]
    public void ActionButton_OnClick_RendersCorrectly(string expectedText)
    {
        //Arrange
        var eventCalled = false;
        var cut = RenderComponent<ActionButton>(parameters => parameters
            .Add(p => p.Text, expectedText)
            .Add(p => p.OnClickCallback, () => { eventCalled = true; }));

        //Act
        cut.Find(".action-btn").Click();

        //Assert
        eventCalled.ShouldBeTrue();
    }
}
