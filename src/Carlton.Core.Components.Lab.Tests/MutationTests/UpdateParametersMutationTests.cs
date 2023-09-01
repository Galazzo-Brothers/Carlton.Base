using AutoFixture;
using AutoFixture.Xunit2;
using Carlton.Core.Components.Lab.State.Mutations;
using Carlton.Core.Components.Lab.Test.Common;

namespace Carlton.Core.Components.Lab.Test.MutationTests;

public class UpdateParametersMutationTests
{
    [Theory, AutoData]
    public void UpdateParameterstMutation_MutatesCorrectly(string someText, int someInt)
    {
        //Arrange
        var labState = LabStateFactory.BuildLabState();
        var expectedParameterObject = new { Param1 = someText, Param2 = someInt };
        var mutation = new UpdateParametersMutation();
        var command = new UpdateParametersCommand(expectedParameterObject);

        //Act
        var mutatedState = mutation.Mutate(labState, command);

        //Assert
        Assert.Equal(expectedParameterObject, mutatedState.SelectedComponentParameters.ParameterObj);
    }
}
