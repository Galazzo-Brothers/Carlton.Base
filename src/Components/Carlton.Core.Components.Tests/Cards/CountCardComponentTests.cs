using Carlton.Core.Components.Cards;
namespace Carlton.Core.Components.Tests;

[Trait("Component", nameof(CountCard))]
public class CountCardComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test"), AutoData]
    public void CountCard_Markup_RendersCorrectly(
        int expectedCount,
        string expectedIconClass,
        string expectedMessageTemplate,
        CountCardTheme expectedTheme)
    {
        //Arrange
        var expectedMarkup = 
@$"<div class=""count-card accent{(int)expectedTheme}"">
    <div class=""content"">
        <div class=""count-icon mdi mdi-48px {expectedIconClass}""></div>
        <span class=""count-message"">{expectedCount} {expectedMessageTemplate}</span>
    </div>
</div>";

        //Act
        var cut = RenderComponent<CountCard>(parameters => parameters
            .Add(p => p.Count, expectedCount)
            .Add(p => p.Icon, expectedIconClass)
            .Add(p => p.MessageTemplate, expectedMessageTemplate)
            .Add(p => p.Theme, expectedTheme));

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory(DisplayName = "Count Parameter Test"), AutoData]
    public void CountCard_CountParameter_RendersCorrectly(
        int expectedCount,
        string expectedIconClass,
        string expectedMessageTemplate,
        CountCardTheme expectedTheme)
    {
        //Act
        var cut = RenderComponent<CountCard>(parameters => parameters
            .Add(p => p.Count, expectedCount)
            .Add(p => p.Icon, expectedIconClass)
            .Add(p => p.MessageTemplate, expectedMessageTemplate)
            .Add(p => p.Theme, expectedTheme));

        var actualCount = int.Parse(cut.Find(".count-message").TextContent.Split(' ')[0]);

        //Assert
        actualCount.ShouldBe(expectedCount);
    }

    [Theory(DisplayName = "MessageTemplate Parameter Test"), AutoData]
    public void CountCard_MessageTemplateParameter_RendersCorrectly(
        int expectedCount,
        string expectedIconClass,
        string expectedMessageTemplate,
        CountCardTheme expectedTheme)
    {
        //Act
        var cut = RenderComponent<CountCard>(parameters => parameters
            .Add(p => p.Count, expectedCount)
            .Add(p => p.Icon, expectedIconClass)
            .Add(p => p.MessageTemplate, expectedMessageTemplate)
            .Add(p => p.Theme, expectedTheme));

        var actualMessageTemplate = string.Join(' ', cut.Find(".count-message").TextContent
                                                         .Split(' ')
                                                         .Skip(1));

        //Assert
        actualMessageTemplate.ShouldBe(expectedMessageTemplate);
    }

    [Theory(DisplayName = "Icon Parameter Test"), AutoData]
    public void CountCard_IconParameter_RendersCorrectly(
        int expectedCount,
        string expectedIcon,
        string expectedMessageTemplate,
        CountCardTheme expectedTheme)
    {
        //Act
        var cut = RenderComponent<CountCard>(parameters => parameters
          .Add(p => p.Count, expectedCount)
          .Add(p => p.Icon, expectedIcon)
          .Add(p => p.MessageTemplate, expectedMessageTemplate)
          .Add(p => p.Theme, expectedTheme));

        var iconElelemnt = cut.Find(".count-icon");

        //Assert
        iconElelemnt.ClassList.ShouldContain(expectedIcon);
    }

    [Theory(DisplayName = "Theme Parameter Test")]
    [InlineAutoData(CountCardTheme.Red)]
    [InlineAutoData(CountCardTheme.Blue)]
    [InlineAutoData(CountCardTheme.Green)]
    [InlineAutoData(CountCardTheme.Purple)]
    public void CountCard_ThemeParameter_RendersCorrectly(
        CountCardTheme expectedTheme,
        int expectedCount,
        string expectedClassIcon,
        string expectedMessageTemplate)
    {
        //Act
        var cut = RenderComponent<CountCard>(parameters => parameters
           .Add(p => p.Count, expectedCount)
           .Add(p => p.Icon, expectedClassIcon)
           .Add(p => p.MessageTemplate, expectedMessageTemplate)
           .Add(p => p.Theme, expectedTheme));

        var countCardElement = cut.Find(".count-card");

        //Assert
        var expectedResults = $"accent{(int)expectedTheme}";
        countCardElement.ClassList.ShouldContain(expectedResults);
    }
}
