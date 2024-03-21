namespace Carlton.Core.Components.Lab.TestData;

internal static class CheckboxTestStates
{
	public static object CheckedState
	{
		get => new
		{
			IsChecked = true
		};
	}

	public static object UncheckedState
	{
		get => new
		{
			IsChecked = false
		};
	}
}
