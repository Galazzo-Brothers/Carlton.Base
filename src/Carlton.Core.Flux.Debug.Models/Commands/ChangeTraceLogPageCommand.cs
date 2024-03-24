namespace Carlton.Core.Flux.Debug.Models.Commands;

/// <summary>
/// Represents a command to change the selected page index for the trace log.
/// </summary>
public sealed record ChangeTraceLogPageCommand
{
	/// <summary>
	/// Gets or initializes the index of the selected page.
	/// </summary>
	[NonNegativeInteger]
	public required int SelectedPageIndex { get; init; }
};
