using Carlton.Core.Flux.Debug.Components.Logging.EventLogging.LogTable;
using Carlton.Core.Flux.Debug.Components.Logging.TraceLogging.LogTable;
using Carlton.Core.Flux.Debug.Components.StateViewer.SubmitMutationConsole;
using Carlton.Core.Flux.Debug.Components.StateViewer.Viewer;
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

	[MapProperty(nameof(SelectedMutationCommandChangedArgs.SelectedMutationCommandIndex), nameof(ChangeSelectedCommandMutationCommand.SelectedMutationCommandIndex))]
	internal static partial ChangeSelectedCommandMutationCommand ToCommand(SelectedMutationCommandChangedArgs args);

	[MapProperty(nameof(SubmitMutationArgs.MutationCommand), nameof(SubmitMutationCommand.MutationCommandToSubmit))]
	internal static partial SubmitMutationCommand ToCommand(SubmitMutationArgs args);

	internal static partial PopMutationCommand ToCommand(PopMutationEventArgs args);
}


