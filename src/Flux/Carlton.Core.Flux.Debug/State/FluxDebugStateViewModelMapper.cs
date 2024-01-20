#pragma warning disable RMG020 // Source member is not mapped to any target member
using Riok.Mapperly.Abstractions;

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

    public static HeaderActionsViewModel FluxDebugStateToHeaderActionsViewModelProjection(FluxDebugState state)
    {
        return new HeaderActionsViewModel
        {
            UserName = "Stephen",
            AvatarUrl = "_content/Carlton.Core.Components/images/avatar.jpg"
        };
    }
}


