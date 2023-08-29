using Carlton.Core.Components.Lab.State.Mutations.RefreshMutations;
using Carlton.Core.Components.Lab.Test.Common;

namespace Carlton.Core.Components.Lab.Test.MutationTests;

public class SourceViewerRefreshMutationTests
{
    [Fact]
    public void SourceViewerRefreshMutation_MutatesCorrectly()
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

        var expectedMarkup = "<div class='testing'>Hello World!</div>";
        var vm = new SourceViewerViewModel(expectedMarkup);
        var mutation = new SourceViewerRefreshMutation();

        //Act
        var mutatedState = mutation.Mutate(labState, vm);

        //Assert
        Assert.Equal(expectedMarkup, mutatedState.SelectedComponentMarkup);
    }
}
