using Bunit;
using Carlton.Core.Components.Flux.Models;
using Carlton.Core.Components.Lab.Models;
using Carlton.Core.Components.Lab.Test.Common;
using Carlton.Core.Components.Lab.Test.Mocks;

namespace Carlton.Core.Components.Lab.Test;

public class ConnectedNavMenuTests : TestContext
{
    [Fact]
    public void ConnectedNavMenuComponentRendersCorrectly()
    {
        //Arrange
        var componentStates = new List<ComponentState>
        {
            new ComponentState("Test Component 1", typeof(DummyComponent), new ComponentParameters(new { TestText = "Test 1"}, ParameterObjectType.ParameterObject)),
            new ComponentState("Test Component 1", typeof(DummyComponent), new ComponentParameters(new { TestText = "Test 2"}, ParameterObjectType.ParameterObject)),
            new ComponentState("Test Component 1", typeof(DummyComponent), new ComponentParameters(new { TestText = "Test 3"}, ParameterObjectType.ParameterObject))
        };


        var vm = new NavMenuViewModel(componentStates, componentStates.First());

        //Act
        var cut = RenderComponent<ConnectedNavMenu>(parameters => parameters
                .Add(p => p.ViewModel, vm));

        //Assert
        cut.MarkupMatches(@"
 <div class="" accordion-select-group"">
   <div class="" accordion-select"">
     <div class="" content"">
       <div class="" accordion-header selected"">
         <span class="" accordion-icon-btn mdi mdi-icon mdi-24px mdi-plus-box-outline""></span>
         <span class="" item-group-name"">DummyComponent</span>
       </div>
       <div class="" item-container collapsed"">
         <div class="" item selected"">
           <span class="" icon mdi mdi-icon mdi-12px mdi-bookmark""></span>
           <span class="" item-name"">Test Component 1</span>
         </div>
         <div class="" item"">
           <span class="" icon mdi mdi-icon mdi-12px mdi-bookmark""></span>
           <span class="" item-name"">Test Component 1</span>
         </div>
         <div class="" item"">
           <span class="" icon mdi mdi-icon mdi-12px mdi-bookmark""></span>
           <span class="" item-name"">Test Component 1</span>
         </div>
       </div>
     </div>
   </div>
 </div>");
    }

    [Fact]
    public void ConnectedNavMenuComponent_EventCallback()
    {
        //Arrange
        var command = new MutationCommand();
        var onComponentEventCalled = false;

        var componentStates = new List<ComponentState>
        {
            new ComponentState("Test Component 1", typeof(DummyComponent), new ComponentParameters(new { TestText = "Test 1"}, ParameterObjectType.ParameterObject)),
            new ComponentState("Test Component 1", typeof(DummyComponent), new ComponentParameters(new { TestText = "Test 2"}, ParameterObjectType.ParameterObject)),
            new ComponentState("Test Component 1", typeof(DummyComponent), new ComponentParameters(new { TestText = "Test 3"}, ParameterObjectType.ParameterObject))
        };

        var expectedState = componentStates[1];
        var vm = new NavMenuViewModel(componentStates, componentStates.First());

        var cut = RenderComponent<ConnectedNavMenu>(parameters => parameters
                .Add(p => p.ViewModel, vm)
                .Add(p => p.OnComponentEvent, cmd =>
                {
                    onComponentEventCalled = true;
                    command = cmd;
                }));

        var btn = cut.FindAll(".item")[1];

        //Act
        btn.Click();

        //Assert
        Assert.True(onComponentEventCalled);
        Assert.IsType<SelectMenuItemCommand>(command);
        Assert.Equal(expectedState, command.Cast<SelectMenuItemCommand>().ComponentState);
    }
}
