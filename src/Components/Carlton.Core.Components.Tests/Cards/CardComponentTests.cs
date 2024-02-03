using Carlton.Core.Components.Cards;
namespace Carlton.Core.Components.Tests;

[Trait("Component", nameof(Card))]
public class CardComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test"), AutoData]
    [InlineData("", "", "", "")]
    [InlineData(null, null, null, null)]
    public void Card_Markup_RendersCorrectly(
        string expectedCardTitle,
        string expectedActionBarContent,
        string expectedHeaderContent,
        string expectedPrimaryCardContent)
    {
        //Arrange
        var expectedMarkup =
@$"<div class=""card"">
    <div class=""content"">
        <div class=""title-content"">
            <span class=""card-title"">{expectedCardTitle}</span>
            <div class=""status-icon"">{expectedActionBarContent}</div>
        </div>
        <div class=""header-content"">
            {expectedHeaderContent}
        </div>
        <div class=""primary-content"">
            {expectedPrimaryCardContent}
        </div>
    </div>
</div>";

        //Act
        var cut = RenderComponent<Card>(parameters => parameters
            .Add(p => p.CardTitle, expectedCardTitle)
            .Add(p => p.ActionBarContent, expectedActionBarContent)
            .Add(p => p.HeaderContent, expectedHeaderContent)
            .Add(p => p.PrimaryCardContent, expectedPrimaryCardContent));

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory(DisplayName = "Title Parameter Test"), AutoData]
    public void Card_CardTitleParam_RendersCorrectly(
        string expectedTitle,
        string expectedActionBarContent,
        string expectedHeaderContent,
        string expectedPrimaryCardContent)
    {
        //Act
        var cut = RenderComponent<Card>(parameters => parameters
            .Add(p => p.CardTitle, expectedTitle)
            .Add(p => p.ActionBarContent, expectedActionBarContent)
            .Add(p => p.HeaderContent, expectedHeaderContent)
            .Add(p => p.PrimaryCardContent, expectedPrimaryCardContent));

        var actualCardTitle = cut.Find(".card-title").TextContent;

        //Assert
        actualCardTitle.ShouldBe(expectedTitle);
    }

    [Theory(DisplayName = "ActionBarContentChild Parameter Test"), AutoData]
    public void Card_ActionBarContentChildParam_RendersCorrectly(
        string expectedTitle,
        string expectedActionBarContent,
        string expectedHeaderContent,
        string expectedPrimaryCardContent)
    {
        //Act
        var cut = RenderComponent<Card>(parameters => parameters
            .Add(p => p.CardTitle, expectedTitle)
            .Add(p => p.ActionBarContent, expectedActionBarContent)
            .Add(p => p.HeaderContent, expectedHeaderContent)
            .Add(p => p.PrimaryCardContent, expectedPrimaryCardContent));

        var actualActionBarContent = cut.Find(".status-icon").InnerHtml;

        //Assert
        actualActionBarContent.ShouldBe(expectedActionBarContent);
    }

    [Theory(DisplayName = "HeaderContentChild Parameter Test"), AutoData]
    public void Card_HeaderContentChildParam_RendersCorrectly(
        string expectedTitle,
        string expectedActionBarContent,
        string expectedHeaderContent,
        string expectedPrimaryCardContent)
    {
        //Act
        var cut = RenderComponent<Card>(parameters => parameters
            .Add(p => p.CardTitle, expectedTitle)
            .Add(p => p.ActionBarContent, expectedActionBarContent)
            .Add(p => p.HeaderContent, expectedHeaderContent)
            .Add(p => p.PrimaryCardContent, expectedPrimaryCardContent));

        var actualHeaderContent = cut.Find(".header-content").InnerHtml;

        //Assert
        actualHeaderContent.ShouldBe(expectedHeaderContent);
    }

    [Theory(DisplayName = "PrimaryContentChild Parameter Test"), AutoData]
    public void Card_PrimaryContentChildParam_RendersCorrectly(
        string expectedTitle,
        string expectedActionBarContent,
        string expectedHeaderContent,
        string expectedPrimaryCardContent)
    {
        //Act
        var cut = RenderComponent<Card>(parameters => parameters
            .Add(p => p.CardTitle, expectedTitle)
            .Add(p => p.ActionBarContent, expectedActionBarContent)
            .Add(p => p.HeaderContent, expectedHeaderContent)
            .Add(p => p.PrimaryCardContent, expectedPrimaryCardContent));

        var actualPrimaryContent = cut.Find(".primary-content").InnerHtml;

        //Assert
        actualPrimaryContent.ShouldBe(expectedPrimaryCardContent);
    }
}
