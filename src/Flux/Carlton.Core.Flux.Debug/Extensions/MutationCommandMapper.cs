using Carlton.Core.Flux.Debug.Components.Logging.EventLogging.Filtering;
using Carlton.Core.Flux.Debug.Components.Logging.EventLogging.LogTable;
using Carlton.Core.Flux.Debug.Components.Logging.TraceLogging.LogTable;
using Riok.Mapperly.Abstractions;

namespace Carlton.Core.Flux.Debug.Extensions;

[Mapper]
internal partial class MutationCommandMapper
{
	internal static partial TCommand Map<TCommand>(object args);

	[MapProperty(nameof(SelectedEventLogMessageChangedArgs.SelectedLogMessage), nameof(ChangeSelectedLogMessageCommand.SelectedLogMessage))]
	internal static partial ChangeSelectedLogMessageCommand ToCommand(SelectedEventLogMessageChangedArgs args);

	[MapProperty(nameof(SelectedTraceLogMessageChangedArgs.SelectedTraceLogMessage), nameof(ChangeSelectedTraceLogMessageCommand.SelectedTraceLogMessage))]
	internal static partial ChangeSelectedTraceLogMessageCommand ToCommand(SelectedTraceLogMessageChangedArgs args);

	[MapProperty(nameof(EventLogLevelFiltersChangedArgs.LogLevel), nameof(ChangeEventLogLevelFiltersCommand.LogLevel))]
	[MapProperty(nameof(EventLogLevelFiltersChangedArgs.IsIncluded), nameof(ChangeEventLogLevelFiltersCommand.IsIncluded))]
	internal static partial ChangeEventLogLevelFiltersCommand ToCommand(EventLogLevelFiltersChangedArgs args);

	[MapProperty(nameof(EventLogLevelFilterTextChangedArgs.FilterText), nameof(ChangeEventLogFilterTextCommand.FilterText))]
	internal static partial ChangeEventLogFilterTextCommand ToCommand(EventLogLevelFilterTextChangedArgs args);
}


