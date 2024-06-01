#pragma warning disable RMG020 // Source member is not mapped to any target member
using Carlton.Core.Flux;
using Carlton.Core.Flux.Debug.Extensions;
using Carlton.Core.Flux.Internals;
using Riok.Mapperly.Abstractions;
namespace Carlton.Core.Lab.State;

[Mapper]
internal sealed partial class FluxDebugStateViewModelMapper : IViewModelProjectionMapper<FluxDebugState>
{
	public partial TViewModel Map<TViewModel>(FluxDebugState state);

	[MapProperty(nameof(FluxDebugState.SelectedLogMessage), nameof(EventLogDetailsViewModel.SelectedLogMessage))]
	public partial EventLogDetailsViewModel ToEventLogDetailsViewModel(FluxDebugState state);

	[MapProperty(nameof(FluxDebugState.SelectedLogMessage), nameof(EventLogScopesViewModel.SelectedLogMessage))]
	public partial EventLogScopesViewModel ToEventLogScopesViewModel(FluxDebugState state);

	[MapProperty(nameof(FluxDebugState.TraceLogMessageGroups), nameof(TraceLogViewerViewModel.TraceLogMessages))]
	[MapProperty(nameof(FluxDebugState.SelectedTraceLogMessageIndex), nameof(TraceLogViewerViewModel.SelectedTraceLogMessageIndex))]
	public partial TraceLogViewerViewModel ToTraceLogViewerViewModel(FluxDebugState state);

	[MapProperty(nameof(FluxDebugState.SelectedState), nameof(FluxStateViewerViewModel.SelectedFluxState))]
	[MapProperty(nameof(FluxDebugState.RecordedMutations), nameof(FluxStateViewerViewModel.RecordedMutations))]
	[MapProperty(nameof(FluxDebugState.SelectedMutationIndex), nameof(FluxStateViewerViewModel.SelectedMutationIndex))]
	public partial FluxStateViewerViewModel ToFluxStateViewerViewModel(FluxDebugState state);

	[MapProperty(nameof(FluxDebugState.ViewModelTypes), nameof(ViewModelProjectionsViewerViewModel.ViewModelTypes))]
	[MapProperty(nameof(FluxDebugState.SelectedState), nameof(ViewModelProjectionsViewerViewModel.FluxState))]
	public partial ViewModelProjectionsViewerViewModel ToViewModelProjectionsViewerViewModel(FluxDebugState state);

	[MapProperty(nameof(FluxDebugState.SelectedCommandMutation), nameof(MutationCommandViewerViewModel.SelectedMutationCommand))]
	public partial MutationCommandViewerViewModel ToMutationCommandViewerViewModel(FluxDebugState state);

	public EventLogViewerViewModel ToEventLogViewerViewModelViewModel(FluxDebugState state)
	{
		return new EventLogViewerViewModel
		{
			SelectedLogMessageIndex = state.SelectedLogMessageIndex,
			LogMessages = state.LogMessages.Select(x => x.ToLogMessageSummary())
		};
	}

	internal static TraceLogRequestContextDetailsViewModel ToTraceLogRequestContextDetailsViewModel(FluxDebugState state)
	{
		if (state.SelectedTraceLogMessage == null)
			return new TraceLogRequestContextDetailsViewModel { SelectedRequestContext = null };

		return new TraceLogRequestContextDetailsViewModel { SelectedRequestContext = state.SelectedTraceLogMessage.GetScopeValue<BaseRequestContext>("FluxRequestContext") };
	}

	public static TraceLogRequestObjectDetailsViewModel ToTraceLogRequestObjectDetailsViewModel(FluxDebugState state)
	{
		var defaultViewModel = new TraceLogRequestObjectDetailsViewModel { SelectedRequestObject = null };

		if (state.SelectedTraceLogMessage == null)
			return defaultViewModel;

		var selectedContext = state.SelectedTraceLogMessage.GetScopeValue<BaseRequestContext>("FluxRequestContext");
		return selectedContext.FluxOperationKind switch
		{
			FluxOperationKind.ViewModelQuery => defaultViewModel with { SelectedRequestObject = ((dynamic)selectedContext).ResultViewModel },
			FluxOperationKind.MutationCommand => defaultViewModel with { SelectedRequestObject = ((dynamic)selectedContext).MutationCommand },
			_ => defaultViewModel
		};
	}

	public static HeaderActionsViewModel ToHeaderActionsViewModel(FluxDebugState state)
	{
		return new HeaderActionsViewModel
		{
			UserName = "Stephen",
			AvatarUrl = "_content/Carlton.Core.Components/images/avatar.jpg"
		};
	}
}



