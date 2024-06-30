using AutoFixture.Xunit2;
using Bunit;
using Bunit.TestDoubles;
using Carlton.Core.Flux.Debug.Components;
using Carlton.Core.Flux.Debug.Components.Logging.TraceLogging.RequestObjectDetails;
using Carlton.Core.Flux.Debug.Models.ViewModels;
using Shouldly;
using Xunit;
namespace Carlton.Core.Flux.Debug.Tests.Components.Logging.TraceLogging.RequestObjectDetails;

public class ConnectedTraceLogRequestObjectDetailsComponentTests : TestContext
{
	[Theory, AutoData]
	public void ConnectedTraceLogRequestObjectDetails_Markup_RendersCorrectly(TraceLogRequestObjectDetailsViewModel expectedViewModel)
	{
		//Arrange
		ComponentFactories.AddStub<TraceLogRequestObjectDetails>("<div>TraceLogRequestObjectDetails Stub</div>");
		var expectedMarkup = @"<div>TraceLogRequestObjectDetails Stub</div>";

		//Act
		var cut = RenderComponent<ConnectedTraceLogRequestObjectDetails>(parameters => parameters
			.Add(p => p.ViewModel, expectedViewModel));

		//Assert
		cut.MarkupMatches(expectedMarkup);
	}

	[Theory, AutoData]
	public void ConnectedTraceLogRequestObjectDetails_ViewModelParameter_RendersCorrectly(TraceLogRequestObjectDetailsViewModel expectedViewModel)
	{
		//Arrange
		ComponentFactories.AddStub<TraceLogRequestObjectDetails>("<div>TraceLogRequestObjectDetails Stub</div>");

		//Act
		var cut = RenderComponent<ConnectedTraceLogRequestObjectDetails>(parameters => parameters
			.Add(p => p.ViewModel, expectedViewModel));

		var stub = cut.FindComponent<Stub<TraceLogRequestObjectDetails>>();

		//Assert
		stub.Instance.Parameters.Get(x => x.SelectedRequestObject).ShouldBeSameAs(expectedViewModel.SelectedRequestObject);
	}
}
