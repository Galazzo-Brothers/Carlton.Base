using Carlton.Core.Components.Pills;
namespace Carlton.Core.Components.Tests.Pills;

[Trait("Component", nameof(Pill))]
public class PillComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test"), AutoData]
    public void Pill_Markup_RendersCorrectly(string expectedText)
    {
        //Arrange
        var expectedMarkup = @$"<div class=""pill"">{expectedText}</div>";

        //Act
        var cut = RenderComponent<Pill>(parameters => parameters
                    .Add(p => p.Text, expectedText));

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }
}
