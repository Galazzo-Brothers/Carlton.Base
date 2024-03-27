using Carlton.Core.LayoutServices.Modals;
namespace Carlton.Core.LayoutServices.Tests.Modals;

[Trait("Component", nameof(ModalTemplate))]
public class ModalTemplateComponentTests : TestContext
{
	private const string ModalContentTemplate = @"<span class=""modal-prompt"">{0}</span><span class=""modal-message"">{1}</span>";

	[Theory(DisplayName = "Markup Test"), AutoData]
	public void Modal_Markup_RendersCorrectly(
	bool expectedIsVisible,
		string expectedModalPrompt,
		string expectedModalMessage)
	{
		//Arrange
		var expectedMarkup =
@$"<div class=""modal {(expectedIsVisible ? "visible" : string.Empty)}"">
    <span class=""modal-prompt"">{expectedModalPrompt}</span>
	<span class=""modal-message"">{expectedModalMessage}</span>
</div>";

		//Act
		var cut = RenderComponent<ModalTemplate>(parameters => parameters
		 .Add(p => p.IsVisible, expectedIsVisible)
		 .Add(p => p.Prompt, expectedModalPrompt)
		 .Add(p => p.Message, expectedModalMessage)
		 .Add(p => p.ModalContent, state => string.Format(ModalContentTemplate, expectedModalPrompt, expectedModalMessage)));

		//Assert
		cut.MarkupMatches(expectedMarkup);
	}
}