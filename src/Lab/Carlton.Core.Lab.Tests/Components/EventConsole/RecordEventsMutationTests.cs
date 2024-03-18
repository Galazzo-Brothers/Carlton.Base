using Carlton.Core.Lab.Components.ComponentViewer;
using Carlton.Core.Lab.Models.Common;
namespace Carlton.Core.Lab.Test.Components.EventConsole;

public class RecordEventsMutationTests
{
	[Theory, AutoData]
	internal void RecordEventMutation_MutatesCorrectly(
		IEnumerable<ComponentConfigurations> componentStates,
		RecordEventMutation sut,
		RecordEventCommand command)
	{
		//Arrange
		var labState = new LabState(componentStates);
		labState = labState with
		{
			ComponentEvents = new List<ComponentRecordedEvent>()
		};

		//Act
		var mutatedState = sut.Mutate(labState, command);

		//Assert
		mutatedState.ComponentEvents.ShouldNotBeEmpty();
		mutatedState.ComponentEvents.First().Name.ShouldBe(command.RecordedEventName);
		mutatedState.ComponentEvents.First().EventObj.ShouldBe(command.EventArgs);
	}
}
