using AutoFixture.Xunit2;

namespace Carlton.Core.Components.Library.Tests;

[Trait("Component", nameof(SlideTab))]
public class SlideTabComponentTests : TestContext
{
    private const string ContentTemplate = "<span class=\"content\">{0}</span>";

    [Theory(DisplayName = "Markup Test"), AutoData]
    public void SlideTab_Collapsed_Markup_RendersCorrectly(
        string title,
        int positionBottom,
        string content)
    {
        //Arrange
        var expectedMarkup = 
@$"<div class=""slide-tab"" style=""--slide-tab-bottom:{positionBottom}px;"">
    <button class=""slide-button"">{title}</button>
    <div class=""slide-container"">
        <span class=""content"">{content}</span>
    </div>
</div>";

        //Act
        var cut = RenderComponent<SlideTab>(parameters => parameters
            .Add(p => p.Title, title)
            .Add(p => p.IsExpanded, false)
            .Add(p => p.PositionBottom, positionBottom)
            .Add(p => p.Content, string.Format(ContentTemplate, content))
            );

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }


    [Theory(DisplayName = "Markup Test"), AutoData]
    public void SlideTab_Expanded_Markup_RendersCorrectly(
        string title,
        int positionBottom,
        string content)
    {
        //Arrange
        var expectedMarkup =
@$"<div class=""slide-tab"" style=""--slide-tab-bottom:{positionBottom}px;"">
    <button class=""slide-button"">{title}</button>
    <div class=""slide-container expanded"">
        <span class=""content"">{content}</span>
    </div>
</div>";

        //Act
        var cut = RenderComponent<SlideTab>(parameters => parameters
            .Add(p => p.Title, title)
            .Add(p => p.IsExpanded, true)
            .Add(p => p.PositionBottom, positionBottom)
            .Add(p => p.Content, string.Format(ContentTemplate, content))
            );

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory(DisplayName = "Title Parameter Test"), AutoData]
    public void SlideTab_TitleParam_RendersCorrectly(
        string title,
        bool isExpanded,
        int positionBottom,
        string content)
    {
        //Act
        var cut = RenderComponent<SlideTab>(parameters => parameters
            .Add(p => p.Title, title)
            .Add(p => p.IsExpanded, isExpanded)
            .Add(p => p.PositionBottom, positionBottom)
            .Add(p => p.Content, string.Format(ContentTemplate, content))
            );

        var btn = cut.Find("button");
        var actualTitle = btn.TextContent;

        //Assert
        Assert.Equal(title, actualTitle);
    }

    [Theory(DisplayName = "IsExpanded Parameter Test")]
    [InlineAutoData(true)]
    [InlineAutoData(false)]
    public void SlideTab_IsExpandedParam_RendersCorrectly(
        bool isExpanded,
        string title,
        int positionBottom,
        string content)
    {
        //Act
        var cut = RenderComponent<SlideTab>(parameters => parameters
           .Add(p => p.Title, title)
           .Add(p => p.IsExpanded, isExpanded)
           .Add(p => p.PositionBottom, positionBottom)
           .Add(p => p.Content, string.Format(ContentTemplate, content))
           );

        var slideContainer = cut.Find(".slide-container");
        var actualIsExpanded = slideContainer.ClassList.Contains("expanded");

        //Assert
        Assert.Equal(isExpanded, actualIsExpanded);
    }

    [Theory(DisplayName = "PositionBottom Parameter Test"), AutoData]
    public void SlideTab_PositionBottomParam_RendersCorrectly(
        string title,
        bool isExpanded,
        int positionBottom,
        string content)
    {
        //Arrange
        var expectedStyleValue = $"--slide-tab-bottom:{positionBottom}px;";

        //Act
        var cut = RenderComponent<SlideTab>(parameters => parameters
          .Add(p => p.Title, title)
          .Add(p => p.IsExpanded, isExpanded)
          .Add(p => p.PositionBottom, positionBottom)
          .Add(p => p.Content, string.Format(ContentTemplate, content))
          );

        var slideTab = cut.Find(".slide-tab");
        var actualStyleValue = slideTab.Attributes.First(_ => _.Name == "style").Value;

        //Assert
        Assert.Equal(expectedStyleValue, actualStyleValue);
    }

    [Theory(DisplayName = "Content Parameter Test"), AutoData]
    public void SlideTab_ContentParam_RendersCorrectly(
        string title,
        bool isExpanded,
        int positionBottom,
        string content)
    {
        //Arrange
        var expectedContent = string.Format(ContentTemplate, content);

        //Act
        var cut = RenderComponent<SlideTab>(parameters => parameters
         .Add(p => p.Title, title)
         .Add(p => p.IsExpanded, isExpanded)
         .Add(p => p.PositionBottom, positionBottom)
         .Add(p => p.Content, string.Format(ContentTemplate, content))
         );

        var slideContainer = cut.Find(".slide-container");
        var actualContent= slideContainer.InnerHtml;

        //Assert
        Assert.Equal(expectedContent, actualContent);
    }
}
