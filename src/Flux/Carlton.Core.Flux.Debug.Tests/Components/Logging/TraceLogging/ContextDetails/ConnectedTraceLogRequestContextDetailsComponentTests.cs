using AutoFixture.Xunit2;
using Bunit;
using Bunit.TestDoubles;
using Carlton.Core.Flux.Debug.Components;
using Carlton.Core.Flux.Debug.Components.Logging.TraceLogging.ContextDetails;
using Carlton.Core.Flux.Debug.Models.ViewModels;
using Shouldly;
using Xunit;
namespace Carlton.Core.Flux.Debug.Tests.Components.Logging.TraceLogging.ContextDetails;

public class ConnectedTraceLogRequestContextDetailsComponentTests : TestContext
{
	[Theory, AutoData]
	public void ConnectedTraceLogRequestContextDetails_Markup_RendersCorrectly(TraceLogRequestContextDetailsViewModel expectedViewModel)
	{
		//Arrange
		ComponentFactories.AddStub<TraceLogRequestContextDetails>("<div>TraceLogRequestContextDetails Stub</div>");
		var expectedMarkup = @"<div>TraceLogRequestContextDetails Stub</div>";

		//Act
		var cut = RenderComponent<ConnectedTraceLogRequestContextDetails>(parameters => parameters
			.Add(p => p.ViewModel, expectedViewModel));

		//Assert
		cut.MarkupMatches(expectedMarkup);
	}

	[Theory, AutoData]
	public void TraceLogRequestContextDetails_ViewModelParameter_RendersCorrectly(TraceLogRequestContextDetailsViewModel expectedViewModel)
	{
		//Arrange
		ComponentFactories.AddStub<TraceLogRequestContextDetails>("<div>JsonViewerConsole Stub</div>");

		//Act
		var cut = RenderComponent<ConnectedTraceLogRequestContextDetails>(parameters => parameters
			.Add(p => p.ViewModel, expectedViewModel));
		var stub = cut.FindComponent<Stub<TraceLogRequestContextDetails>>();

		//Assert
		stub.Instance.Parameters.Get(x => x.SelectedRequestContext).ShouldBe(expectedViewModel.SelectedRequestContext);
	}
}
