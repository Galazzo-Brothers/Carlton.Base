namespace Carlton.Base.Components.Test;

public class LogoComponentTests : TestContext
{
    private static readonly string LogoMarkup =
  @"<div class=""logo"" b-2psdcgopyx>
        <div class=""content"" b-2psdcgopyx>
            <img src=""_content/Carlton.Base.Components/images/CarltonLogo.png"" b-2psdcgopyx>
            <span class=""title"" b-2psdcgopyx></span>
        </div>
    </div>";


    [Fact]
    public void Logo_Markup_RendersCorrectly()
    {
        //Arrange
        var layoutState = new LayoutState(false, () => { });

        //Act
        var cut = RenderComponent<Logo>(paramaters => paramaters
            .AddCascadingValue<LayoutState>(layoutState));

        //Assert
        cut.MarkupMatches(LogoMarkup);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Logo_IsCollapsedParam_ShouldBeExpanded(bool isCollapsed)
    {
        //Arrange
        var layoutState = new LayoutState(isCollapsed, () => { });

        //Act
        var cut = RenderComponent<HamburgerCollapser>(paramaters => paramaters
            .AddCascadingValue(layoutState));

        //Assert
        Assert.Equal(isCollapsed, layoutState.IsCollapsed);
    }
}
