using Carlton.Core.Components.Toasts;
namespace Carlton.Core.Components.Tests.Toasts;

[Trait("Component", nameof(Toast))]
public class ToastComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test")]
    [InlineAutoData(ToastTypes.Success)]
    [InlineAutoData(ToastTypes.Info)]
    [InlineAutoData(ToastTypes.Warning)]
    [InlineAutoData(ToastTypes.Error)]

    public void Toast_Markup_RendersCorrectly(
       ToastTypes expectedToastType,
       int expectedId,
       string expectedTitle,
       string expectedMessage,
       bool expectedIsDissmissed)
    {
        //Arrange
        var moduleInterop = JSInterop.SetupModule(Toast.ImportPath);
        moduleInterop.Setup<Task>(Toast.InitNewToast, _ => true);
        var expectedMarkup =
@$"<div id=""toast-{expectedId}"" class=""toast {expectedToastType.ToString().ToLower()} {(expectedIsDissmissed ? "dismissed" : string.Empty)}"">
    <div class=""content"">
        <span class=""icon mdi mdi-24px {ExpectedToastIconClass(expectedToastType)}""></span>
        <div class=""message-container"">
            <span class=""title"">{expectedTitle}</span>
            <span class=""message"">{expectedMessage}</span>
        </div>
        <span class=""dismiss mdi mdi-18px mdi-close""></span>
    </div>
</div>";

        //Act
        var cut = RenderComponent<Toast>(parameters => parameters
            .Add(p => p.Id, expectedId)
            .Add(p => p.Title, expectedTitle)
            .Add(p => p.Message, expectedMessage)
            .Add(p => p.ToastType, expectedToastType)
            .Add(p => p.FadeOutEnabled, false)
            .Add(p => p.IsDismissed, expectedIsDissmissed));

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory(DisplayName = "Id Parameter Test"), AutoData]
    public void Toast_IdParameter_RendersCorrectly(
       int expectedId,
       string expectedTitle,
       string expectedMessage,
       ToastTypes expectedToastType,
       bool expectedIsDissmissed)
    {
        //Arrange
        var moduleInterop = JSInterop.SetupModule(Toast.ImportPath);
        moduleInterop.Setup<Task>(Toast.InitNewToast, _ => true);

        //Act
        var cut = RenderComponent<Toast>(parameters => parameters
            .Add(p => p.Id, expectedId)
            .Add(p => p.Title, expectedTitle)
            .Add(p => p.Message, expectedMessage)
            .Add(p => p.ToastType, expectedToastType)
            .Add(p => p.FadeOutEnabled, false)
            .Add(p => p.IsDismissed, expectedIsDissmissed));

        var expectedToastId = $"toast-{expectedId}"; ;
        var actualId = cut.Find(".toast").Id;

        //Assert
        actualId.ShouldBe(expectedToastId);
    }

    [Theory(DisplayName = "Title Parameter Test"), AutoData]
    public void Toast_TitleParameter_RendersCorrectly(
        int expectedId,
        string expectedTitle,
        string expectedMessage,
        ToastTypes expectedToastType,
        bool expectedIsDissmissed)
    {
        //Arrange
        var moduleInterop = JSInterop.SetupModule(Toast.ImportPath);
        moduleInterop.Setup<Task>(Toast.InitNewToast, _ => true);

        //Act
        var cut = RenderComponent<Toast>(parameters => parameters
            .Add(p => p.Id, expectedId)
            .Add(p => p.Title, expectedTitle)
            .Add(p => p.Message, expectedMessage)
            .Add(p => p.ToastType, expectedToastType)
            .Add(p => p.FadeOutEnabled, false)
            .Add(p => p.IsDismissed, expectedIsDissmissed));

        var actualTitle = cut.Find(".title").TextContent;

        //Assert
        actualTitle.ShouldBe(expectedTitle);
    }

    [Theory(DisplayName = "Message Parameter Test"), AutoData]

    public void Toast_MessageParameter_RendersCorrectly(
        int expectedId,
        string expectedTitle,
        string expectedMessage,
        ToastTypes expectedToastType,
        bool expectedIsDissmissed)
    {
        //Arrange
        var moduleInterop = JSInterop.SetupModule(Toast.ImportPath);
        moduleInterop.Setup<Task>(Toast.InitNewToast, _ => true);

        //Act
        var cut = RenderComponent<Toast>(parameters => parameters
            .Add(p => p.Id, expectedId)
            .Add(p => p.Title, expectedTitle)
            .Add(p => p.Message, expectedMessage)
            .Add(p => p.ToastType, expectedToastType)
            .Add(p => p.FadeOutEnabled, false)
            .Add(p => p.IsDismissed, expectedIsDissmissed));

        var actualMessage = cut.Find(".message").TextContent;

        //Assert
        actualMessage.ShouldBe(expectedMessage);
    }

    [Theory(DisplayName = "ToastType Parameter Test")]
    [InlineAutoData(ToastTypes.Success)]
    [InlineAutoData(ToastTypes.Info)]
    [InlineAutoData(ToastTypes.Warning)]
    [InlineAutoData(ToastTypes.Error)]
    public void Toast_ToastTypeParameter_RendersCorrectly(
      ToastTypes expectedToastType,
      int expectedId,
      string expectedTitle,
      string expectedMessage,
      bool expectedIsDissmissed)
    {
        //Arrange
        var moduleInterop = JSInterop.SetupModule(Toast.ImportPath);
        moduleInterop.Setup<Task>(Toast.InitNewToast, _ => true);

        //Act
        var cut = RenderComponent<Toast>(parameters => parameters
            .Add(p => p.Id, expectedId)
            .Add(p => p.Title, expectedTitle)
            .Add(p => p.Message, expectedMessage)
            .Add(p => p.ToastType, expectedToastType)
            .Add(p => p.FadeOutEnabled, false)
            .Add(p => p.IsDismissed, expectedIsDissmissed));

        var actualToastClassList = cut.Find(".toast").ClassList;
        var actualIconClassList = cut.Find(".icon").ClassList;

        //Assert
        actualToastClassList.ShouldContain(expectedToastType.ToString().ToLower());
        actualIconClassList.ShouldContain(ExpectedToastIconClass(expectedToastType));
    }

    [Theory(DisplayName = "FadeOutEnabled Parameter Test")]
    [InlineAutoData(true)]
    [InlineAutoData(false)]
    public async Task Toast_FadeOutEnabledParameter_RendersCorrectly(
     bool expectedFadeOutEnabled,
     int expectedId,
     string expectedTitle,
     string expectedMessage,
     ToastTypes expectedToastType)
    {
        //Arrange
        var moduleInterop = JSInterop.SetupModule(Toast.ImportPath);
        moduleInterop.Setup<Task>(Toast.InitNewToast, _ => true);

        //Act
        var cut = RenderComponent<Toast>(parameters => parameters
            .Add(p => p.Id, expectedId)
            .Add(p => p.Title, expectedTitle)
            .Add(p => p.Message, expectedMessage)
            .Add(p => p.ToastType, expectedToastType)
            .Add(p => p.FadeOutEnabled, expectedFadeOutEnabled)
            .Add(p => p.IsDismissed, false));

        await Task.Delay(3000);

        //Assert
        cut.Instance.IsDismissed.ShouldBe(expectedFadeOutEnabled);
    }

    [Theory(DisplayName = "IsDismissed Parameter Test")]
    [InlineAutoData(true)]
    [InlineAutoData(false)]
    public void Toast_IsDismissedParameter_RendersCorrectly(
      bool expectedIsDissmissed,
      int expectedId,
      string expectedTitle,
      string expectedMessage,
      ToastTypes expectedToastType)
    {
        //Arrange
        var moduleInterop = JSInterop.SetupModule(Toast.ImportPath);
        moduleInterop.Setup<Task>(Toast.InitNewToast, _ => true);

        //Act
        var cut = RenderComponent<Toast>(parameters => parameters
            .Add(p => p.Id, expectedId)
            .Add(p => p.Title, expectedTitle)
            .Add(p => p.Message, expectedMessage)
            .Add(p => p.ToastType, expectedToastType)
            .Add(p => p.FadeOutEnabled, false)
            .Add(p => p.IsDismissed, expectedIsDissmissed));

        var toastClassList = cut.Find(".toast").ClassList;
        var actualDismissedClassExists = toastClassList.Contains("dismissed");

        //Assert
        actualDismissedClassExists.ShouldBe(expectedIsDissmissed);
    }

    [Theory(DisplayName = "Dismiss Click Test"), AutoData]
    public void Toast_DismissClick_ShouldDismiss(
    int expectedId,
    string expectedTitle,
    string expectedMessage,
    ToastTypes expectedToastType)
    {
        //Arrange
        var moduleInterop = JSInterop.SetupModule(Toast.ImportPath);
        moduleInterop.Setup<Task>(Toast.InitNewToast, _ => true);

        var cut = RenderComponent<Toast>(parameters => parameters
            .Add(p => p.Id, expectedId)
            .Add(p => p.Title, expectedTitle)
            .Add(p => p.Message, expectedMessage)
            .Add(p => p.ToastType, expectedToastType)
            .Add(p => p.FadeOutEnabled, false)
            .Add(p => p.IsDismissed, false));

        var dismissBtn = cut.Find(".dismiss");

        //Act
        dismissBtn.Click();

        var actualToastClassList = cut.Find(".toast").ClassList;

        //Assert
        actualToastClassList.ShouldContain("dismissed");
    }

    [Theory(DisplayName = "OnDismissed Parameter Test"), AutoData]
    public async Task Toast_OnDismissedParameter_ShouldFire(
     int expectedId,
     string expectedTitle,
     string expectedMessage,
     ToastTypes expectedToastType)
    {
        //Arrange
        var eventFired = false;
        var moduleInterop = JSInterop.SetupModule(Toast.ImportPath);
        moduleInterop.Setup<Task>(Toast.InitNewToast, _ => true);

        var cut = RenderComponent<Toast>(parameters => parameters
            .Add(p => p.Id, expectedId)
            .Add(p => p.Title, expectedTitle)
            .Add(p => p.Message, expectedMessage)
            .Add(p => p.ToastType, expectedToastType)
            .Add(p => p.FadeOutEnabled, false)
            .Add(p => p.IsDismissed, false)
            .Add(p => p.OnDismissed, () => eventFired = true));

        var dismissBtn = cut.Find(".dismiss");

        //Act
        await cut.Instance.HandleDismiss();

        //Assert
        eventFired.ShouldBeTrue();
    }

    [Theory(DisplayName = "JavaScript Interop Init Test"), AutoData]
    public void Toast_OnAfterRender_ShouldFireJavascript(
    int expectedId,
    string expectedTitle,
    string expectedMessage,
    ToastTypes expectedToastType)
    {
        //Arrange
        var moduleInterop = JSInterop.SetupModule(Toast.ImportPath);
        moduleInterop.Setup<Task>(Toast.InitNewToast, _ => true);

        //Act
        var cut = RenderComponent<Toast>(parameters => parameters
            .Add(p => p.Id, expectedId)
            .Add(p => p.Title, expectedTitle)
            .Add(p => p.Message, expectedMessage)
            .Add(p => p.ToastType, expectedToastType)
            .Add(p => p.FadeOutEnabled, false)
            .Add(p => p.IsDismissed, false));

        //Assert
        moduleInterop.VerifyInvoke(Toast.InitNewToast);
    }

    [Theory(DisplayName = "JavaScript Interop Dispose Test"), AutoData]
    public void Toast_OnDisposed_ShouldFireJavascript(
       int expectedId,
       string expectedTitle,
       string expectedMessage,
       ToastTypes expectedToastType)
    {
        //Arrange
        var moduleInterop = JSInterop.SetupModule(Toast.ImportPath);
        moduleInterop.Setup<Task>(Toast.InitNewToast, _ => true);
        moduleInterop.Setup<Task>(Toast.DisposeToast, _ => true);

        var cut = RenderComponent<Toast>(parameters => parameters
            .Add(p => p.Id, expectedId)
            .Add(p => p.Title, expectedTitle)
            .Add(p => p.Message, expectedMessage)
            .Add(p => p.ToastType, expectedToastType)
            .Add(p => p.FadeOutEnabled, false)
            .Add(p => p.IsDismissed, false));

        //Act
        DisposeComponents();

        //Assert
        moduleInterop.VerifyInvoke(Toast.DisposeToast);
    }

    private static string ExpectedToastIconClass(ToastTypes expectedToastType)
        => expectedToastType switch
        {
            ToastTypes.Success => "mdi-check-circle",
            ToastTypes.Info => "mdi-alert-circle-outline",
            ToastTypes.Warning => "mdi-alert",
            ToastTypes.Error => "mdi-alert-circle-outline",
            _ => string.Empty,
        };
}

