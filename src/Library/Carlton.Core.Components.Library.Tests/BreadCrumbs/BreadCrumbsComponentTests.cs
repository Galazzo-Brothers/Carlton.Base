using AutoFixture;
using AutoFixture.Xunit2;

namespace Carlton.Core.Components.Library.Tests;

[Trait("Component", nameof(BreadCrumbs))]
public class BreadCrumbsComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test"), AutoData]
    public void BreadCrumbs_Markup_RendersCorrectly(Fixture fixture, string title, char seperator)
    {
        //Arrange
        var items = fixture.CreateMany<string>(3);
        var expectedMarkup =
@$"<div class=""page-title"">
    <span class=""title"">{title}</span>
    <span class=""bread-crumbs"">{string.Join($" {seperator} ", items)}</span>
</div>";

        //Act
        var cut = RenderComponent<BreadCrumbs>(parameters => parameters
            .Add(p => p.Title, title)
            .Add(p => p.Separator, seperator)
            .Add(p => p.BreadCrumbItems, items)
            );

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory(DisplayName = "Title Parameter Test"), AutoData]
    public void BreadCrumbs_TitleParam_RendersCorrectly(string title, char seperator, IEnumerable<string> items)
    {
        //Act
        var cut = RenderComponent<BreadCrumbs>(parameters => parameters
            .Add(p => p.Title, title)
            .Add(p => p.Separator, seperator)
            .Add(p => p.BreadCrumbItems, items)
            );

        var titleElement = cut.Find(".title");

        //Assert
        Assert.Equal(title, titleElement.InnerHtml);
    }

    [Theory(DisplayName = "Seperator Parameter Test"), AutoData]
    public void BreadCrumbs_SeparatorParam_RendersCorrectly(string title, char seperator, IEnumerable<string> items)
    {
        //Act
        var cut = RenderComponent<BreadCrumbs>(parameters => parameters
            .Add(p => p.Title, title)
            .Add(p => p.Separator, seperator)
            .Add(p => p.BreadCrumbItems, items)
            );

        var element = cut.Find(".bread-crumbs");
        var separatorExists = HttpUtility.HtmlDecode(element.InnerHtml).IndexOf(seperator) > -1;

        //Assert
        Assert.True(separatorExists);
    }

    [Theory(DisplayName = "BreadCrumbItems Parameter Test"), AutoData]
    public void BreadCrumbs_BreadCrumbItemsParam_RendersCorrectly(string title, char seperator, IEnumerable<string> items)
    {
        //Arrange
        var expected = string.Join($" {seperator} ", items);

        //Act
        var cut = RenderComponent<BreadCrumbs>(parameters => parameters
          .Add(p => p.Title, title)
          .Add(p => p.Separator, seperator)
          .Add(p => p.BreadCrumbItems, items)
          );

        var titleElement = cut.Find(".bread-crumbs");
        var actualString = HttpUtility.HtmlDecode(titleElement.InnerHtml);

        //Assert
        Assert.Equal(expected, actualString);
    }
}
