using Carlton.Core.Components.Layouts.Panel;
namespace Carlton.Core.Components.Layouts.Tests.Panel;

public class PanelStateTests
{
    [Theory(DisplayName = "PanelState Set Visibility Event Test")]
    [InlineData(true)]
    [InlineData(false)]
    public void FullScreenState_SetVisibility_EventFires(
     bool expectedPanelIsVisible)
    {
        //Arrange
        var eventFired = false;
        var sut = new PanelState(expectedPanelIsVisible);
        sut.PanelVisibilityChangedChanged += (sender, args) => eventFired = true;
        var newVisibility = !expectedPanelIsVisible;

        //Act
        sut.SetPanelVisibility(newVisibility);

        //Assert
        eventFired.ShouldBe(true);
        sut.IsPanelVisible.ShouldBe(!expectedPanelIsVisible);
    }

    [Theory(DisplayName = "PanelState Toggle Visibility Event Test")]
    [InlineData(true)]
    [InlineData(false)]
    public void FullScreenState_ToggleVisibility_EventFires(
       bool expectedPanelIsVisible)
    {
        //Arrange
        var eventFired = false;
        var sut = new PanelState(expectedPanelIsVisible);
        sut.PanelVisibilityChangedChanged += (sender, args) => eventFired = true;

        //Act
        sut.TogglePanelVisibility();

        //Assert
        eventFired.ShouldBe(true);
        sut.IsPanelVisible.ShouldBe(!expectedPanelIsVisible);
    }
}
