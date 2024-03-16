using Carlton.Core.Lab.Layouts;
namespace Carlton.Core.Lab.Test.Layout;

public class LabTabsComponentTests : TestContext
{
	[Fact]
	public void LabTabs_Markup_RendersCorrectly()
	{
		//Arrange
		var expectedMarkup = $@"";

		//Act
		var cut = RenderComponent<LabTabs>();

		//Assert
		cut.MarkupMatches(expectedMarkup);
	}
}
