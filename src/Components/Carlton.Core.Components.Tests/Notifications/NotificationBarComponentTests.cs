//using AutoFixture.Xunit2;

//namespace Carlton.Core.Components.Library.Tests;

//[Trait("Component", nameof(NotificationBar))]
//public class NotificationBarComponentTests : TestContext
//{
//    private const string ModuleString = "./_content/Carlton.Core.Components.Library/Notifications/BaseNotification.razor.js";
//    private const string ModuleFuncString = "applyTransitionedCallback";

//    [Theory(DisplayName = "Markup Test"), AutoData]
//    public void NotificationBar_Markup_RendersCorrectly(int top, int right)
//    {
//        //Arrange
//        var expectedMarkup = BuildExpectedNotificationBarMarkup(top, right);

//        //Act
//        var cut = RenderComponent<NotificationBar>(parameters => parameters
//            .Add(p => p.IsTestMode, true)
//            .Add(p => p.FadeOutEnabled, false)
//            .Add(p => p.Top, top)
//            .Add(p => p.Right, right));

//        //Assert
//        cut.MarkupMatches(expectedMarkup);
//    }

//    [Theory(DisplayName = "IsTestMode Parameter False Test"), AutoData]
//    public void NotificationBar_IsTestModeFalseParam_RendersCorrectly(bool fadeOutEnabled, int top, int right)
//    {
//        //Act
//        var cut = RenderComponent<NotificationBar>(parameters => parameters
//            .Add(p => p.IsTestMode, false)
//            .Add(p => p.FadeOutEnabled, fadeOutEnabled)
//            .Add(p => p.Top, top)
//            .Add(p => p.Right, right));

//        //Assert
//        Assert.Throws<ElementNotFoundException>(() => cut.Find(".test-btns"));
//    }

//    [Theory(DisplayName = "IsTestMode Parameter True Test"), AutoData]
//    public void NotificationBar_IsTestModeTrueParam_RendersCorrectly(bool fadeOutEnabled, int top, int right)
//    {
//        //Arrange
//        var expectedMarkup = BuildExpectedNotificationBarBtnsMarkup();

//        //Act
//        var cut = RenderComponent<NotificationBar>(parameters => parameters
//            .Add(p => p.IsTestMode, true)
//            .Add(p => p.FadeOutEnabled, fadeOutEnabled)
//            .Add(p => p.Top, top)
//            .Add(p => p.Right, right));

//        var testBtns = cut.Find(".test-btns");

//        //Assert
//        testBtns.MarkupMatches(expectedMarkup);
//    }

//    [Theory(DisplayName = "FadeOutEnabled Parameter Test")]
//    [InlineAutoData(true, "success-btn")]
//    [InlineAutoData(true, "info-btn")]
//    [InlineAutoData(true, "warning-btn")]
//    [InlineAutoData(true, "error-btn")]
//    [InlineAutoData(false, "success-btn")]
//    [InlineAutoData(false, "info-btn")]
//    [InlineAutoData(false, "warning-btn")]
//    [InlineAutoData(false, "error-btn")]
//    public void NotificationBar_FadeOutEnabledParam_RendersCorrectly(bool fadeOutEnabled, string notificationClass, int top, int right)
//    {
//        //Arrange
//        var moduleInterop = JSInterop.SetupModule(ModuleString);
//        moduleInterop.Setup<Task>(ModuleFuncString);

//        //Act
//        var cut = RenderComponent<NotificationBar>(parameters => parameters
//            .Add(p => p.IsTestMode, true)
//            .Add(p => p.FadeOutEnabled, fadeOutEnabled)
//            .Add(p => p.Top, top)
//            .Add(p => p.Right, right));

//        cut.Find($".{notificationClass}").Click();

//        var notification = cut.FindComponent<BaseNotification>().Instance;

//        //Assert
//        Assert.Equal(fadeOutEnabled, notification.FadeOutEnabled);
//    }

//    [Theory(DisplayName = "TopRight Parameter Test"), AutoData]
//    public void NotificationBar_TopRightParams_RendersCorrectly(int top, int right)
//    {
//        //Arrange
//        var expectedResult = $"top:{top}px;right:{right}px;";
//        var moduleInterop = JSInterop.SetupModule(ModuleString);
//        moduleInterop.Setup<Task>(ModuleFuncString);

