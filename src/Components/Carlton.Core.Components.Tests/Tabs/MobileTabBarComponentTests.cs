using Carlton.Core.Components.Tabs;
using Carlton.Core.Components.Tests.Tabs;
using static Carlton.Core.Components.Tests.Tabs.TabTestHelper;
namespace Carlton.Core.Components.Tests;

[Trait("Component", nameof(MobileDrawerTabBar))]
public class MobileTabBarComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test"), AutoData]
    public void MobileTabBar_Markup_RendersCorrectly(
        IList<TabConstructionData> expectedTabData)
    {
        //Arrange
        var selectedTabIndex = RandomUtilities.GetRandomIndex(expectedTabData.Count);
        var expectedMarkup = BuildExpectedMarkup(expectedTabData, selectedTabIndex);

        //Act
        var cut = RenderComponent<MobileDrawerTabBar>(parameters =>
                    parameters.Add(p => p.ActiveTabIndex, selectedTabIndex)
                              .AddTabs(expectedTabData));
        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory(DisplayName = "Tab Count Test"), AutoData]
    public void MobileTabBar_ItemsParameter_Count_RendersCorrectly(
         IList<TabConstructionData> expectedTabData)
    {
        //Act
        var cut = RenderComponent<MobileDrawerTabBar>(parameter =>
                            parameter.AddTabs(expectedTabData));

        var tabs = cut.FindAll(".tab");

        //Assert
        var count = tabs.Count;
        count.ShouldBe(expectedTabData.Count);
    }

    [Theory(DisplayName = "Default Active Tab, Render Test"), AutoData]
    public void MobileTabBar_DefaultActiveTab_RendersCorrectly(
         IList<TabConstructionData> expectedTabData)
    {
        //Act
        var cut = RenderComponent<MobileDrawerTabBar>(parameter =>
                            parameter.AddTabs(expectedTabData));

        var defaultTab = cut.FindAll(".tab")[0];

        //Assert
        defaultTab.ClassList.ShouldContain("active");
    }

    [Theory(DisplayName = "Default Active Tab, CSS Active Class Test"), AutoData]
    public void MobileTabBar_ActiveTabClass_RendersCorrectly(
         IList<TabConstructionData> expectedTabData)
    {
        //Arrange
        var selectedTabIndex = RandomUtilities.GetRandomIndex(expectedTabData.Count);
        var cut = RenderComponent<MobileDrawerTabBar>(parameters =>
                    parameters.Add(p => p.ActiveTabIndex, selectedTabIndex)
                              .AddTabs(expectedTabData));

        var tabs = cut.FindAll(".tab", true);
        var selectedTab = tabs.ElementAt(selectedTabIndex);
        var anchor = selectedTab.Children.First();

        //Act
        anchor.Click();

        //Assert
        var activeTab = tabs.ElementAt(selectedTabIndex);
        activeTab.ClassList.Contains("active");
    }

    [Theory(DisplayName = "Selected Active Tab, CSS Active Class Test"), AutoData]
    public void MobileTabBar_ActiveTab_RendersCorrectly(
        IList<TabConstructionData> expectedTabData)
    {
        //Arrange
        var selectedTabIndex = RandomUtilities.GetRandomIndex(expectedTabData.Count);
        var cut = RenderComponent<MobileDrawerTabBar>(parameters =>
                    parameters.Add(p => p.ActiveTabIndex, selectedTabIndex)
                              .AddTabs(expectedTabData));
        var tabToClick = cut.FindAll(".tab-link").ElementAt(selectedTabIndex);

        //Act
        tabToClick.Click();

        //Assert
        var tabElement = cut.FindAll(".tab").ElementAt(selectedTabIndex);
        tabElement.ClassList.ShouldContain("active");
    }

    [Theory(DisplayName = "ChildIcon Parameter Test"), AutoData]
    public void MobileTabBar_ChildIconParam_RendersCorrectly(
         IList<TabConstructionData> expectedTabData)
    {
        //Arrange
        var selectedTabIndex = RandomUtilities.GetRandomIndex(expectedTabData.Count);
        
        //Act
        var cut = RenderComponent<MobileDrawerTabBar>(parameters =>
                    parameters.Add(p => p.ActiveTabIndex, selectedTabIndex)
                              .AddTabs(expectedTabData));

        var iconClasses = cut.FindAll("i")
                             .SelectMany(_ => _.ClassList)
                             .Where(_ => _ != "mdi" && _ != "mdi-24px");

        var expectedClasses = expectedTabData.Select(_ => _.IconClass);

        //Assert
        iconClasses.ShouldBe(expectedClasses);
    }

    private static string BuildExpectedMarkup(IList<TabConstructionData> data, int selectedIndex)
    {
        var slideButton = @"<button class=""slide-button""></button>";
        var content = data.ElementAt(selectedIndex).ChildContent;

        var tabs = string.Join(Environment.NewLine, data.Select((tab, i) => $@"
        <div class=""tab {(selectedIndex == i ? "active" : string.Empty)}"" >
            <div class=""tab-link"">
                <i class=""mdi mdi-24px {tab.IconClass}""></i>
            </div>
        </div>"));

        var tabBar = @$"
        <div class=""mobile-collapsable-tab-bar"">
            <div class=""tab-bar"">
                {slideButton}
                <div class=""content"">
                    {content}
                </div>
                <div class=""tabs"">
                    {tabs}
                </div>
            </div>
        </div>";

        return tabBar;
    }
}
