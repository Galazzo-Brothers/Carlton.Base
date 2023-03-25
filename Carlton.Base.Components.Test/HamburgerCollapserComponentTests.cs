namespace Carlton.Base.Components.Test;

public class HamburgerCollapserCompoentTests : TestContext
{
    private static readonly string HamburgerCollapserMarkup =
    @"<div class=""collapser"" b-uskw0t9tqy><a href=""#"" blazor:onclick=""1"" b-uskw0t9tqy><span class=""mdi mdi-24px mdi-menu"" b-uskw0t9tqy></span></a></div>";


    [Fact]
    public void HamburgerCollapser_Markup_RendersCorrectly()
    {
        //Arrange
        var layoutState = new LayoutState(false, () => { });

        //Act
        var cut = RenderComponent<HamburgerCollapser>(paramaters => paramaters
            .AddCascadingValue<LayoutState>(layoutState));

        //Assert
        cut.MarkupMatches(HamburgerCollapserMarkup);

    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void HamburgerCollapser_ClickEvent_ShouldRenderCorrectly(bool isCollapsed)
    {
        //Arrange
        var layoutState = new LayoutState(isCollapsed, () => { });

        var cut = RenderComponent<HamburgerCollapser>(paramaters => paramaters
            .AddCascadingValue(layoutState));

        var ele = cut.Find(".collapser a");
        var expectedResult = !isCollapsed;

        //Act
        ele.Click();

        //Assert
        Assert.Equal(expectedResult, layoutState.IsCollapsed);
    }
}
