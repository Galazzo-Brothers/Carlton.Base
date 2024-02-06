using Carlton.Core.Components.PsudoElementComponents;
namespace Carlton.Core.Components.Tests;

[Trait("Component", nameof(HorizontalAccentDivider))]
public class HorizontalAccentDividerComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test"), AutoData]
    public void HorizontalAccentDivider_Markup_RendersCorrectly(
        int expectedWidth,
        int expectedHeight)
    {
        //Arrange
        var expectedMarkup = @$"<span class=""horizontal-spacer"" style=""--spacer-width:{expectedWidth}%;--spacer-height:{expectedHeight}px;""></span>";

        //Act
        var cut = RenderComponent<HorizontalAccentDivider>(parameters => parameters
                    .Add(p => p.HeightPixels, expectedHeight)
                    .Add(p => p.WidthPercentage, expectedWidth));

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }
}
