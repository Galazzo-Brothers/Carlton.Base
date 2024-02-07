using Carlton.Core.Components.Spinners;
namespace Carlton.Core.Components.Tests;


[Trait("Component", nameof(Spinner))]
public class SpinnerComponentTests : TestContext
{
    [Fact(DisplayName = "Markup Test")]
    public void Spinner_Markup_RendersCorrectly()
    {
        //Arrange
        var expectedMarkup = @$"<div class=""spinner-container""><div class=""spinner""></div></div>";

        //Act
        var cut = RenderComponent<Spinner>();

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }
}
