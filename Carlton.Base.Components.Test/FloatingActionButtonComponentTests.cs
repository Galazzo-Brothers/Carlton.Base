namespace Carlton.Base.Components.Test;

public class FloatingActionButtonComponentTests : TestContext
{
    private static readonly string FloatingActionButtonMarkup = 
    @"<div class=""carlton-fab mdi mdi-24px mdi-delete"" style=""--carlton-fab-bottom:10%;--carlton-fab-right:50%;"" b-f8128unqw5></div>";


    [Fact]
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

    [Fact]
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
        cut.Find(".carlton-fab").Click();

        //Assert
        Assert.True(eventCalled);
    }
}
