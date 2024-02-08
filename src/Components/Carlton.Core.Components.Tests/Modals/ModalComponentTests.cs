using Carlton.Core.Components.Modals;
namespace Carlton.Core.Components.Tests.Modals;

[Trait("Component", nameof(Modal))]
public class ModalComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test"), AutoData]
    public void Modal_Collapsed_Markup_RendersCorrectly(
        bool expectedIsVisible,
        string expectedModalPrompt,
        string expectedModalMessage)
    {
        //Arrange
        var expectedMarkup =
@$"<div class=""modal {(expectedIsVisible ? "visible" : string.Empty)}"">
    <div class=""modal-content"">
        <span class=""close"">×</span>
        <span class=""modal-prompt"">{expectedModalPrompt}</span><span class=""modal-message"">{expectedModalMessage}</span>
    </div>
</div>";

        //Act
        var cut = RenderComponent<Modal>(parameters => parameters
         .Add(p => p.IsVisible, expectedIsVisible)
         .Add(p => p.ModalPrompt, expectedModalPrompt)
         .Add(p => p.ModalMessage, expectedModalMessage)
         .Add(p => p.ModalContent, state => $@"<span class=""modal-prompt"">{state.ModalPrompt}</span><span class=""modal-message"">{state.ModalMessage}</span>"));

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

}

//[Parameter]
//public bool IsVisible { get; set; }
//[Parameter]
//public string ModalPrompt { get; set; }
//[Parameter]
//public string ModalMessage { get; set; }
//[Parameter]
//public RenderFragment<ModalRenderFragmentState> ModalContent { get; set; }
//[Parameter]
//public EventCallback ModalDismissed { get; set; }
//[Parameter]
//public EventCallback ModalClosed { get; set; }


