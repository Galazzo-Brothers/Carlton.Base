using Bunit;
using Carlton.Core.Flux.Debug.Components.Nav;
using Xunit;
namespace Carlton.Core.Flux.Debug.Tests.Components.Nav;

public class FluxDebugNavMenuComponentTests : TestContext
{
	[Fact]
	public void FluxDebugNavMenu_Markup_RendersCorrectly()
	{
		//Arrange
		var expectedMarkup =
			@"<nav class=""nav-menu"" >
				<div class=""nav-item"">
					<a href=""/debug/logs"">
						<div class=""nav-link-content"">
							<span class=""nav-item-icon mdi mdi-18px mdi-file-search""></span>
							<span class=""nav-item-text"">Logs</span>
						</div>
					</a>
				</div>
				<div class=""nav-item"">
					<a href=""/debug/trace"">
						<div class=""nav-link-content"">
							<span class=""nav-item-icon mdi mdi-18px mdi-weather-lightning""></span>
							<span class=""nav-item-text"">Event Tracing</span>
						</div>
					</a>
				</div>
				<div class=""nav-item"">
					<a href=""/debug/flux-state"">
						<div class=""nav-link-content"">
							<span class=""nav-item-icon mdi mdi-18px mdi-database""></span>
							<span class=""nav-item-text"">Flux State</span>
						</div>
					</a>
				</div>
			</nav>";

		//Act
		var cut = RenderComponent<FluxDebugNavMenu>();

		//Assert
		cut.MarkupMatches(expectedMarkup);
	}
}
