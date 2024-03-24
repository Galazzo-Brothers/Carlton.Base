namespace Carlton.Core.Flux.Debug.Models.ViewModels;

/// <summary>
/// Represents the view model for displaying scopes of a log message.
/// </summary>
public sealed record EventLogScopesViewModel
{
	/// <summary>
	/// Gets or initializes the selected log message to display scopes for.
	/// </summary>
	public required LogMessage SelectedLogMessage { get; init; }
};

