using Carlton.Core.Components.Tables;
using Carlton.Core.Flux.Debug.Components.Logging.EventLogging.Filtering;
using Carlton.Core.Flux.Debug.Components.Logging.EventLogging.LogTable;
using Carlton.Core.Flux.Debug.Components.Logging.TraceLogging.LogTable;
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

	[MapProperty(nameof(TraceLogMessageExpansionChangedArgs.Index), nameof(ChangeLogMessageExpansionCommand.TraceLogMessageIndex))]
	[MapProperty(nameof(TraceLogMessageExpansionChangedArgs.IsExpanded), nameof(ChangeLogMessageExpansionCommand.IsExpanded))]
	internal static partial ChangeLogMessageExpansionCommand ToCommand(TraceLogMessageExpansionChangedArgs args);

	[MapProperty(nameof(EventLogLevelFiltersChangedArgs.LogLevel), nameof(ChangeEventLogLevelFiltersCommand.LogLevel))]
	[MapProperty(nameof(EventLogLevelFiltersChangedArgs.IsIncluded), nameof(ChangeEventLogLevelFiltersCommand.IsIncluded))]
	internal static partial ChangeEventLogLevelFiltersCommand ToCommand(EventLogLevelFiltersChangedArgs args);

	[MapProperty(nameof(EventLogLevelFilterTextChangedArgs.FilterText), nameof(ChangeEventLogFilterTextCommand.FilterText))]
	internal static partial ChangeEventLogFilterTextCommand ToCommand(EventLogLevelFilterTextChangedArgs args);


	[MapProperty(nameof(RowsPerPageChangeEventArgs.SelectedRowsPerPageIndex), nameof(ChangeEventLogMessageTableRowsPerPageOptsCommand.SelectedRowsPerPageIndex))]
	internal static partial ChangeEventLogMessageTableRowsPerPageOptsCommand ToCommandEventLogTableRowsPerPage(RowsPerPageChangeEventArgs args);

	[MapProperty(nameof(RowsPerPageChangeEventArgs.SelectedRowsPerPageIndex), nameof(ChangeTraceLogMessageTableRowsPerPageOptsCommand.SelectedRowsPerPageIndex))]
	internal static partial ChangeTraceLogMessageTableRowsPerPageOptsCommand ToCommandTraceLogTableRowsPerPage(RowsPerPageChangeEventArgs args);

	[MapProperty(nameof(PageChangeEventArgs.CurrentPage), nameof(ChangeEventLogPageCommand.SelectedPageIndex))]
	internal static partial ChangeEventLogPageCommand ToCommandEventLogTablePageChange(PageChangeEventArgs args);

	[MapProperty(nameof(PageChangeEventArgs.CurrentPage), nameof(ChangeTraceLogPageCommand.SelectedPageIndex))]
	internal static partial ChangeTraceLogPageCommand ToCommandTraceLogTablePageChange(PageChangeEventArgs args);

	[MapProperty(nameof(ItemsSortEventArgs.SortColumn), nameof(ChangeEventLogTableOrderingCommand.OrderByColum))]
	[MapProperty(nameof(ItemsSortEventArgs.SortAscending), nameof(ChangeEventLogTableOrderingCommand.OrderAscending))]
	internal static partial ChangeEventLogTableOrderingCommand ToCommand(ItemsSortEventArgs args);
}


