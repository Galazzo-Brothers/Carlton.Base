using AutoFixture.Xunit2;

namespace Carlton.Core.Components.Library.Tests;

[Trait("Component", nameof(SlideTab))]
public class SlideTabComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test")]
    [InlineAutoData(true, "<span class=\"content\">{0}</span>")]
    [InlineAutoData(true, "<div class=\"content\">{0}</div>")]
    [InlineAutoData(false, "<span class=\"content\">{0}</span>")]
    [InlineAutoData(false, "<div class=\"content\">{0}</div>")]
    public void SlideTab_Collapsed_Markup_RendersCorrectly(
        bool isExpanded,
        string contentTemplate,
        string title,
        int positionBottom,
        string content)
    {
        //Arrange
        var expectedMarkup = 
@$"<div class=""slide-tab"" style=""--slide-tab-bottom:{positionBottom}px;"">
    <button class=""slide-button"">{title}</button>
    <div class=""slide-container {(isExpanded ? "expanded" : string.Empty)}"">
        {string.Format(contentTemplate, content)}
    </div>
</div>";

        //Act
        var cut = RenderComponent<SlideTab>(parameters => parameters
            .Add(p => p.Title, title)
            .Add(p => p.IsExpanded, isExpanded)
            .Add(p => p.PositionBottom, positionBottom)
            .Add(p => p.Content, string.Format(contentTemplate, content)));

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory(DisplayName = "Title Parameter Test")]
    [InlineAutoData(true, "<span class=\"content\">{0}</span>")]
    [InlineAutoData(true, "<div class=\"content\">{0}</div>")]
    [InlineAutoData(false, "<span class=\"content\">{0}</span>")]
    [InlineAutoData(false, "<div class=\"content\">{0}</div>")]
    public void SlideTab_TitleParam_RendersCorrectly(
        bool isExpanded,
        string contentTemplate,
        string title,
        int positionBottom,
        string content)
    {
        //Act
        var cut = RenderComponent<SlideTab>(parameters => parameters
            .Add(p => p.Title, title)
            .Add(p => p.IsExpanded, isExpanded)
            .Add(p => p.PositionBottom, positionBottom)
            .Add(p => p.Content, string.Format(contentTemplate, content)));

        var btn = cut.Find("button");
        var actualTitle = btn.TextContent;

        //Assert
        Assert.Equal(title, actualTitle);
    }

    [Theory(DisplayName = "IsExpanded Parameter Test")]
    [InlineAutoData(true, "<span class=\"content\">{0}</span>")]
    [InlineAutoData(true, "<div class=\"content\">{0}</div>")]
    [InlineAutoData(false, "<span class=\"content\">{0}</span>")]
    [InlineAutoData(false, "<div class=\"content\">{0}</div>")]
    public void SlideTab_IsExpandedParam_RendersCorrectly(
        bool isExpanded,
        string contentTemplate,
        string title,
        int positionBottom,
        string content)
    {
        //Act
        var cut = RenderComponent<SlideTab>(parameters => parameters
           .Add(p => p.Title, title)
           .Add(p => p.IsExpanded, isExpanded)
           .Add(p => p.PositionBottom, positionBottom)
           .Add(p => p.Content, string.Format(contentTemplate, content)));

        var slideContainer = cut.Find(".slide-container");
        var actualIsExpanded = slideContainer.ClassList.Contains("expanded");

        //Assert
        Assert.Equal(isExpanded, actualIsExpanded);
    }

    [Theory(DisplayName = "PositionBottom Parameter Test")]
    [InlineAutoData(true, "<span class=\"content\">{0}</span>")]
    [InlineAutoData(true, "<div class=\"content\">{0}</div>")]
    [InlineAutoData(false, "<span class=\"content\">{0}</span>")]
    [InlineAutoData(false, "<div class=\"content\">{0}</div>")]
    public void SlideTab_PositionBottomParam_RendersCorrectly(
        bool isExpanded,
        string contentTemplate,
        string title,
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
          .Add(p => p.Content, string.Format(contentTemplate, content)));

        var slideTab = cut.Find(".slide-tab");
        var actualStyleValue = slideTab.Attributes.First(_ => _.Name == "style").Value;

        //Assert
        Assert.Equal(expectedStyleValue, actualStyleValue);
    }

    [Theory(DisplayName = "Content Parameter Test")]
    [InlineAutoData(true, "<span class=\"content\">{0}</span>")]
    [InlineAutoData(true, "<div class=\"content\">{0}</div>")]
    [InlineAutoData(false, "<span class=\"content\">{0}</span>")]
    [InlineAutoData(false, "<div class=\"content\">{0}</div>")]
    public void SlideTab_ContentParam_RendersCorrectly(
        bool isExpanded,
        string contentTemplate,
        string title,
        int positionBottom,
        string content)
    {
        //Arrange
        var expectedContent = string.Format(contentTemplate, content);

        //Act
        var cut = RenderComponent<SlideTab>(parameters => parameters
         .Add(p => p.Title, title)
         .Add(p => p.IsExpanded, isExpanded)
         .Add(p => p.PositionBottom, positionBottom)
         .Add(p => p.Content, string.Format(contentTemplate, content)));

        var slideContainer = cut.Find(".slide-container");
        var actualContent= slideContainer.InnerHtml;

        //Assert
        Assert.Equal(expectedContent, actualContent);
    }
}
