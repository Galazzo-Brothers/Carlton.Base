using Carlton.Core.Layouts.DashboardLayouts.TabbedPanelLayout;
using Carlton.Core.LayoutServices.Viewport;
namespace Carlton.Core.Layouts.Tests.DashboardLayouts.TabbedPanelLayout;


[Trait("Component", nameof(PageTabs))]
public class PageTabsComponentTests : TestContext
{
	[Theory(DisplayName = "Markup Test"), AutoData]
	public void PageTabs_Markup_RendersCorrectly(
		double expectedHeight,
		string expectedContent)
	{
		//Arrange
		var viewportStateMock = Substitute.For<IViewportState>();
		var expectedViewportState = new ViewportModel(expectedHeight, 1000);
		Services.AddSingleton(viewportStateMock);
		viewportStateMock.GetCurrentViewport().Returns(expectedViewportState);
		var expectedMarkup = $@"";

		//Act
		var cut = RenderComponent<PageTabs>(
			parameters => parameters.Add(p => p.TabContent, expectedContent));


		//Assert
		cut.MarkupMatches(expectedMarkup);
	}
}
