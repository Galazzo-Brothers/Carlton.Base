namespace Carlton.Core.Lab.Components.ComponentViewer;

public class InitSelectionCommand
{
	[Required]
	public required string ComponentName { get; init; }

	[Required]
	public required string ComponentState { get; init; }
}
