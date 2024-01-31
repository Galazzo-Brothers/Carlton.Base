using Microsoft.Extensions.Logging;
namespace Carlton.Core.Flux.Debug.Components.Logging.EventLogging;

public record EventLogLevelFiltersChangedArgs(LogLevel LogLevel, bool IsIncluded);
