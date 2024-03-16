using Carlton.Core.Lab.Components.ParametersViewer;
using Carlton.Core.Lab.Models.Common;
namespace Carlton.Core.Lab.Test.Components.ParametersViewer;

public class UpdateParametersMutationTests
{
	[Theory, AutoData]
	public void UpdateParametersMutation_MutatesCorrectly(
		IEnumerable<ComponentConfigurations> componentConfigurations,
		UpdateParametersMutation sut,
		TestParameters expectedParameters)
	{
		//Arrange
		var state = new LabState(componentConfigurations);
		var command = new UpdateParametersCommand { Parameters = expectedParameters };

		//Act
		var mutatedState = sut.Mutate(state, command);

		//Assert
		mutatedState.SelectedComponentParameters.ShouldBe(command.Parameters);
	}
}
