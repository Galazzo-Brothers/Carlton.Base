using Carlton.Core.Components.Tabs;
using static Carlton.Core.Components.Tests.Tabs.TabTestHelper;
namespace Carlton.Core.Components.Tests.Tabs;

[Trait("Component", nameof(TextTabBar))]
public class TextTabBarComponentTests : TestContext
{
	[Theory(DisplayName = "Markup Test"), AutoData]
	public void DesktopTabBar_Markup_RendersCorrectly(
		IList<TabConstructionData> expectedTabData)
	{
		//Arrange
		var selectedTabIndex = RandomUtilities.GetRandomIndex(expectedTabData.Count);
		var expectedMarkup = BuildExpectedMarkup(expectedTabData, selectedTabIndex);

		//Act
		var cut = RenderComponent<TextTabBar>(parameters =>
			parameters.Add(p => p.ActiveTabIndex, selectedTabIndex)
					  .AddTabs(expectedTabData));

		//Assert
		cut.MarkupMatches(expectedMarkup);
	}

	[Theory(DisplayName = "Tab Count Test, Render Test"), AutoData]
	public void DesktopTabBar_ItemsParameter_Count_RendersCorrectly(
		IList<TabConstructionData> expectedTabData)
	{
		//Act
		var cut = RenderComponent<TextTabBar>(parameters => parameters.AddTabs(expectedTabData));
		var tabElements = cut.FindAll(".tab");
		var actualCount = tabElements.Count;

		//Assert
		actualCount.ShouldBe(expectedTabData.Count);
	}

	[Theory(DisplayName = "Default Active Tab Test"), AutoData]
	public void DesktopTabBar_DefaultActiveTab_RendersCorrectly(
		IList<TabConstructionData> expectedTabData)
	{
		//Act
		var cut = RenderComponent<TextTabBar>(parameters => parameters.AddTabs(expectedTabData));
		var defaultTab = cut.FindAll(".tab")[0]; //The first tab should be active by default

		//Assert
		defaultTab.ClassList.ShouldContain("active");
	}

	[Theory(DisplayName = "Default Active Tab, CSS Active Class Test"), AutoData]
	public void DesktopTabBar_ActiveTabClass_RendersCorrectly(
		IList<TabConstructionData> expectedTabData)
	{
		//Arrange
		var newTabIndex = RandomUtilities.GetRandomIndex(expectedTabData.Count);

		var cut = RenderComponent<TextTabBar>(parameters =>
		   parameters.AddTabs(expectedTabData));

		var tabs = cut.FindAll(".tab", true);
		var selectedTab = tabs.ElementAt(newTabIndex);
		var anchor = selectedTab.Children.First();

		//Active
		anchor.Click();

		//Assert
		selectedTab = tabs.ElementAt(newTabIndex);
		selectedTab.ClassList.ShouldContain("active"); ;
	}

	[Theory(DisplayName = "Active Tab, Render Test"), AutoData]
	public void DesktopTabBar_ActiveTab_RendersCorrectly(
		IList<TabConstructionData> expectedTabData)
	{
		//Arrange
		var selectedTabIndex = RandomUtilities.GetRandomIndex(expectedTabData.Count);

		//Act
		var cut = RenderComponent<TextTabBar>(parameters =>
			parameters.Add(p => p.ActiveTabIndex, selectedTabIndex)
					  .AddTabs(expectedTabData));

		//Assert
		var contentElement = cut.Find(".content");
		var expectedContent = expectedTabData.ElementAt(selectedTabIndex).ChildContent;
		contentElement.TextContent.ShouldBe(expectedContent);
	}

	[Theory(DisplayName = "ChildDisplayText Parameter"), AutoData]
	public void DesktopTabBar_ChildDisplayTextParameter_RendersCorrectly(
		 IList<TabConstructionData> expectedTabData)
	{
		//Arrange
		var selectedTabIndex = RandomUtilities.GetRandomIndex(expectedTabData.Count);

		//Act
		var cut = RenderComponent<TextTabBar>(parameters =>
			parameters.Add(p => p.ActiveTabIndex, selectedTabIndex)
					  .AddTabs(expectedTabData));

		//Assert
		var expectedTabHeaders = expectedTabData.Select(x => x.DisplayText);
		var tabHeaders = cut.FindAll(".tab-link").Select(x => x.TextContent);
		tabHeaders.ShouldBe(expectedTabHeaders);
	}

	private static string BuildExpectedMarkup(IEnumerable<TabConstructionData> data, int selectedIndex)
	{
		var spacer = @"<span class=""horizontal-spacer"" style=""--spacer-width:100%;--spacer-height:3px;"" ></span>";
		var content = data.ElementAt(selectedIndex).ChildContent;

		var tabs = string.Join(Environment.NewLine, data.Select((data, i) =>
		$@"<div class=""tab {(selectedIndex == i ? "active" : string.Empty)}"">
             <div class=""tab-link"">
                <div class=""text-tab"">
                    <span class=""tab-title"">{data.DisplayText}</span>
                    {(selectedIndex == i ? spacer : string.Empty)}
                </div>
             </div>
        </div>"));

		var tabBar = @$"
        <div class=""text-tab-bar"">
            <div class=""tab-bar"">
                <div class=""content expanded"">
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
