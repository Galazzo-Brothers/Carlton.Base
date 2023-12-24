using AutoFixture;
using Carlton.Core.Components.Lab.Models.Common;
using Carlton.Core.Components.Lab.State.Mutations;
using Carlton.Core.Components.Lab.Test.Common;

namespace Carlton.Core.Components.Lab.Test.MutationTests;

public class ExpandedMenuItemMutationTests
{
    [Theory]
    [InlineData(1, 0, false)]
    [InlineData(10, 5, false)]
    [InlineData(3, 2, true)]
    [InlineData(5, 1, true)]
    public void ClearEventsMutation_MutatesCorrectly(int numOfAvailableStates, int index, bool isExpanded)
    {
        //Arrange
        var componentStates = new Fixture().CreateMany<ComponentAvailableStates>(numOfAvailableStates).ToList();
        var labState = LabStateFactory.BuildLabState() with
        {
            ComponentStates = componentStates
        };
        var mutation = new ExpandedMenuItemMutation();
        var command = new SelectMenuExpandedCommand(index, isExpanded);

        //Act
        var mutatedState = mutation.Mutate(labState, command);

        //Assert
        Assert.Equal(isExpanded, mutatedState.ComponentStates[index].IsExpanded);
    }
}
