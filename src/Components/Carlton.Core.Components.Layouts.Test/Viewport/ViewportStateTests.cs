using Carlton.Core.Components.Layouts.Viewport;
using Microsoft.JSInterop;
namespace Carlton.Core.Components.Layouts.Tests.Viewport;

public class ViewportStateTests
{
    [Theory(DisplayName = "ViewportState Initialized Test"), AutoData]
    public async Task ViewportState_Initialized_ShouldCallJavaScript(
        ViewportModel expectedViewportModel)
    {
        //Arrange
        var mockJs = Substitute.For<IJSRuntime>();
        var mockJsModule = Substitute.For<IJSObjectReference>();
        mockJs.InvokeAsync<IJSObjectReference>(Constants.Import, ViewportState.ModulePath).ReturnsForAnyArgs(ValueTask.FromResult(mockJsModule));
        mockJsModule.InvokeAsync<ViewportModel>("viewport.getViewport").Returns(ValueTask.FromResult(expectedViewportModel));

        //Act
        var sut = new ViewportState(mockJs);

        //Assert
        await mockJs.Received(1).InvokeAsync<IJSObjectReference>(Constants.Import, Arg.Any<object[]>());
        await mockJsModule.Received(1).InvokeAsync<ViewportModel>("viewport.getViewport", Arg.Any<object[]>());
        await mockJsModule.Received(1).InvokeAsync<ViewportModel>("viewport.registerViewportChangedHandler", Arg.Any<object[]>());
    }

    [Theory(DisplayName = "ViewportState Dispose Test"), AutoData]
    public async Task ViewportState_Dispose_ShouldCallJavaScript(
       ViewportModel expectedViewportModel)
    {
        //Arrange
        var mockJs = Substitute.For<IJSRuntime>();
        var mockJsModule = Substitute.For<IJSObjectReference>();
        mockJs.InvokeAsync<IJSObjectReference>(Constants.Import, ViewportState.ModulePath).ReturnsForAnyArgs(ValueTask.FromResult(mockJsModule));
        mockJsModule.InvokeAsync<ViewportModel>("viewport.getViewport").Returns(ValueTask.FromResult(expectedViewportModel));
        var sut = new ViewportState(mockJs);

        //Act
        await sut.DisposeAsync();

        //Assert
        await mockJsModule.Received(1).InvokeAsync<ViewportModel>("viewport.removeViewportChangedHandlers", Arg.Any<object[]>());
    }

    [Theory(DisplayName = "ViewportChanged Event Test")]
    [InlineAutoData(1000, 500)]
    [InlineAutoData(1000, 700)]
    public async Task ViewportState_ViewportChanged_EventFires(
      double initialViewportWidth,
      double updatedViewportWidth,
      double initialViewportHeight,
      double updatedViewportHeight)
    {
        //Arrange
        var initialViewport = new ViewportModel(initialViewportHeight, initialViewportWidth);
        var updatedViewport = new ViewportModel(updatedViewportHeight, updatedViewportWidth);
        var eventFired = false;
        var mockJs = Substitute.For<IJSRuntime>();
        var mockJsModule = Substitute.For<IJSObjectReference>();
        mockJs.InvokeAsync<IJSObjectReference>(Constants.Import, ViewportState.ModulePath).ReturnsForAnyArgs(ValueTask.FromResult(mockJsModule));
        mockJsModule.InvokeAsync<ViewportModel>("viewport.getViewport").Returns(ValueTask.FromResult(initialViewport));
        var sut = new ViewportState(mockJs);
        sut.ViewportChanged += (sender, args) => eventFired = true;

        //Act
        await sut.ViewportUpdated(updatedViewport);
        var actualViewport = await sut.GetCurrentViewport();

        //Assert
        eventFired.ShouldBeTrue();
        actualViewport.Height.ShouldBe(updatedViewportHeight);
        actualViewport.Width.ShouldBe(updatedViewportWidth);

    }
}
