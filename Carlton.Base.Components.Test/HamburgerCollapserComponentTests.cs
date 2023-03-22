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

    [Fact]
    public void HamburgerCollapser_IsCollapsedParam_False_ClickEvent_ShouldCollapse()
    {
        //Arrange
        var layoutState = new LayoutState(false, () => { });

        var cut = RenderComponent<HamburgerCollapser>(paramaters => paramaters
            .AddCascadingValue(layoutState));

        var ele = cut.Find(".collapser a");

        //Act
        ele.Click();

        //Assert
        Assert.True(layoutState.IsCollapsed);
    }

    [Fact]
    public void HamburgerCollapser_IsCollapsedParam_True_ClickEvent_ShouldExpand()
    {
        //Arrange
        var layoutState = new LayoutState(true, () => { });

        var cut = RenderComponent<HamburgerCollapser>(paramaters => paramaters
            .AddCascadingValue(layoutState));

        var ele = cut.Find(".collapser a");

        //Act
        ele.Click();

        //Assert
        Assert.False(layoutState.IsCollapsed);
    }
}
