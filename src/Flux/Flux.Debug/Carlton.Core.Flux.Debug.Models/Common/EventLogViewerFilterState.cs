using Microsoft.Extensions.Logging;
namespace Carlton.Core.Flux.Debug.Models.Common;

/// <summary>
/// Represents the filter state for an event log viewer.
/// </summary>
public sealed record EventLogViewerFilterState
{
	/// <summary>
	/// Gets or initializes the included log levels.
	/// </summary>
	public IEnumerable<LogLevel> IncludedLogLevels { get; init; } = new List<LogLevel> { LogLevel.Trace, LogLevel.Debug, LogLevel.Information, LogLevel.Warning, LogLevel.Error, LogLevel.Critical };

	/// <summary>
	/// Gets or initializes the filter text.
	/// </summary>
	public string FilterText { get; init; } = string.Empty;
}