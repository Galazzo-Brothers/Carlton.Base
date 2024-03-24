using Microsoft.Extensions.Logging;
namespace Carlton.Core.Flux.Debug.Components.Logging.EventLogging.Filtering;

public record EventLogLevelFiltersChangedArgs(LogLevel LogLevel, bool IsIncluded);

public record EventLogLevelFilterTextChangedArgs(string FilterText);

