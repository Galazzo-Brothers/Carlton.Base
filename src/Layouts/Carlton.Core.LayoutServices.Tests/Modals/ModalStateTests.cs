using Carlton.Core.LayoutServices.Modals;
namespace Carlton.Core.LayoutServices.Tests.Modals;

public class ModalStateTests
{
	[Theory(DisplayName = "ModalState Event Test"), AutoData]
	public void ModalState_EventFires(
	   string expectedModalType,
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
		sut.Model.Prompt.ShouldBe(expectedModalViewModel.Prompt);
		sut.Model.Message.ShouldBe(expectedModalViewModel.Message);
	}
}
