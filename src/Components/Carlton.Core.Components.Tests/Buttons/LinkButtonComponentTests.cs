using Carlton.Core.Components.Buttons;
namespace Carlton.Core.Components.Tests.Buttons;

[Trait("Component", nameof(LinkButton))]
public class LinkButtonComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test"), AutoData]
    public void LinkButton_Markup_RendersCorrectly(string expectedText)
    {
        //Arrange
        var expectedMarkup = @$"<div class=""link-btn""><span>{expectedText}</span></div>";

        //Act
        var cut = RenderComponent<LinkButton>(parameters => parameters
           .Add(p => p.Text, expectedText));

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }


    [Theory(DisplayName = "Text Parameter Test"), AutoData]
    public void LinkButton_TextParameter_RendersCorrectly(string expectedText)
    {
        //Arrange
        var cut = RenderComponent<LinkButton>(parameters => parameters
            .Add(p => p.Text, expectedText));

        //Act
        var btn = cut.Find(".link-btn");

        //Assert
        btn.TextContent.ShouldBe(expectedText);
    }

    [Theory(DisplayName = "Button Click Test"), AutoData]
    public void LinkButton_OnClick_RendersCorrectly(string expectedText)
    {
        //Arrange
        var eventCalled = false;
        var cut = RenderComponent<LinkButton>(parameters => parameters
            .Add(p => p.Text, expectedText)
            .Add(p => p.OnClick, () => { eventCalled = true; }));

        //Act
        cut.Find(".link-btn").Click();

        //Assert
        eventCalled.ShouldBeTrue();
    }
}
