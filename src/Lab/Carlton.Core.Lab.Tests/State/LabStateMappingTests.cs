using Carlton.Core.Lab.Models.Common;
namespace Carlton.Core.Lab.Test.MappingTests;

public class LabStateMappingTests
{
	[Theory, AutoData]
	public void LabStateMapper_ShouldMap_LabState_To_NavMenuViewModel(
		IEnumerable<ComponentConfigurations> componentConfigurations,
		LabStateViewModelMapper sut)
	{
		//Arrange
		var labState = new LabState(componentConfigurations);

		//Act
		var result = sut.Map<NavMenuViewModel>(labState);

		//Assert
		result.SelectedComponentIndex.ShouldBe(labState.SelectedComponentIndex);
		result.SelectedStateIndex.ShouldBe(labState.SelectedComponentStateIndex);
	}

	[Theory, AutoData]
	public void LabStateMapper_ShouldMap_LabState_To_ComponentViewerViewModel(
		IEnumerable<ComponentConfigurations> componentConfigurations,
		LabStateViewModelMapper sut)
	{
		//Arrange
		var labState = new LabState(componentConfigurations);

		//Act
		var result = sut.Map<ComponentViewerViewModel>(labState);

		//Assert
		result.ComponentType.ShouldBe(labState.SelectedComponentType);
		result.ComponentParameters.ShouldBe(labState.SelectedComponentParameters);
	}

	[Theory, AutoData]
	public void LabStateMapper_ShouldMap_LabState_To_EventConsoleViewModel(
		IEnumerable<ComponentConfigurations> componentConfigurations,
		IEnumerable<ComponentRecordedEvent> events,
		LabStateViewModelMapper sut)
	{
		//Arrange
		var labState = new LabState(componentConfigurations);
		labState = labState with
		{
			ComponentEvents = events
		};

		//Act
		var result = sut.Map<EventConsoleViewModel>(labState);


		//Assert
		result.RecordedEvents.ShouldBe(labState.ComponentEvents);
	}

	[Theory, AutoData]
	public void LabStateMapper_ShouldMap_LabState_To_ParametersViewerViewModel(
		IEnumerable<ComponentConfigurations> componentConfigurations,
		LabStateViewModelMapper sut)
	{
		//Arrange
		var labState = new LabState(componentConfigurations);

		//Act
		var result = sut.Map<ParametersViewerViewModel>(labState);


		//Assert
		result.ComponentParameters.ShouldBe(labState.SelectedComponentParameters);
	}

	[Theory, AutoData]
	public void LabStateMapper_ShouldMap_LabState_To_BreadCrumbsViewModel(
		IEnumerable<ComponentConfigurations> componentConfigurations,
		LabStateViewModelMapper sut)
	{
		//Arrange
		var labState = new LabState(componentConfigurations);

		//Act
		var result = sut.Map<BreadCrumbsViewModel>(labState);

		//Assert
		result.SelectedComponent.ShouldBe(labState.SelectedComponentType.GetDisplayName());
		result.SelectedComponentState.ShouldBe(labState.SelectedComponentState.DisplayName);
	}
}
