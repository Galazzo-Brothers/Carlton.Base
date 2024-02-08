using Carlton.Core.Components.Buttons;
namespace Carlton.Core.Components.Tests;

[Trait("Component", nameof(IconButton))]
public class IconButtonComponentTests : TestContext
{

    [Theory(DisplayName = "Markup Test"), AutoData]
    public void IconButton_Markup_RendersCorrectly(string expectedIconClass)
    {
        //Arrange
        var expectedMarkup = @$"
<div class=""icon-btn"">
    <span class=""mdi mdi-24px mdi-{expectedIconClass}""></span>
</div>";

        //Act
        var cut = RenderComponent<IconButton>(parameters => parameters
           .Add(p => p.Icon, expectedIconClass));

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory(DisplayName = "Icon Parameter Test"), AutoData]
    public void IconButton_IconParameter_RendersCorrectly(string expectedIconClass)
    {
        //Arrange
        var cut = RenderComponent<IconButton>(parameters => parameters
            .Add(p => p.Icon, expectedIconClass));

        //Act
        var iconSpan = cut.Find(".icon-btn span.mdi");

        //Assert
        iconSpan.ClassList.ShouldContain($"mdi-{expectedIconClass}");
    }

    [Theory(DisplayName = "Button Click Test"), AutoData]
    public void IconButton_OnClick_RendersCorrectly(string expectedIconClass)
    {
        //Arrange
        var eventCalled = false;
        var cut = RenderComponent<IconButton>(parameters => parameters
            .Add(p => p.Icon, expectedIconClass)
            .Add(p => p.OnClick, () => { eventCalled = true; }));

        //Act
        cut.Find(".icon-btn").Click();

        //Assert
        eventCalled.ShouldBeTrue();
    }
}
