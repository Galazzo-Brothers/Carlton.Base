using AutoFixture.Xunit2;

namespace Carlton.Core.Components.Library.Tests;

[Trait("Component", nameof(FloatingActionButton))]
public class FloatingActionButtonComponentTests : TestContext
{

    [Theory(DisplayName = "Markup Test"), AutoData]
    public void FloatingActionButton_Markup_RendersCorrectly(string icon, int positionBottom, int positionRight)
    {
        //Arrange
        var expectedMarkup = @$"
<div class=""fab mdi mdi-24px mdi-{icon}"" style=""--fab-bottom:{positionBottom}%;--fab-right:{positionRight}%;""></div>";

        //Act
        var cut = RenderComponent<FloatingActionButton>(parameters => parameters
           .Add(p => p.Icon, icon)
           .Add(p => p.PositionBottom, positionBottom)
           .Add(p => p.PositionRight, positionRight)
       );

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory(DisplayName = "Button Click Test"), AutoData]
    public void FloatingActionButton_OnClick_RendersCorrectly(string icon, int positionBottom, int positionRight)
    {
        //Arrange
        var eventCalled = false;
        var cut = RenderComponent<FloatingActionButton>(parameters => parameters
            .Add(p => p.Icon, icon)
            .Add(p => p.PositionBottom, positionBottom)
            .Add(p => p.PositionRight, positionRight)
            .Add(p => p.OnClickCallback, () => { eventCalled = true; })
        );

        //Act
        cut.Find(".fab").Click();

        //Assert
        Assert.True(eventCalled);
    }
}
