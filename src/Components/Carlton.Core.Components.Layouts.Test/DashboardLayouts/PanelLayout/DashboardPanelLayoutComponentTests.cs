using Carlton.Core.Components.Layouts.DashboardLayouts.PanelLayout;
using Carlton.Core.Components.Layouts.FullScreen;
using Carlton.Core.Components.Layouts.Panel;
using Carlton.Core.Components.Layouts.Viewport;
using Microsoft.AspNetCore.Components.Sections;
namespace Carlton.Core.Components.Layouts.Test.DashboardLayouts.PanelLayout;

[Trait("Component", nameof(DashboardPanelLayout))]
public class DashboardPanelLayoutComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test")]
    [InlineAutoData(1000)]
    [InlineAutoData(500)]
    public void DashboardPanelLayout_Markup_RendersCorrectly(
        double expectedViewportWidth)
    {
        //Arrange
        var viewportStateMock = Substitute.For<IViewportState>();
        var fullScreenStateMock = Substitute.For<IFullScreenState>();
        var panelStateMock = Substitute.For<IPanelState>();

        Services.AddSingleton(viewportStateMock);
        Services.AddSingleton(fullScreenStateMock);
        Services.AddSingleton(panelStateMock);

        var expectedViewportState = new ViewportModel(10, expectedViewportWidth);
        viewportStateMock.GetCurrentViewport().Returns(Task.FromResult(expectedViewportState));
        
        ComponentFactories.AddStub<DashboardPanelDesktopLayout>("<div>Desktop Content</div>");
        ComponentFactories.AddStub<DashboardPanelMobileLayout>("<div>Mobile Content</div>");

        var expectedMarkup = $@"
        <div class=""media-query-wrapper"" >
          <div class=""{(expectedViewportState.IsMobile ? "mobile" : "desktop")}"" >
            <div>{(expectedViewportState.IsMobile ? "Mobile Content" : "Desktop Content")}</div>
          </div>
        </div>";

        //Act
        var cut = RenderComponent<DashboardPanelLayout>();

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }   
}

