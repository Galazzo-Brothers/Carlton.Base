using Carlton.Core.Components.Lab.State.Mutations;
using Carlton.Core.Components.Lab.Test.Common;

namespace Carlton.Core.Components.Lab.Test.MutationTests;

public class UpdateParametersMutationTests
{
    [Fact]
    public void UpdateParameterstMutation_MutatesCorrectly()
    {
        //Arrange
        var labState = LabStateFactory.BuildLabState() with
        {
            ComponentEvents = new List<ComponentRecordedEvent>
                {
                    new ComponentRecordedEvent("Event 1", new object { }),
                    new ComponentRecordedEvent("Event 2", new object { }),
                    new ComponentRecordedEvent("Event 3", new object { })
                }
        };

        var expectedParameterObject = new { Param1 = "This is a test", Param2 = 7 };
        var mutation = new UpdateParametersMutation();
        var command = new UpdateParametersCommand(expectedParameterObject);

        //Act
        var mutatedState = mutation.Mutate(labState, command);

        //Assert
        Assert.Equal(expectedParameterObject, mutatedState.SelectedComponentParameters.ParameterObj);
    }
}
