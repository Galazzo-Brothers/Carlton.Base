using AutoFixture.Xunit2;
using Bunit;
using Carlton.Core.Components.Dropdowns;
using Carlton.Core.Components.Modals;
using Carlton.Core.Flux.Debug.Components;
using Carlton.Core.Flux.Debug.Components.Header;
using Carlton.Core.Flux.Debug.Models.Commands;
using Carlton.Core.Flux.Debug.Models.ViewModels;
using Carlton.Core.LayoutServices.Modals;
using Carlton.Core.LayoutServices.Theme;
using Carlton.Core.LayoutServices.Toasts;
using Carlton.Core.Utilities.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Shouldly;
using System.Reflection;
using Xunit;
namespace Carlton.Core.Flux.Debug.Tests.Components.Header;

public class ConnectedHeaderActionsComponentTests : TestContext
{
	private readonly MemoryLogger _memoryLogger;
	private readonly IToastState _toastState;
	private readonly IModalState _modalState;
	private readonly IThemeState _themeState;

	public ConnectedHeaderActionsComponentTests()
	{
		_memoryLogger = (MemoryLogger)new MemoryLoggerProvider().CreateLogger("Testing");
		_toastState = Substitute.For<IToastState>();
		_modalState = Substitute.For<IModalState>();
		_themeState = Substitute.For<IThemeState>();
		Services.AddSingleton(_memoryLogger);
		Services.AddSingleton(_toastState);
		Services.AddSingleton(_modalState);
		Services.AddSingleton(_themeState);
	}

	[Theory, AutoData]
	public void ConnectedHeaderActions_ClickClearLocalStorage_ShouldCallModalState(HeaderActionsViewModel expectedViewModel)
	{
		//Arrange
		ComponentFactories.AddStub<FluxDebugHeaderActions>("<div>FluxDebugHeaderActions Stub</div>");

		//Act
		var cut = RenderComponent<ConnectedHeaderActions>(parameters => parameters
			.Add(p => p.ViewModel, expectedViewModel));

		//Assert
		cut.MarkupMatches("<div>FluxDebugHeaderActions Stub</div>");
	}

	[Theory, AutoData]
	public void ConnectedHeaderActions_ClearLocalStorage_ShouldRaiseEventAndClearMemoryLogger(HeaderActionsViewModel expectedViewModel)
	{
		//Arrange
		var cut = RenderComponent<ConnectedHeaderActions>(parameters => parameters
			.Add(p => p.ViewModel, expectedViewModel));
		var dropdown = cut.FindComponent<ProfileAvatarDropdown>();

		//Act
		dropdown.Instance.DropdownMenuItems.ElementAt(2).MenuItemSelected.Invoke();

		//Assert
		_modalState.Received().RaiseModal(
			Arg.Is<string>(
				x => x == ModalTypes.ConfirmationModal.ToString()),
			Arg.Is<ModalViewModel>(x =>
				x.Prompt == "Are you sure" &&
				x.Message == "Are you sure you want to clear the local storage logs?"));
	}

	[Theory, AutoData]
	public void ConnectedHeaderActions_ClickClearLocalStorage_ShouldRaiseEventAndClearMemoryLogger(HeaderActionsViewModel expectedViewModel)
	{
		//Arrange
		_memoryLogger.LogDebug("Testing 123");
		var eventFired = false;
		var eventArgs = (object)null;
		var cut = RenderComponent<ConnectedHeaderActions>(parameters => parameters
			.Add(p => p.ViewModel, expectedViewModel)
			.Add(p => p.OnComponentEvent, args =>
			{
				eventFired = true;
				eventArgs = args;
			}));

		//Act
		var args = new ModalCloseEventArgs(true);
		typeof(ConnectedHeaderActions).GetMethod("ClearLocalStorageConfirmed", BindingFlags.Instance | BindingFlags.NonPublic)
			.Invoke(cut.Instance, [args]);

		//Assert
		_memoryLogger.GetLogMessages().ShouldBeEmpty();
		eventFired.ShouldBeTrue();
		eventArgs.ShouldBeOfType<ClearLogsCommand>();
	}
}
