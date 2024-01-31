using Carlton.Core.Flux.Debug.Components.Logging.EventLogging;
using Carlton.Core.Flux.Debug.Components.Logging.TraceLogging;
using Riok.Mapperly.Abstractions;

namespace Carlton.Core.Flux.Debug.Extensions;

[Mapper]
internal partial class MutationCommandMapper
{
    internal static partial TCommand Map<TCommand>(object args);

    [MapProperty(nameof(SelectedEventLogMessageChangedArgs.SelectedLogMessageIndex), nameof(ChangeSelectedLogMessageCommand.SelectedLogMessageIndex))]
    internal static partial ChangeSelectedLogMessageCommand ToCommand(SelectedEventLogMessageChangedArgs args);

    [MapProperty(nameof(SelectedTraceLogMessageChangedArgs.SelectedTraceLogMessageIndex), nameof(ChangeSelectedTraceLogMessageCommand.SelectedTraceLogMessageIndex))]
    internal static partial ChangeSelectedTraceLogMessageCommand ToCommand(SelectedTraceLogMessageChangedArgs args);

    [MapProperty(nameof(TraceLogMessageExapansionChangedArgs.TraceLogMessage), nameof(ChangeLogMessageExpansionCommand.TraceLogMessage))]
    [MapProperty(nameof(TraceLogMessageExapansionChangedArgs.IsExpanded), nameof(ChangeLogMessageExpansionCommand.IsExpanded))]
    internal static partial ChangeLogMessageExpansionCommand ToCommand(TraceLogMessageExapansionChangedArgs args);

    [MapProperty(nameof(EventLogLevelFiltersChangedArgs.LogLevel), nameof(ChangeEventLogLevelFiltersCommand.LogLevel))]
    [MapProperty(nameof(EventLogLevelFiltersChangedArgs.IsIncluded), nameof(ChangeEventLogLevelFiltersCommand.IsIncluded))]
    internal static partial ChangeEventLogLevelFiltersCommand ToCommand(EventLogLevelFiltersChangedArgs args);

    [MapProperty(nameof(EventLogLevelFilterTextChangedArgs.FilterText), nameof(ChangeEventLogFilterTextCommand.FilterText))]
    internal static partial ChangeEventLogFilterTextCommand ToCommand(EventLogLevelFilterTextChangedArgs args);
}


