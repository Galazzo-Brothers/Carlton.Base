using Carlton.Core.Components.Tabs;
namespace Carlton.Core.Components.Tests.Tabs;

[Trait("Component", nameof(TabComponentTests))]
public class TabComponentTests : TestContext
{
    private readonly IRenderedComponent<TabBarBase> parent;

    public TabComponentTests()
        => parent = RenderComponent<TabBarBase>();
    

    [Theory(DisplayName = "Markup Test"), AutoData]
    public void Tab_Markup_RendersCorrectly(
        string expectedContent,
        string expectedDisplayText)
    {
        //Arrange
        var expectedMarkup = expectedContent;

        //Act
        var cut = RenderComponent<Tab>(parameters => parameters
                    .AddCascadingValue(parent.Instance)
                    .Add(p => p.DisplayText, expectedDisplayText)
                    .AddChildContent(expectedContent));

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }
   
    [Theory(DisplayName = "Parent ActiveParam Render Test"), AutoData]
    public void Tab_Parent_ActiveParam_False_RendersCorrectly(string displayText, string childContent)
    {
        //Arrange
        parent.Instance.ActivateTab(2); //Activate another tab

        //Act
        var cut = RenderComponent<Tab>(parameters => parameters
                    .AddCascadingValue(parent.Instance)
                    .Add(p => p.DisplayText, displayText)
                    .Add(p => p.ChildContent, childContent));

        //Assert
        var content = cut.Markup;
        content.ShouldBeEmpty();
    }

    [Theory(DisplayName = "Parent Active True, Render Test"), AutoData]
    public void Tab_Parent_ActiveParameter_True_RendersCorrectly(
        string expectedDisplayText,
        string expectedChildContent)
    {
        //Arrange
        parent.Instance.ActivateTab(0); //Activate this tab

        //Act
        var cut = RenderComponent<Tab>(parameters => parameters
            .AddCascadingValue(parent.Instance)
            .Add(p => p.DisplayText, expectedDisplayText)
            .AddChildContent(expectedChildContent));

        //Assert
        var content = cut.Markup;
        content.ShouldNotBeEmpty();
        content.ShouldBe(expectedChildContent);
    }

    [Theory(DisplayName = "Tab Without Parent, Throws Argument Exception"), AutoData]
    public void Tab_WithOutParent_ThrowsArgumentNullException(string displayText)
    {
        //Act
        IRenderedComponent<Tab> act() => RenderComponent<Tab>(parameters => parameters
            .Add(p => p.DisplayText, displayText));

        //Assert
        Should.Throw<ArgumentNullException>(act);
    }
}
