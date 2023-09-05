using AutoFixture.Xunit2;

namespace Carlton.Core.Components.Library.Tests;

[Trait("Component", nameof(CountCard))]
public class CountCardComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test"), AutoData]
    public void CountCard_Markup_RendersCorrectly(int count, string icon, string messageTemplate, CountCardTheme theme)
    {
        //Arrange
        var expectedMarkup = 
@$"<div class=""count-card accent{(int)theme}"">
    <div class=""content"">
        <div class=""count-icon mdi mdi-48px {icon}""></div>
        <span class=""count-message"">{count} {messageTemplate}</span>
    </div>
</div>";

        //Act
        var cut = RenderComponent<CountCard>(parameters => parameters
            .Add(p => p.Count, count)
            .Add(p => p.Icon, icon)
            .Add(p => p.MessageTemplate, messageTemplate)
            .Add(p => p.Theme, theme));

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory(DisplayName = "Count Parameter Test"), AutoData]
    public void CountCard_CountParam_RendersCorrectly(int count, string icon, string messageTemplate, CountCardTheme theme)
    {
        //Act
        var cut = RenderComponent<CountCard>(parameters => parameters
            .Add(p => p.Count, count)
            .Add(p => p.Icon, icon)
            .Add(p => p.MessageTemplate, messageTemplate)
            .Add(p => p.Theme, theme));

        var displayCount = int.Parse(cut.Find(".count-message").TextContent.Split(' ')[0]);

        //Assert
        Assert.Equal(count, displayCount);
    }

    [Theory(DisplayName = "MessageTemplate Parameter Test"), AutoData]
    public void CountCard_MessageTemplateParam_RendersCorrectly(int count, string icon, string messageTemplate, CountCardTheme theme)
    {
        //Act
        var cut = RenderComponent<CountCard>(parameters => parameters
            .Add(p => p.Count, count)
            .Add(p => p.Icon, icon)
            .Add(p => p.MessageTemplate, messageTemplate)
            .Add(p => p.Theme, theme));

        var displayMessageTemplate = string.Join(' ', cut.Find(".count-message").TextContent
                                                         .Split(' ')
                                                         .Skip(1));

        //Assert
        Assert.Equal(messageTemplate, displayMessageTemplate);
    }

    [Theory(DisplayName = "Icon Parameter Test"), AutoData]
    public void CountCard_IconParam_RendersCorrectly(int count, string icon, string messageTemplate, CountCardTheme theme)
    {
        //Act
        var cut = RenderComponent<CountCard>(parameters => parameters
          .Add(p => p.Count, count)
          .Add(p => p.Icon, icon)
          .Add(p => p.MessageTemplate, messageTemplate)
          .Add(p => p.Theme, theme));

        var iconElm = cut.Find(".count-icon");

        //Assert
        Assert.Contains(icon, iconElm.ClassList);
    }

    [Theory(DisplayName = "Theme Parameter Test"), AutoData]
    public void CountCard_ThemeParam_RendersCorrectly(int count, string icon, string messageTemplate, CountCardTheme theme)
    {
        //Act
        var cut = RenderComponent<CountCard>(parameters => parameters
           .Add(p => p.Count, count)
           .Add(p => p.Icon, icon)
           .Add(p => p.MessageTemplate, messageTemplate)
           .Add(p => p.Theme, theme));

        var countCardElm = cut.Find(".count-card");
        var expectedResults = $"accent{(int)theme}";

        //Assert
        Assert.Contains(expectedResults, countCardElm.ClassList);
    }
}
