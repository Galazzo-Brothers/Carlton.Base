using AutoFixture.Xunit2;

namespace Carlton.Core.Components.Library.Tests;

[Trait("Component", nameof(TabComponentTests))]
public class TabComponentTests : TestContext
{
    private readonly IRenderedComponent<TabBarBase> parent;

    public TabComponentTests()
    {
        parent = RenderComponent<TabBarBase>();
    }

    [Theory(DisplayName = "Tab Without Parent, Throws Argument Exception"), AutoData]
    public void Tab_WithOutParent_ThrowsArgumentNullException(string displayText)
    {
        //Act
        var act = () => RenderComponent<Tab>(parameters => parameters
            .Add(p => p.DisplayText, displayText));

        //Assert
        Assert.Throws<ArgumentNullException>(act);
    }

    [Theory(DisplayName = "Markup Test"), AutoData]
    public void Tab_Markup_RendersCorrectly(string expectedContent, string displayText)
    {
        //Arrange
        var expectedMarkup = $@"<div class=""tab"">{expectedContent}</div>";

        //Act
        var cut = RenderComponent<Tab>(parameters => parameters
                .AddCascadingValue(parent.Instance)
                .Add(p => p.DisplayText, displayText)
                .AddChildContent(expectedContent));

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory(DisplayName = "Parent ActiveParam Render Test"), AutoData]
    public void Tab_Parent_ActiveParam_False_RendersCorrectly(string displayText, string childContent)
    {
        //Arrange
        var cut = RenderComponent<Tab>(parameters => parameters
            .AddCascadingValue(parent.Instance)
            .Add(p => p.DisplayText, displayText)
            .Add(p => p.ChildContent, childContent));

        var tabElement = cut.Find(".tab");

        //Act
        parent.Instance.ActiveTab = null;
        cut.Render();

        //Assert
        Assert.Empty(tabElement.InnerHtml);
    }

    [Theory(DisplayName = "Parent Active True, Render Test"), AutoData]
    public void Tab_Parent_ActiveParam_True_RendersCorrectly(string displayText, string childContent)
    {
        //Arrange
        var cut = RenderComponent<Tab>(parameters => parameters
            .AddCascadingValue(parent.Instance)
            .Add(p => p.DisplayText, displayText)
            .AddChildContent(childContent));

        var tabElement = cut.Find(".tab");

        //Act
        parent.Instance.ActiveTab = cut.Instance;
        cut.Render();

        //Assert
        Assert.NotEmpty(tabElement.InnerHtml);
        Assert.Equal(childContent, tabElement.InnerHtml);
    }
}
