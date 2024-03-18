using Carlton.Core.Lab.EventConsole;
using Carlton.Core.Lab.Models.Common;
namespace Carlton.Core.Lab.Test.Components.EventConsole;

public class ClearEventsMutationTests
{
	[Theory, AutoData]
	internal void ClearEventsMutation_MutatesCorrectly(
		IEnumerable<ComponentConfigurations> componentStates,
		IEnumerable<ComponentRecordedEvent> events,
		ClearEventsMutation sut,
		ClearEventsCommand command)
	{
		//Arrange
		var labState = new LabState(componentStates);
		labState = labState with
		{
			ComponentEvents = events
		};

		//Act
		var mutatedState = sut.Mutate(labState, command);

		//Assert
		mutatedState.ComponentEvents.ShouldBeEmpty();
	}
}
