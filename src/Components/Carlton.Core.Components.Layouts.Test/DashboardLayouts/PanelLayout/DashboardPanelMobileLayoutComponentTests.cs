using Carlton.Core.Components.Layouts.DashboardLayouts.PanelLayout;
using Carlton.Core.Components.Layouts.State.FullScreen;
using Carlton.Core.Components.Panels;
namespace Carlton.Core.Components.Layouts.Test.DashboardLayouts.PanelLayout;

[Trait("Component", nameof(DashboardPanelMobileLayout))]
public class DashboardPanelMobileLayoutComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test")]
    [InlineAutoData(true)]
    [InlineAutoData(false)]
    public void DashboardPanelLayout_Markup_RendersCorrectly(
       bool expectedIsFullScreen,
       string expectedLogoUrl,
       string expectedContent)
    {
        //Arrange
        var moduleInterop = JSInterop.SetupModule(ResizablePanel.ImportPath);
        moduleInterop.SetupVoid(ResizablePanel.InitResizablePanel);
        var navStateMock = Substitute.For<IFullScreenState>();
        Services.AddSingleton(navStateMock);

        var expectedMarkup = $@"
        <div class=""layout-container {(expectedIsFullScreen ? "collapsed" : string.Empty)}"" >
          <header >
            <div class=""header-bar"" >
              <div class=""nav"" >
                <div class=""slide-out-nav"" >
                  <a href=""#""   >
                    <span class=""icon mdi mdi-24px mdi-menu"" ></span>
                  </a>
                </div>
                <div class=""content-container "" ></div>
              </div>
              <div class=""logo"" >
                <div class=""logo collapsed"" >
                  <div class=""content"" >
                    <img src=""{expectedLogoUrl}"" >
                    <span class=""title"" ></span>
                  </div>
                </div>
              </div>
              <div class=""title"" ></div>
              <div class=""action"" ></div>
            </div>
          </header>
          <main >
            <div class=""mobile-body-content"">{expectedContent}</div>
            <div class=""mobile-panel-content"" ></div>
          </main>
        </div>";

        //Act
        var cut = RenderComponent<DashboardPanelMobileLayout>(parameters =>
            parameters.Add(p => p.BodyContent, expectedContent)
                      .Add(p => p.LogoUrl, expectedLogoUrl)
                      .Add(p => p.IsFullScreen, expectedIsFullScreen));


        //Assert
        cut.MarkupMatches(expectedMarkup);
    }
}
