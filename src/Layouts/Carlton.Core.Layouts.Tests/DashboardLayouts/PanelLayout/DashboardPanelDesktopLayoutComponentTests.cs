using Carlton.Core.Components.Panels;
using Carlton.Core.Layouts.DashboardLayouts.PanelLayout;
using Carlton.Core.LayoutServices.FullScreen;
namespace Carlton.Core.Layouts.Tests.DashboardLayouts.PanelLayout;

[Trait("Component", nameof(DashboardPanelLayout))]
public class DashboardPanelDesktopLayoutComponentTests : TestContext
{
	[Theory(DisplayName = "Markup Test")]
	[InlineAutoData(false, false)]
	[InlineAutoData(true, false)]
	[InlineAutoData(false, true)]
	[InlineAutoData(true, true)]
	public void DashboardPanelDesktopLayout_Markup_RendersCorrectly(
		bool expectedIsFullScreen,
		bool expectedShowPanel,
		string expectedContent)
	{
		//Arrange
		var moduleInterop = JSInterop.SetupModule(ResizablePanel.ImportPath);
		moduleInterop.SetupVoid(ResizablePanel.InitResizablePanel);
		var navStateMock = Substitute.For<IFullScreenState>();
		Services.AddSingleton(navStateMock);

		var expectedPanelMarkup = $@"
            <div class=""resizable-panel"">
              <div class=""panel-top"" >{expectedContent}</div>
              <div class=""splitter-horizontal""></div>
              <div class=""panel-bottom""></div>
            </div>";

		var expectedMarkup = $@"
        <div class=""dashboard-panel-layout {(expectedIsFullScreen ? "collapsed" : string.Empty)}"" >
            <header>
                <div>
                  <div class=""header-bar"">
                    <div class=""menu"">
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
            <nav>
            </nav>
            <main>
                {(expectedShowPanel ? expectedPanelMarkup : expectedContent)}  
            </main>
            <footer></footer>
        </div>";


		//Act
		var cut = RenderComponent<DashboardPanelDesktopLayout>(parameters =>
			parameters.Add(p => p.BodyContent, expectedContent)
					  .Add(p => p.IsFullScreen, expectedIsFullScreen)
					  .Add(p => p.ShowPanel, expectedShowPanel));

		//Assert
		cut.MarkupMatches(expectedMarkup);
	}
}
