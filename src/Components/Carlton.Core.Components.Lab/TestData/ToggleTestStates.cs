namespace Carlton.Core.Components.Lab.TestData;

internal static class ToggleTestStates
{
	public static object DefaultState
	{
		get => new
		{
			FirstOption = new KeyValuePair<string, int>("Option 1", 1),
			SecondOption = new KeyValuePair<string, int>("Option 2", 2)
		};
	}
}
