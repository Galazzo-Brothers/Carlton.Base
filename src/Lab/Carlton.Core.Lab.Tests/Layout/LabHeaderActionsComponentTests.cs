using Carlton.Core.Components.Dropdowns;
using Carlton.Core.Foundation.Test;
using Carlton.Core.Lab.Layouts;
using Carlton.Core.LayoutServices.Theme;
using Carlton.Core.LayoutServices.Toasts;
using Microsoft.Extensions.DependencyInjection;
namespace Carlton.Core.Lab.Test.Layout;

public class LabHeaderActionsComponentTests : TestContext
{
	[Theory, AutoNSubstituteData]
	public void LabHeaderActions_Markup_RendersCorrectly(
		IThemeState themeState)
	{
		//Arrange
		Services.AddSingleton(themeState);
		ComponentFactories.AddStub<ThemeMenu>();
		ComponentFactories.AddStub<NotificationMenu>();
		ComponentFactories.AddStub<DebugMenu>();
		ComponentFactories.AddStub<ProfileAvatarDropdown>();
		var expectedMarkup = $@"
			<div class=""component-lab-header-actions"" ></div>";

		//Act
		var cut = RenderComponent<LabHeaderActions>();

		//Assert
		cut.MarkupMatches(expectedMarkup);
	}
}
