using System.Linq;

namespace Carlton.Base.Components.Test;

public class CountCardComponentTests : TestContext
{
    private static readonly string CountCardMarkup = @"
<div class=""count-card accent4"" b-xj8ojuw516>
    <div class=""content"" b-xj8ojuw516>
        <div class=""count-icon mdi mdi-48px mdi-delete"" b-xj8ojuw516></div>
        <span class=""count-message"" b-xj8ojuw516>5 Items Tracked by this component</span>
    </div>
</div>";


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
        cut.MarkupMatches(CountCardMarkup);
    }

    [Fact]
    public void CountCard_CountParam_ShouldRenderCountCorrectly()
    {
        //Act
        var cut = RenderComponent<CountCard>(parameters => parameters
            .Add(p => p.Count, 15)
            .Add(p => p.Icon, "mdi-camera")
            .Add(p => p.MessageTemplate, "Items Tracked by this component")
            .Add(p => p.Theme, CountCardTheme.Purple)
            );

        var displayCount = int.Parse(cut.Find(".count-message").TextContent.Split(' ')[0]);

        //Assert
        Assert.Equal(15, displayCount);
    }

    [Fact]
    public void CountCard_MessageTemplateParam_ShouldRenderMessageCorrectly()
    {
        //Act
        var cut = RenderComponent<CountCard>(parameters => parameters
            .Add(p => p.Count, 15)
            .Add(p => p.Icon, "mdi-camera")
            .Add(p => p.MessageTemplate, "Items Tracked by this component")
            .Add(p => p.Theme, CountCardTheme.Purple)
            );

        var displayMessageTemplate = string.Join(' ', cut.Find(".count-message")
                                        .TextContent
                                        .Split(' ')
                                        .Skip(1));

        //Assert
        Assert.Equal("Items Tracked by this component", displayMessageTemplate);
    }

    [Fact]
    public void CountCard_IconParam_ShouldRenderIconClassCorrectly()
    {
        //Act
        var cut = RenderComponent<CountCard>(parameters => parameters
            .Add(p => p.Count, 15)
            .Add(p => p.Icon, "mdi-camera")
            .Add(p => p.MessageTemplate, "Items Tracked by this component")
            .Add(p => p.Theme, CountCardTheme.Purple)
            );

        var iconElm = cut.Find(".count-icon");

        //Assert
        Assert.Contains("mdi-camera", iconElm.ClassList);
    }

    [Fact]
    public void CountCard_ThemeParam_ShouldRenderThemeClassCorrectly()
    {
        //Act
        var cut = RenderComponent<CountCard>(parameters => parameters
            .Add(p => p.Count, 15)
            .Add(p => p.Icon, "mdi-camera")
            .Add(p => p.MessageTemplate, "Items Tracked by this component")
            .Add(p => p.Theme, CountCardTheme.Red)
            );

        var countCardElm = cut.Find(".count-card");

        //Assert
        Assert.Contains("accent3", countCardElm.ClassList);
    }
}
