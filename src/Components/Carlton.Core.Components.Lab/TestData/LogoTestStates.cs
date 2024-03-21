namespace Carlton.Core.Components.Lab.TestData;

internal static class LogoTestStates
{
	public static object Expanded
	{
		get => new
		{
			IsCollapsed = false,
			Title = "Project Carlton",
			LogoUrl = "_content/Carlton.Core.Components/images/CarltonLogo.png"
		};
	}

	public static object Collapsed
	{
		get => new
		{
			IsCollapsed = true,
			Title = "Project Carlton",
			LogoUrl = "_content/Carlton.Core.Components/images/CarltonLogo.png"
		};
	}
}
