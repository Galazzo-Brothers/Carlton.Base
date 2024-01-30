using Carlton.Core.Flux.Debug.Components.Logging.EventLogging;
using Carlton.Core.Flux.Debug.Components.Logging.TraceLogging;
using Riok.Mapperly.Abstractions;

namespace Carlton.Core.Flux.Debug.Extensions;

[Mapper]
internal partial class MutationCommandMapper
{
    internal static partial TViewModel Map<TViewModel>(object args);

    [MapProperty(nameof(SelectedEventLogMessageChangedArgs.SelectedLogMessage), nameof(ChangeSelectedLogMessageCommand.SelectedLogMessage))]
    internal static partial ChangeSelectedLogMessageCommand ToCommand(SelectedEventLogMessageChangedArgs args);

    [MapProperty(nameof(SelectedTraceLogMessageChangedArgs.SelectedTraceLogMessage), nameof(ChangeSelectedTraceLogMessageCommand.SelectedTraceLogMessage))]
    internal static partial ChangeSelectedTraceLogMessageCommand ToCommand(SelectedTraceLogMessageChangedArgs args);
}


