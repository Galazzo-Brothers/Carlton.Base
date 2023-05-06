namespace Carlton.Base.Components.Test;

[Trait("Component", nameof(BaseNotification))]
public class BaseNotificationComponentTests : TestContext
{
    [Fact(DisplayName = "Markup Test")]
    public void BaseNotification_Markup_RendersCorrectly()
    {
        //Arrange
        var moduleInterop = JSInterop.SetupModule("./_content/Carlton.Base.Components/Notifications/BaseNotification.razor.js");
        moduleInterop.Setup<Task>("applyTransitionedCallback");

        //Act
        var cut = RenderComponent<BaseNotification>(parameters => parameters
            .Add(p => p.Title, "Notification Title")
            .Add(p => p.Message, "Some Notification Message")
            .Add(p => p.IconClass, "icon-class")
            .Add(p => p.FadeOutEnabled, false)
            );

        //Assert
        cut.MarkupMatches(NotificationTestHelper.BaseNotificationMarkup);
    }

    [Theory(DisplayName = "Title Parameter Test")]
    [InlineData("Notification Title Test 1")]
    [InlineData("Notification Title Test 2")]
    [InlineData("Notification Title Test 3")]
    public void BaseNotification_TitleParam_RendersCorrectly(string expectedTitle)
    {
        //Arrange
        var moduleInterop = JSInterop.SetupModule("./_content/Carlton.Base.Components/Notifications/BaseNotification.razor.js");
        moduleInterop.Setup<Task>("applyTransitionedCallback");

        //Act
        var cut = RenderComponent<BaseNotification>(parameters => parameters
            .Add(p => p.Title, expectedTitle)
            .Add(p => p.Message, "Some Notification Message")
            .Add(p => p.IconClass, "icon-class")
            .Add(p => p.FadeOutEnabled, false)
            );

        var title = cut.Find(".title").TextContent;

        //Assert
        Assert.Equal(expectedTitle, title);
    }

    [Theory(DisplayName = "Message Parameter Test")]
    [InlineData("Notification Message Test 1")]
    [InlineData("Notification Message Test 2")]
    [InlineData("Notification Message Test 3")]
    public void BaseNotification_MessageParam_RendersCorrectly(string expectedMessage)
    {
        //Arrange
        var moduleInterop = JSInterop.SetupModule("./_content/Carlton.Base.Components/Notifications/BaseNotification.razor.js");
        moduleInterop.Setup<Task>("applyTransitionedCallback");

        //Act
        var cut = RenderComponent<BaseNotification>(parameters => parameters
            .Add(p => p.Title, "Notification Title")
            .Add(p => p.Message, expectedMessage)
            .Add(p => p.IconClass, "icon-class")
            .Add(p => p.FadeOutEnabled, false)
            );

        var message = cut.Find(".message").TextContent;

        //Assert
        Assert.Equal(expectedMessage, message);
    }

    [Theory(DisplayName = "IconClass Parameter Test")]
    [InlineData("icon-class-test-1")]
    [InlineData("icon-class-test-2")]
    [InlineData("icon-class-test-3")]
    public void BaseNotification_IconClassParameter_RendersCorrectly(string expectedIconClass)
    {
        //Arrange
        var moduleInterop = JSInterop.SetupModule("./_content/Carlton.Base.Components/Notifications/BaseNotification.razor.js");
        moduleInterop.Setup<Task>("applyTransitionedCallback");

        //Act
        var cut = RenderComponent<BaseNotification>(parameters => parameters
            .Add(p => p.Title, "Notification Title")
            .Add(p => p.Message, "Some Test Notification Message")
            .Add(p => p.IconClass, expectedIconClass)
            .Add(p => p.FadeOutEnabled, false)
            );

        var icon = cut.Find(".icon");

        //Assert
        Assert.Contains(expectedIconClass, icon.ClassList);
    }

    [Fact(DisplayName = "OnDismiss Parameter Test")]
    public void BaseNotification_OnDismissParameter_RendersCorrectly()
    {
        //Arrange
        var eventCalled = false;
        var moduleInterop = JSInterop.SetupModule("./_content/Carlton.Base.Components/Notifications/BaseNotification.razor.js");
        moduleInterop.Setup<Task>("applyTransitionedCallback");
        moduleInterop.Setup<Task>("removeTransitionedCallback");

        var cut = RenderComponent<BaseNotification>(parameters => parameters
            .Add(p => p.Title, "Notification Title")
            .Add(p => p.Message, "Some Test Notification Message")
            .Add(p => p.IconClass, "icon-class-test")
            .Add(p => p.FadeOutEnabled, false)
            .Add(p => p.OnDismiss, () => eventCalled = true)
            );

        //Act
        cut.Find(".dismiss").Click();
        cut.WaitForState(() => cut.Find(".carlton-notification").ClassList.Contains("dismissed"));

        //Assert
        cut.WaitForAssertion(() =>
            {
                var invocationIdentifierToAssert = moduleInterop.Invocations.ElementAt(1).Identifier;
                Assert.True(invocationIdentifierToAssert == "removeTransitionedCallback");
                Assert.True(eventCalled);
            }
        );
    }

    [Fact(DisplayName = "Dispose Component Test")]
    public void BaseNotification_DisposeComponent_ShouldSucceed()
    {
        //Arrange
        var moduleInterop = JSInterop.SetupModule("./_content/Carlton.Base.Components/Notifications/BaseNotification.razor.js");
        moduleInterop.Setup<Task>("applyTransitionedCallback");

        RenderComponent<BaseNotification>(parameters => parameters
            .Add(p => p.Title, "Notification Title")
            .Add(p => p.Message, "Some Test Notification Message")
            .Add(p => p.IconClass, "icon-class-test")
            .Add(p => p.FadeOutEnabled, false)
            );

        //Act
        var ex = Record.Exception(() => DisposeComponents());

        //Assert
        Assert.Null(ex);
    }
}
