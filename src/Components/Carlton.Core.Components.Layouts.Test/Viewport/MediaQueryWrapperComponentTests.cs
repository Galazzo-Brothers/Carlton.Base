using Carlton.Core.Components.Layouts.Viewport;
namespace Carlton.Core.Components.Layouts.Tests.Viewport;


[Trait("Component", nameof(MediaQueryWrapper))]
public class MediaQueryWrapperComponentTests : TestContext
{
    public const string DesktopContent = @"<div class=""desktop-content""><span>Desktop Content</span></div>";
    public const string MobileContent = @"<div class=""mobile-content""><span>Mobile Content</span></div>";

    public const string DesktopMarkup = $@"
    <div class=""media-query-wrapper"" >
        <div class=""desktop"" >
            {DesktopContent}
        </div>
    </div>";

    public const string MobileMarkup = $@"
    <div class=""media-query-wrapper"" >
        <div class=""mobile"" >
            {MobileContent}
        </div>
    </div>";

    [Theory(DisplayName = "Markup Test")]
    [InlineData(500, 1000, DesktopMarkup)] // Desktop
    [InlineData(500, 200, MobileMarkup)] //Mobile
    public void MediaQueryWrapper_Markup_RendersCorrectly(
        double expectedHeight,
        double expectedWidth,
        string expectedMarkup)
    {
        //Arrange
        var viewportStateMock = Substitute.For<IViewportState>();
        var expectedViewportState = new ViewportModel(expectedHeight, expectedWidth);
        Services.AddSingleton(viewportStateMock);
        viewportStateMock.GetCurrentViewport().Returns(expectedViewportState);


        //Act
        var cut = RenderComponent<MediaQueryWrapper>(parameters =>
            parameters.Add(p => p.DesktopContent, DesktopContent)
                      .Add(p => p.MobileContent, MobileContent));

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory(DisplayName = "Viewport Changed Event Test"), AutoData]
    public void MediaQueryWrapper_ViewportChanged_FiresEvent(
       double expectedInitialHeight,
       double expectedInitialWidth,
       double expectedNewHeight,
       double expectedNewWidth)
    {
        //Arrange
        var viewportStateMock = Substitute.For<IViewportState>();
        var expectedViewportState = new ViewportModel(expectedInitialHeight, expectedInitialWidth);
        Services.AddSingleton(viewportStateMock);
        viewportStateMock.GetCurrentViewport().Returns(expectedViewportState);
        var cut = RenderComponent<MediaQueryWrapper>(parameters =>
            parameters.Add(p => p.DesktopContent, DesktopContent)
                      .Add(p => p.MobileContent, MobileContent));

        //Act
        var args = new ViewportChangedEventArgs(new ViewportModel(expectedNewHeight, expectedNewWidth));
        cut.InvokeAsync(() => viewportStateMock.ViewportChanged += Raise.Event<EventHandler<ViewportChangedEventArgs>>(new object(), args));

        //Assert
        cut.RenderCount.ShouldBe(2); //second render from the event triggering a stateHasChanged call
    }
}
