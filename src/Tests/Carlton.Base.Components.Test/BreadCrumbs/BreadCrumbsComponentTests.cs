using System.Web;

namespace Carlton.Base.Components.Test;

public class BreadCrumbsComponentTests : TestContext
{
    [Fact]
    public void BreadCrumbs_Markup_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<BreadCrumbs>(parameters => parameters
            .Add(p => p.Title, BreadCrumbsTestHelper.Title)
            .Add(p => p.Separator, BreadCrumbsTestHelper.Separator)
            .Add(p => p.BreadCrumbItems, BreadCrumbsTestHelper.BreadCrumbItems)
            );

        //Assert
        cut.MarkupMatches(BreadCrumbsTestHelper.BreadCrumbsMarkup);
    }

    [Theory]
    [InlineData("Test Title")]
    [InlineData("Another Test Title")]
    [InlineData("Yet Another Test Title")]
    public void BreadCrumbs_TitleParam_RendersCorrectly(string expectedTitle)
    {
        //Act
        var cut = RenderComponent<BreadCrumbs>(parameters => parameters
            .Add(p => p.Title, expectedTitle)
            .Add(p => p.Separator, BreadCrumbsTestHelper.Separator)
            .Add(p => p.BreadCrumbItems, BreadCrumbsTestHelper.BreadCrumbItems)
            );

        var title = cut.Find(".title");

        //Assert
        Assert.Equal(expectedTitle, title.InnerHtml);
    }

    [Theory]
    [InlineData(">")]
    [InlineData("/")]
    [InlineData("|")]
    public void BreadCrumbs_SeparatorParam_RendersCorrectly(string expectedSeparator)
    {
        //Act
        var cut = RenderComponent<BreadCrumbs>(parameters => parameters
            .Add(p => p.Title, BreadCrumbsTestHelper.Title)
            .Add(p => p.Separator, expectedSeparator)
            .Add(p => p.BreadCrumbItems, BreadCrumbsTestHelper.BreadCrumbItems)
            );

        var title = cut.Find(".bread-crumbs");
        var separatorExists = HttpUtility.HtmlDecode(title.InnerHtml).IndexOf(expectedSeparator) > -1;

        //Assert
        Assert.True(separatorExists);
    }

    [Theory]
    [MemberData(nameof(BreadCrumbsTestHelper.GetItems), MemberType = typeof(BreadCrumbsTestHelper))]
    public void BreadCrumbs_BreadCrumbItemsParam_RendersCorrectly((List<string> Items, string BreadCrumbString) expected)
    {
        //Act
        var cut = RenderComponent<BreadCrumbs>(parameters => parameters
            .Add(p => p.Title, BreadCrumbsTestHelper.Title)
            .Add(p => p.Separator, BreadCrumbsTestHelper.Separator)
            .Add(p => p.BreadCrumbItems, expected.Items)
            );

        var title = cut.Find(".bread-crumbs");
        var actualString = HttpUtility.HtmlDecode(title.InnerHtml);

        //Assert
        Assert.Equal(expected.BreadCrumbString, actualString);
    }
}
