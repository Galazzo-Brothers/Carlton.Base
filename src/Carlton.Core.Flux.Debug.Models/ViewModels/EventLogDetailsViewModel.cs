namespace Carlton.Core.Flux.Debug.Models.ViewModels;

/// <summary>
/// Represents the view model for displaying details of a log message.
/// </summary>
public sealed record EventLogDetailsViewModel
{
	/// <summary>
	/// Gets or initializes the selected log message to display details for.
	/// </summary>
	public required LogMessage SelectedLogMessage { get; init; }
};
