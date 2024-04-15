using Carlton.Core.LayoutServices.FullScreen;
using Carlton.Core.LayoutServices.Modals;
using Carlton.Core.LayoutServices.Panel;
using Carlton.Core.LayoutServices.Theme;
using Carlton.Core.Layouts.Manager;
using Carlton.Core.LayoutServices.Toasts;
using Carlton.Core.Layouts.Modals;
using Bunit.TestDoubles;
using Carlton.Core.Components.Modals;
namespace Carlton.Core.Layouts.Tests.Manager;


[Trait("Component", nameof(LayoutManager))]
public class LayoutManagerComponentTests : TestContext
{
	private readonly IFullScreenState _navStateMock = Substitute.For<IFullScreenState>();
	private readonly IThemeState _themeStateMock = Substitute.For<IThemeState>();
	private readonly IModalState _modalStateMock = Substitute.For<IModalState>();
	private readonly IToastState _toastStateMock = Substitute.For<IToastState>();
	private readonly IPanelState _panelStateMock = Substitute.For<IPanelState>();

	//Arrange
	public LayoutManagerComponentTests()
	{
		//Mocks
		_navStateMock = Substitute.For<IFullScreenState>();
		_themeStateMock = Substitute.For<IThemeState>();
		_modalStateMock = Substitute.For<IModalState>();
		_toastStateMock = Substitute.For<IToastState>();
		_panelStateMock = Substitute.For<IPanelState>();

		//Container Registrations
		Services.AddSingleton(_navStateMock);
		Services.AddSingleton(_themeStateMock);
		Services.AddSingleton(_modalStateMock);
		Services.AddSingleton(_toastStateMock);
		Services.AddSingleton(_panelStateMock);

		//Component Stubs
		var layoutToasterMock = Substitute.For<LayoutToaster>();
		ComponentFactories.Add(layoutToasterMock);
		ComponentFactories.AddStub<Modal>();
	}

	[Theory(DisplayName = "Markup Test")]
	[InlineData(Themes.light)]
	[InlineData(Themes.dark)]
	public void LayoutManager_Markup_RendersCorrectly(
		Themes expectedTheme)
	{
		//Arrange
		_themeStateMock.Theme.Returns(expectedTheme);
		var expectedMarkup = $@"<div class=""layout-manager"" data-theme=""{expectedTheme}"" ></div>";

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
		var cut = RenderComponent<LayoutManager>();

		//Act
		var args = new ToastRaisedEventArgs(expectedToastViewModel);
		cut.InvokeAsync(() => _toastStateMock.ToastAdded += Raise.Event<EventHandler<ToastRaisedEventArgs>>(new object(), args));

		//Assert
		cut.RenderCount.ShouldBe(2); //second render from the event triggering a stateHasChanged call
	}

	[Theory(DisplayName = "LayoutChanged Event Test"), AutoData]
	public void LayoutManager_LayoutChanged_FiresEvent(
		bool expectedIsCollapsed)
	{
		//Arrange
		var cut = RenderComponent<LayoutManager>();

		//Act
		var args = new FullScreenStateChangedEventArgs(expectedIsCollapsed);
		cut.InvokeAsync(() => _navStateMock.FullScreenStateChanged += Raise.Event<EventHandler<FullScreenStateChangedEventArgs>>(new object(), args));

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
		_themeStateMock.Theme.Returns(expectedTheme);
		var cut = RenderComponent<LayoutManager>();

		//Act
		var args = new ThemeChangedEventArgs(expectedTheme);
		cut.InvokeAsync(() => _themeStateMock.ThemeChanged += Raise.Event<EventHandler<ThemeChangedEventArgs>>(new object(), args));
		var element = cut.Find(".layout-manager");

		//Assert
		cut.RenderCount.ShouldBe(2); //second render from the event triggering a stateHasChanged call
		element.Attributes["data-theme"].Value.ShouldBe(expectedTheme.ToString());
	}

	[Theory(DisplayName = "ModalState Changed Event Test"), AutoData]
	public void LayoutManager_ModalStateChanged_FiresEvent(
		bool expectedIsVisible,
		string expectedModalType,
		ModalViewModel expectedModalModel)
	{
		//Arrange
		var cut = RenderComponent<LayoutManager>();

		//Act
		var args = new ModalStateChangedEventArgs
		{
			IsVisible = expectedIsVisible,
			ModalType = expectedModalType,
			Model = expectedModalModel
		};
		cut.InvokeAsync(() => _modalStateMock.ModalStateChanged += Raise.Event<EventHandler<ModalStateChangedEventArgs>>(new object(), args));

		//Assert
		cut.RenderCount.ShouldBe(2); //second render from the event triggering a stateHasChanged call
	}

	[Theory(DisplayName = "PanelVisibility Changed Event Test")]
	[InlineAutoData(true)]
	[InlineAutoData(false)]
	public void LayoutManager_PanelStateChanged_FiresEvent(
	   bool expectedIsVisible)
	{
		//Arrange
		var cut = RenderComponent<LayoutManager>();

		//Act
		var args = new PanelVisibilityChangedEventArgs(expectedIsVisible);
		cut.InvokeAsync(() => _panelStateMock.PanelVisibilityChangedChanged += Raise.Event<EventHandler<PanelVisibilityChangedEventArgs>>(new object(), args));

		//Assert
		cut.RenderCount.ShouldBe(2); //second render from the event triggering a stateHasChanged call
	}

	[Theory(DisplayName = "ToastRaised Event Test"), AutoData]
	public void LayoutManager_ToastRaised_FiresEvent(
		ModalTypes expectedModalType,
		ModalViewModel expectedModalModel)
	{
		//Arrange
		_modalStateMock.ModalType.Returns(expectedModalType.ToString());
		_modalStateMock.Model.Returns(expectedModalModel);

		//Act
		var cut = RenderComponent<LayoutManager>();
		var stubbedModal = cut.FindComponent<Stub<Modal>>();

		//Assert
		stubbedModal.Instance.Parameters.Get(x => x.ModalType).ShouldBe(expectedModalType);
		stubbedModal.Instance.Parameters.Get(x => x.ModalPrompt).ShouldBe(expectedModalModel.Prompt);
		stubbedModal.Instance.Parameters.Get(x => x.ModalMessage).ShouldBe(expectedModalModel.Message);
	}
}
