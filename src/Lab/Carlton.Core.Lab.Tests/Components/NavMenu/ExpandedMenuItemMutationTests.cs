using Carlton.Core.Lab.Components.NavMenu;
using Carlton.Core.Lab.Models.Common;
namespace Carlton.Core.Lab.Test.Components.NavMenu;

public class ExpandedMenuItemMutationTests
{
	[Theory, AutoData]
	public void ClearEventsMutation_MutatesCorrectly(
		IEnumerable<ComponentConfigurations> componentStates,
		ExpandedMenuItemMutation sut,
		bool expectedIsExpanded)
	{
		//Arrange
		var labState = new LabState(componentStates);
		var selectedIndex = RandomUtilities.GetRandomIndex(labState.ComponentConfigurations.Count);
		var command = new SelectMenuExpandedCommand
		{
			SelectedComponentIndex = selectedIndex,
			IsExpanded = expectedIsExpanded
		};

		//Act
		var mutatedState = sut.Mutate(labState, command);

		//Assert
		mutatedState.ComponentConfigurations[selectedIndex].IsExpanded.ShouldBe(expectedIsExpanded);
	}
}
