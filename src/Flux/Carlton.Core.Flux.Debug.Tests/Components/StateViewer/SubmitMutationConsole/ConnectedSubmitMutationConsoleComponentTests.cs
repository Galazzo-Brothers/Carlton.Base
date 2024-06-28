using AutoFixture.Xunit2;
using Bunit;
using Bunit.TestDoubles;
using Carlton.Core.Flux.Debug.Components.StateViewer.SubmitMutationConsole;
using Carlton.Core.Flux.Debug.Models.Commands;
using Carlton.Core.Flux.Debug.Models.ViewModels;
using Carlton.Core.Flux.Debug.Tests.Common;
using Carlton.Core.Utilities.Random;
using Shouldly;
using Xunit;

namespace Carlton.Core.Flux.Debug.Tests.Components.StateViewer.SubmitMutationConsole;

public class ConnectedSubmitMutationConsoleComponentTests : TestContext
{
	[Theory, AutoData]
	public void ConnectedSubmitMutationConsole_Markup_RendersCorrectly(SubmitMutationConsoleViewModel expectedViewModel)
	{
		//Arrange
		ComponentFactories.AddStub<ViewStateSubmitMutationConsole>("<div>ViewStateSubmitMutationConsole Stub</div>");
		var expectedMarkup = @"<div>ViewStateSubmitMutationConsole Stub</div>";

		//Act
		var cut = RenderComponent<ConnectedSubmitMutationConsole>(parameters => parameters
			.Add(p => p.ViewModel, expectedViewModel));

		//Assert
		cut.MarkupMatches(expectedMarkup);
	}

	[Fact]
	public void ConnectedSubmitMutationConsole_ViewModelParameter_RendersCorrectly()
	{
		//Arrange
		ComponentFactories.AddStub<ViewStateSubmitMutationConsole>("<div>ViewStateSubmitMutationConsole Stub</div>");
		var expectedViewModel = new SubmitMutationConsoleViewModel
		{
			MutationCommandTypes = new List<Type> { typeof(TestCommand1), typeof(TestCommand2), typeof(TestCommand3) }
		};

		//Act
		var cut = RenderComponent<ConnectedSubmitMutationConsole>(parameters => parameters
			.Add(p => p.ViewModel, expectedViewModel));

		var stub = cut.FindComponent<Stub<ViewStateSubmitMutationConsole>>();

		//Assert
		stub.Instance.Parameters.Get(x => x.MutationTypes).ShouldBe(expectedViewModel.MutationCommandTypes);
	}

	[Fact]
	public void ConnectedSubmitMutationConsole_OnMutationSubmit_RaisesMutationCommand()
	{
		//Arrange
		ComponentFactories.AddStub<ViewStateSubmitMutationConsole>("<div>ViewStateSubmitMutationConsole Stub</div>");
		var eventFired = false;
		var eventObject = (object)null;
		var expectedViewModel = new SubmitMutationConsoleViewModel
		{
			MutationCommandTypes = new List<Type> { typeof(TestCommand1), typeof(TestCommand2), typeof(TestCommand3) }
		};
		var selectedIndex = RandomUtilities.GetRandomIndex(3);

		var cut = RenderComponent<ConnectedSubmitMutationConsole>(parameters => parameters
			.Add(p => p.ViewModel, expectedViewModel)
			.Add(p => p.OnComponentEvent, args =>
			{
				eventFired = true;
				eventObject = args;
			}));

		var stub = cut.FindComponent<Stub<ViewStateSubmitMutationConsole>>();

		//Act
		cut.InvokeAsync(() => stub.Instance.Parameters.Get(x => x.OnMutationSubmit)
			.InvokeAsync(new SubmitMutationArgs(new object())));

		//Assert
		eventFired.ShouldBeTrue();
		eventObject.ShouldBeOfType<SubmitMutationCommand>();
	}
}
