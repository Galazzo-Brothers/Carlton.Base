namespace Carlton.Core.Flux.Debug.Models.ViewModels;

/// <summary>
/// Represents the view model for displaying details of a trace log request context.
/// </summary>
public sealed record TraceLogRequestContextDetailsViewModel
{
	/// <summary>
	/// Gets or initializes the selected request context.
	/// </summary>
	public required object? SelectedRequestContext { get; init; }
};
