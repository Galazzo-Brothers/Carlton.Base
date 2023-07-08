namespace Carlton.Base.Components.Test;

[Trait("Component", nameof(FloatingActionButton))]
public class FloatingActionButtonComponentTests : TestContext
{
    private static readonly string FloatingActionButtonMarkup = 
    @"<div class=""fab mdi mdi-24px mdi-delete"" style=""--fab-bottom:10%;--fab-right:50%;"" b-f8128unqw5></div>";


    [Fact(DisplayName = "Markup Test")]
    public void FloatingActionButton_Markup_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<FloatingActionButton>(parameters => parameters
            .Add(p => p.Icon, "delete")
            .Add(p => p.PositionBottom, 10)
            .Add(p => p.PositionRight, 50)
            );

        //Assert
        cut.MarkupMatches(FloatingActionButtonMarkup);
    }

    [Fact(DisplayName = "Button Click Test")]
    public void FloatingActionButton_OnClick_RendersCorrectly()
    {
        //Arrange
        var eventCalled = false;
        var cut = RenderComponent<FloatingActionButton>(parameters => parameters
            .Add(p => p.Icon, "delete")
            .Add(p => p.PositionBottom, 10)
            .Add(p => p.PositionRight, 50)
            .Add(p => p.OnClickCallback, () => { eventCalled = true; })
        );

        //Act
        cut.Find(".fab").Click();

        //Assert
        Assert.True(eventCalled);
    }
}