//        //Act
//        var cut = RenderComponent<NotificationBar>(parameters => parameters
//            .Add(p => p.IsTestMode, true)
//            .Add(p => p.FadeOutEnabled, false)
//            .Add(p => p.Top, top)
//            .Add(p => p.Right, right));

//        var content = cut.Find(".content");
//        var style = content?.Attributes?.GetNamedItem("style")?.TextContent;

//        //Assert
//        Assert.Equal(expectedResult, style);
//    }

//    [Theory(DisplayName = "NotificationBar Click Test")]
//    [InlineData("success-btn", typeof(SuccessNotification))]
//    [InlineData("info-btn", typeof(InfoNotification))]
//    [InlineData("warning-btn", typeof(WarningNotification))]
//    [InlineData("error-btn", typeof(ErrorNotification))]
//    public void NotificationBar_ClickEvent_RendersCorrectly(string btnClass, Type expectedType)
//    {
//        //Arrange
//        var moduleInterop = JSInterop.SetupModule(ModuleString);
//        moduleInterop.Setup<Task>(ModuleFuncString);

//        //Act
//        var cut = RenderComponent<NotificationBar>(parameters => parameters
//            .Add(p => p.IsTestMode, true)
//            .Add(p => p.FadeOutEnabled, false)
//            .Add(p => p.Top, 10)
//            .Add(p => p.Right, 10));

//        cut.Find($".{btnClass}").Click();
//        var component = cut.FindComponent<BaseNotification>().Instance;

//        //Assert
//        Assert.Equal(expectedType, component.GetType());
//    }

//    [Theory(DisplayName = "Notification Bar Multiple Click Test")]
//    [InlineData(new int[] { 2, 7, 3, 4 })]
//    [InlineData(new int[] { 1, 2, 3, 4 })]
//    [InlineData(new int[] { 0, 1, 3, 0 })]
//    public void NotificationBar_ClickEventRepeatedly_RendersCorrectly(IEnumerable<int> clickCounts)
//    {
//        //Arrange
//        var moduleInterop = JSInterop.SetupModule(ModuleString);
//        moduleInterop.Setup<Task>(ModuleFuncString);

//        var cut = RenderComponent<NotificationBar>(parameters => parameters
//            .Add(p => p.IsTestMode, true)
//            .Add(p => p.FadeOutEnabled, false)
//            .Add(p => p.Top, 10)
//            .Add(p => p.Right, 10));

//        var successBtn = cut.Find(".success-btn");
//        var infoBtn = cut.Find(".info-btn");
//        var warningBtn = cut.Find(".warning-btn");
//        var errorBtn = cut.Find(".error-btn");

//        //Act
//        for(var i = 0; i < clickCounts.ElementAt(0); i++)
//            successBtn.Click();
//        for(var i = 0; i < clickCounts.ElementAt(1); i++)
//            infoBtn.Click();
//        for(var i = 0; i < clickCounts.ElementAt(2); i++)
//            warningBtn.Click();
//        for(var i = 0; i < clickCounts.ElementAt(3); i++)
//            errorBtn.Click();


//        var successActual = cut.FindComponents<SuccessNotification>().Count;
//        var infoActual = cut.FindComponents<InfoNotification>().Count;
//        var warnActual = cut.FindComponents<WarningNotification>().Count;
//        var errorActual = cut.FindComponents<ErrorNotification>().Count;

//        var successExpected = clickCounts.ElementAt(0);
//        var infoExpected = clickCounts.ElementAt(1);
//        var warnExpected = clickCounts.ElementAt(2);
//        var errorExpected = clickCounts.ElementAt(3);

//        //Assert
//        Assert.Equal(successExpected, successActual);
//        Assert.Equal(infoExpected, infoActual);
//        Assert.Equal(warnExpected, warnActual);
//        Assert.Equal(errorExpected, errorActual);
//    }

//    private static string BuildExpectedNotificationBarMarkup(int top, int right)
//    {
//        return @$"
//<div class=""notification-bar"">
//    <div class=""content"" style=""top:{top}px;right:{right}px;""></div>
//</div>
//{BuildExpectedNotificationBarBtnsMarkup()}";
//    }

//    private static string BuildExpectedNotificationBarBtnsMarkup()
//    {
//        return @"<div class=""test-btns"">
//    <button class=""success-btn"">Success</button>
//    <button class=""info-btn"">Info</button>
//    <button class=""warning-btn"">Warning</button>
//    <button class=""error-btn"">Error</button>
//</div>";
//    }
//}
