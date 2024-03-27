using Microsoft.Extensions.Logging;
namespace Carlton.Core.Flux.Debug.Models.Commands;

/// <summary>
/// Represents a command to change the log level filters for event logging.
/// </summary>
public sealed record ChangeEventLogLevelFiltersCommand
{
	/// <summary>
	/// Gets or initializes the log level to be filtered.
	/// </summary>
	[Required]
	public required LogLevel LogLevel { get; init; }

	/// <summary>
	/// Gets or initializes a value indicating whether the specified log level is included in the filter.
	/// </summary>
	[Required]
	public required bool IsIncluded { get; init; }
}

