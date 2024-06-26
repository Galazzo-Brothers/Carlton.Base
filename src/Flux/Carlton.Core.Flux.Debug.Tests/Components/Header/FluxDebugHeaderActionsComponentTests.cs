using AutoFixture.Xunit2;
using Bunit;
using Bunit.TestDoubles;
using Carlton.Core.Components.Dropdowns;
using Carlton.Core.Flux.Debug.Components.Header;
using Carlton.Core.LayoutServices.Theme;
using Carlton.Core.LayoutServices.Toasts;
using Shouldly;
using Xunit;
namespace Carlton.Core.Flux.Debug.Tests.Components.Header;

public class FluxDebugHeaderActionsComponentTests : TestContext
{
	[Theory, AutoData]
	public void FluxDebugHeaderActions_Markup_RendersCorrectly(
		string userName,
		string avatarUrl)
	{
		//Arrange
		ComponentFactories.AddStub<ThemeMenu>("<div>ThemeMenu Stub</div>");
		ComponentFactories.AddStub<NotificationMenu>("<div>NotificationMenu Stub</div>");
		ComponentFactories.AddStub<FluxDebugReturnMenu>("<div>FluxDebugReturnMenu Stub</div>");
		ComponentFactories.AddStub<ProfileAvatarDropdown>("<div>ProfileAvatarDropdown Stub</div>");
		var expectedMarkup = @$"
		<div class=""flux-debug-header-actions"" >
			<div>ThemeMenu Stub</div>
			<div>NotificationMenu Stub</div>
			<div>FluxDebugReturnMenu Stub</div>
			<div>ProfileAvatarDropdown Stub</div>
		</div>";

		//Act
		var cut = RenderComponent<FluxDebugHeaderActions>(parameters => parameters
			.Add(p => p.UserName, userName)
			.Add(p => p.AvatarUrl, avatarUrl));

		//Assert
		cut.MarkupMatches(expectedMarkup);
	}

	[Theory, AutoData]
	public void FluxDebugHeaderActions_Parameters_RendersCorrectly(
		string expectedUserName,
		string expectedAvatarUrl,
		IEnumerable<DropdownMenuItem<int>> expectedDropdownMenuItems)
	{
		//Arrange
		ComponentFactories.AddStub<ThemeMenu>("<div>ThemeMenu Stub</div>");
		ComponentFactories.AddStub<NotificationMenu>("<div>NotificationMenu Stub</div>");
		ComponentFactories.AddStub<FluxDebugReturnMenu>("<div>FluxDebugReturnMenu Stub</div>");
		ComponentFactories.AddStub<ProfileAvatarDropdown>("<div>ProfileAvatarDropdown Stub</div>");

		//Act
		var cut = RenderComponent<FluxDebugHeaderActions>(parameters => parameters
			.Add(p => p.UserName, expectedUserName)
			.Add(p => p.AvatarUrl, expectedAvatarUrl)
			.Add(p => p.DropdownMenuItems, expectedDropdownMenuItems));

		var stub = cut.FindComponent<Stub<ProfileAvatarDropdown>>();

		//Assert
		stub.Instance.Parameters.Get(x => x.Username).ShouldBe(expectedUserName);
		stub.Instance.Parameters.Get(x => x.AvatarImgUrl).ShouldBe(expectedAvatarUrl);
		stub.Instance.Parameters.Get(x => x.DropdownMenuItems).ShouldBe(expectedDropdownMenuItems);
	}
}