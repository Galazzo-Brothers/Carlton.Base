using Carlton.Core.Components.Panels;
using Carlton.Core.Layouts.DashboardLayouts.PanelLayout;
using Carlton.Core.LayoutServices.FullScreen;
namespace Carlton.Core.Layouts.Tests.DashboardLayouts.PanelLayout;

[Trait("Component", nameof(DashboardPanelMobileLayout))]
public class DashboardPanelMobileLayoutComponentTests : TestContext
{
	[Theory(DisplayName = "Markup Test")]
	[InlineAutoData(false, false)]
	[InlineAutoData(true, false)]
	[InlineAutoData(false, true)]
	[InlineAutoData(true, true)]
	public void DashboardPanelLayout_Markup_RendersCorrectly(
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
            <div class=""mobile-body-content"">{expectedContent}</div>
            <div class=""mobile-panel-content"" ></div>";

		var expectedMarkup = $@"
        <div class=""dashboard-panel-layout {(expectedIsFullScreen ? "collapsed" : string.Empty)}"" >
            <header>
                <div class=""header-bar"" >
                    <div class=""nav"" >
                        <div class=""slide-out-nav"" >
                          <a href=""#""   >
                            <span class=""icon mdi mdi-24px mdi-menu"" ></span>
                          </a>
                          <div class=""content-container "" ></div>
                        </div>
                    </div>
                    <div class=""logo"" ></div>
                    <div class=""title"" ></div>
                    <div class=""action"" ></div>
                </div>
          </header>
          <main>
            {(expectedShowPanel ? expectedPanelMarkup : expectedContent)}
          </main>
          </div>";

		//Act
		var cut = RenderComponent<DashboardPanelMobileLayout>(parameters =>
			parameters.Add(p => p.BodyContent, expectedContent)
					  .Add(p => p.IsFullScreen, expectedIsFullScreen)
					  .Add(p => p.ShowPanel, expectedShowPanel));


		//Assert
		cut.MarkupMatches(expectedMarkup);
	}
}
