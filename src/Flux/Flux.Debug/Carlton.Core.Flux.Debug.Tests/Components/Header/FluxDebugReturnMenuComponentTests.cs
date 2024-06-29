using Bunit;
using Bunit.TestDoubles;
using Carlton.Core.Flux.Debug.Components.Header;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;
namespace Carlton.Core.Flux.Debug.Tests.Components.Header;

public class FluxDebugReturnMenuComponentTests : TestContext
{
	[Fact]
	public void FluxDebugReturnMenu_Markup_ShouldNavigate()
	{
		//Arrange
		var expectedMarkup = @"
		<a href=""#"" class=""return-menu""   >
			<i class=""mdi mdi-24px mdi-open-in-app"" ></i>
		</a>";

		//Act
		var cut = RenderComponent<FluxDebugReturnMenu>();

		//Assert
		cut.MarkupMatches(expectedMarkup);
	}

	[Fact]
	public void FluxDebugReturnMenu_ClickNavigateToDebug_ShouldNavigate()
	{
		//Arrange
		var navigationManager = Services.GetRequiredService<FakeNavigationManager>();
		var cut = RenderComponent<FluxDebugReturnMenu>();
		var anchor = cut.Find("a");

		//Act
		anchor.Click();

		//Assert
		navigationManager.Uri.ShouldBe("http://localhost/");
	}
}
