using Carlton.Core.Components.Tables;
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

    [MapProperty(nameof(TraceLogMessageExapansionChangedArgs.Index), nameof(ChangeLogMessageExpansionCommand.TraceLogMessageIndex))]
    [MapProperty(nameof(TraceLogMessageExapansionChangedArgs.IsExpanded), nameof(ChangeLogMessageExpansionCommand.IsExpanded))]
    internal static partial ChangeLogMessageExpansionCommand ToCommand(TraceLogMessageExapansionChangedArgs args);

    [MapProperty(nameof(EventLogLevelFiltersChangedArgs.LogLevel), nameof(ChangeEventLogLevelFiltersCommand.LogLevel))]
    [MapProperty(nameof(EventLogLevelFiltersChangedArgs.IsIncluded), nameof(ChangeEventLogLevelFiltersCommand.IsIncluded))]
    internal static partial ChangeEventLogLevelFiltersCommand ToCommand(EventLogLevelFiltersChangedArgs args);

    [MapProperty(nameof(EventLogLevelFilterTextChangedArgs.FilterText), nameof(ChangeEventLogFilterTextCommand.FilterText))]
    internal static partial ChangeEventLogFilterTextCommand ToCommand(EventLogLevelFilterTextChangedArgs args);


    [MapProperty(nameof(TableRowsPerPageChangedArgs.SelectedRowsPerPageIndex), nameof(ChangeEventLogMessageTableRowsPerPageOptsCommand.SelectedRowsPerPageIndex))]
    internal static partial ChangeEventLogMessageTableRowsPerPageOptsCommand ToCommandEventLogTableRowsPerPage(TableRowsPerPageChangedArgs args);

    [MapProperty(nameof(TableRowsPerPageChangedArgs.SelectedRowsPerPageIndex), nameof(ChangeTraceLogMessageTableRowsPerPageOptsCommand.SelectedRowsPerPageIndex))]
    internal static partial ChangeTraceLogMessageTableRowsPerPageOptsCommand ToCommandTraceLogTableRowsPerPage(TableRowsPerPageChangedArgs args);

    [MapProperty(nameof(TablePageChangedArgs.CurrentPage), nameof(ChangeEventLogPageCommand.SelectedPageIndex))]
    internal static partial ChangeEventLogPageCommand ToCommandEventLogTablePageChange(TablePageChangedArgs args);

    [MapProperty(nameof(TablePageChangedArgs.CurrentPage), nameof(ChangeTraceLogPageCommand.SelectedPageIndex))]
    internal static partial ChangeTraceLogPageCommand ToCommandTraceLogTablePageChange(TablePageChangedArgs args);

    [MapProperty(nameof(TableOrderingChangedArgs.OrderColumn), nameof(ChangeEventLogTableOrderingCommand.OrderByColum))]
    [MapProperty(nameof(TableOrderingChangedArgs.OrderAscending), nameof(ChangeEventLogTableOrderingCommand.OrderAscending))]
    internal static partial ChangeEventLogTableOrderingCommand ToCommand(TableOrderingChangedArgs args);
}


