using Carlton.Core.Components.Logos;
namespace Carlton.Core.Components.Tests.Logos;

[Trait("Component", nameof(Logo))]
public class LogoComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test")]
    [InlineAutoData(true)]
    [InlineAutoData(false)]
    public void Logo_Markup_RendersCorrectly(
        bool expectedIsCollapsed,
        string expectedTitle,
        string expectedLogoUrl)
    {
        //Arrange
        var expectedMarkup =
@$"<div class=""logo {(expectedIsCollapsed ? "collapsed" : string.Empty)}"">
    <div class=""content"">
        <img src=""{expectedLogoUrl}"">
        <span class=""title"">{expectedTitle}</span>
    </div>
</div>";

        //Act
        var cut = RenderComponent<Logo>(parameters => parameters
                .Add(p => p.IsCollapsed, expectedIsCollapsed)
                .Add(p => p.Title, expectedTitle)
                .Add(p => p.LogoUrl, expectedLogoUrl));

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory(DisplayName = "IsCollapsed Parameter Test")]
    [InlineAutoData(true)]
    [InlineAutoData(false)]
    public void Logo_IsCollapsedParam_RendersCorrectly(
        bool expectedIsCollapsed,
        string expectedTitle,
        string expectedLogoUrl)
    {
        //Act
        var cut = RenderComponent<Logo>(parameters => parameters
              .Add(p => p.IsCollapsed, expectedIsCollapsed)
              .Add(p => p.Title, expectedTitle)
              .Add(p => p.LogoUrl, expectedLogoUrl));
        var logoElement = cut.Find(".logo");
        var actualHasCollapsedClass = logoElement.ClassList.Contains("collapsed");

        //Assert
        actualHasCollapsedClass.ShouldBe(expectedIsCollapsed);
    }

    [Theory(DisplayName = "Title Parameter Test"), AutoData]
    public void Logo_TitleParameter_RendersCorrectly(
        bool expectedIsCollapsed,
        string expectedTitle,
        string expectedLogoUrl)
    {
        //Act
        var cut = RenderComponent<Logo>(parameters => parameters
              .Add(p => p.IsCollapsed, expectedIsCollapsed)
              .Add(p => p.Title, expectedTitle)
              .Add(p => p.LogoUrl, expectedLogoUrl));
        var titleElement = cut.Find(".title");

        //Assert
        titleElement.TextContent.ShouldBe(expectedTitle);
    }

    [Theory(DisplayName = "LogoUrl Parameter Test"), AutoData]
    public void Logo_LogoUrlParameter_RendersCorrectly(
        bool expectedIsCollapsed,
        string expectedTitle,
        string expectedLogoUrl)
    {
        //Act
        var cut = RenderComponent<Logo>(parameters => parameters
              .Add(p => p.IsCollapsed, expectedIsCollapsed)
              .Add(p => p.Title, expectedTitle)
              .Add(p => p.LogoUrl, expectedLogoUrl));
        var imgElement = cut.Find("img");

        //Assert
        imgElement.Attributes["src"].TextContent.ShouldBe(expectedLogoUrl);
    }
}
