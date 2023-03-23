namespace Carlton.Base.Components.Test;

public class CardComponentTests : TestContext
{
    private static readonly string CardMarkup =@"
<div class=""card"" b-g3swmy425k>
    <div class=""content"" b-g3swmy425k>
        <div class=""title-content"" b-g3swmy425k>
            <span class=""card-title"" b-g3swmy425k>Test Title</span>
            <div class=""status-icon"" b-g3swmy425k>Test ActionBar Content</div>
        </div>
        <div class=""header-content"" b-g3swmy425k>
            <span>Test Header</span>
        </div>
        <div class=""primary-content"" b-g3swmy425k>
            <span>Test Primary Content</span>
        </div>
    </div>
</div>";


    [Fact]
    public void Card_Markup_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<Card>(parameters => parameters
            .Add(p => p.CardTitle, "Test Title")
            .Add(p => p.ActionBarContent, "Test ActionBar Content")
            .Add(p => p.HeaderContent, "<span>Test Header</span>")
            .Add(p => p.PrimaryCardContent, "<span>Test Primary Content</span>")
            );

        //Assert
        cut.MarkupMatches(CardMarkup);
    }

    [Fact]
    public void Card_CardTitleParam_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<Card>(parameters => parameters
            .Add(p => p.CardTitle, "Some Test Title")
            .Add(p => p.ActionBarContent, "Test ActionBar Content")
            .Add(p => p.HeaderContent, "<span>Test Header</span>")
            .Add(p => p.PrimaryCardContent, "<span>Test Primary Content</span>")
            );

        var cardTitle = cut.Find(".card-title").TextContent;

        //Assert
        Assert.Equal("Some Test Title", cardTitle);
    }

    [Fact]
    public void Card_ActionBarContentChildParam_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<Card>(parameters => parameters
            .Add(p => p.CardTitle, "Some Test Title")
            .Add(p => p.ActionBarContent, "ActionBar Testing Content")
            .Add(p => p.HeaderContent, "<span>Test Header</span>")
            .Add(p => p.PrimaryCardContent, "<span>Primary Testing Content</span>")
            );

        var actionBarContent = cut.Find(".status-icon").InnerHtml;

        //Assert
        Assert.Equal("ActionBar Testing Content", actionBarContent);
    }

    [Fact]
    public void Card_HeaderContentChildParam_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<Card>(parameters => parameters
            .Add(p => p.CardTitle, "Some Test Title")
            .Add(p => p.ActionBarContent, "ActionBar Content")
            .Add(p => p.HeaderContent, "<span>Header Testing Content</span>")
            .Add(p => p.PrimaryCardContent, "<span>Test Primary Content</span>")
            );

        var headerContent = cut.Find(".header-content").InnerHtml;

        //Assert
        Assert.Equal("<span>Header Testing Content</span>", headerContent);
    }

    [Fact]
    public void Card_PrimaryContentChildParam_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<Card>(parameters => parameters
            .Add(p => p.CardTitle, "Some Test Title")
            .Add(p => p.ActionBarContent, "ActionBar Content")
            .Add(p => p.HeaderContent, "<span>Header Content</span>")
            .Add(p => p.PrimaryCardContent, "<span>Primary Testing Content</span>")
            );

        var primaryContent = cut.Find(".primary-content").InnerHtml;

        //Assert
        Assert.Equal("<span>Primary Testing Content</span>", primaryContent);
    }
}
