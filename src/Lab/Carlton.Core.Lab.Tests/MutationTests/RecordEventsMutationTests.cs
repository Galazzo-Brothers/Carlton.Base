using AutoFixture;
using Carlton.Core.Components.Lab.State.Mutations;
using Carlton.Core.Components.Lab.Test.Common;

namespace Carlton.Core.Components.Lab.Test.MutationTests;

public class RecordEventsMutationTests
{
    private readonly IFixture _fixture;

    public RecordEventsMutationTests()
    {
        _fixture = new Fixture();
    }

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

        var mutation = new RecordEventMutation();
        var command = _fixture.Create<RecordEventCommand>();

        //Act
        var mutatedState = mutation.Mutate(labState, command);

        //Assert
        Assert.Equal(4, mutatedState.ComponentEvents.Count());
        Assert.Equal(command.RecordedEventName, mutatedState.ComponentEvents.Last().Name);
        Assert.Equal(command.EventArgs, mutatedState.ComponentEvents.Last().EventObj);
    }
}
