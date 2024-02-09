//namespace Carlton.Core.Components.Library.Tests;

//[Trait("Component", nameof(HamburgerCollapser))]
//public class HamburgerCollapserComponentTests : TestContext
//{
//    [Theory(DisplayName = "Markup Test")]
//    [InlineData(true)]
//    [InlineData(false)]
//    public void HamburgerCollapser_Markup_RendersCorrectly(bool isCollapsed)
//    {
//        //Arrange
//        var expectedMarkup = @"<div class=""collapser""><a href=""#""><span class=""mdi mdi-24px mdi-menu""></span></a></div>";
//        var layoutState = new LayoutState(isCollapsed, () => { });

//        //Act
//        var cut = RenderComponent<HamburgerCollapser>(parameters => parameters
//                    .AddCascadingValue(layoutState));

//        //Assert
//        cut.MarkupMatches(expectedMarkup);
//    }

//    [Theory(DisplayName = "Collapser Click Event Test")]
//    [InlineData(true)]
//    [InlineData(false)]
//    public void HamburgerCollapser_ClickEvent_ShouldRenderCorrectly(bool isCollapsed)
//    {
//        //Arrange
//        var layoutState = new LayoutState(isCollapsed, () => { });

//        var cut = RenderComponent<HamburgerCollapser>(parameters => parameters
//                    .AddCascadingValue(layoutState));

//        var element = cut.Find(".collapser a");
//        var expectedResult = !isCollapsed;

//        //Act
//        element.Click();

//        //Assert
//        Assert.Equal(expectedResult, layoutState.IsCollapsed);
//    }
//}
