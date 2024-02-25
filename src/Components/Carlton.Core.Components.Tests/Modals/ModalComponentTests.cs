using Bunit.TestDoubles;
using Carlton.Core.Components.Modals;
namespace Carlton.Core.Components.Tests.Modals;


[Trait("Component", nameof(Modal))]
public class ModalComponentTests : TestContext
{
    public const string ConfirmationModalTemplate = @"
    <div class=""modal {0}"">
        <div class=""modal-content"">
          <div class=""confirmation-modal-content"">
            <span class=""dismiss"">×</span>
            <div class=""modal-header"" >
              <div class=""mdi mdi-48px mdi-alert-circle-outline""></div>
            </div>
            <div class=""modal-prompt-message"">
              <span class=""message-text"">{1}</span>
            </div>
            <div class=""modal-message"">
              <span class=""message-text"">{2}</span>
            </div>
            <div class=""modal-actions"">
              <button class=""btn-cancel"">Cancel</button>
              <button class=""btn-confirm"">Delete</button>
            </div>
          </div>
        </div>
  </div>";

    public const string SingleActionModalTemplate = @"
      <div class=""modal {0}"" >
          <div class=""modal-content"" >
            <span class=""dismiss"">×</span>
            <div class=""modal-prompt-message"" >
              <span class=""message-text"" >{1}</span>
            </div>
            <div class=""modal-message"">
              <span class=""message-text"">{2}</span>
            </div>
            <div class=""modal-actions"" >
              <button class=""btn-ok""  >Ok</button>
            </div>
          </div>
        </div>";

    [Theory(DisplayName = "Markup Test")]
    [InlineAutoData(true, ModalTypes.ConfirmationModal, ConfirmationModalTemplate)]
    [InlineAutoData(false, ModalTypes.ConfirmationModal, ConfirmationModalTemplate)]
    [InlineAutoData(true, ModalTypes.SingleActionModal, SingleActionModalTemplate)]
    [InlineAutoData(false, ModalTypes.SingleActionModal, SingleActionModalTemplate)]
    public void Model_Markup_RendersCorrectly(
       bool expectedIsVisible,
       ModalTypes expectedModalType,
       string expectedTemplate,
       string expectedModalPrompt,
       string expectedModalMessage)
    {
        //Arrange
        var expectedMarkup = string.Format(expectedTemplate, expectedIsVisible ? "visible" : string.Empty, expectedModalPrompt, expectedModalMessage);

        //Act
        var cut = RenderComponent<Modal>(parameters => parameters
            .Add(p => p.IsVisible, expectedIsVisible)
            .Add(p => p.ModalType, expectedModalType)
            .Add(p => p.ModalPrompt, expectedModalPrompt)
            .Add(p => p.ModalMessage, expectedModalMessage));

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory(DisplayName = "ModalType Parameter Test"), AutoData]
    public void Model_ConfirmationModalContent_ModalTypeParameter_RendersCorrectly(
        bool expectedIsVisible,
        string expectedModalPrompt,
        string expectedModalMessage)
    {
        //Act
        var cut = RenderComponent<Modal>(parameters => parameters
            .Add(p => p.IsVisible, expectedIsVisible)
            .Add(p => p.ModalType, ModalTypes.ConfirmationModal)
            .Add(p => p.ModalPrompt, expectedModalPrompt)
            .Add(p => p.ModalMessage, expectedModalMessage));

        //Assert
        cut.FindComponent<ConfirmationModalContent>();
    }

    [Theory(DisplayName = "ModalType Parameter Test"), AutoData]
    public void Model_SingleActionModalContent_ModalTypeParameter_RendersCorrectly(
      bool expectedIsVisible,
      string expectedModalPrompt,
      string expectedModalMessage)
    {
        //Act
        var cut = RenderComponent<Modal>(parameters => parameters
            .Add(p => p.IsVisible, expectedIsVisible)
            .Add(p => p.ModalType, ModalTypes.SingleActionModal)
            .Add(p => p.ModalPrompt, expectedModalPrompt)
            .Add(p => p.ModalMessage, expectedModalMessage));

        //Assert
        cut.FindComponent<SingleActionModalContent>();
    }

    [Theory(DisplayName = "ModalType Parameter Test"), AutoData]
    public void Model_ParametersTest_RendersCorrectly(
       bool expectedIsVisible,
       ModalTypes expectedModalType,
       string expectedModalPrompt,
       string expectedModalMessage)
    {
        //Arrange
        ComponentFactories.AddStub<ModalTemplate>();

        //Act
        var cut = RenderComponent<Modal>(parameters => parameters
            .Add(p => p.IsVisible, expectedIsVisible)
            .Add(p => p.ModalType, expectedModalType)
            .Add(p => p.ModalPrompt, expectedModalPrompt)
            .Add(p => p.ModalMessage, expectedModalMessage));

        var stub = cut.FindComponent<Stub<ModalTemplate>>();

        //Assert
        stub.Instance.Parameters.Get(_ => _.IsVisible).ShouldBe(expectedIsVisible);
        stub.Instance.Parameters.Get(_ => _.Prompt).ShouldBe(expectedModalPrompt);
        stub.Instance.Parameters.Get(_ => _.Message).ShouldBe(expectedModalMessage);
    }
}
