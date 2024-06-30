using Bunit.TestDoubles;
using Microsoft.Extensions.DependencyInjection;

namespace Carlton.Core.Lab.Test.Layout;

public class DebugMenuComponentTests : TestContext
{
	[Fact]
	public void DebugMenu_Markup_RendersCorrectly()
	{
		//Arrange
		var expectedMarkup = $@"
			<a href=""#"" class=""debug-menu""   >
				<i class=""mdi mdi-24px mdi-bug-outline"" ></i>
			</a>";

		//Act
		var cut = RenderComponent<DebugMenu>();

		//Assert
		cut.MarkupMatches(expectedMarkup);
	}

	[Fact]
	public void DebugMenu_Click_NavigatesCorrectly()
	{
		//Arrange
		var cut = RenderComponent<DebugMenu>();
		var navMan = Services.GetRequiredService<FakeNavigationManager>();

		//Act
		cut.Find("a").Click();

		//Assert
		navMan.Uri.ShouldBe("http://localhost/debug/logs");
	}
}
