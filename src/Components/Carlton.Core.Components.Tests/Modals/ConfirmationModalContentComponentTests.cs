using Carlton.Core.Components.Modals;
namespace Carlton.Core.Components.Tests.Modals;

[Trait("Component", nameof(ConfirmationModalContent))]
public class ConfirmationModalContentComponentTests : TestContext
{
    private const string ExpectedMarkup = @"
    <div class=""confirmation-modal-content"">
        {0}
        <div class=""modal-header"">
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
    </div>";

    [Theory(DisplayName = "Markup Test"), AutoData]
    public void ConfirmationModelContent_Markup_RendersCorrectly(
        string expectedModalPrompt,
        string expectedModalMessage)
    {
        //Arrange
        var dismissMarkup = @"<span class=""dismiss"">×</span>";
        var expectedMarkup = string.Format(ExpectedMarkup, dismissMarkup, expectedModalPrompt, expectedModalMessage);

        //Act
        var cut = RenderComponent<ConfirmationModalContent>(parameters => parameters
            .Add(p => p.State, new ModalRenderFragmentState
            {
                Prompt = expectedModalPrompt,
                Message = expectedModalMessage,
                HandleClose = (args) => Task.CompletedTask,
                HandleDismiss = () => Task.CompletedTask
            }));

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory(DisplayName = "Markup Test"), AutoData]
    public void ConfirmationModelContent_Stub_Markup_RendersCorrectly(
        string expectedModalPrompt,
        string expectedModalMessage)
    {
        //Arrange
        ComponentFactories.AddStub<ModalDismissMark>(@"<span class=""stub"">x</span>");
        var stubMarkup = @"<span class=""stub"">x</span>";
        var expectedMarkup = string.Format(ExpectedMarkup, stubMarkup, expectedModalPrompt, expectedModalMessage);

        //Act
        var cut = RenderComponent<ConfirmationModalContent>(parameters => parameters
            .Add(p => p.State, new ModalRenderFragmentState
            {
                Prompt = expectedModalPrompt,
                Message = expectedModalMessage,
                HandleClose = (args) => Task.CompletedTask,
                HandleDismiss = () => Task.CompletedTask
            }));

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory(DisplayName = "OnModalCloseParameter Test")]
    [InlineAutoData(true)]
    [InlineAutoData(false)]
    public void ConfirmationModelContent_OnModalCloseParameter_FiresEvent(
        bool expectedUserConfirmed,
        string expectedModalPrompt,
        string expectedModalMessage)
    {
        //Arrange
        var eventFired = false;
        var userConfirmed = false;
        var cut = RenderComponent<ConfirmationModalContent>(parameters => parameters
            .Add(p => p.State, new ModalRenderFragmentState
            {
                Prompt = expectedModalPrompt,
                Message = expectedModalMessage,
                HandleClose = (args) => 
                {
                    eventFired = true;
                    userConfirmed = args.UserConfirmed;
                    return Task.CompletedTask;
                },
                HandleDismiss = () => Task.CompletedTask
            }));

        var btnToClickClass = expectedUserConfirmed ? ".btn-confirm" : ".btn-cancel";
        
        //Act
        cut.Find(btnToClickClass).Click();

        //Assert
        eventFired.ShouldBeTrue();
        userConfirmed.ShouldBe(expectedUserConfirmed);
    }

    [Theory(DisplayName = "OnModalDismissParameter Test"), AutoData]
    public void ConfirmationModelContent_OnModalDismissParameter_FiresEvent(
       string expectedModalPrompt,
       string expectedModalMessage)
    {
        //Arrange
        var eventFired = false;
        var cut = RenderComponent<ConfirmationModalContent>(parameters => parameters
            .Add(p => p.State, new ModalRenderFragmentState
            {
                Prompt = expectedModalPrompt,
                Message = expectedModalMessage,
                HandleClose = (args) => Task.CompletedTask,
                HandleDismiss = () =>
                {
                    eventFired = true;
                    return Task.CompletedTask;
                }
            }));

        //Act
        cut.Find(".dismiss").Click();

        //Assert
        eventFired.ShouldBeTrue();
    }
}
