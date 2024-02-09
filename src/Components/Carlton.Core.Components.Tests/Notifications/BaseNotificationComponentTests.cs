//using AutoFixture.Xunit2;

//namespace Carlton.Core.Components.Library.Tests;

//[Trait("Component", nameof(BaseNotification))]
//public class BaseNotificationComponentTests : TestContext
//{
//    private const string ModuleString = "./_content/Carlton.Core.Components.Library/Notifications/BaseNotification.razor.js";
//    private const string ApplyTransitionedCallback = "applyTransitionedCallback";
//    private const string RemoveTransitionedCallback = "removeTransitionedCallback";

//    [Theory(DisplayName = "Markup Test"), AutoData]
//    public void BaseNotification_Markup_RendersCorrectly(string title, string message, string iconClass, bool fadeOutEnabled)
//    {
//        //Arrange
//        var expectedMarkup = BuildExpectedMarkup(title, message, iconClass);
//        var moduleInterop = JSInterop.SetupModule(ModuleString);
//        moduleInterop.Setup<Task>(ApplyTransitionedCallback);

//        //Act
//        var cut = RenderComponent<BaseNotification>(parameters => parameters
//            .Add(p => p.Title, title)
//            .Add(p => p.Message, message)
//            .Add(p => p.IconClass, iconClass)
//            .Add(p => p.FadeOutEnabled, fadeOutEnabled));

//        //Assert
//        cut.MarkupMatches(expectedMarkup);
//    }

//    [Theory(DisplayName = "Title Parameter Test"), AutoData]
//    public void BaseNotification_TitleParam_RendersCorrectly(
//        string expectedTitle,
//        string message,
//        string iconClass,
//        bool fadeOutEnabled)
//    {
//        //Arrange
//        var moduleInterop = JSInterop.SetupModule(ModuleString);
//        moduleInterop.Setup<Task>(ApplyTransitionedCallback);

//        //Act
//        var cut = RenderComponent<BaseNotification>(parameters => parameters
//            .Add(p => p.Title, expectedTitle)
//            .Add(p => p.Message, message)
//            .Add(p => p.IconClass, iconClass)
//            .Add(p => p.FadeOutEnabled, fadeOutEnabled));

//        var title = cut.Find(".title").TextContent;

//        //Assert
//        Assert.Equal(expectedTitle, title);
//    }

//    [Theory(DisplayName = "Message Parameter Test"), AutoData]
//    public void BaseNotification_MessageParam_RendersCorrectly(
//        string title,
//        string expectedMessage,
//        string iconClass,
//        bool fadeOutEnabled)
//    {
//        //Arrange
//        var moduleInterop = JSInterop.SetupModule(ModuleString);
//        moduleInterop.Setup<Task>(ApplyTransitionedCallback);

//        //Act
//        var cut = RenderComponent<BaseNotification>(parameters => parameters
//            .Add(p => p.Title, title)
//            .Add(p => p.Message, expectedMessage)
//            .Add(p => p.IconClass, iconClass)
//            .Add(p => p.FadeOutEnabled, fadeOutEnabled));

//        var message = cut.Find(".message").TextContent;

//        //Assert
//        Assert.Equal(expectedMessage, message);
//    }

//    [Theory(DisplayName = "IconClass Parameter Test"), AutoData]
//    public void BaseNotification_IconClassParameter_RendersCorrectly(
//        string title,
//        string message,
//        string iconClass,
//        bool fadeOutEnabled)
//    {
//        //Arrange
//        var moduleInterop = JSInterop.SetupModule(ModuleString);
//        moduleInterop.Setup<Task>(ApplyTransitionedCallback);

//        //Act
//        var cut = RenderComponent<BaseNotification>(parameters => parameters
//            .Add(p => p.Title, title)
//            .Add(p => p.Message, message)
//            .Add(p => p.IconClass, iconClass)
//            .Add(p => p.FadeOutEnabled, fadeOutEnabled));

//        var icon = cut.Find(".icon");

//        //Assert
//        Assert.Contains(iconClass, icon.ClassList);
//    }

//    [Theory(DisplayName = "OnDismiss Parameter Test"), AutoData]
//    public void BaseNotification_OnDismissParameter_RendersCorrectly(
//        string title,
//        string message,
//        string iconClass,
//        bool fadeOutEnabled)
//    {
//        //Arrange
//        var eventCalled = false;
//        var moduleInterop = JSInterop.SetupModule(ModuleString);
//        moduleInterop.Setup<Task>(ApplyTransitionedCallback);
//        moduleInterop.Setup<Task>(RemoveTransitionedCallback);

//        var cut = RenderComponent<BaseNotification>(parameters => parameters
//            .Add(p => p.Title, title)
//            .Add(p => p.Message, message)
//            .Add(p => p.IconClass, iconClass)
//            .Add(p => p.FadeOutEnabled, fadeOutEnabled)
//            .Add(p => p.OnDismiss, () => eventCalled = true));

//        //Act
//        cut.Find(".dismiss").Click();
//        cut.WaitForState(() => cut.Find(".carlton-notification").ClassList.Contains("dismissed"));

//        //Assert
//        cut.WaitForAssertion(() =>
//            {
//                var invocationIdentifierToAssert = moduleInterop.Invocations.ElementAt(1).Identifier;
//                Assert.True(invocationIdentifierToAssert == "removeTransitionedCallback");
//                Assert.True(eventCalled);
//            }
//        );
//    }

//    [Theory(DisplayName = "Dispose Component Test"), AutoData]
//    public void BaseNotification_DisposeComponent_ShouldSucceed(
//        string title,
//        string message,
//        string iconClass,
//        bool fadeOutEnabled)
//    {
//        //Arrange
//        var moduleInterop = JSInterop.SetupModule(ModuleString);
//        moduleInterop.Setup<Task>(ApplyTransitionedCallback);

//        RenderComponent<BaseNotification>(parameters => parameters
//            .Add(p => p.Title, title)
//            .Add(p => p.Message, message)
//            .Add(p => p.IconClass, iconClass)
//            .Add(p => p.FadeOutEnabled, fadeOutEnabled));

//        //Act
//        var ex = Record.Exception(() => DisposeComponents());

//        //Assert
//        Assert.Null(ex);
//    }

//    private static string BuildExpectedMarkup(string title, string message, string iconClass)
//    {
//        return @$"
//<div class=""carlton-notification"">
//    <div class=""content"">
//        <span class=""icon mdi mdi-24px {iconClass}""></span>
//        <div class=""message-container"">
//            <span class=""title"">{title}</span>
//            <span class=""message"">{message}</span>
//        </div>
//        <span class=""dismiss mdi mdi-18px mdi-close""></span>
//    </div>
//</div>";

//    }
//}
