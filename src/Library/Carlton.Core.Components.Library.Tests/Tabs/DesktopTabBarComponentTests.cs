using AutoFixture;
using AutoFixture.Xunit2;
using Carlton.Core.Components.Library.Tests.Common;
using Carlton.Core.Utilities.UnitTesting;

namespace Carlton.Core.Components.Library.Tests;

[Trait("Component", nameof(DesktopTabBar))]
public class DesktopTabBarComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test"), AutoData]
    public void DesktopTabBar_Markup_RendersCorrectly(Fixture fixture)
    {
        //Arrange
        var displayText = fixture.CreateMany<string>(3).ToList();
        var childContent = fixture.CreateMany<string>(3).ToList();
        var selectedIndex = TestingRndUtilities.GetRandomActiveIndex(3);
        var expectedMarkup =
@$"
<div class=""tabs"">
    <div class=""content"">
        <div class=""carlton-tab {(selectedIndex == 0 ? "active" : string.Empty)}"">
            <a class="""" href=""#"">{displayText[0]}</a>
            <span class=""tab-selected-bar""></span>
        </div>
        <div class=""carlton-tab {(selectedIndex == 1 ? "active" : string.Empty)}"">
            <a class="""" href=""#"">{displayText[1]}</a>
            <span class=""tab-selected-bar""></span>
        </div>
        <div class=""carlton-tab {(selectedIndex == 2 ? "active" : string.Empty)}"">
            <a class="""" href=""#"">{displayText[2]}</a>
            <span class=""tab-selected-bar""></span>
        </div>
    </div>
</div>
<div class=""tab"">
{(selectedIndex == 0 ? childContent[0] : string.Empty)}
</div>
<div class=""tab"">
{(selectedIndex == 1 ? childContent[1] : string.Empty)}
</div>
<div class=""tab"">
{(selectedIndex == 2 ? childContent[2] : string.Empty)}
</div>";

        //Act
        var cut = RenderComponent<DesktopTabBar>(parameters => parameters
            .AddChildContent<Tab>(parameters => parameters
                .Add(p => p.DisplayText, displayText[0])
                .Add(p => p.ChildContent, childContent[0]))
            .AddChildContent<Tab>(parameters => parameters
                .Add(p => p.DisplayText, displayText[1])
                .Add(p => p.ChildContent, childContent[1]))
             .AddChildContent<Tab>(parameters => parameters
                .Add(p => p.DisplayText, displayText[2])
                .Add(p => p.ChildContent, childContent[2])));
        var tabs = cut.FindAll(".carlton-tab");
        tabs[selectedIndex].Children.First().Click();

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory(DisplayName = "Tab Count Test, Render Test"), AutoData]
    public void DesktopTabBar_WithThreeTabs_RendersCorrectly(int numOfTabs)
    {
        //Act
        var cut = RenderComponent<DesktopTabBar>(parameters => parameters.AddTabs(numOfTabs));
        var tabs = cut.FindAll(".carlton-tab");
        var count = tabs.Count;

        //Assert
        Assert.Equal(numOfTabs, count);
    }

    [Theory(DisplayName = "Default Active Tab Test"), AutoData]
    public void DesktopTabBar_DefaultActiveTab_RendersCorrectly(int numOfTabs)
    {
        //Act
        var cut = RenderComponent<DesktopTabBar>(parameters => parameters.AddTabs(numOfTabs));

        var defaultTab = cut.FindAll(".carlton-tab")[0];
        var isActive = defaultTab.ClassList.Contains("active");

        //Assert
        Assert.True(isActive);
    }

    [Theory(DisplayName = "Default Active Tab, CSS Active Class Test"), AutoData]
    public void DesktopTabBar_ActiveTabClass_RendersCorrectly(int numOfTabs)
    {
        //Arrange
        var activeTabIndex = TestingRndUtilities.GetRandomActiveIndex(numOfTabs);

        //Act
        var cut = RenderComponent<DesktopTabBar>(parameters => parameters.AddTabs(numOfTabs));
        var tabs = cut.FindAll(".carlton-tab", true);
        var selectedTab = tabs.ElementAt(activeTabIndex);
        var anchor = selectedTab.Children.First();
        anchor.Click();
        var isActive = tabs.ElementAt(activeTabIndex).ClassList.Contains("active");

        //Assert
        Assert.True(isActive);
    }

    [Theory(DisplayName = "Active Tab, Render Test"), AutoData]
    public void DesktopTabBar_ActiveTab_RendersCorrectly(int numOfTabs)
    {
        //Arrange
        var activeTabIndex = TestingRndUtilities.GetRandomActiveIndex(numOfTabs);
        var expectedContent = string.Empty;

        //Act
        var cut = RenderComponent<DesktopTabBar>(parameters => parameters.AddTabs(numOfTabs, activeTabIndex, ref expectedContent));
        var tabs = cut.FindAll(".carlton-tab");
        var selectedTab = tabs.ElementAt(activeTabIndex);
        var anchor = selectedTab.Children.First();
        anchor.Click();
        var actualContent = cut.FindAll(".tab").ElementAt(activeTabIndex).InnerHtml;

        //Assert
        Assert.Equal(expectedContent, actualContent);
    }

    [Theory(DisplayName = "ChildDisplayText Parameter"), AutoData]
    public void DesktopTabBar_ChildDisplayTextParam_RendersCorrectly(string displayText, string icon, string childContent)
    {
        //Act
        var cut = RenderComponent<DesktopTabBar>(parameters => parameters
            .AddChildContent<Tab>(parameters => parameters
                .Add(p => p.DisplayText, displayText)
                .Add(p => p.Icon, icon)
                .Add(p => p.ChildContent, childContent)));

        var anchor = cut.Find("a");
        var actualText = anchor.TextContent;

        //Assert
        Assert.Equal(displayText, actualText);
    }
}
