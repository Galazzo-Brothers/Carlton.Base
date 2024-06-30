using AutoFixture.Xunit2;
using Bunit;
using Bunit.TestDoubles;
using Carlton.Core.Components.Buttons;
using Carlton.Core.Components.Consoles;
using Carlton.Core.Components.Dropdowns;
using Carlton.Core.Flux.Debug.Components.StateViewer.SubmitMutationConsole;
using Carlton.Core.Flux.Debug.Tests.Common;
using Carlton.Core.Utilities.Extensions;
using Carlton.Core.Utilities.Random;
using Shouldly;
using System.Reflection;
using Xunit;
using Console = Carlton.Core.Flux.Debug.Components.StateViewer.SubmitMutationConsole;
namespace Carlton.Core.Flux.Debug.Tests.Components.StateViewer.SubmitMutationConsole;

public class SubmitMutationConsoleComponentTests : TestContext
{
	[Fact]
	public void SubmitMutationConsole_Markup_RendersCorrectly()
	{
		//Arrange
		ComponentFactories.AddStub<Dropdown<Type>>("<div>Dropdown Stub</div>");
		ComponentFactories.AddStub<JsonViewerConsole>("<div>JsonViewer Stub</div>");
		ComponentFactories.AddStub<IconButton>("<div>IconButton Stub</div>");
		var expectedMutationTypes = new List<Type> { typeof(TestCommand1), typeof(TestCommand2), typeof(TestCommand3) };
		var expectedSelectedIndex = RandomUtilities.GetRandomIndex(expectedMutationTypes.Count());
		var expectedMarkup = @"
		<div class=""new-mutation-console"" >
			<div class=""mutation-type-selection"" >
				<span class=""mutation-type-label"" >Mutation Command:</span>
			<div>Dropdown Stub</div>
		</div>
		<div>JsonViewer Stub</div>
			<div class=""action-btn"" >
				<div>IconButton Stub</div>
			</div>
		</div>";

		//Act
		var cut = RenderComponent<Console.SubmitMutationConsole>(parameters => parameters
			.Add(p => p.MutationTypes, expectedMutationTypes)
			.Add(p => p.SelectedIndex, expectedSelectedIndex)
		);

		//Assert
		cut.MarkupMatches(expectedMarkup);
	}

	[Fact]
	public void SubmitMutationConsole_MutationTypesParameter_RendersCorrectly()
	{
		//Arrange
		ComponentFactories.AddStub<Dropdown<Type>>("<div>Dropdown Stub</div>");
		ComponentFactories.AddStub<JsonViewerConsole>("<div>JsonViewer Stub</div>");
		ComponentFactories.AddStub<IconButton>("<div>IconButton Stub</div>");
		var expectedMutationTypes = new List<Type> { typeof(TestCommand1), typeof(TestCommand2), typeof(TestCommand3) };
		var expectedSelectedIndex = RandomUtilities.GetRandomIndex(expectedMutationTypes.Count());

		//Act
		var cut = RenderComponent<Console.SubmitMutationConsole>(parameters => parameters
			.Add(p => p.MutationTypes, expectedMutationTypes)
			.Add(p => p.SelectedIndex, expectedSelectedIndex)
		);

		var stub = cut.FindComponent<Stub<Dropdown<Type>>>();

		//Assert
		stub.Instance.Parameters
					 .Get(x => x.Options)
					 .ShouldBe(expectedMutationTypes.ToDictionary(x => x.GetDisplayName(), x => x));
	}

	[Theory]
	[InlineAutoData(0)]
	[InlineAutoData(1)]
	[InlineAutoData(2)]

