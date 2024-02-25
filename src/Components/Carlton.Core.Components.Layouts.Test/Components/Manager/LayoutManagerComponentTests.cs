using Bunit.TestDoubles;
using Carlton.Core.Components.Layouts.State.FullScreen;
using Carlton.Core.Components.Layouts.State.LayoutSettings;
using Carlton.Core.Components.Layouts.State.Modals;
using Carlton.Core.Components.Layouts.State.Theme;
using Carlton.Core.Components.Layouts.State.Toasts;
using Carlton.Core.Components.Layouts.Toasts;
using Carlton.Core.Components.Modals;
namespace Carlton.Core.Components.Layouts.Test.Components.Manager;


[Trait("Component", nameof(LayoutManager))]
public class LayoutManagerComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test")]
    [InlineData(Themes.light)]
    [InlineData(Themes.dark)]
    public void LayoutManager_Markup_RendersCorrectly(
        Themes expectedTheme)
    {
        //Arrange
        var navStateMock = Substitute.For<IFullScreenState>();
        var themeStateMock = Substitute.For<IThemeState>();
        var modalStateMock = Substitute.For<IModalState>();
        var toastStateMock = Substitute.For<IToastState>();
        var layoutSettingsMock = Substitute.For<ILayoutSettings>();
        Services.AddSingleton(navStateMock);
        Services.AddSingleton(themeStateMock);
        Services.AddSingleton(modalStateMock);
        Services.AddSingleton(toastStateMock);
        Services.AddSingleton(layoutSettingsMock);

        var layoutToasterMock = Substitute.For<LayoutToaster>();
        ComponentFactories.Add(layoutToasterMock);
        ComponentFactories.AddStub<Modal>();

        themeStateMock.Theme.Returns(expectedTheme);

        var expectedMarkup = $@"<div class=""layout-manager"" data-theme=""{expectedTheme.ToString()}"" ></div>";

        //Act
        var cut = RenderComponent<LayoutManager>();

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory(DisplayName = "Stub Child Parameters Test"), AutoData]
    public void LayoutManager_Parameters_RendersCorrectly(
    ToastViewModel expectedToastViewModel)
    {
        //Arrange
        var navStateMock = Substitute.For<IFullScreenState>();
        var themeStateMock = Substitute.For<IThemeState>();
        var modalStateMock = Substitute.For<IModalState>();
        var toastStateMock = Substitute.For<IToastState>();
        var layoutSettingsMock = Substitute.For<ILayoutSettings>();

        Services.AddSingleton(navStateMock);
        Services.AddSingleton(themeStateMock);
        Services.AddSingleton(modalStateMock);
        Services.AddSingleton(toastStateMock);
        Services.AddSingleton(layoutSettingsMock);

        var layoutToasterMock = Substitute.For<LayoutToaster>();
        ComponentFactories.Add(layoutToasterMock);
        ComponentFactories.AddStub<Modal>();
        var cut = RenderComponent<LayoutManager>();

        //Act
        var args = new ToastRaisedEventArgs(expectedToastViewModel);
        cut.InvokeAsync(() => toastStateMock.ToastAdded += Raise.Event<EventHandler<ToastRaisedEventArgs>>(new object(), args));

        //Assert
        cut.RenderCount.ShouldBe(2); //second render from the event triggering a stateHasChanged call
    }

    [Theory(DisplayName = "LayoutChanged Event Test"), AutoData]
    public void LayoutManager_LayoutChanged_FiresEvent(
        bool expectedIsCollapsed)
    {
        //Arrange
        var navStateMock = Substitute.For<IFullScreenState>();
        var themeStateMock = Substitute.For<IThemeState>();
        var modalStateMock = Substitute.For<IModalState>();
        var toastStateMock = Substitute.For<IToastState>();
        var layoutSettingsMock = Substitute.For<ILayoutSettings>();

        Services.AddSingleton(navStateMock);
        Services.AddSingleton(themeStateMock);
        Services.AddSingleton(modalStateMock);
        Services.AddSingleton(toastStateMock);
        Services.AddSingleton(layoutSettingsMock);

        var layoutToasterMock = Substitute.For<LayoutToaster>();
        ComponentFactories.Add(layoutToasterMock);
        ComponentFactories.AddStub<Modal>();

        var cut = RenderComponent<LayoutManager>();

        //Act
        var args = new FullScreenStateChangedEventArgs(expectedIsCollapsed);
        cut.InvokeAsync(() => navStateMock.FullScreenStateChanged += Raise.Event<EventHandler<FullScreenStateChangedEventArgs>>(new object(), args));

        //Assert
        cut.RenderCount.ShouldBe(2); //second render from the event triggering a stateHasChanged call
    }

    [Theory(DisplayName = "ThemeChanged Event Test")]
    [InlineData(Themes.light)]
    [InlineData(Themes.dark)]
    public void LayoutManager_ThemeChanged_FiresEvent(
        Themes expectedTheme)
    {
        //Arrange
        var navStateMock = Substitute.For<IFullScreenState>();
        var themeStateMock = Substitute.For<IThemeState>();
        var modalStateMock = Substitute.For<IModalState>();
        var toastStateMock = Substitute.For<IToastState>();
        var layoutSettingsMock = Substitute.For<ILayoutSettings>();

        Services.AddSingleton(navStateMock);
        Services.AddSingleton(themeStateMock);
        Services.AddSingleton(modalStateMock);
        Services.AddSingleton(toastStateMock);
        Services.AddSingleton(layoutSettingsMock);

        var layoutToasterMock = Substitute.For<LayoutToaster>();
        ComponentFactories.Add(layoutToasterMock);
        ComponentFactories.AddStub<Modal>();

        themeStateMock.Theme.Returns(expectedTheme);
        var cut = RenderComponent<LayoutManager>();

        //Act
        var args = new ThemeChangedEventArgs(expectedTheme);
        cut.InvokeAsync(() => themeStateMock.ThemeChanged += Raise.Event<EventHandler<ThemeChangedEventArgs>>(new object(), args));
        var element = cut.Find(".layout-manager");

        //Assert
        cut.RenderCount.ShouldBe(2); //second render from the event triggering a stateHasChanged call
        element.Attributes["data-theme"].Value.ShouldBe(expectedTheme.ToString());
    }

    [Theory(DisplayName = "ModalState Changed Event Test"), AutoData]
    public void LayoutManager_ModalStateChanged_FiresEvent(
        bool expectedIsVisible,
        ModalTypes expectedModalType,
        ModalViewModel expectedModalModel)
    {
        //Arrange
        var navStateMock = Substitute.For<IFullScreenState>();
        var themeStateMock = Substitute.For<IThemeState>();
        var modalStateMock = Substitute.For<IModalState>();
        var toastStateMock = Substitute.For<IToastState>();
        var layoutSettingsMock = Substitute.For<ILayoutSettings>();

        Services.AddSingleton(navStateMock);
        Services.AddSingleton(themeStateMock);
        Services.AddSingleton(modalStateMock);
        Services.AddSingleton(toastStateMock);
        Services.AddSingleton(layoutSettingsMock);

        var layoutToasterMock = Substitute.For<LayoutToaster>();
        ComponentFactories.Add(layoutToasterMock);
        ComponentFactories.AddStub<Modal>();
        var cut = RenderComponent<LayoutManager>();

        //Act
        var args = new ModalStateChangedEventArgs { IsVisible = expectedIsVisible, ModalType = expectedModalType, ModalModel = expectedModalModel };
        cut.InvokeAsync(() => modalStateMock.ModalStateChanged += Raise.Event<EventHandler<ModalStateChangedEventArgs>>(new object(), args));

        //Assert
        cut.RenderCount.ShouldBe(2); //second render from the event triggering a stateHasChanged call
    }

    [Theory(DisplayName = "ToastRaised Event Test"), AutoData]
    public void LayoutManager_ToastRaised_FiresEvent(
        ModalTypes expectedModalType,
        ModalViewModel expectedModalModel)
    {
        //Arrange
        var navStateMock = Substitute.For<IFullScreenState>();
        var themeStateMock = Substitute.For<IThemeState>();
        var modalStateMock = Substitute.For<IModalState>();
        var toastStateMock = Substitute.For<IToastState>();
        var layoutSettingsMock = Substitute.For<ILayoutSettings>();

        Services.AddSingleton(navStateMock);
        Services.AddSingleton(themeStateMock);
        Services.AddSingleton(modalStateMock);
        Services.AddSingleton(toastStateMock);
        Services.AddSingleton(layoutSettingsMock);

        modalStateMock.ModalType.Returns(expectedModalType);
        modalStateMock.ModalModel.Returns(expectedModalModel); 

        var layoutToasterMock = Substitute.For<LayoutToaster>();
        ComponentFactories.Add(layoutToasterMock);
        ComponentFactories.AddStub<Modal>();

        //Act
        var cut = RenderComponent<LayoutManager>();
        var stubbedModal = cut.FindComponent<Stub<Modal>>();

        //Assert
        stubbedModal.Instance.Parameters.Get(_ => _.ModalType).ShouldBe(expectedModalType);
        stubbedModal.Instance.Parameters.Get(_ => _.ModalPrompt).ShouldBe(expectedModalModel.Prompt);
        stubbedModal.Instance.Parameters.Get(_ => _.ModalMessage).ShouldBe(expectedModalModel.Message);
    }
}
