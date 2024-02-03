using Carlton.Core.Components.Navigation;
namespace Carlton.Core.Components.Tests;

[Trait("Component", nameof(BreadCrumbs))]
public class BreadCrumbsComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test"), AutoData]
    public void BreadCrumbs_Markup_RendersCorrectly(
        string expectedTitle,
        char expectedSeparator,
        IEnumerable<string> expectedItems)
    {
        //Arrange
        var expectedMarkup =
@$"<div class=""bread-crumbs"">
    <span class=""page-title"">{expectedTitle}</span>
    <span class=""page-bread-crumbs"">{string.Join($" {expectedSeparator} ", expectedItems)}</span>
</div>";

        //Act
        var cut = RenderComponent<BreadCrumbs>(parameters => parameters
            .Add(p => p.Title, expectedTitle)
            .Add(p => p.Separator, expectedSeparator)
            .Add(p => p.BreadCrumbItems, expectedItems));

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory(DisplayName = "Title Parameter Test"), AutoData]
    public void BreadCrumbs_TitleParameter_RendersCorrectly(
        string exectedTitle,
        char expectedSeparator,
        IEnumerable<string> expectedItems)
    {
        //Act
        var cut = RenderComponent<BreadCrumbs>(parameters => parameters
            .Add(p => p.Title, exectedTitle)
            .Add(p => p.Separator, expectedSeparator)
            .Add(p => p.BreadCrumbItems, expectedItems));

        var titleElement = cut.Find(".page-title");

        //Assert
        titleElement.InnerHtml.ShouldBe(exectedTitle);
    }

    [Theory(DisplayName = "Separator Parameter Test"), AutoData]
    public void BreadCrumbs_SeparatorParameter_RendersCorrectly(
        string expectedTitle,
        char expectedSeparator,
        IEnumerable<string> expectedItems)
    {
        //Act
        var cut = RenderComponent<BreadCrumbs>(parameters => parameters
            .Add(p => p.Title, expectedTitle)
            .Add(p => p.Separator, expectedSeparator)
            .Add(p => p.BreadCrumbItems, expectedItems));

        var element = cut.Find(".page-bread-crumbs");
        var breadCrumbText = HttpUtility.HtmlDecode(element.InnerHtml);

        //Assert
        breadCrumbText.ShouldContain(expectedSeparator);
    }

    [Theory(DisplayName = "BreadCrumbItems Parameter Test"), AutoData]
    public void BreadCrumbs_BreadCrumbItemsParameter_RendersCorrectly(
        IEnumerable<string> expectedItems,
        string expectedTitle,
        char expectedSeparator)
    {
        //Arrange
        var expected = string.Join($" {expectedSeparator} ", expectedItems);

        //Act
        var cut = RenderComponent<BreadCrumbs>(parameters => parameters
          .Add(p => p.Title, expectedTitle)
          .Add(p => p.Separator, expectedSeparator)
          .Add(p => p.BreadCrumbItems, expectedItems));

        var titleElement = cut.Find(".page-bread-crumbs");
        var titleTextContent = HttpUtility.HtmlDecode(titleElement.InnerHtml);

        //Assert
        titleTextContent.ShouldBe(expected);
    }
}
