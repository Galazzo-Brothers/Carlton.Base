using Carlton.Core.Foundation.Test;
using Carlton.Core.Lab.Layouts;
using Carlton.Core.LayoutServices.FullScreen;
using Microsoft.Extensions.DependencyInjection;
namespace Carlton.Core.Lab.Test.Layout;

public class LabLayoutComponentTests : TestContext
{
	[Theory, AutoNSubstituteData]
	public void LabLayout_Markup_RendersCorrectly(
		string body,
		IFullScreenState fullScreenState)
	{
		//Arrange
		Services.AddSingleton(fullScreenState);
		var expectedMarkup = $@"{body}";

		//Act
		var cut = RenderComponent<LabLayout>(parameters =>
			parameters.Add(p => p.Body, body));

		//Assert
		cut.MarkupMatches(expectedMarkup);
	}
}
