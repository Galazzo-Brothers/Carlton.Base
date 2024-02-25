using Carlton.Core.Components.Layouts.State.Modals;
using Carlton.Core.Components.Modals;

namespace Carlton.Core.Components.Layouts.Test.State.Modals;

public class ModalStateTests
{
    [Theory(DisplayName = "ModalState Event Test"), AutoData]
    public void ModalState_EventFires(
       ModalTypes expectedModalType,
       ModalViewModel expectedModalViewModel)
    {
        //Arrange
        var eventFired = false;
        var sut = new ModalState();
        sut.ModalStateChanged += (sender, args) => eventFired = true;

        //Act
        sut.RaiseModal(expectedModalType, expectedModalViewModel);

        //Assert
        eventFired.ShouldBe(true);
        sut.IsVisible.ShouldBeTrue();
        sut.ModalType.ShouldBe(expectedModalType);
        sut.ModalModel.Prompt.ShouldBe(expectedModalViewModel.Prompt);
        sut.ModalModel.Message.ShouldBe(expectedModalViewModel.Message);
    }
}
