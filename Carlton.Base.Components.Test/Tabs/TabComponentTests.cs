namespace Carlton.Base.Components.Test;

public class TabComponentTests : TestContext
{
    private static readonly string TabMarkup =
        @"<div class=""tab"">
            <div>
                <span class=""message"">This is some test content under this tab</span>
                <div class=""main"">
                    <button>Click Me!</button>
                </div>
            </div>
        </div>";
    private readonly IRenderedComponent<TabBarBase> parent;

    public TabComponentTests()
    {
        parent = RenderComponent<TabBarBase>();
    }

    [Fact]
    public void Tab_WithOutParent_ThrowsArgumentNullException()
    {
        //Act
        var act = () => RenderComponent<Tab>(paramaters => paramaters
            .Add(p => p.DisplayText, "Test Tab")
            );

        //Assert
        Assert.Throws<ArgumentNullException>(act);
    }

    [Fact]
    public void Tab_Markup_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<Tab>(paramaters => paramaters
            .AddCascadingValue(parent.Instance)
            .Add(p => p.DisplayText, "Test Tab")
            .AddChildContent("<div><span class=\"message\">This is some test content under this tab</span><div class=\"main\"><button>Click Me!</button></div></div>")
            );

        //Assert
        cut.MarkupMatches(TabMarkup);
    }

    [Fact]
    public void Tab_Parent_ActiveParam_False_RendersCorrectly()
    {

        //Arrange
        var cut = RenderComponent<Tab>(paramaters => paramaters
            .AddCascadingValue(parent.Instance)
            .Add(p => p.DisplayText, "Test Tab")
            .AddChildContent("<div><span class=\"message\">This is some test content under this tab</span><div class=\"main\"><button>Click Me!</button></div></div>")
            );

        var tabElement = cut.Find(".tab");

        //Act
        parent.Instance.ActiveTab = null;
        cut.Render();

        //Assert
        Assert.Empty(tabElement.InnerHtml);
    }

    [Fact]
    public void Tab_Parent_ActiveParam_True_RendersCorrectly()
    {

        //Arrange
        var childContent = "<div><span class=\"message\">This is some test content under this tab</span><div class=\"main\"><button>Click Me!</button></div></div>";
        var cut = RenderComponent<Tab>(paramaters => paramaters
            .AddCascadingValue(parent.Instance)
            .Add(p => p.DisplayText, "Test Tab")
            .AddChildContent(childContent)
            );

        var tabElement = cut.Find(".tab");

        //Act
        parent.Instance.ActiveTab = cut.Instance;
        cut.Render();

        //Assert
        Assert.NotEmpty(tabElement.InnerHtml);
        Assert.Equal(childContent, tabElement.InnerHtml);
    }
}
