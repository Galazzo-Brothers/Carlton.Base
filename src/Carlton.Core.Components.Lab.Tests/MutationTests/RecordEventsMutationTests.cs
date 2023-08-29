using Carlton.Core.Components.Lab.State.Mutations;
using Carlton.Core.Components.Lab.Test.Common;
using System.Net.Http.Headers;

namespace Carlton.Core.Components.Lab.Test.MutationTests;

public class RecordEventsMutationTests
{
    [Fact]
    public void RecordEventMutation_MutatesCorrectly()
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

        var expectedEventName = "Some New Event";
        var expectedEventArgs = new { Param1 = "Test Param", Param2 = false };
        var mutation = new RecordEventMutation();
        var command = new RecordEventCommand(expectedEventName, expectedEventArgs);

        //Act
        var mutatedState = mutation.Mutate(labState, command);

        //Assert
        Assert.Equal(4, mutatedState.ComponentEvents.Count());
        Assert.Equal(expectedEventName, mutatedState.ComponentEvents.Last().Name);
        Assert.Equal(expectedEventArgs, mutatedState.ComponentEvents.Last().EventObj);
    }
}
