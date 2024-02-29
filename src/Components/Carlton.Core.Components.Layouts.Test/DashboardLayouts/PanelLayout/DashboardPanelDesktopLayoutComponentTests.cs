using Carlton.Core.Components.Layouts.DashboardLayouts.PanelLayout;
using Carlton.Core.Components.Layouts.FullScreen;
using Carlton.Core.Components.Panels;
namespace Carlton.Core.Components.Layouts.Test.DashboardLayouts.PanelLayout;

[Trait("Component", nameof(DashboardPanelLayout))]
public class DashboardPanelDesktopLayoutComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test")]
    [InlineAutoData(true)]
    [InlineAutoData(false)]
    public void DashboardPanelDesktopLayout_Markup_RendersCorrectly(
        bool expectedIsFullScreen,
        string expectedAppName,
        string expectedLogoUrl,
        string expectedContent)
    {
        //Arrange
        var moduleInterop = JSInterop.SetupModule(ResizablePanel.ImportPath);
        moduleInterop.SetupVoid(ResizablePanel.InitResizablePanel);
        var navStateMock = Substitute.For<IFullScreenState>();
        Services.AddSingleton(navStateMock);

        var expectedMarkup = $@" <div class=""dashboard-panel-layout {(expectedIsFullScreen ? "collapsed" : string.Empty)}"" >
          <header >
            <div >
              <div class=""header-bar"" >
                <div class=""menu"" >
                  <div class=""collapser"" >
                    <a href=""#""   >
                      <span class=""mdi mdi-24px mdi-menu"" ></span>
                    </a>
                  </div>
                </div>
                <div class=""title"" ></div>
                <div class=""action"" ></div>
              </div>
              <span class=""horizontal-spacer"" style=""--spacer-width:100%;--spacer-height:1px;"" ></span>
            </div>
          </header>
          <nav >
            <div class=""logo {(expectedIsFullScreen ? "collapsed" : string.Empty)}"" >
                <div class=""content"">
                    <img src=""{expectedLogoUrl}"">
                    <span class=""title"">{expectedAppName}</span>
                </div>              
            </div>
          </nav>
          <main >
            <div class=""resizable-panel"" >
              <div class=""panel-top"" >{expectedContent}</div>
              <div class=""splitter-horizontal"" ></div>
              <div class=""panel-bottom"" ></div>
            </div>
          </main>
          <footer ></footer>
        </div>";

        //Act
        var cut = RenderComponent<DashboardPanelDesktopLayout>(parameters =>
            parameters.Add(p => p.BodyContent, expectedContent)
                      .Add(p => p.AppName, expectedAppName)
                      .Add(p => p.LogoUrl, expectedLogoUrl)
                      .Add(p => p.IsFullScreen, expectedIsFullScreen));


        //Assert
        cut.MarkupMatches(expectedMarkup);
    }
}
