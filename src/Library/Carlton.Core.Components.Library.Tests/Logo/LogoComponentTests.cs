namespace Carlton.Core.Components.Library.Tests;

[Trait("Component", nameof(Logo))]
public class LogoComponentTests : TestContext
{
    [Fact(DisplayName = "Markup Test")]
    public void Logo_Collapsed_Markup_RendersCorrectly()
    {
        //Arrange
        var layoutState = new LayoutState(true, () => { });
        var expectedMarkup = 
@"<div class=""logo collapsed"">
    <div class=""content"">
        <img src=""_content/Carlton.Core.Components.Library/images/CarltonLogo.png"">
        <span class=""title""></span>
    </div>
</div>";

    //Act
    var cut = RenderComponent<Logo>(parameters => parameters
            .AddCascadingValue(layoutState));

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Fact(DisplayName = "Markup Test")]
    public void Logo_Expanded_Markup_RendersCorrectly()
    {
        //Arrange
        var layoutState = new LayoutState(false, () => { });
        var expectedMarkup =
@"<div class=""logo"">
    <div class=""content"">
        <img src=""_content/Carlton.Core.Components.Library/images/CarltonLogo.png"">
        <span class=""title""></span>
    </div>
</div>";

        //Act
        var cut = RenderComponent<Logo>(parameters => parameters
                .AddCascadingValue(layoutState));

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory(DisplayName = "IsCollapsed Parameter Test")]
    [InlineData(true)]
    [InlineData(false)]
    public void Logo_IsCollapsedParam_ShouldBeExpanded(bool isCollapsed)
    {
        //Arrange
        var layoutState = new LayoutState(isCollapsed, () => { });

        //Act
        var cut = RenderComponent<HamburgerCollapser>(parameters => parameters
            .AddCascadingValue(layoutState));

        //Assert
        Assert.Equal(isCollapsed, layoutState.IsCollapsed);
    }
}
