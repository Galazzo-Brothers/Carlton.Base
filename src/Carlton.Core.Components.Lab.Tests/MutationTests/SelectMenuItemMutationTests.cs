using Carlton.Core.Components.Lab.State.Mutations;
using Carlton.Core.Components.Lab.Test.Common;
using Carlton.Core.Components.Lab.Test.Mocks;

namespace Carlton.Core.Components.Lab.Test.MutationTests;

public class SelectMenuItemMutationTests
{
    [Fact]
    public void SelectMenuItemMutation_MutatesCorrectly()
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

        var expectedComponentState = labState.ComponentStates[labState.ComponentStates.Count - 1];
        var mutation = new SelectMenuItemMutation();
        var command = new SelectMenuItemCommand(expectedComponentState);

        //Act
        var mutatedState = mutation.Mutate(labState, command);

        //Assert
        Assert.Equal(expectedComponentState, mutatedState.SelectedComponentState);
    }

    [Fact]
    public void SelectMenuItemMutation_SelectingInvalidState_ThrowsInvalidOperationException()
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

        var expectedComponentState = new ComponentState("Invalid State", typeof(DummyComponent), new ComponentParameters(new object(), ParameterObjectType.ParameterObject));
        var mutation = new SelectMenuItemMutation();
        var command = new SelectMenuItemCommand(expectedComponentState);

        //Act
        var ex = Assert.Throws<InvalidOperationException>(() => mutation.Mutate(labState, command));

        //Assert
        Assert.Equal("Only a previously existing NavMenutItem can be selected.", ex.Message);
    }
}
