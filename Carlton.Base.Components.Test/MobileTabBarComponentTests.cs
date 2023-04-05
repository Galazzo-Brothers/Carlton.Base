namespace Carlton.Base.Components.Test;

public class MobileTabBarComponentTests : TestContext
{

    private readonly string MobileTabBarMarkup = @"
<div class="" tab"">
  <span>This is the first tab</span>
</div>
<div class="" tab""></div>
<div class="" tab""></div>

<div class="" mobile-tab-bar"" b-9b34pv0gms>
  <div class="" content"" b-9b34pv0gms>
    <div class="" mobile-tabs"" b-9b34pv0gms>
      <div class="" mobile-tab active"" b-9b34pv0gms>
        <div class="" mobile-tab-link"" blazor:onclick="" 1"" b-9b34pv0gms>
          <i class="" mdi mdi-24px Icon 1"" b-9b34pv0gms></i>
          <span b-9b34pv0gms>Test Tab 1</span>
        </div>
      </div>
      <div class="" mobile-tab "" b-9b34pv0gms>
        <div class="" mobile-tab-link"" blazor:onclick="" 2"" b-9b34pv0gms>
          <i class="" mdi mdi-24px Icon 2"" b-9b34pv0gms></i>
          <span b-9b34pv0gms>Test Tab 2</span>
        </div>
      </div>
      <div class="" mobile-tab "" b-9b34pv0gms>
        <div class="" mobile-tab-link"" blazor:onclick="" 3"" b-9b34pv0gms>
          <i class="" mdi mdi-24px Icon 3"" b-9b34pv0gms></i>
          <span b-9b34pv0gms>Test Tab 3</span>
        </div>
      </div>
    </div>
  </div>
</div>";


    [Fact]
    public void MobileTabBar_Markup_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<MobileTabBar>(paramaters => paramaters
            .Add(p => p.ShowDisplayIcon, true)
            .Add(p => p.ShowDisplayText, true)
            .AddChildContent<Tab>(parameters => parameters
                .Add(p => p.DisplayText, "Test Tab 1")
                .Add(p => p.Icon, "Icon 1")
                .Add(p => p.ChildContent, "<span>This is the first tab</span>"))
            .AddChildContent<Tab>(parameters => parameters
                .Add(p => p.DisplayText, "Test Tab 2")
                .Add(p => p.Icon, "Icon 2")
                .Add(p => p.ChildContent, "<span>This is the second tab</span>"))
             .AddChildContent<Tab>(parameters => parameters
                .Add(p => p.DisplayText, "Test Tab 3")
                .Add(p => p.Icon, "Icon 3")
                .Add(p => p.ChildContent, "<span>This is the third tab</span>"))
            );

