#pragma warning disable RMG020 // Source member is not mapped to any target member
using Carlton.Core.Flux.Debug.Components.Logging.TraceLogging;
using Riok.Mapperly.Abstractions;
using static Carlton.Core.Flux.Debug.Components.Logging.TraceLogging.TraceLogTable;

namespace Carlton.Core.Lab.State;

[Mapper]
public partial class FluxDebugStateViewModelMapper : IViewModelMapper<FluxDebugState>
{
    public partial TViewModel Map<TViewModel>(FluxDebugState state);

    [MapProperty(nameof(FluxDebugState.SelectedLogMessage), nameof(EventLogDetailsViewModel.SelectedLogMessage))]
    public partial EventLogDetailsViewModel FluxDebugStateToEventLogDetailsViewModelProjection(FluxDebugState state);

    [MapProperty(nameof(FluxDebugState.SelectedLogMessage), nameof(EventLogScopesViewModel.SelectedLogMessage))]
    public partial EventLogScopesViewModel FluxDebugStateToEventLogScopesViewModelProjection(FluxDebugState state);

    [MapProperty(nameof(FluxDebugState.LogMessages), nameof(EventLogViewerViewModel.LogMessages))]
    public partial EventLogViewerViewModel FluxDebugStateToEventLogViewerViewModelViewModelProjection(FluxDebugState state);

    [MapProperty(nameof(FluxDebugState.LogMessages), nameof(TraceLogViewerViewModel.LogMessages))]
    public partial TraceLogViewerViewModel FluxDebugStateToTraceLogViewerViewModelProjection(FluxDebugState state);

    [MapProperty(nameof(FluxDebugState.SelectedTraceLogMessage), nameof(TraceLogRequestContextDetailsViewModel.SelectedTraceLogMessage))]
    public partial TraceLogRequestContextDetailsViewModel FluxDebugStateToTraceLogRequestContextDetailsViewModelProjection(FluxDebugState state);

    public static HeaderActionsViewModel FluxDebugStateToHeaderActionsViewModelProjection(FluxDebugState state)
    {
        return new HeaderActionsViewModel
        {
            UserName = "Stephen",
            AvatarUrl = "_content/Carlton.Core.Components/images/avatar.jpg"
        };
    }
}



