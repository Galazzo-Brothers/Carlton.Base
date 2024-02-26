using Carlton.Core.Components.Footers;
namespace Carlton.Core.Components.Tests.Footers;

[Trait("Component", nameof(Footer))]
public class FooterComponentTests : TestContext
{
    [Fact(DisplayName = "Markup Test")]
    public void Footer_Markup_RendersCorrectly()
    {
        //Arrange
        var expectedMarkup = @$"
             <div class=""content"" >
              <div class=""external-urls"" >
                <a href=""#"" >Dashboard</a>
                <a href=""#"" >Documentation</a>
                <a href=""#"" >About Us</a>
              </div>
              <div class=""contacts"" >
                <div class=""msg"" >Coded and Designed by Galazzo Brothers.</div>
                <i class=""mdi mdi-24px mdi-github-circle"" ></i>
                <i class=""mdi mdi-24px mdi-twitter"" ></i>
                <i class=""mdi mdi-24px mdi-facebook"" ></i>
              </div>
            </div>";

        //Act
        var cut = RenderComponent<Footer>();

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

}
