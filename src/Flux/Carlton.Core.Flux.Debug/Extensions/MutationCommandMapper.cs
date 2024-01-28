using Carlton.Core.Flux.Debug.Components.Logging.TraceLogging;
using Riok.Mapperly.Abstractions;

namespace Carlton.Core.Flux.Debug.Extensions;

[Mapper]
public partial class MutationCommandMapper
{
    public static partial TViewModel Map<TViewModel>(object args);

    [MapProperty(nameof(SelectedTraceLogMessageChangedArgs.SelectedTraceLogMessage), nameof(ChangeSelectedTraceLogMessageCommand.SelectedTraceLogMessage))]
    public static partial ChangeSelectedTraceLogMessageCommand ToCommand(SelectedTraceLogMessageChangedArgs args);
}


