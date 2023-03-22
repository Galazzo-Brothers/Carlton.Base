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

    [Fact]
    public void Logo_IsCollapsedParam_False_ShouldBeExpanded()
    {
        //Arrange
        var layoutState = new LayoutState(false, () => { });

        //Act
        var cut = RenderComponent<HamburgerCollapser>(paramaters => paramaters
            .AddCascadingValue(layoutState));

        //Assert
        Assert.False(layoutState.IsCollapsed);
    }

    [Fact]
    public void Logo_IsCollapsedParam_True_ShouldBeCollapsed()
    {
        //Arrange
        var layoutState = new LayoutState(true, () => { });

        //Act
        var cut = RenderComponent<HamburgerCollapser>(paramaters => paramaters
            .AddCascadingValue(layoutState));

        //Assert
        Assert.True(layoutState.IsCollapsed);
    }
}
