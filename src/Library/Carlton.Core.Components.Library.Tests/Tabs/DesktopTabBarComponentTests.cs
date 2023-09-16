using AutoFixture;
using AutoFixture.Xunit2;
using Carlton.Core.Components.Library.Tests.Common;

namespace Carlton.Core.Components.Library.Tests;

[Trait("Component", nameof(DesktopTabBar))]
public class DesktopTabBarComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test")]
    [InlineAutoData(3, 0)]
    [InlineAutoData(3, 1)]
    [InlineAutoData(3, 2)]
    public void DesktopTabBar_Markup_RendersCorrectly(int numOfTabs, int selectedIndex)
    {
        //Arrange
        var fixture = new Fixture();
        var displayText = fixture.CreateMany<string>(numOfTabs).ToList();
        var icons = fixture.CreateMany<string>(numOfTabs).ToList();
        var childContent = fixture.CreateMany<string>(numOfTabs).ToList();
        var expectedMarkup = BuildExpectedMarkup(displayText, childContent, selectedIndex);

        //Act
        var cut = RenderComponent<DesktopTabBar>(parameters =>
            parameters.AddTabs(numOfTabs, selectedIndex, displayText, icons, childContent));
        var tabs = cut.FindAll(".carlton-tab");
        tabs[selectedIndex].Children.First().Click();

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory(DisplayName = "Tab Count Test, Render Test")]
    [InlineData(3)]
    public void DesktopTabBar_WithThreeTabs_RendersCorrectly(int numOfTabs)
    {
        //Arrange
        var fixture = new Fixture();
        var displayText = fixture.CreateMany<string>(numOfTabs).ToList();
        var icons = fixture.CreateMany<string>(numOfTabs).ToList();
        var childContent = fixture.CreateMany<string>(numOfTabs).ToList();

        //Act
        var cut = RenderComponent<DesktopTabBar>(parameters => parameters.AddTabs(numOfTabs, 0, displayText, icons, childContent));
        var tabs = cut.FindAll(".carlton-tab");
        var count = tabs.Count;

        //Assert
        Assert.Equal(numOfTabs, count);
    }

    [Theory(DisplayName = "Default Active Tab Test")]
    [InlineData(3)]
    public void DesktopTabBar_DefaultActiveTab_RendersCorrectly(int numOfTabs)
    {
        //Arrange
        var fixture = new Fixture();
        var displayText = fixture.CreateMany<string>(numOfTabs).ToList();
        var icons = fixture.CreateMany<string>(numOfTabs).ToList();
        var childContent = fixture.CreateMany<string>(numOfTabs).ToList();

        //Act
        var cut = RenderComponent<DesktopTabBar>(parameters => parameters.AddTabs(numOfTabs, 0, displayText, icons, childContent));

        var defaultTab = cut.FindAll(".carlton-tab")[0];
        var isActive = defaultTab.ClassList.Contains("active");

        //Assert
        Assert.True(isActive);
    }

    [Theory(DisplayName = "Default Active Tab, CSS Active Class Test")]
    [InlineAutoData(3, 0)]
    [InlineAutoData(3, 1)]
    [InlineAutoData(3, 2)]
    public void DesktopTabBar_ActiveTabClass_RendersCorrectly(int numOfTabs, int activeTabIndex)
    {
        //Arrange
        var fixture = new Fixture();
        var displayText = fixture.CreateMany<string>(numOfTabs).ToList();
        var icons = fixture.CreateMany<string>(numOfTabs).ToList();
        var childContent = fixture.CreateMany<string>(numOfTabs).ToList();

        //Act
        var cut = RenderComponent<DesktopTabBar>(parameters => parameters.AddTabs(numOfTabs, activeTabIndex, displayText, icons, childContent));
        var tabs = cut.FindAll(".carlton-tab", true);
        var selectedTab = tabs.ElementAt(activeTabIndex);
        var anchor = selectedTab.Children.First();
        anchor.Click();
        var isActive = tabs.ElementAt(activeTabIndex).ClassList.Contains("active");

        //Assert
        Assert.True(isActive);
    }

    [Theory(DisplayName = "Active Tab, Render Test")]
    [InlineAutoData(3, 0)]
    [InlineAutoData(3, 1)]
    [InlineAutoData(3, 2)]
    public void DesktopTabBar_ActiveTab_RendersCorrectly(int numOfTabs, int activeTabIndex)
    {
        //Arrange
        var fixture = new Fixture();
        var displayText = fixture.CreateMany<string>(numOfTabs).ToList();
        var icons = fixture.CreateMany<string>(numOfTabs).ToList();
        var childContent = fixture.CreateMany<string>(numOfTabs).ToList();
        var expectedContent = childContent.ElementAt(activeTabIndex);

        //Act
        var cut = RenderComponent<DesktopTabBar>(parameters => parameters.AddTabs(numOfTabs, activeTabIndex, displayText, icons, childContent));
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

    private static string BuildExpectedMarkup(List<string> displayText, List<string> childContent, int selectedIndex)
    {
        var tabHeadings = string.Join(Environment.NewLine, displayText.Select((text, i) =>
             @$"<div class=""carlton-tab {(selectedIndex == i ? "active" : string.Empty)}"">
                <a class="""" href=""#"">{text}</a>
                <span class=""tab-selected-bar""></span>
            </div>"
        ));

        var tabs = string.Join(Environment.NewLine, childContent.Select((text, i) =>
        $@"<div class= ""tab"">
            {(selectedIndex == i ? childContent[i] : string.Empty)}
        </div>"));

        return
@$"<div class=""tabs"">
    <div class=""content"">
       {tabHeadings}
    </div>
</div>
{tabs}";
    }
}
