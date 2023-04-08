namespace Carlton.Base.Components.Test;

public class CountCardComponentTests : TestContext
{
    [Fact]
    public void CountCard_Markup_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<CountCard>(parameters => parameters
            .Add(p => p.Count, 5)
            .Add(p => p.Icon, "mdi-delete")
            .Add(p => p.MessageTemplate, "Items Tracked by this component")
            .Add(p => p.Theme, CountCardTheme.Purple)
            );

        //Assert
        cut.MarkupMatches(CardTestHelper.CountCardMarkup);
    }

    [Theory]
    [InlineData(10)]
    [InlineData(15)]
    [InlineData(30)]
    public void CountCard_CountParam_RendersCorrectly(int count)
    {
        //Act
        var cut = RenderComponent<CountCard>(parameters => parameters
            .Add(p => p.Count, count)
            .Add(p => p.Icon, "mdi-camera")
            .Add(p => p.MessageTemplate, "Items Tracked by this component")
            .Add(p => p.Theme, CountCardTheme.Purple)
            );

        var displayCount = int.Parse(cut.Find(".count-message").TextContent.Split(' ')[0]);

        //Assert
        Assert.Equal(count, displayCount);
    }

    [Theory]
    [InlineData("Items Tracked by this component")]
    [InlineData("More Items Tracked by this component")]
    [InlineData("Event Items Tracked by this component")]
    public void CountCard_MessageTemplateParam_RendersCorrectly(string messageTemplate)
    {
        //Act
        var cut = RenderComponent<CountCard>(parameters => parameters
            .Add(p => p.Count, 15)
            .Add(p => p.Icon, "mdi-camera")
            .Add(p => p.MessageTemplate, messageTemplate)
            .Add(p => p.Theme, CountCardTheme.Purple)
            );

        var displayMessageTemplate = string.Join(' ', cut.Find(".count-message")
                                        .TextContent
                                        .Split(' ')
                                        .Skip(1));

        //Assert
        Assert.Equal(messageTemplate, displayMessageTemplate);
    }

    [Theory]
    [InlineData("icon-1")]
    [InlineData("icon-2")]
    [InlineData("icon-3")]
    public void CountCard_IconParam_RendersCorrectly(string icon)
    {
        //Act
        var cut = RenderComponent<CountCard>(parameters => parameters
            .Add(p => p.Count, 15)
            .Add(p => p.Icon, icon)
            .Add(p => p.MessageTemplate, "Items Tracked by this component")
            .Add(p => p.Theme, CountCardTheme.Purple)
            );

        var iconElm = cut.Find(".count-icon");

        //Assert
        Assert.Contains(icon, iconElm.ClassList);
    }

    [Theory]
    [InlineData(CountCardTheme.Blue)]
    [InlineData(CountCardTheme.Green)]
    [InlineData(CountCardTheme.Purple)]
    [InlineData(CountCardTheme.Red)]
    public void CountCard_ThemeParam_RendersCorrectly(CountCardTheme theme)
    {
        //Act
        var cut = RenderComponent<CountCard>(parameters => parameters
            .Add(p => p.Count, 15)
            .Add(p => p.Icon, "mdi-camera")
            .Add(p => p.MessageTemplate, "Items Tracked by this component")
            .Add(p => p.Theme, theme)
            );

        var countCardElm = cut.Find(".count-card");
        var expectedResults = $"accent{(int) theme}";

        //Assert
        Assert.Contains(expectedResults, countCardElm.ClassList);
    }
}
