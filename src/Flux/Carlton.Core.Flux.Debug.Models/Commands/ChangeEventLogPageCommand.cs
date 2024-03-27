namespace Carlton.Core.Flux.Debug.Models.Commands;

/// <summary>
/// Represents a command to change the selected page index for event logging.
/// </summary>
public sealed record ChangeEventLogPageCommand
{

	/// <summary>
	/// Gets the index of the selected page.
	/// </summary>
	[NonNegativeInteger]
	public required int SelectedPageIndex { get; init; }
};
