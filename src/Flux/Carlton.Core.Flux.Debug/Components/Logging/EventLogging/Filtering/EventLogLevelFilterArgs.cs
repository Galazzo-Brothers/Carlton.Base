using Microsoft.Extensions.Logging;
namespace Carlton.Core.Flux.Debug.Components.Logging.EventLogging.Filtering;

/// <summary>
/// Represents arguments for when event log level filters change.
/// </summary>
/// <param name="LogLevel">The log level affected by the change.</param>
/// <param name="IsIncluded">Indicates whether the log level is included or excluded.</param>
public sealed record EventLogLevelFiltersChangedArgs(LogLevel LogLevel, bool IsIncluded);

/// <summary>
/// Represents arguments for when event log level filter text changes.
/// </summary>
/// <param name="FilterText">The new filter text.</param>
public sealed record EventLogLevelFilterTextChangedArgs(string FilterText);

