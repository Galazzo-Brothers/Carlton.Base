namespace Carlton.Core.Flux.Debug.Models.ViewModels;

/// <summary>
/// Represents the view model for displaying details of a trace log request object.
/// </summary>
public sealed record TraceLogRequestObjectDetailsViewModel
{
	/// <summary>
	/// Gets or initializes the selected request object.
	/// </summary>
	public required object SelectedRequestObject { get; init; }
};
