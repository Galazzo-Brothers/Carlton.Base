using Carlton.Core.Components.Layouts.State.Toasts;
using Carlton.Core.Components.Toasts;
namespace Carlton.Core.Components.Layouts.Test.State.Toasts;

public class ToastStateTests
{
    [Theory(DisplayName = "ToastState Event Test"), AutoData]
    public void ToastState_EventFires(
      string expectedTitle,
      string expectedMessage,
      ToastTypes expectedToastType)
    {
        //Arrange
        var eventFired = false;
        var sut = new ToastState();
        sut.ToastAdded += (sender, args) => eventFired = true;

        //Act
        sut.RaiseToast(expectedTitle, expectedMessage, expectedToastType);

        //Assert
        eventFired.ShouldBe(true);
        sut.Toasts.ShouldNotBeEmpty();
        sut.Toasts[0].Title.ShouldBe(expectedTitle);
        sut.Toasts[0].Message.ShouldBe(expectedMessage);
        sut.Toasts[0].ToastType.ShouldBe(expectedToastType);
    }

    [Theory(DisplayName = "ToastState ToastList Test"), AutoData]
    public void ToastState_ToastList_ShouldHaveCorrectCount(
        int expectedToastCount,
        string expectedTitle,
        string expectedMessage,
        ToastTypes expectedToastType)
    {
        //Arrange
        var eventFired = false;
        var sut = new ToastState();
        sut.ToastAdded += (sender, args) => eventFired = true;

        //Act
        foreach(var i in Enumerable.Range(0, expectedToastCount))
            sut.RaiseToast(expectedTitle, expectedMessage, expectedToastType);

        //Assert
        eventFired.ShouldBe(true);
        sut.Toasts.Count.ShouldBe(expectedToastCount);
    }
}
