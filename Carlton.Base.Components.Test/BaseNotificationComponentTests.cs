namespace Carlton.Base.Components.Test;

public class BaseNotificationComponentTests : TestContext
{
    private static readonly string BaseNotificationMarkup =
@"
<div class=""carlton-notification"" b-rlbygplu4k>
    <div class=""content"" b-rlbygplu4k>
        <span class=""icon mdi mdi-24px icon-class"" b-rlbygplu4k></span>
        <div class=""message-container"" b-rlbygplu4k>
            <span class=""title"" b-rlbygplu4k>Notification Title</span>
            <span class=""message"" b-rlbygplu4k>Some Notification Message</span>
        </div>
        <span class=""dismiss mdi mdi-18px mdi-close"" blazor:onclick=""1"" b-rlbygplu4k></span>
    </div>
</div>";

    [Fact]
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
        cut.MarkupMatches(BaseNotificationMarkup);
    }

    [Fact]
    public void BaseNotification_TitleParam_RendersCorrectly()
    {
        //Arrange
        var moduleInterop = JSInterop.SetupModule("./_content/Carlton.Base.Components/Notifications/BaseNotification.razor.js");
        moduleInterop.Setup<Task>("applyTransitionedCallback");

        //Act
        var cut = RenderComponent<BaseNotification>(parameters => parameters
            .Add(p => p.Title, "Notification Title Test")
            .Add(p => p.Message, "Some Notification Message")
            .Add(p => p.IconClass, "icon-class")
            .Add(p => p.FadeOutEnabled, false)
            );

        var title = cut.Find(".title").TextContent;

        //Assert
        Assert.Equal("Notification Title Test", title);
    }

    [Fact]
    public void BaseNotification_MessageParam_RendersCorrectly()
    {
        //Arrange
        var moduleInterop = JSInterop.SetupModule("./_content/Carlton.Base.Components/Notifications/BaseNotification.razor.js");
        moduleInterop.Setup<Task>("applyTransitionedCallback");

        //Act
        var cut = RenderComponent<BaseNotification>(parameters => parameters
            .Add(p => p.Title, "Notification Title")
            .Add(p => p.Message, "Some Test Notification Message")
            .Add(p => p.IconClass, "icon-class")
            .Add(p => p.FadeOutEnabled, false)
            );

        var message = cut.Find(".message").TextContent;

        //Assert
        Assert.Equal("Some Test Notification Message", message);
    }

    [Fact]
    public void BaseNotification_IconClassParameter_RendersCorrectly()
    {
        //Arrange
        var moduleInterop = JSInterop.SetupModule("./_content/Carlton.Base.Components/Notifications/BaseNotification.razor.js");
        moduleInterop.Setup<Task>("applyTransitionedCallback");

        //Act
        var cut = RenderComponent<BaseNotification>(parameters => parameters
            .Add(p => p.Title, "Notification Title")
            .Add(p => p.Message, "Some Test Notification Message")
            .Add(p => p.IconClass, "icon-class-test")
            .Add(p => p.FadeOutEnabled, false)
            );

        var icon = cut.Find(".icon");

        //Assert
        Assert.Contains("icon-class-test", icon.ClassList);
    }

    [Fact]
    public void BaseNotification_FadeOutEnabledParameter_RendersCorrectly()
    {
        //Arrange
        var moduleInterop = JSInterop.SetupModule("./_content/Carlton.Base.Components/Notifications/BaseNotification.razor.js");
        moduleInterop.Setup<Task>("applyTransitionedCallback");

        //Act
        var cut = RenderComponent<BaseNotification>(parameters => parameters
            .Add(p => p.Title, "Notification Title")
            .Add(p => p.Message, "Some Test Notification Message")
            .Add(p => p.IconClass, "icon-class-test")
            .Add(p => p.FadeOutEnabled, true)
            );

        //Assert
        cut.WaitForAssertion(() => cut.Find(".carlton-notification").ClassList.Contains("dismissed"));
    }

    [Fact]
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

    [Fact]
    public void BaseNotification_DisposeComponent_ShouldSucced()
    {
        //Arrange
        var moduleInterop = JSInterop.SetupModule("./_content/Carlton.Base.Components/Notifications/BaseNotification.razor.js");
        moduleInterop.Setup<Task>("applyTransitionedCallback");

        var cut = RenderComponent<BaseNotification>(parameters => parameters
            .Add(p => p.Title, "Notification Title")
            .Add(p => p.Message, "Some Test Notification Message")
            .Add(p => p.IconClass, "icon-class-test")
            .Add(p => p.FadeOutEnabled, false)
            );

        //Act
        DisposeComponents();
    }
}
