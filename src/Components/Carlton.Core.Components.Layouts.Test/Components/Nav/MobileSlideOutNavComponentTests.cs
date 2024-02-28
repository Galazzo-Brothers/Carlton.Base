using Carlton.Core.Components.Navigation;
namespace Carlton.Core.Components.Layouts.Test.Components.Nav;


[Trait("Component", nameof(MobileSlideOutNav))]
public class MobileSlideOutNavComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test")]
    [InlineAutoData(true)]
    [InlineAutoData(false)]
    public void MobileSlideOutNav_Markup_RendersCorrectly(bool expectedIsExpanded, string expectedContent)
    {
        //Arrange
        var expectedMarkup = $@"
        <div class=""slide-out-nav"">
            <a href = ""#"" >
                <span class=""icon mdi mdi-24px {(expectedIsExpanded ? "mdi-close" : "mdi-menu")}"" />
            </a>
                <div class=""content-container {(expectedIsExpanded ? "active" : string.Empty)}"">
                {expectedContent}
            </div>
        </div>";


        //Act
        var cut = RenderComponent<MobileSlideOutNav>(parameters =>
            parameters.Add(p => p.IsExpanded, expectedIsExpanded)
                      .Add(p => p.Content, expectedContent));

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory(DisplayName = "IsExpanded Parameter Test")]
    [InlineAutoData(true)]
    [InlineAutoData(false)]
    public void MobileSlideOutNav_IsExpandedParameter_RendersCorrectly(bool expectedIsExpanded, string expectedContent)
    {
        //Act
        var cut = RenderComponent<MobileSlideOutNav>(parameters =>
            parameters.Add(p => p.IsExpanded, expectedIsExpanded)
                      .Add(p => p.Content, expectedContent));

        var icon = cut.Find(".icon");
        var isMenuIcon = icon.ClassList.Contains("mdi-menu");
        var isCloseIcon = icon.ClassList.Contains("mdi-close");
        var contentActive = cut.Find(".content-container").ClassList.Contains("active");

        //Assert
        isCloseIcon.ShouldBe(expectedIsExpanded);
        isMenuIcon.ShouldBe(!expectedIsExpanded);
        contentActive.ShouldBe(expectedIsExpanded);
    }

    [Theory(DisplayName = "Content Parameter Test"), AutoData]
    public void MobileSlideOutNav_ContentParameter_RendersCorrectly(bool expectedIsExpanded, string expectedContent)
    {
        //Act
        var cut = RenderComponent<MobileSlideOutNav>(parameters =>
            parameters.Add(p => p.IsExpanded, expectedIsExpanded)
                      .Add(p => p.Content, expectedContent));

        var actualContent = cut.Find(".content-container").TextContent;

        //Assert
        actualContent.ShouldBe(expectedContent);
    }

    [Theory(DisplayName = "Click Event Test")]
    [InlineAutoData(true)]
    [InlineAutoData(false)]
    public void MobileSlideOutNav_Click_RendersCorrectly(bool expectedIsExpanded, string expectedContent)
    {
        //Arrange
        var cut = RenderComponent<MobileSlideOutNav>(parameters =>
            parameters.Add(p => p.IsExpanded, expectedIsExpanded)
                      .Add(p => p.Content, expectedContent));


        //Act
        cut.Find(".icon").Click();

        var icon = cut.Find(".icon");
        var isMenuIcon = icon.ClassList.Contains("mdi-menu");
        var isCloseIcon = icon.ClassList.Contains("mdi-close");
        var contentActive = cut.Find(".content-container").ClassList.Contains("active");

        //Assert
        isCloseIcon.ShouldBe(!expectedIsExpanded); //Opposite because of click
        isMenuIcon.ShouldBe(expectedIsExpanded); //Opposite because of click
        contentActive.ShouldBe(!expectedIsExpanded); //Opposite because of click
    }
}
