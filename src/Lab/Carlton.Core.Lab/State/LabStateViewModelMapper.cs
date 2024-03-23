#pragma warning disable RMG020 // Source member is not mapped to any target member
using Riok.Mapperly.Abstractions;
namespace Carlton.Core.Lab.State;

[Mapper]
internal partial class LabStateViewModelMapper : IViewModelProjectionMapper<LabState>
{
	public partial TViewModel Map<TViewModel>(LabState state);

	[MapProperty(nameof(LabState.SelectedComponentTypeDisplayName), nameof(BreadCrumbsViewModel.SelectedComponent))]
	[MapProperty($"{nameof(LabState.SelectedComponentState)}.{nameof(LabState.SelectedComponentState.DisplayName)}", nameof(BreadCrumbsViewModel.SelectedComponentState))]
	public partial BreadCrumbsViewModel LabStateToBreadCrumbsViewModelProjections(LabState state);

	[MapProperty(nameof(LabState.SelectedComponentType), nameof(ComponentViewerViewModel.ComponentType))]
	[MapProperty(nameof(LabState.SelectedComponentParameters), nameof(ComponentViewerViewModel.ComponentParameters))]
	public partial ComponentViewerViewModel LabStateToComponentViewerViewModelProjections(LabState state);

	[MapProperty(nameof(LabState.ComponentEvents), nameof(EventConsoleViewModel.RecordedEvents))]
	public partial EventConsoleViewModel LabStateToEventConsoleViewModelViewModelProjections(LabState state);


	[MapProperty(nameof(LabState.ComponentConfigurations), nameof(NavMenuViewModel.MenuItems))]
	[MapProperty(nameof(LabState.SelectedComponentIndex), nameof(NavMenuViewModel.SelectedComponentIndex))]
	[MapProperty(nameof(LabState.SelectedComponentStateIndex), nameof(NavMenuViewModel.SelectedStateIndex))]
	public partial NavMenuViewModel LabStateToNavMenuViewModelViewModelProjections(LabState state);


	[MapProperty(nameof(LabState.SelectedComponentParameters), nameof(ParametersViewerViewModel.ComponentParameters))]
	public partial ParametersViewerViewModel LabStateToParametersViewerViewModelViewModelProjections(LabState state);
}


