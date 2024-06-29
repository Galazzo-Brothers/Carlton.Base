using AutoFixture.Xunit2;
using Bunit;
using Bunit.TestDoubles;
using Carlton.Core.Flux.Debug.Components;
using Carlton.Core.Flux.Debug.Components.Logging.EventLogging.Scopes;
using Carlton.Core.Flux.Debug.Models.ViewModels;
using Shouldly;
using Xunit;

namespace Carlton.Core.Flux.Debug.Tests.Components.Logging.EventLogging.Scopes;

public class ConnectedEventLogScopesComponentTests : TestContext
{
	[Theory, AutoData]
	public void ConnectedEventLogScopes_Markup_RendersCorrectly(EventLogScopesViewModel expectedViewModel)
	{
		//Arrange
		var expectedMarkup = @"<div>EventLogDetails Stub</div>";
		ComponentFactories.AddStub<EventLogScopes>(@"<div>EventLogDetails Stub</div>");

		//Act
		var cut = RenderComponent<ConnectedEventLogScopes>(parameters => parameters
			.Add(p => p.ViewModel, expectedViewModel));

		//Assert
		cut.MarkupMatches(expectedMarkup);
	}

	[Theory, AutoData]
	public void EventLogScopes_LogMessageParameter_RendersCorrectly(EventLogScopesViewModel expectedViewModel)
	{
		//Arrange
		ComponentFactories.AddStub<EventLogScopes>(@"<div>EventLogDetails Stub</div>");

		//Act
		var cut = RenderComponent<ConnectedEventLogScopes>(parameters => parameters
			.Add(p => p.ViewModel, expectedViewModel));
		var stub = cut.FindComponent<Stub<EventLogScopes>>();

		//Assert
		stub.Instance.Parameters.Get(x => x.LogMessage).ShouldBe(expectedViewModel.SelectedLogMessage);
	}
}
