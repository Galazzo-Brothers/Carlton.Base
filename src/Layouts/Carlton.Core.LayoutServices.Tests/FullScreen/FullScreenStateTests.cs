using Carlton.Core.LayoutServices.FullScreen;
namespace Carlton.Core.LayoutsServices.Tests.FullScreen;

public class FullScreenStateTests
{
	[Theory(DisplayName = "FullScreenState Event Test")]
	[InlineData(true)]
	[InlineData(false)]
	public void FullScreenState_EventFires(
		bool expectedIsFullScreen)
	{
		//Arrange
		var eventFired = false;
		var sut = new FullScreenState(expectedIsFullScreen);
		sut.FullScreenStateChanged += (sender, args) => eventFired = true;

		//Act
		sut.ToggleFullScreen();

		//Assert
		eventFired.ShouldBe(true);
		sut.IsFullScreen.ShouldBe(!expectedIsFullScreen);
	}
}
