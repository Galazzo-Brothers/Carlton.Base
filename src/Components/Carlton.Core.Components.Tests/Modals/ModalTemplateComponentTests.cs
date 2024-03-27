//using Carlton.Core.Components.Modals;
//namespace Carlton.Core.Components.Tests.Modals;

//[Trait("Component", nameof(ModalTemplate))]
//public class ModalTemplateComponentTests : TestContext
//{
//    private const string ModalContentTemplate = @"<span class=""modal-prompt"">{0}</span><span class=""modal-message"">{1}</span>";

//    [Theory(DisplayName = "Markup Test"), AutoData]
//    public void Modal_Markup_RendersCorrectly(
//    bool expectedIsVisible,
//        string expectedModalPrompt,
//        string expectedModalMessage)
//    {
//        //Arrange
//        var expectedMarkup =
//@$"<div class=""modal {(expectedIsVisible ? "visible" : string.Empty)}"">
//    <div class=""modal-content"">
//        <span class=""modal-prompt"">{expectedModalPrompt}</span><span class=""modal-message"">{expectedModalMessage}</span>
//    </div>
//</div>";

//        //Act
//        var cut = RenderComponent<ModalTemplate>(parameters => parameters
//         .Add(p => p.IsVisible, expectedIsVisible)
//         .Add(p => p.Prompt, expectedModalPrompt)
//         .Add(p => p.Message, expectedModalMessage)
//         .Add(p => p.ModalContent, state => string.Format(ModalContentTemplate, expectedModalPrompt, expectedModalMessage)));

//        //Assert
//        cut.MarkupMatches(expectedMarkup);
//    }

//    [Theory(DisplayName = "ModalClosedParameter Test"), AutoData]
//    public void Modal_ModalClosedParameter_FiresEvent(
//       bool expectedIsVisible,
//       string expectedModalPrompt,
//       string expectedModalMessage)
//    {
//        //Arrange
//        var eventFired = false;
//        var modalConfiremd = false;
//        var cut = RenderComponent<ModalTemplate>(parameters => parameters
//         .Add(p => p.IsVisible, expectedIsVisible)
//         .Add(p => p.Prompt, expectedModalPrompt)
//         .Add(p => p.Message, expectedModalMessage)
//         .Add<ConfirmationModalContent, ModalRenderFragmentState>(p => p.ModalContent, value =>
//            childParams => childParams.Add(_ => _.State, value))
//         .Add(p => p.OnClose, (args) =>
//         {
//             eventFired = true;
//             modalConfiremd = args.UserConfirmed;
//         }));

//        //Act
//        cut.Find(".btn-confirm").Click();

//        //Assert
//        eventFired.ShouldBeTrue();
//        modalConfiremd.ShouldBeTrue();
//    }

//    [Theory(DisplayName = "ModalDismissParameter Test"), AutoData]
//    public void Modal_ModalDismissParameter_FiresEvent(
//      bool expectedIsVisible,
//      string expectedModalPrompt,
//      string expectedModalMessage)
//    {
//        //Arrange
//        var eventFired = false;
//        var cut = RenderComponent<ModalTemplate>(parameters => parameters
//         .Add(p => p.IsVisible, expectedIsVisible)
//         .Add(p => p.Prompt, expectedModalPrompt)
//         .Add(p => p.Message, expectedModalMessage)
//         .Add<ConfirmationModalContent, ModalRenderFragmentState>(p => p.ModalContent, value =>
//            childParams => childParams.Add(_ => _.State, value))
//         .Add(p => p.OnDismiss, () =>
//         {
//             eventFired = true;
//         }));

//        //Act
//        cut.Find(".dismiss").Click();

//        //Assert
//        eventFired.ShouldBeTrue();
//    }
//}