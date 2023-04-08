namespace Carlton.Base.Components.Test;

public class PageTitleComponentTests : TestContext
{
    private static readonly string PageTitleMarkup =@"
    <div class=""page-title"" b-hv0lfh6f33>
        <span class=""title"" b-hv0lfh6f33>Test Title</span>
        <span class=""bread-crumbs"" b-hv0lfh6f33>SomeThing &gt; AnotherThing</span>
    </div>";


    [Fact]
    public void PageTitle_Markup_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<TitleBreadCrumbs>(paramaters => paramaters
            .Add(p => p.Title, "Test Title")
            .Add(p => p.BreadCrumbs, "SomeThing > AnotherThing")
            );

        //Assert
        cut.MarkupMatches(PageTitleMarkup);
    }

    [Theory]
    [InlineData("Test Title")]
    [InlineData("Another Test Title")]
    [InlineData("Yet Another Test Title")]
    public void PageTitle_TitleParam_RendersCorrectly(string expectedTitle)
    {
        //Act
        var cut = RenderComponent<TitleBreadCrumbs>(paramaters => paramaters
            .Add(p => p.Title, expectedTitle)
            .Add(p => p.BreadCrumbs, "SomeThing > AnotherThing")
            );

        var title = cut.Find(".title");

        //Assert
        Assert.Equal(expectedTitle, title.InnerHtml);
    }

    [Theory]
    [InlineData("SomeThing > AnotherThing")]
    [InlineData("SomeOtherThing > AnotherThing")]
    [InlineData("YetAnotherThing > AnotherThing")]
    public void PageTitle_BreadCrumbParam_RendersCorrectly(string expectedBreadCrumbs)
    {
        //Act
        var cut = RenderComponent<TitleBreadCrumbs>(paramaters => paramaters
            .Add(p => p.Title, "Test Title")
            .Add(p => p.BreadCrumbs, expectedBreadCrumbs)
            );

        var breadCrumbs = cut.Find(".bread-crumbs");

        //Assert
        Assert.Equal(expectedBreadCrumbs, breadCrumbs.TextContent);
    }
}
