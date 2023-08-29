using Carlton.Core.Components.Lab.State.Mutations;
using Carlton.Core.Components.Lab.Test.Common;

namespace Carlton.Core.Components.Lab.Test.MutationTests;

public class ClearEventsMutationTests
{
    [Fact]
    public void ClearEventsMutation_MutatesCorrectly()
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
        var mutation = new ClearEventsMutation();
        var command = new ClearEventsCommand();

        //Act
        var mutatedState = mutation.Mutate(labState, command);

        //Assert
        Assert.False(mutatedState.ComponentEvents.Any());
    }
}
