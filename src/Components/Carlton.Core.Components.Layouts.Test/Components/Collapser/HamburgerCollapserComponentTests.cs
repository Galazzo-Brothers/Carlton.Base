using Carlton.Core.Components.Layouts.State.FullScreen;
namespace Carlton.Core.Components.Layouts.Test.Components.Collapser;

[Trait("Component", nameof(HamburgerCollapser))]
public class HamburgerCollapserComponentTests : TestContext
{
    [Fact(DisplayName = "Markup Test")]
    public void HamburgerCollapser_Markup_RendersCorrectly()
    {
        //Arrange
        var mock = Substitute.For<IFullScreenState>();
        Services.AddSingleton(mock);
        var expectedMarkup = $@"
            <div class=""collapser"">
                <a href=""#"">
                    <span class=""mdi mdi-24px mdi-menu"" ></span>
                </a>
            </div>";

        //Act
        var cut = RenderComponent<HamburgerCollapser>();

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Fact(DisplayName = "Click Event Test")]
    public void HamburgerCollapser_ClickEvent_CallsFullScreenService()
    {
        //Arrange
        var mock = Substitute.For<IFullScreenState>();
        Services.AddSingleton(mock);
        var cut = RenderComponent<HamburgerCollapser>();

        //Act
        cut.Find("a").Click();

        //Assert
        mock.Received(1).ToggleFullScreen();
    }
}