        //Assert
        cut.MarkupMatches(MobileTabBarMarkup);
    }

    [Fact]
    public void MobileTabBar_WithOneTab_RendersCorrectly()
    {
        //Arrange
        var expectedCount = 1;

        //Act
        var cut = RenderComponent<MobileTabBar>(paramaters => paramaters
                    .Add(p => p.ShowDisplayIcon, true)
                    .Add(p => p.ShowDisplayText, true)
                    .AddChildContent<Tab>(parameters => parameters
                        .Add(p => p.DisplayText, "Test Tab 1")
                        .Add(p => p.Icon, "Icon 1")
                        .Add(p => p.ChildContent, "<span>This is the first tab</span>")));

        var tabs = cut.FindAll(".mobile-tab");
        var count = tabs.Count;


        //Assert
        Assert.Equal(expectedCount, count);
    }

    [Fact]
    public void MobileTabBar_WithTwoTabs_RendersCorrectly()
    {
        //Arrange
        var expectedCount = 2;

        //Act
        var cut = RenderComponent<MobileTabBar>(paramaters => paramaters
                    .Add(p => p.ShowDisplayIcon, true)
                    .Add(p => p.ShowDisplayText, true)
                    .AddChildContent<Tab>(parameters => parameters
                        .Add(p => p.DisplayText, "Test Tab 1")
                        .Add(p => p.Icon, "Icon 1")
                        .Add(p => p.ChildContent, "<span>This is the first tab</span>"))
                    .AddChildContent<Tab>(parameters => parameters
                        .Add(p => p.DisplayText, "Test Tab 2")
                        .Add(p => p.Icon, "Icon 2")
                        .Add(p => p.ChildContent, "<span>This is the second tab</span>")));

        var tabs = cut.FindAll(".mobile-tab");
        var count = tabs.Count;


        //Assert
        Assert.Equal(expectedCount, count);
    }

    [Fact]
    public void MobileTabBar_WithThreeTabs_RendersCorrectly()
    {
        //Arrange
        var expectedCount = 3;

        //Act
        var cut = RenderComponent<MobileTabBar>(paramaters => paramaters
                    .Add(p => p.ShowDisplayIcon, true)
                    .Add(p => p.ShowDisplayText, true)
                    .AddChildContent<Tab>(parameters => parameters
                        .Add(p => p.DisplayText, "Test Tab 1")
                        .Add(p => p.Icon, "Icon 1")
                        .Add(p => p.ChildContent, "<span>This is the first tab</span>"))
                    .AddChildContent<Tab>(parameters => parameters
                        .Add(p => p.DisplayText, "Test Tab 2")
                        .Add(p => p.Icon, "Icon 2")
                        .Add(p => p.ChildContent, "<span>This is the second tab</span>"))
                    .AddChildContent<Tab>(parameters => parameters
                        .Add(p => p.DisplayText, "Test Tab 3")
                        .Add(p => p.Icon, "Icon 3")
                        .Add(p => p.ChildContent, "<span>This is the third tab</span>")));

        var tabs = cut.FindAll(".mobile-tab");
        var count = tabs.Count;


        //Assert
        Assert.Equal(expectedCount, count);
    }

    [Fact]
    public void MobileTabBar_DefaultActiveTab_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<MobileTabBar>(paramaters => paramaters
            .Add(p => p.ShowDisplayIcon, true)
            .Add(p => p.ShowDisplayText, true)
            .AddChildContent<Tab>(parameters => parameters
                .Add(p => p.DisplayText, "Test Tab 1")
                .Add(p => p.Icon, "Icon 1")
                .Add(p => p.ChildContent, "<span>This is the first tab</span>"))
            .AddChildContent<Tab>(parameters => parameters
                .Add(p => p.DisplayText, "Test Tab 2")
                .Add(p => p.Icon, "Icon 2")
                .Add(p => p.ChildContent, "<span>This is the second tab</span>"))
             .AddChildContent<Tab>(parameters => parameters
                .Add(p => p.DisplayText, "Test Tab 3")
                .Add(p => p.Icon, "Icon 2")
                .Add(p => p.ChildContent, "<span>This is the third tab</span>"))
            );

        var defaultTab = cut.FindAll(".mobile-tab")[0];
        var isActive = defaultTab.ClassList.Contains("active");

        //Assert
        Assert.True(isActive);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public void MobileTabBar_ActiveTabClass_RendersCorrectly(int activeTabIndex)
    {
        //Act
        var cut = RenderComponent<MobileTabBar>(paramaters => paramaters
            .Add(p => p.ShowDisplayIcon, true)
            .Add(p => p.ShowDisplayText, true)
            .AddChildContent<Tab>(parameters => parameters
                .Add(p => p.DisplayText, "Test Tab 1")
                .Add(p => p.Icon, "Icon 1")
                .Add(p => p.ChildContent, "<span>This is the first tab</span>"))
            .AddChildContent<Tab>(parameters => parameters
                .Add(p => p.DisplayText, "Test Tab 2")
                .Add(p => p.Icon, "Icon 2")
                .Add(p => p.ChildContent, "<span>This is the second tab</span>"))
             .AddChildContent<Tab>(parameters => parameters
                .Add(p => p.DisplayText, "Test Tab 3")
                .Add(p => p.Icon, "Icon 3")
                .Add(p => p.ChildContent, "<span>This is the third tab</span>"))
            );

        var tabs = cut.FindAll(".mobile-tab", true);
        var selectedTab = tabs.ElementAt(activeTabIndex);
        var anchor = selectedTab.Children.First();
        anchor.Click();
        var isActive = tabs.ElementAt(activeTabIndex).ClassList.Contains("active");

        //Assert
        Assert.True(isActive);
    }

    [Theory]
    [InlineData(0, "<span>This is the first tab</span>")]
    [InlineData(1, "<span>This is the second tab</span>")]
    [InlineData(2, "<span>This is the third tab</span>")]
    public void MobileTabBar_ActiveTab_RendersCorrectly(int activeTabIndex, string expectedText)
    {
        //Act
        var cut = RenderComponent<MobileTabBar>(paramaters => paramaters
            .Add(p => p.ShowDisplayIcon, true)
            .Add(p => p.ShowDisplayText, true)
            .AddChildContent<Tab>(parameters => parameters
                .Add(p => p.DisplayText, "Test Tab 1")
                .Add(p => p.Icon, "Icon 1")
                .Add(p => p.ChildContent, "<span>This is the first tab</span>"))
            .AddChildContent<Tab>(parameters => parameters
                .Add(p => p.DisplayText, "Test Tab 2")
                .Add(p => p.Icon, "Icon 2")
                .Add(p => p.ChildContent, "<span>This is the second tab</span>"))
             .AddChildContent<Tab>(parameters => parameters
                .Add(p => p.DisplayText, "Test Tab 3")
                .Add(p => p.Icon, "Icon 3")
                .Add(p => p.ChildContent, "<span>This is the third tab</span>"))
            );

        var tabs = cut.FindAll(".mobile-tab");
        var selectedTab = tabs.ElementAt(activeTabIndex);
        var linkBtn = selectedTab.Children.First();
        linkBtn.Click();
        var actualText = cut.FindAll(".tab").ElementAt(activeTabIndex).InnerHtml;

        //Assert
        Assert.Equal(expectedText, actualText);
    }

    [Theory]
    [InlineData("Icon-Test")]
    [InlineData("Icon-Still-Testing")]
    [InlineData("Icon-Done-Testing")]
    public void MobileTabBar_ChildIconParam_RendersCorrectly(string icon)
    {
        //Act
        var cut = RenderComponent<MobileTabBar>(paramaters => paramaters
            .Add(p => p.ShowDisplayIcon, true)
            .Add(p => p.ShowDisplayText, true)
            .AddChildContent<Tab>(parameters => parameters
                .Add(p => p.DisplayText, "Test Text")
                .Add(p => p.Icon, icon)
                .Add(p => p.ChildContent, "<span>This is the first tab</span>"))
            );

        var iTag = cut.Find(".mobile-tab-link i");
        var hasIcon = iTag.ClassList.Contains(icon);

        //Assert
        Assert.True(hasIcon);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void MobileTabBar_ShowDisplayIconParam_RendersCorrectly(bool showDisplayIcon)
    {
        //Act
        var cut = RenderComponent<MobileTabBar>(paramaters => paramaters
            .Add(p => p.ShowDisplayIcon, showDisplayIcon)
            .Add(p => p.ShowDisplayText, true)
            .AddChildContent<Tab>(parameters => parameters
                .Add(p => p.DisplayText, "Test Text")
                .Add(p => p.Icon, "icon")
                .Add(p => p.ChildContent, "<span>This is the first tab</span>"))
            );

        var iTag = cut.FindAll(".mobile-tab-link i");

        //Assert
        Assert.Equal(showDisplayIcon, iTag.Any());
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void MobileTabBar_ShowDisplayTextParam_RendersCorrectly(bool showDisplayText)
    {
        //Act
        var cut = RenderComponent<MobileTabBar>(paramaters => paramaters
            .Add(p => p.ShowDisplayIcon, true)
            .Add(p => p.ShowDisplayText, showDisplayText)
            .AddChildContent<Tab>(parameters => parameters
                .Add(p => p.DisplayText, "Test Text")
                .Add(p => p.Icon, "icon")
                .Add(p => p.ChildContent, "<span>This is the first tab</span>"))
            );

        var span = cut.FindAll(".mobile-tab-link span");

        //Assert
        Assert.Equal(showDisplayText, span.Any());
    }
}
