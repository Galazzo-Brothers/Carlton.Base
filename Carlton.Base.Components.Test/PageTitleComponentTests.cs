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

    [Fact]
    public void PageTitle_TitleParam_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<TitleBreadCrumbs>(paramaters => paramaters
            .Add(p => p.Title, "Test")
            .Add(p => p.BreadCrumbs, "SomeThing > AnotherThing")
            );

        var title = cut.Find(".title");

        //Assert
        Assert.Equal("Test", title.InnerHtml);
    }

    [Fact]
    public void PageTitle_BreadCrumbParam_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<TitleBreadCrumbs>(paramaters => paramaters
            .Add(p => p.Title, "Test Title")
            .Add(p => p.BreadCrumbs, "SomeThing > AnotherThing")
            );

        var breadCrumbs = cut.Find(".bread-crumbs");

        //Assert
        Assert.Equal("SomeThing > AnotherThing", breadCrumbs.TextContent);
    }
}
