using AutoFixture;
using Carlton.Core.Lab.Components.ComponentViewer;
using Carlton.Core.Lab.Models.Common;
using Carlton.Core.Lab.Pages;
namespace Carlton.Core.Lab.Tests.Components;

public class InitSelectionMutationTests
{
	private readonly List<ComponentConfigurations> _componentConfigurations;

	public InitSelectionMutationTests()
	{
		var fixture = new Fixture();
		_componentConfigurations =
		[
			new()
			{
				ComponentType = typeof(object),
				ComponentStates = fixture.CreateMany<ComponentState>()
			},
			new()
			{
				ComponentType = typeof(string),
				ComponentStates = fixture.CreateMany<ComponentState>()
			},
			new()
			{
				ComponentType = typeof(int),
				ComponentStates = fixture.CreateMany<ComponentState>()
			},
		];
	}

	[Fact]
	internal void InitSelectionMutation_MutatesCorrectly()
	{
		//Arrange
		var sut = new InitSelectionMutation();
		var labState = new LabState(_componentConfigurations);
		var selectedComponentIndex = RandomUtilities.GetRandomIndex(labState.ComponentConfigurations.Count);
		var selectedComponentStateIndex = RandomUtilities.GetRandomIndex(labState.ComponentConfigurations.ElementAt(selectedComponentIndex).ComponentStates.Count());

		var componentToSelect = labState.ComponentConfigurations.ElementAt(selectedComponentIndex).ComponentType.GetDisplayName();
		var componentStateToSelect = labState.ComponentConfigurations.ElementAt(selectedComponentIndex).ComponentStates.ElementAt(selectedComponentStateIndex).DisplayName;

		var command = new InitSelectionCommand
		{ ComponentName = componentToSelect, ComponentState = componentStateToSelect };

		//Act
		var mutatedState = sut.Mutate(labState, command);

		//Assert
		mutatedState.SelectedComponentTypeDisplayName.ShouldBe(componentToSelect);
		mutatedState.SelectedComponentState.DisplayName.ShouldBe(componentStateToSelect);
	}

	[Fact]
	internal void InitSelectionMutation_WithInvalidComponentState_MutatesCorrectly()
	{
		//Arrange
		var sut = new InitSelectionMutation();
		var labState = new LabState(_componentConfigurations);
		var selectedComponentIndex = RandomUtilities.GetRandomIndex(labState.ComponentConfigurations.Count);
		var selectedComponentStateIndex = 0;

		var componentToSelect = labState.ComponentConfigurations.ElementAt(selectedComponentIndex).ComponentType.GetDisplayName();
		var componentStateToSelect = labState.ComponentConfigurations.ElementAt(selectedComponentIndex).ComponentStates.ElementAt(selectedComponentStateIndex).DisplayName;

		var command = new InitSelectionCommand
		{ ComponentName = componentToSelect, ComponentState = "Nonsense" };

		//Act
		var mutatedState = sut.Mutate(labState, command);

		//Assert
		mutatedState.SelectedComponentTypeDisplayName.ShouldBe(componentToSelect);
		mutatedState.SelectedComponentState.DisplayName.ShouldBe(componentStateToSelect);
	}

	[Fact]
	internal void InitSelectionMutation_WithInvalidComponent_MutatesCorrectly()
	{
		//Arrange
		var sut = new InitSelectionMutation();
		var labState = new LabState(_componentConfigurations);
		var selectedComponentIndex = 0;
		var selectedComponentStateIndex = 0;

		var componentToSelect = labState.ComponentConfigurations.ElementAt(selectedComponentIndex).ComponentType.GetDisplayName();
		var componentStateToSelect = labState.ComponentConfigurations.ElementAt(selectedComponentIndex).ComponentStates.ElementAt(selectedComponentStateIndex).DisplayName;

		var command = new InitSelectionCommand
		{ ComponentName = "Nonsense", ComponentState = "Nonsense" };

		//Act
		var mutatedState = sut.Mutate(labState, command);

		//Assert
		mutatedState.SelectedComponentTypeDisplayName.ShouldBe(componentToSelect);
		mutatedState.SelectedComponentState.DisplayName.ShouldBe(componentStateToSelect);
	}
}
