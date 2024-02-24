using Carlton.Core.Components.Layouts.State.Theme;
namespace Carlton.Core.Components.Layouts.Test.Components.Theme;

[Trait("Component", nameof(ThemeMenu))]
public class ThemeMenuComponentTests : TestContext
{
    [Fact(DisplayName = "Markup Test")]
    public void ThemeMenu_Markup_RendersCorrectly()
    {
        //Arrange
        var mock = Substitute.For<IThemeState>();
        Services.AddSingleton(mock);
        var expectedMarkup = $@"
        <a href=""#"" class=""theme-menu"">
            <i class=""mdi mdi-24px""></i>
        </a>";

        //Act
        var cut = RenderComponent<ThemeMenu>();

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Fact(DisplayName = "Click Event Test")]
    public void HamburgerCollapser_ClickEvent_CallsFullScreenService()
    {
        //Arrange
        var mock = Substitute.For<IThemeState>();
        Services.AddSingleton(mock);
        var cut = RenderComponent<ThemeMenu>();

        //Act
        cut.Find("a").Click();

        //Assert
        mock.Received(1).SetTheme(Arg.Any<Themes>());
    }
}