	public void SubmitMutationConsole_SelectedIndexParameter_RendersCorrectly(int expectedSelectedIndex)
	{
		//Arrange
		ComponentFactories.AddStub<Dropdown<Type>>("<div>Dropdown Stub</div>");
		ComponentFactories.AddStub<JsonViewerConsole>("<div>JsonViewer Stub</div>");
		ComponentFactories.AddStub<IconButton>("<div>IconButton Stub</div>");
		var expectedMutationTypes = new List<Type> { typeof(TestCommand1), typeof(TestCommand2), typeof(TestCommand3) };

		//Act
		var cut = RenderComponent<Console.SubmitMutationConsole>(parameters => parameters
			.Add(p => p.MutationTypes, expectedMutationTypes)
			.Add(p => p.SelectedIndex, expectedSelectedIndex)
		);

		var mutationCommand = typeof(Console.SubmitMutationConsole)
								.GetProperty("NewMutationCommand", BindingFlags.Instance | BindingFlags.NonPublic)
								.GetValue(cut.Instance);

		//Assert
		mutationCommand.ShouldBeOfType(expectedMutationTypes.ElementAt(expectedSelectedIndex));
	}

	[Theory]
	[InlineAutoData(0)]
	[InlineAutoData(1)]
	[InlineAutoData(2)]
	public void SubmitMutationConsole_OnMutationSelectionChangeParameter_ShouldFireEvent(int expectedSelectedIndex)
	{
		//Arrange
		var eventFired = false;
		ComponentFactories.AddStub<Dropdown<Type>>("<div>Dropdown Stub</div>");
		ComponentFactories.AddStub<JsonViewerConsole>("<div>JsonViewer Stub</div>");
		ComponentFactories.AddStub<IconButton>("<div>IconButton Stub</div>");
		var expectedMutationTypes = new List<Type> { typeof(TestCommand1), typeof(TestCommand2), typeof(TestCommand3) };
		var actualSelectedIndex = -1;

		var cut = RenderComponent<Console.SubmitMutationConsole>(parameters => parameters
			.Add(p => p.MutationTypes, expectedMutationTypes)
			.Add(p => p.SelectedIndex, 0)
			.Add(p => p.OnMutationSelectionChange, args =>
			{
				eventFired = true;
				actualSelectedIndex = args;
			})
		);

		var stub = cut.FindComponent<Stub<Dropdown<Type>>>();

		//Act
		cut.InvokeAsync(() => stub.Instance
			.Parameters.Get(x => x.OnValueChange)
			.InvokeAsync(new ValueChangeArgs<Type>
			{
				SelectedIndex = expectedSelectedIndex,
				SelectedKey = expectedMutationTypes.ElementAt(expectedSelectedIndex).GetDisplayName(),
				SelectedValue = expectedMutationTypes.ElementAt(expectedSelectedIndex)
			}));

		//Assert
		eventFired.ShouldBeTrue();
		actualSelectedIndex.ShouldBe(expectedSelectedIndex);
	}

	[Theory]
	[InlineAutoData(0)]
	[InlineAutoData(1)]
	[InlineAutoData(2)]
	public void SubmitMutationConsole_OnMutationSubmitParameter_ShouldFireEvent(int expectedSelectedIndex)
	{
		//Arrange
		var eventFired = false;
		ComponentFactories.AddStub<Dropdown<Type>>("<div>Dropdown Stub</div>");
		ComponentFactories.AddStub<JsonViewerConsole>("<div>JsonViewer Stub</div>");
		ComponentFactories.AddStub<IconButton>("<div>IconButton Stub</div>");
		var expectedMutationTypes = new List<Type> { typeof(TestCommand1), typeof(TestCommand2), typeof(TestCommand3) };
		var actualArgs = (SubmitMutationArgs)null;

		var cut = RenderComponent<Console.SubmitMutationConsole>(parameters => parameters
			.Add(p => p.MutationTypes, expectedMutationTypes)
			.Add(p => p.SelectedIndex, expectedSelectedIndex)
			.Add(p => p.OnMutationSubmit, args =>
			{
				eventFired = true;
				actualArgs = args;
			})
		);

		var stub = cut.FindComponent<Stub<IconButton>>();

		//Act
		cut.InvokeAsync(() => stub.Instance
			.Parameters.Get(x => x.OnClick)
			.InvokeAsync());

		//Assert
		eventFired.ShouldBeTrue();
		actualArgs.MutationCommand.ShouldBeOfType(expectedMutationTypes.ElementAt(expectedSelectedIndex));
	}
}

