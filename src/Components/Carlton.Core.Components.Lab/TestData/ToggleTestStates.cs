using Carlton.Core.Components.Toggles;

namespace Carlton.Core.Components.Lab.TestData;

internal static class ToggleTestStates
{
	public static object FirstOptionSelected
	{
		get => new
		{
			FirstOption = new KeyValuePair<string, int>("Option 1", 1),
			SecondOption = new KeyValuePair<string, int>("Option 2", 2),
			SelectedOption = ToggleSelectOption.FirstOption
		};
	}

	public static object SecondOptionSelected
	{
		get => new
		{
			FirstOption = new KeyValuePair<string, int>("Option 1", 1),
			SecondOption = new KeyValuePair<string, int>("Option 2", 2),
			SelectedOption = ToggleSelectOption.SecondOption
		};
	}
}
