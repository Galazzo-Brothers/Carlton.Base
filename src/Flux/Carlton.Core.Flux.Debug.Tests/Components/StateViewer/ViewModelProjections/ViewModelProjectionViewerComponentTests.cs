using AutoFixture.Xunit2;
using Bunit;
using Bunit.TestDoubles;
using Carlton.Core.Components.Consoles;
using Carlton.Core.Components.Dropdowns;
using Carlton.Core.Flux.Contracts;
using Carlton.Core.Flux.Debug.Components.StateViewer.ViewModelProjections;
using Carlton.Core.Flux.Debug.Tests.Common;
using Carlton.Core.Utilities.Extensions;
using Carlton.Core.Utilities.Random;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;
namespace Carlton.Core.Flux.Debug.Tests.Components.StateViewer.ViewModelProjections;

public class ViewModelProjectionViewerComponentTests : TestContext
{
	[Theory, AutoData]
	public void ViewModelProjectionViewer_Markup_RendersCorrectly(
		TestState expectedState)
	{
		//Arrange
		Services.AddSingleton<IViewModelProjectionMapper<TestState>, TestStateViewModelProjectionMapper>();
		ComponentFactories.AddStub<Dropdown<Type>>("<div>Dropdown Stub</div>");
		ComponentFactories.AddStub<JsonViewerConsole>("<div>JsonViewerConsole Stub</div>");

		var expectedTypes = new List<Type> { typeof(TestViewModel1), typeof(TestViewModel2), typeof(TestViewModel3) };
		var expectedMarkup = @"
		<div class=""view-model-projection-viewer"">
			<div class=""view-model-selection"">
				<span class=""view-model-label"">View Model:</span>
				<div>Dropdown Stub</div>
			</div>
			<div>JsonViewerConsole Stub</div>
		</div>";

		//Act
		var cut = RenderComponent<ViewModelProjectionViewer>(parameters => parameters
			.Add(p => p.State, expectedState)
			.Add(p => p.ViewModelTypes, expectedTypes));

		//Assert
		cut.MarkupMatches(expectedMarkup);
	}

	[Theory, AutoData]
	public void ViewModelProjectionViewer_StateParameter_RendersCorrectly(
		TestState expectedState)
	{
		//Arrange
		Services.AddSingleton<IViewModelProjectionMapper<TestState>, TestStateViewModelProjectionMapper>();
		ComponentFactories.AddStub<Dropdown<Type>>("<div>Dropdown Stub</div>");
		ComponentFactories.AddStub<JsonViewerConsole>("<div>JsonViewerConsole Stub</div>");
		var expectedTypes = new List<Type> { typeof(TestViewModel1), typeof(TestViewModel2), typeof(TestViewModel3) };

		//Assert
		var cut = RenderComponent<ViewModelProjectionViewer>(parameters => parameters
			.Add(p => p.State, expectedState)
			.Add(p => p.ViewModelTypes, expectedTypes));
	}

	[Theory, AutoData]
	public void ViewModelProjectionViewer_ViewModelTypesParameter_RendersCorrectly(
		TestState expectedState)
	{
		//Arrange
		Services.AddSingleton<IViewModelProjectionMapper<TestState>, TestStateViewModelProjectionMapper>();
		ComponentFactories.AddStub<Dropdown<Type>>("<div>Dropdown Stub</div>");
		ComponentFactories.AddStub<JsonViewerConsole>("<div>JsonViewerConsole Stub</div>");
		var expectedTypes = new List<Type> { typeof(TestViewModel1), typeof(TestViewModel2), typeof(TestViewModel3) };

		//Act
		var cut = RenderComponent<ViewModelProjectionViewer>(parameters => parameters
			.Add(p => p.State, expectedState)
			.Add(p => p.ViewModelTypes, expectedTypes));

		var stub = cut.FindComponent<Stub<Dropdown<Type>>>();

		//Assert
		stub.Instance.Parameters.Get(x => x.Options).Select(x => x.Value).ShouldBe(expectedTypes);
	}

	[Theory, AutoData]
	public void ViewModelProjectionViewer_SelectedIndexParameter_RendersCorrectly(
		TestState expectedState)
	{
		//Arrange
		Services.AddSingleton<IViewModelProjectionMapper<TestState>, TestStateViewModelProjectionMapper>();
		ComponentFactories.AddStub<Dropdown<Type>>("<div>Dropdown Stub</div>");
		ComponentFactories.AddStub<JsonViewerConsole>("<div>JsonViewerConsole Stub</div>");
		var expectedTypes = new List<Type> { typeof(TestViewModel1), typeof(TestViewModel2), typeof(TestViewModel3) };
		var expectedIndex = RandomUtilities.GetRandomIndex(expectedTypes.Count);

		//Act
		var cut = RenderComponent<ViewModelProjectionViewer>(parameters => parameters
			.Add(p => p.State, expectedState)
			.Add(p => p.ViewModelTypes, expectedTypes)
			.Add(p => p.SelectedIndex, expectedIndex));

		var stub = cut.FindComponent<Stub<Dropdown<Type>>>();

		//Assert
		stub.Instance.Parameters.Get(x => x.InitialSelectedIndex).ShouldBe(expectedIndex);
	}

	[Theory, AutoData]
	public void ViewModelProjectionViewer_OnViewModelChangedParameter_FiresEvent(
		TestState expectedState)
	{
		//Arrange
		var eventFired = true;
		var eventIndex = -1;
		Services.AddSingleton<IViewModelProjectionMapper<TestState>, TestStateViewModelProjectionMapper>();
		ComponentFactories.AddStub<Dropdown<Type>>("<div>Dropdown Stub</div>");
		ComponentFactories.AddStub<JsonViewerConsole>("<div>JsonViewerConsole Stub</div>");
		var expectedTypes = new List<Type> { typeof(TestViewModel1), typeof(TestViewModel2), typeof(TestViewModel3) };
		var expectedIndex = RandomUtilities.GetRandomIndex(expectedTypes.Count);

		var cut = RenderComponent<ViewModelProjectionViewer>(parameters => parameters
			.Add(p => p.State, expectedState)
			.Add(p => p.ViewModelTypes, expectedTypes)
			.Add(p => p.SelectedIndex, expectedIndex)
			.Add(p => p.OnViewModelChange, args =>
			{
				eventFired = true;
				eventIndex = args;
			}));

		var stub = cut.FindComponent<Stub<Dropdown<Type>>>();

		//Act
		cut.InvokeAsync(() => stub.Instance.Parameters.Get(x => x.OnValueChange).InvokeAsync(new ValueChangeArgs<Type>
		{
			SelectedIndex = expectedIndex,
			SelectedKey = expectedTypes.ElementAt(expectedIndex).GetDisplayName(),
			SelectedValue = expectedTypes.ElementAt(expectedIndex)
		}));

		//Assert
		eventFired.ShouldBeTrue();
		eventIndex.ShouldBe(expectedIndex);
	}
}
