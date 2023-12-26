using AutoFixture;
using AutoFixture.Xunit2;
using Carlton.Core.Components.Library.Tests.Common;
using Carlton.Core.Utilities.UnitTesting;

namespace Carlton.Core.Components.Library.Tests;

[Trait("Component", nameof(MobileTabBar))]
public class MobileTabBarComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test"), AutoData]
    public void MobileTabBar_Markup_RendersCorrectly(Fixture fixture)
    {
        //Arrange
        var displayText = fixture.CreateMany<string>(3).ToList();
        var icon = fixture.CreateMany<string>(3).ToList();
        var childContent = fixture.CreateMany<string>(3).ToList();
        var selectedIndex = TestingRndUtilities.GetRandomActiveIndex(3);

        var expectedMarkup = 
@$"
<div class="" tab"">{(selectedIndex == 0 ? childContent[selectedIndex] : string.Empty)}</div>
<div class="" tab"">{(selectedIndex == 1 ? childContent[selectedIndex] : string.Empty)}</div>
<div class="" tab"">{(selectedIndex == 2 ? childContent[selectedIndex] : string.Empty)}</div>
<div class="" mobile-tab-bar"">
  <div class="" content"">
    <div class="" mobile-tabs"">
      <div class="" mobile-tab {(selectedIndex == 0 ? "active" : string.Empty)}"">
        <div class="" mobile-tab-link"">
          <i class="" mdi mdi-24px {icon[0]}""></i>
          <span>{displayText[0]}</span>
        </div>
      </div>
      <div class="" mobile-tab {(selectedIndex == 1 ? "active" : string.Empty)}"">
        <div class="" mobile-tab-link"">
          <i class="" mdi mdi-24px {icon[1]}""></i>
          <span>{displayText[1]}</span>
        </div>
      </div>
      <div class="" mobile-tab {(selectedIndex == 2 ? "active" : string.Empty)}"">
        <div class="" mobile-tab-link"">
          <i class="" mdi mdi-24px {icon[2]}""></i>
          <span>{displayText[2]}</span>
        </div>
      </div>
    </div>
  </div>
</div>";

        //Act
        var cut = RenderComponent<MobileTabBar>(parameters => parameters
            .Add(p => p.ShowDisplayIcon, true)
            .Add(p => p.ShowDisplayText, true)
            .AddChildContent<Tab>(parameters => parameters
                .Add(p => p.DisplayText, displayText[0])
                .Add(p => p.Icon, icon[0])
                .Add(p => p.ChildContent, childContent[0]))
            .AddChildContent<Tab>(parameters => parameters
                .Add(p => p.DisplayText, displayText[1])
                .Add(p => p.Icon, icon[1])
                .Add(p => p.ChildContent, childContent[1]))
             .AddChildContent<Tab>(parameters => parameters
                .Add(p => p.DisplayText, displayText[2])
                .Add(p => p.Icon, icon[2])
                .Add(p => p.ChildContent, childContent[2]))
            );

        var tabs = cut.FindAll(".mobile-tab");
        tabs[selectedIndex].Children.First().Click();

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory(DisplayName = "Tab Count Test")]
    [InlineData(3)]
    [InlineData(5)]
    public void MobileTabBar_WithThreeTabs_RendersCorrectly(int numOfTabs)
    {
        //Arrange
        var fixture = new Fixture();
        var displayText = fixture.CreateMany<string>(numOfTabs).ToList();
        var icons = fixture.CreateMany<string>(numOfTabs).ToList();
        var childContent = fixture.CreateMany<string>(numOfTabs).ToList();

        //Act
        var cut = RenderComponent<MobileTabBar>(parameter =>
                            parameter.Add(p => p.ShowDisplayIcon, true)
                                     .Add(p => p.ShowDisplayText, true)
                                     .AddTabs(numOfTabs, 0, displayText, icons, childContent));

        var tabs = cut.FindAll(".mobile-tab");
        var count = tabs.Count;

        //Assert
        Assert.Equal(numOfTabs, count);
    }

    [Theory(DisplayName = "Default Active Tab, Render Test")]
    [InlineData(3)]
    [InlineData(5)]
    public void MobileTabBar_DefaultActiveTab_RendersCorrectly(int numOfTabs)
    {
        //Arrange
        var fixture = new Fixture();
        var displayText = fixture.CreateMany<string>(numOfTabs).ToList();
        var icons = fixture.CreateMany<string>(numOfTabs).ToList();
        var childContent = fixture.CreateMany<string>(numOfTabs).ToList();

        //Act
        var cut = RenderComponent<MobileTabBar>(parameters => parameters
            .Add(p => p.ShowDisplayIcon, true)
            .Add(p => p.ShowDisplayText, true)
            .AddTabs(numOfTabs, 0, displayText, icons, childContent));

        var defaultTab = cut.FindAll(".mobile-tab")[0];
        var isActive = defaultTab.ClassList.Contains("active");

        //Assert
        Assert.True(isActive);
    }

    [Theory(DisplayName = "Default Active Tab, CSS Active Class Test")]
    [InlineData(3, 0)]
    [InlineData(3, 1)]
    [InlineData(3, 2)]
    public void MobileTabBar_ActiveTabClass_RendersCorrectly(int numOfTabs, int selectedTabIndex)
    {
        //Arrange
        var fixture = new Fixture();
        var displayText = fixture.CreateMany<string>(numOfTabs).ToList();
        var icons = fixture.CreateMany<string>(numOfTabs).ToList();
        var childContent = fixture.CreateMany<string>(numOfTabs).ToList();

        //Act
        var cut = RenderComponent<MobileTabBar>(parameters => parameters
            .Add(p => p.ShowDisplayIcon, true)
            .Add(p => p.ShowDisplayText, true)
            .AddTabs(numOfTabs, selectedTabIndex, displayText, icons, childContent));

        var tabs = cut.FindAll(".mobile-tab", true);
        var selectedTab = tabs.ElementAt(selectedTabIndex);
        var anchor = selectedTab.Children.First();
        anchor.Click();
        var isActive = tabs.ElementAt(selectedTabIndex).ClassList.Contains("active");

        //Assert
        Assert.True(isActive);
    }

    [Theory(DisplayName = "Selected Active Tab, CSS Active Class Test")]
    [InlineData(3, 0)]
    [InlineData(3, 1)]
    [InlineData(3, 2)]
    public void MobileTabBar_ActiveTab_RendersCorrectly(int numOfTabs, int selectedTabIndex)
    {
        //Arrange
        var fixture = new Fixture();
        var displayText = fixture.CreateMany<string>(numOfTabs).ToList();
        var icons = fixture.CreateMany<string>(numOfTabs).ToList();
        var childContent = fixture.CreateMany<string>(numOfTabs).ToList();
        var expectedContent = childContent.ElementAt(selectedTabIndex);

        var cut = RenderComponent<MobileTabBar>(parameters => parameters
            .Add(p => p.ShowDisplayIcon, true)
            .Add(p => p.ShowDisplayText, true)
            .AddTabs(numOfTabs, selectedTabIndex, displayText, icons, childContent));

        var tabs = cut.FindAll(".mobile-tab");
        var selectedTab = tabs.ElementAt(selectedTabIndex);
        var linkBtn = selectedTab.Children.First();
        
        //Act
        linkBtn.Click();
        var actualContent = cut.FindAll(".tab").ElementAt(selectedTabIndex).InnerHtml;

        //Assert
        Assert.Equal(expectedContent, actualContent);
    }

    [Theory(DisplayName = "ChildIcon Parameter Test"), AutoData]
    public void MobileTabBar_ChildIconParam_RendersCorrectly(
        string displayText,
        string icon,
        string childContent)
    {
        //Act
        var cut = RenderComponent<MobileTabBar>(parameters => parameters
            .Add(p => p.ShowDisplayIcon, true)
            .Add(p => p.ShowDisplayText, true)
            .AddChildContent<Tab>(parameters => parameters
                .Add(p => p.DisplayText, displayText)
                .Add(p => p.Icon, icon)
                .Add(p => p.ChildContent, childContent))
            );

        var iTag = cut.Find(".mobile-tab-link i");
        var hasIcon = iTag.ClassList.Contains(icon);

        //Assert
        Assert.True(hasIcon);
    }

    [Theory(DisplayName = "ShowDisplayIcon Parameter Test")]
    [InlineAutoData(true)]
    [InlineAutoData(false)]
    public void MobileTabBar_ShowDisplayIconParam_RendersCorrectly(
        bool showDisplayIcon,
        string displayText,
        string icon,
        string childContent)
    {
        //Act
        var cut = RenderComponent<MobileTabBar>(parameters => parameters
            .Add(p => p.ShowDisplayIcon, showDisplayIcon)
            .Add(p => p.ShowDisplayText, true)
            .AddChildContent<Tab>(parameters => parameters
                .Add(p => p.DisplayText, displayText)
                .Add(p => p.Icon, icon)
                .Add(p => p.ChildContent, childContent))
            );

        var iTag = cut.FindAll(".mobile-tab-link i");

        //Assert
        Assert.Equal(showDisplayIcon, iTag.Any());
    }

    [Theory(DisplayName = "ShowDisplayText Parameter Test")]
    [InlineAutoData(true)]
    [InlineAutoData(false)]
    public void MobileTabBar_ShowDisplayTextParam_RendersCorrectly(
        bool showDisplayText,
        string displayText,
        string icon,
        string childContent)
    {
        //Act
        var cut = RenderComponent<MobileTabBar>(parameters => parameters
            .Add(p => p.ShowDisplayIcon, true)
            .Add(p => p.ShowDisplayText, showDisplayText)
            .AddChildContent<Tab>(parameters => parameters
                .Add(p => p.DisplayText, displayText)
                .Add(p => p.Icon, icon)
                .Add(p => p.ChildContent, childContent))
            );

        var span = cut.FindAll(".mobile-tab-link span");

        //Assert
        Assert.Equal(showDisplayText, span.Any());
    }
}
