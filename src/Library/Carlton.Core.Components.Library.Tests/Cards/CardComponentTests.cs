using AutoFixture.Xunit2;

namespace Carlton.Core.Components.Library.Tests;

[Trait("Component", nameof(Card))]
public class CardComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test"), AutoData]
    [InlineData("", "", "", "")]
    [InlineData(null, null, null, null)]
    public void Card_Markup_RendersCorrectly(string cardTitle, string actionBarContent, string headerContent, string primaryCardContent)
    {
        //Arrange
        var expectedMarkup =
@$"<div class=""card"">
    <div class=""content"">
        <div class=""title-content"">
            <span class=""card-title"">{cardTitle}</span>
            <div class=""status-icon"">{actionBarContent}</div>
        </div>
        <div class=""header-content"">
            {headerContent}
        </div>
        <div class=""primary-content"">
            {primaryCardContent}
        </div>
    </div>
</div>";

        //Act
        var cut = RenderComponent<Card>(parameters => parameters
            .Add(p => p.CardTitle, cardTitle)
            .Add(p => p.ActionBarContent, actionBarContent)
            .Add(p => p.HeaderContent, headerContent)
            .Add(p => p.PrimaryCardContent, primaryCardContent));

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory(DisplayName = "Title Parameter Test"), AutoData]
    public void Card_CardTitleParam_RendersCorrectly(string title, string actionBarContent, string headerContent, string primaryCardContent)
    {
        //Act
        var cut = RenderComponent<Card>(parameters => parameters
            .Add(p => p.CardTitle, title)
            .Add(p => p.ActionBarContent, actionBarContent)
            .Add(p => p.HeaderContent, headerContent)
            .Add(p => p.PrimaryCardContent, primaryCardContent));

        var cardTitle = cut.Find(".card-title").TextContent;

        //Assert
        Assert.Equal(title, cardTitle);
    }

    [Theory(DisplayName = "ActionBarContentChild Parameter Test"), AutoData]
    public void Card_ActionBarContentChildParam_RendersCorrectly(string title, string actionBarContent, string headerContent, string primaryCardContent)
    {
        //Act
        var cut = RenderComponent<Card>(parameters => parameters
            .Add(p => p.CardTitle, title)
            .Add(p => p.ActionBarContent, actionBarContent)
            .Add(p => p.HeaderContent, headerContent)
            .Add(p => p.PrimaryCardContent, primaryCardContent));

        var actualActionBarContent = cut.Find(".status-icon").InnerHtml;

        //Assert
        Assert.Equal(actionBarContent, actualActionBarContent);
    }

    [Theory(DisplayName = "HeaderContentChild Parameter Test"), AutoData]
    public void Card_HeaderContentChildParam_RendersCorrectly(string title, string expectedActionBarContent, string expectedHeaderContent, string primaryCardContent)
    {
        //Act
        var cut = RenderComponent<Card>(parameters => parameters
            .Add(p => p.CardTitle, title)
            .Add(p => p.ActionBarContent, expectedActionBarContent)
            .Add(p => p.HeaderContent, expectedHeaderContent)
            .Add(p => p.PrimaryCardContent, primaryCardContent));

        var headerContent = cut.Find(".header-content").InnerHtml;

        //Assert
        Assert.Equal(expectedHeaderContent, headerContent);
    }

    [Theory(DisplayName = "PrimaryContentChild Parameter Test"), AutoData]
    public void Card_PrimaryContentChildParam_RendersCorrectly(string title, string actionBarContent, string headerContent, string primaryCardContent)
    {
        //Act
        var cut = RenderComponent<Card>(parameters => parameters
            .Add(p => p.CardTitle, title)
            .Add(p => p.ActionBarContent, actionBarContent)
            .Add(p => p.HeaderContent, headerContent)
            .Add(p => p.PrimaryCardContent, primaryCardContent));

        var actualPrimaryContent = cut.Find(".primary-content").InnerHtml;

        //Assert
        Assert.Equal(primaryCardContent, actualPrimaryContent);
    }
}
