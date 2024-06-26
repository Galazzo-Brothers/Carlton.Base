using AutoFixture.Xunit2;
using Bunit;
using Shouldly;
using Xunit;
using Carlton.Core.Flux.Debug.Components;
using Carlton.Core.Flux.Debug.Models.ViewModels;
using Carlton.Core.Flux.Debug.Components.Logging.EventLogging.Details;
using Bunit.TestDoubles;

namespace Carlton.Core.Flux.Debug.Tests.Components.Logging.EventLogging.Details;

public class ConnectedEventLogDetailsComponentTests : TestContext
{
	[Theory, AutoData]
	public void ConnectedEventLogDetails_Markup_RendersCorrectly(EventLogDetailsViewModel expectedViewModel)
	{
		//Arrange
		ComponentFactories.AddStub<EventLogDetails>("<div>EventLogDetails Stub</div>");

		//Act
		var cut = RenderComponent<ConnectedEventLogDetails>(parameters => parameters
			.Add(p => p.ViewModel, expectedViewModel));

		//Assert
		cut.MarkupMatches("<div>EventLogDetails Stub</div>");
	}

	[Theory, AutoData]
	public void ConnectedEventLogDetails_ViewModelParameter_RendersCorrectly(EventLogDetailsViewModel expectedViewModel)
	{
		//Arrange
		ComponentFactories.AddStub<EventLogDetails>("<div>EventLogDetails Stub</div>");

		//Act
		var cut = RenderComponent<ConnectedEventLogDetails>(parameters => parameters
			.Add(p => p.ViewModel, expectedViewModel));
		var stub = cut.FindComponent<Stub<EventLogDetails>>();

		//Assert
		stub.Instance.Parameters.Get(x => x.LogMessage).ShouldBe(expectedViewModel.SelectedLogMessage);
	}
}
