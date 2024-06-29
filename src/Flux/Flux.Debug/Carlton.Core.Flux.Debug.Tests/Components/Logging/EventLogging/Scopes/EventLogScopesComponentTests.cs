using AutoFixture.Xunit2;
using Bunit;
using Bunit.TestDoubles;
using Carlton.Core.Flux.Debug.Components.Logging.EventLogging.Scopes;
using Carlton.Core.Utilities.Logging;
using Shouldly;
using System.Reflection;
using Xunit;
using Consoles = Carlton.Core.Components.Consoles;
namespace Carlton.Core.Flux.Debug.Tests.Components.Logging.EventLogging.Scopes;

public class EventLogScopesComponentTests : TestContext
{
	[Theory, AutoData]
	public void EventLogScopes_Markup_RendersCorrectly(LogMessage expectedLogMessage)
	{
		//Arrange
		var expectedMarkup = @"<div class=""log-details""><div>Console Stub</div></div>";
		ComponentFactories.AddStub<Consoles.Console>(@"<div>Console Stub</div>");

		//Act
		var cut = RenderComponent<EventLogScopes>(parameters => parameters
			.Add(p => p.LogMessage, expectedLogMessage));

		//Assert
		cut.MarkupMatches(expectedMarkup);
	}

	[Theory, AutoData]
	public void EventLogScopes_LogMessageParameter_RendersCorrectly(LogMessage expectedLogMessage)
	{
		//Arrange
		ComponentFactories.AddStub<Consoles.Console>(@"<div class=""log-details""><div>Console Stub</div></div>");

		//Act
		var cut = RenderComponent<EventLogScopes>(parameters => parameters
			.Add(p => p.LogMessage, expectedLogMessage));
		var stub = cut.FindComponent<Stub<Consoles.Console>>();
		var expectedScopes = typeof(EventLogScopes).GetMethod("ScopeText", BindingFlags.Instance | BindingFlags.NonPublic)
			.Invoke(cut.Instance, []);

		//Assert
		stub.Instance.Parameters.Get(x => x.Text).ShouldBe(expectedScopes);
	}
}
