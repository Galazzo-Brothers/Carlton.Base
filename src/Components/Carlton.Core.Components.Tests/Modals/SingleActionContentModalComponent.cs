using Carlton.Core.Components.Modals;
namespace Carlton.Core.Components.Tests.Modals;

[Trait("Component", nameof(SingleActionModalContent))]
public class SingleActionContentModalComponent : TestContext
{
	private const string ExpectedMarkup = @"
    <div class=""single-action-modal-content"" >
        <span class=""dismiss"">×</span>
        <div class=""modal-prompt-message"">
            <span class=""message-text"">{0}</span>
        </div>
        <div class=""modal-message"">
            <span class=""message-text"">{1}</span>
        </div>
        <div class=""modal-actions"">
            <button class=""btn-ok"">Ok</button>
        </div>
    </div>";

	[Theory(DisplayName = "Markup Test"), AutoData]
	public void SingleActionModelContent_Markup_RendersCorrectly(
		string expectedModalPrompt,
		string expectedModalMessage)
	{
		//Arrange
		var expectedMarkup = string.Format(ExpectedMarkup, expectedModalPrompt, expectedModalMessage);

		//Act
		var cut = RenderComponent<SingleActionModalContent>(parameters => parameters
			.Add(p => p.Prompt, expectedModalPrompt)
			.Add(p => p.Message, expectedModalMessage));

		//Assert
		cut.MarkupMatches(expectedMarkup);
	}

	[Theory(DisplayName = "Markup Test"), AutoData]
	public void SingleActionModelContent_Stub_Markup_RendersCorrectly(
	   string expectedModalPrompt,
	   string expectedModalMessage)
	{
		//Arrange
		ComponentFactories.AddStub<ModalDismissMark>(@"<span class=""dismiss"">×</span>");
		var expectedMarkup = string.Format(ExpectedMarkup, expectedModalPrompt, expectedModalMessage);

		//Act
		var cut = RenderComponent<SingleActionModalContent>(parameters => parameters
			.Add(p => p.Prompt, expectedModalPrompt)
			.Add(p => p.Message, expectedModalMessage));

		//Assert
		cut.MarkupMatches(expectedMarkup);
	}

	[Theory(DisplayName = "OnModalCloseParameter Test"), AutoData]
	public void SingleActionModelContent_OnModalCloseParameter_FiresEvent(
	   string expectedModalPrompt,
	   string expectedModalMessage)
	{
		//Arrange
		var eventFired = false;
		var userConfirmed = false;
		var cut = RenderComponent<SingleActionModalContent>(parameters => parameters
			.Add(p => p.Prompt, expectedModalPrompt)
			.Add(p => p.Message, expectedModalMessage)
			.Add(p => p.OnClose, (confirmed) =>
			{
				eventFired = true;
				userConfirmed = confirmed;
			}));

		//Act
		cut.Find(".btn-ok").Click();

		//Assert
		eventFired.ShouldBeTrue();
		userConfirmed.ShouldBeTrue();
	}

	[Theory(DisplayName = "OnModalDismissParameter Test"), AutoData]
	public void ConfirmationModelContent_OnModalDismissParameter_FiresEvent(
	  string expectedModalPrompt,
	  string expectedModalMessage)
	{
		//Arrange
		var eventFired = false;
		var cut = RenderComponent<SingleActionModalContent>(parameters => parameters
			.Add(p => p.Prompt, expectedModalPrompt)
			.Add(p => p.Message, expectedModalMessage)
			.Add(p => p.OnDismiss, () => eventFired = true));

		//Act
		cut.Find(".dismiss").Click();

		//Assert
		eventFired.ShouldBeTrue();
	}
}
