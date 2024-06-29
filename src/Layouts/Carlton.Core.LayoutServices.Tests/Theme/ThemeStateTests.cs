using Carlton.Core.LayoutServices.Theme;
namespace Carlton.Core.LayoutServices.Tests.Theme;

public class ThemeStateTests
{
	[Theory(DisplayName = "ThemeState Event Test")]
	[InlineData(Themes.light)]
	[InlineData(Themes.dark)]
	public void ThemeState_ToggleTheme_EventFires(
	   Themes expectedTheme)
	{
		//Arrange
		var eventFired = false;
		var sut = new ThemeState(expectedTheme);
		sut.ThemeChanged += (sender, args) => eventFired = true;
		var newTheme = expectedTheme == Themes.light ? Themes.dark : Themes.light;

		//Act
		sut.ToggleTheme();

		//Assert
		eventFired.ShouldBe(true);
		sut.Theme.ShouldBe(newTheme);
	}

	[Theory(DisplayName = "ThemeState Event Test")]
	[InlineData(Themes.light)]
	[InlineData(Themes.dark)]
	public void ThemeState_SetTheme_EventFires(
	  Themes expectedTheme)
	{
		//Arrange
		var eventFired = false;
		var sut = new ThemeState(expectedTheme);
		sut.ThemeChanged += (sender, args) => eventFired = true;
		var newTheme = expectedTheme == Themes.light ? Themes.dark : Themes.light;

		//Act
		sut.SetTheme(newTheme);

		//Assert
		eventFired.ShouldBe(true);
		sut.Theme.ShouldBe(newTheme);
	}

}
