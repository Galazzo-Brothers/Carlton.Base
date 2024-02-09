//using AutoFixture.Xunit2;

//namespace Carlton.Core.Components.Library.Tests;

//[Trait("Component", nameof(Logo))]
//public class LogoComponentTests : TestContext
//{
//    [Theory(DisplayName = "Markup Test")]
//    [InlineAutoData(true)]
//    [InlineAutoData(false)]
//    public void Logo_Collapsed_Markup_RendersCorrectly(bool isCollapsed, string title)
//    {
//        //Arrange
//        var layoutState = new LayoutState(isCollapsed, () => { });
//        var expectedMarkup =
//@$"<div class=""logo {(isCollapsed ? "collapsed" : string.Empty)}"">
//    <div class=""content"">
//        <img src=""_content/Carlton.Core.Components.Library/images/CarltonLogo.png"">
//        <span class=""title"">{title}</span>
//    </div>
//</div>";

//        //Act
//        var cut = RenderComponent<Logo>(parameters => parameters
//                .AddCascadingValue(layoutState)
//                .Add(p => p.Title, title));

//        //Assert
//        cut.MarkupMatches(expectedMarkup);
//    }

//    [Theory(DisplayName = "IsCollapsed Parameter Test")]
//    [InlineAutoData(true)]
//    [InlineAutoData(false)]
//    public void Logo_IsCollapsedParam_RendersCorrectly(bool isCollapsed, string title)
//    {
//        //Arrange
//        var layoutState = new LayoutState(isCollapsed, () => { });

//        //Act
//        var cut = RenderComponent<Logo>(parameters => parameters
//            .AddCascadingValue(layoutState)
//            .Add(p => p.Title, title));
//        var logoElement = cut.Find(".logo");
//        var hasCollapsedClass = logoElement.ClassList.Contains("collapsed");

//        //Assert
//        Assert.Equal(hasCollapsedClass, layoutState.IsCollapsed);
//    }

//    [Theory(DisplayName = "Title Parameter Test")]
//    [InlineAutoData(true)]
//    [InlineAutoData(false)]
//    public void Logo_TitleParameterRendersCorrectly(bool isCollapsed, string title)
//    {
//        //Arrange
//        var layoutState = new LayoutState(isCollapsed, () => { });

//        //Act
//        var cut = RenderComponent<Logo>(parameters => parameters
//            .AddCascadingValue(layoutState)
//            .Add(p => p.Title, title));
//        var titleElement = cut.Find(".title");

//        //Assert
//        Assert.Equal(titleElement.TextContent, title);
//    }
//}
