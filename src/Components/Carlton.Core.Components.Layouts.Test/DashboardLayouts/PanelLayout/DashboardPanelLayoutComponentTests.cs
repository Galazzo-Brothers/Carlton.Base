using Bunit.TestDoubles;
using Carlton.Core.Components.Layouts.DashboardLayouts.PanelLayout;
using Carlton.Core.Components.Layouts.Manager;
using Carlton.Core.Components.Layouts.Viewport;
namespace Carlton.Core.Components.Layouts.Test.DashboardLayouts.PanelLayout;

[Trait("Component", nameof(DashboardPanelLayout))]
public class DashboardPanelLayoutComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test")]
    [InlineAutoData(1000)]
    [InlineAutoData(500)]
    public void DashboardPanelLayout_Markup_RendersCorrectly(
        double expectedViewportWidth,
        bool expectedIsFullScreen,
        string expectedAppName,
        string expectedLogoUrl,
        bool expectedShowPanel)
    {
        //Arrange
        var viewportStateMock = Substitute.For<IViewportState>();
        var expectedViewportState = new ViewportModel(10, expectedViewportWidth);
        viewportStateMock.GetCurrentViewport().Returns(Task.FromResult(expectedViewportState));
        Services.AddSingleton(viewportStateMock);
        var state = new LayoutManagerCascadingState
        {
            IsFullScreen = expectedIsFullScreen,
            LayoutSettings = new Dictionary<string, object>
            {
                {"AppName", expectedAppName },
                { "LogoUrl",  expectedLogoUrl },
                { "ShowPanel", expectedShowPanel }
            }
        };
        ComponentFactories.AddStub<DashboardPanelDesktopLayout>("<div>Desktop Content</div>");
        ComponentFactories.AddStub<DashboardPanelMobileLayout>("<div>Mobile Content</div>");

        var expectedMarkup = $@"
        <div class=""media-query-wrapper"" >
          <div class=""{(expectedViewportState.IsMobile ? "mobile" : "desktop")}"" >
            <div>{(expectedViewportState.IsMobile ? "Mobile Content" : "Desktop Content")}</div>
          </div>
        </div>";

        //Act
        var cut = RenderComponent<DashboardPanelLayout>(parameters => parameters.AddCascadingValue(state));

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory(DisplayName = "Cascading Parameter Desktop Test"), AutoData]
    public void DashboardPanelLayout_CascadingParameter_Desktop_RendersCorrectly(
       bool expectedIsFullScreen,
       string expectedAppName,
       string expectedLogoUrl,
       bool expectedShowPanel)
    {
        //Arrange
        var viewportStateMock = Substitute.For<IViewportState>();
        var expectedViewportState = new ViewportModel(10, 1000);
        viewportStateMock.GetCurrentViewport().Returns(Task.FromResult(expectedViewportState));
        viewportStateMock.ViewportChanged += (sender, args) => { };
        Services.AddSingleton(viewportStateMock);
        var state = new LayoutManagerCascadingState
        {
            IsFullScreen = expectedIsFullScreen,
            LayoutSettings = new Dictionary<string, object>
            {
                {"AppName", expectedAppName },
                { "LogoUrl",  expectedLogoUrl },
                { "ShowPanel", expectedShowPanel }
            }
        };
        ComponentFactories.AddStub<DashboardPanelDesktopLayout>("<div>Desktop Content</div>");
        ComponentFactories.AddStub<DashboardPanelMobileLayout>("<div>Mobile Content</div>");

        //Act
        var cut = RenderComponent<DashboardPanelLayout>(parameters => parameters.AddCascadingValue(state));
        var stub = cut.FindComponent<Stub<DashboardPanelDesktopLayout>>();

        //Assert
        stub.Instance.Parameters.Get(_ => _.AppName).ShouldBe(expectedAppName);
        stub.Instance.Parameters.Get(_ => _.LogoUrl).ShouldBe(expectedLogoUrl);
        stub.Instance.Parameters.Get(_ => _.IsFullScreen).ShouldBe(expectedIsFullScreen);
    }

    [Theory(DisplayName = "Cascading Parameter Mobile Test"), AutoData]
    public void DashboardPanelLayout_CascadingParameter_Mobile_RendersCorrectly(
     bool expectedIsFullScreen,
     string expectedAppName,
     string expectedLogoUrl,
     bool expectedShowPanel)
    {
        //Arrange
        var viewportStateMock = Substitute.For<IViewportState>();
        var expectedViewportState = new ViewportModel(10, 500);
        viewportStateMock.GetCurrentViewport().Returns(Task.FromResult(expectedViewportState));
        viewportStateMock.ViewportChanged += (sender, args) => { };
        Services.AddSingleton(viewportStateMock);
        var state = new LayoutManagerCascadingState
        {
            IsFullScreen = expectedIsFullScreen,
            LayoutSettings = new Dictionary<string, object>
            {
                {"AppName", expectedAppName },
                { "LogoUrl",  expectedLogoUrl },
                { "ShowPanel", expectedShowPanel }
            }
        };
        ComponentFactories.AddStub<DashboardPanelDesktopLayout>("<div>Desktop Content</div>");
        ComponentFactories.AddStub<DashboardPanelMobileLayout>("<div>Mobile Content</div>");

        //Act
        var cut = RenderComponent<DashboardPanelLayout>(parameters => parameters.AddCascadingValue(state));
        var stub = cut.FindComponent<Stub<DashboardPanelMobileLayout>>();

        //Assert
        stub.Instance.Parameters.Get(_ => _.LogoUrl).ShouldBe(expectedLogoUrl);
        stub.Instance.Parameters.Get(_ => _.IsFullScreen).ShouldBe(expectedIsFullScreen);
    }

    [Theory(DisplayName = "Cascading Parameter Property Test"), AutoData]
    public void DashboardPanelLayout_CascadingParameter_ShouldSetProperties(
        bool expectedIsFullScreen,
        string expectedAppName,
        string expectedLogoUrl,
        bool expectedShowPanel)
    {
        //Arrange
        var viewportStateMock = Substitute.For<IViewportState>();
        var expectedViewportState = new ViewportModel(10, 500);
        viewportStateMock.GetCurrentViewport().Returns(Task.FromResult(expectedViewportState));
        viewportStateMock.ViewportChanged += (sender, args) => { };
        Services.AddSingleton(viewportStateMock);
        var state = new LayoutManagerCascadingState
        {
            IsFullScreen = expectedIsFullScreen,
            LayoutSettings = new Dictionary<string, object>
            {
                {"AppName", expectedAppName },
                { "LogoUrl",  expectedLogoUrl },
                { "ShowPanel", expectedShowPanel }
            }
        };
        ComponentFactories.AddStub<DashboardPanelDesktopLayout>("<div>Desktop Content</div>");
        ComponentFactories.AddStub<DashboardPanelMobileLayout>("<div>Mobile Content</div>");

        //Act
        var cut = RenderComponent<DashboardPanelLayout>(parameters => parameters.AddCascadingValue(state));
        var stub = cut.FindComponent<Stub<DashboardPanelMobileLayout>>();

        //Assert
        cut.Instance.AppName.ShouldBe(expectedAppName);
        cut.Instance.LogoUrl.ShouldBe(expectedLogoUrl);
        cut.Instance.ShowPanel.ShouldBe(expectedShowPanel);
    }
}

