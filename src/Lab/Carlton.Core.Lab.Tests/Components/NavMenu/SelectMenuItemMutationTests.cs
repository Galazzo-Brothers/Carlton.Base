using Carlton.Core.Lab.Components.NavMenu;
using Carlton.Core.Lab.Models.Common;
namespace Carlton.Core.Lab.Test.MutationTests;

public class SelectMenuItemMutationTests
{
	[Theory, AutoData]
	internal void SelectMenuItemMutation_MutatesCorrectly(
		IEnumerable<ComponentConfigurations> componentStates,
		SelectMenuItemMutation sut)
	{
		//Arrange
		var labState = new LabState(componentStates);
		var selectedComponentIndex = RandomUtilities.GetRandomIndex(labState.ComponentConfigurations.Count);
		var selectedStateIndex = RandomUtilities.GetRandomIndex(labState.ComponentConfigurations.ElementAt(selectedComponentIndex).ComponentStates.Count());
		var expectedComponentState = labState.ComponentConfigurations.ElementAt(selectedComponentIndex).ComponentStates.ElementAt(selectedStateIndex);
		var command = new SelectMenuItemCommand
		{
			ComponentIndex = selectedComponentIndex,
			ComponentStateIndex = selectedStateIndex
		};

		//Act
		var mutatedState = sut.Mutate(labState, command);

		//Assert
		mutatedState.SelectedComponentState.ShouldBe(expectedComponentState);
	}
}
