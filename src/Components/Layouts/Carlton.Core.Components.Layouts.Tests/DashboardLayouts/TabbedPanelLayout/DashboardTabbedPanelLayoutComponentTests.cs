using Carlton.Core.Components.Layouts.DashboardLayouts.TabbedPanelLayout;
namespace Carlton.Core.Components.Layouts.Tests.DashboardLayouts.TabbedPanelLayout;

[Trait("Component", nameof(DashboardTabbedPanelLayout))]
public class DashboardTabbedPanelLayoutComponentTests : TestContext
{
    [Fact(DisplayName = "Markup Test")]
    public void DashboardTabbedPanelLayout_Markup_RendersCorrectly()
    {
        //Arrange
        var expectedMarkup = $@"";

        //Act
        var cut = RenderComponent<DashboardTabbedPanelLayout>();


        //Assert
        cut.MarkupMatches(expectedMarkup);
    }
}
