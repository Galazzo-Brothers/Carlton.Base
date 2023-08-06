namespace Carlton.Core.Components.Library.Tests;

[Trait("Component", nameof(Logo))]
public class LogoComponentTests : TestContext
{
    private static readonly string LogoMarkup =
  @"<div class=""logo"" b-2psdcgopyx>
        <div class=""content"" b-2psdcgopyx>
            <img src=""_content/Carlton.Core.Components.Library/images/CarltonLogo.png"" b-2psdcgopyx>
            <span class=""title"" b-2psdcgopyx></span>
        </div>
    </div>";


    [Fact(DisplayName = "Markup Test")]
    public void Logo_Markup_RendersCorrectly()
    {
        //Arrange
        var layoutState = new LayoutState(false, () => { });

        //Act
        var cut = RenderComponent<Logo>(parameters => parameters
            .AddCascadingValue(layoutState));

        //Assert
        cut.MarkupMatches(LogoMarkup);
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
