using Carlton.Core.Components.Tabs;
namespace Carlton.Core.Components.Tests.Tabs;

[Trait("Component", nameof(DrawerTab))]
public class DrawerTabComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test"), AutoData]
    public void DrawerTab_Collapsed_Markup_RendersCorrectly(
        string expectedTitle,
        bool expectedIsExpanded,
        string expectedContent)
    {
        //Arrange
        var expectedMarkup = 
@$"<div class=""tab-bar"">
    <button class=""slide-button"">{expectedTitle}</button>
    <div class=""content {(expectedIsExpanded ? "expanded" : string.Empty)}"">
        {expectedContent}
    </div>
</div>";

        //Act
        var cut = RenderComponent<DrawerTab>(parameters => parameters
            .Add(p => p.Title, expectedTitle)
            .Add(p => p.IsExpanded, expectedIsExpanded)
            .Add(p => p.ChildContent, expectedContent));

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory(DisplayName = "Title Parameter Test"), AutoData]
    public void DrawerTab_TitleParam_RendersCorrectly(
        string expectedTitle,
        bool expectedIsExpanded,
        string expectedContent)
    {
        //Act
        var cut = RenderComponent<DrawerTab>(parameters => parameters
            .Add(p => p.Title, expectedTitle)
            .Add(p => p.IsExpanded, expectedIsExpanded)
            .Add(p => p.ChildContent, expectedContent));

        var btn = cut.Find("button");
        var actualTitle = btn.TextContent;

        //Assert
        actualTitle.ShouldBe(expectedTitle);
    }

    [Theory(DisplayName = "IsExpanded Parameter Test"), AutoData]
    public void DrawerTab_IsExpandedParam_RendersCorrectly(
        string expectedTitle,
        bool expectedIsExpanded,
        string expectedContent)
    {
        //Act
        var cut = RenderComponent<DrawerTab>(parameters => parameters
           .Add(p => p.Title, expectedTitle)
           .Add(p => p.IsExpanded, expectedIsExpanded)
           .Add(p => p.ChildContent, expectedContent));

        var slideContainer = cut.Find(".content");
        var actualIsExpanded = slideContainer.ClassList.Contains("expanded");

        //Assert
        actualIsExpanded.ShouldBe(expectedIsExpanded);
    }

    [Theory(DisplayName = "Content Parameter Test"), AutoData]
    public void DrawerTab_ContentParam_RendersCorrectly(
        string expectedTitle,
        bool expectedIsExpanded,
        string expectedContent)
    {
        //Act
        var cut = RenderComponent<DrawerTab>(parameters => parameters
         .Add(p => p.Title, expectedTitle)
         .Add(p => p.IsExpanded, expectedIsExpanded)
         .Add(p => p.ChildContent, expectedContent));

        var slideContainer = cut.Find(".content");
        var actualContent= slideContainer.InnerHtml;

        //Assert
        actualContent.ShouldBe(expectedContent);
    }
}
