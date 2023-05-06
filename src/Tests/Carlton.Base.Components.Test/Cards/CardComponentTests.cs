namespace Carlton.Base.Components.Test;

[Trait("Component", nameof(Card))]
public class CardComponentTests : TestContext
{
    [Fact(DisplayName = "Markup Test")]
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
        cut.MarkupMatches(CardTestHelper.CardMarkup);
    }

    [Theory(DisplayName = "Title Parameter Test")]
    [InlineData("Test Title 1")]
    [InlineData("Test Title 2")]
    [InlineData("Test Title 3")]
    public void Card_CardTitleParam_RendersCorrectly(string expectedTitle)
    {
        //Act
        var cut = RenderComponent<Card>(parameters => parameters
            .Add(p => p.CardTitle, expectedTitle)
            .Add(p => p.ActionBarContent, "Test ActionBar Content")
            .Add(p => p.HeaderContent, "<span>Test Header</span>")
            .Add(p => p.PrimaryCardContent, "<span>Test Primary Content</span>")
            );

        var cardTitle = cut.Find(".card-title").TextContent;

        //Assert
        Assert.Equal(expectedTitle, cardTitle);
    }

    [Theory(DisplayName = "ActionBarContentChild Parameter Test")]
    [InlineData("ActionBar Testing Content 1")]
    [InlineData("ActionBar Testing Content 2")]
    [InlineData("ActionBar Testing Content 3")]
    public void Card_ActionBarContentChildParam_RendersCorrectly(string expectedActionBarContent)
    {
        //Act
        var cut = RenderComponent<Card>(parameters => parameters
            .Add(p => p.CardTitle, "Some Test Title")
            .Add(p => p.ActionBarContent, expectedActionBarContent)
            .Add(p => p.HeaderContent, "<span>Test Header</span>")
            .Add(p => p.PrimaryCardContent, "<span>Primary Testing Content</span>")
            );

        var actionBarContent = cut.Find(".status-icon").InnerHtml;

        //Assert
        Assert.Equal(expectedActionBarContent, actionBarContent);
    }

    [Theory(DisplayName = "HeaderContentChild Parameter Test")]
    [InlineData("<span>Header Testing Content 1</span>")]
    [InlineData("<span>Header Testing Content 2</span>")]
    [InlineData("<span>Header Testing Content 3</span>")]
    public void Card_HeaderContentChildParam_RendersCorrectly(string expectedHeaderContent)
    {
        //Act
        var cut = RenderComponent<Card>(parameters => parameters
            .Add(p => p.CardTitle, "Some Test Title")
            .Add(p => p.ActionBarContent, "ActionBar Content")
            .Add(p => p.HeaderContent, expectedHeaderContent)
            .Add(p => p.PrimaryCardContent, "<span>Test Primary Content</span>")
            );

        var headerContent = cut.Find(".header-content").InnerHtml;

        //Assert
        Assert.Equal(expectedHeaderContent, headerContent);
    }

    [Theory(DisplayName = "PrimaryContentChild Parameter Test")]
    [InlineData("<span>Primary Testing Content 1</span>")]
    [InlineData("<span>Primary Testing Content 2</span>")]
    [InlineData("<span>Primary Testing Content 3</span>")]
    public void Card_PrimaryContentChildParam_RendersCorrectly(string expectedPrimaryCardContent)
    {
        //Act
        var cut = RenderComponent<Card>(parameters => parameters
            .Add(p => p.CardTitle, "Some Test Title")
            .Add(p => p.ActionBarContent, "ActionBar Content")
            .Add(p => p.HeaderContent, "<span>Header Content</span>")
            .Add(p => p.PrimaryCardContent, expectedPrimaryCardContent)
            );

        var primaryContent = cut.Find(".primary-content").InnerHtml;

        //Assert
        Assert.Equal(expectedPrimaryCardContent, primaryContent);
    }
}
