namespace Carlton.Core.Components.Lab.TestData;

internal static class DropdownTestStates
{
	public static object Default
	{
		get => new
		{
			IsDisabled = false,
			Label = "Test",
			Options = new Dictionary<string, int>
			{
				{ "Option 1", 1 },
				{ "Option 2", 2 },
				{ "Option 3", 3 }
			},
			SelectedIndex = 0,
			IsPristineEnabled = false
		};
	}

	public static object Disabled
	{
		get => new
		{
			IsDisabled = true,
			Label = "Test",
			Options = new Dictionary<string, int>
			{
				{ "Option 1", 1 },
				{ "Option 2", 2 },
				{ "Option 3", 3 }
			},
			SelectedIndex = 0,
			IsPristineEnabled = false
		};
	}

	public static object Pristine
	{
		get => new
		{
			IsDisabled = false,
			Label = "Test",
			Options = new Dictionary<string, int>
			{
				{ "Option 1", 1 },
				{ "Option 2", 2 },
				{ "Option 3", 3 }
			},
			SelectedIndex = 0,
			IsPristineEnabled = true
		};
	}
}


