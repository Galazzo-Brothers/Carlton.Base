using AutoFixture;
using Bunit;
using Carlton.Core.Components.Flux.Models;
using Carlton.Core.Components.Lab.Test.Common;
using Carlton.Core.Components.Lab.Test.Mocks;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace Carlton.Core.Components.Lab.Test.ComponentTests;

public class ConnectedNavMenuTests : TestContext
{
    private readonly IFixture _fixture;

    public ConnectedNavMenuTests()
    {
        _fixture = new Fixture();
    }

    [Fact]
    public void ConnectedNavMenuComponentRendersCorrectly()
    {
        //Arrange
        var componentStates = new List<ComponentState>
        {
            new ComponentState("Test Component 1", typeof(DummyComponent), new ComponentParameters(new { TestText = "Test Component 1 State 1"}, ParameterObjectType.ParameterObject)),
            new ComponentState("Test Component 1", typeof(DummyComponent), new ComponentParameters(new { TestText = "Test Component 1 State 2"}, ParameterObjectType.ParameterObject)),
            new ComponentState("Test Component 2", typeof(object), new ComponentParameters(new { TestText = "Test Component 2 State 2"}, ParameterObjectType.ParameterObject))
        };

        var vm = new NavMenuViewModel(componentStates, 0);

        //Act
        var cut = RenderComponent<ConnectedNavMenu>(parameters => parameters
                .Add(p => p.ViewModel, vm));

        //Assert
        cut.MarkupMatches(@"
<div class=""accordion-select-group"" >
    <div class=""accordion-select"" >
        <div class=""content"" >
            <div class=""accordion-header selected""  >
                <span class=""accordion-icon-btn mdi mdi-icon mdi-24px mdi-minus-box-outline"" ></span>
                <span class=""item-group-name"" >DummyComponent</span>
            </div>
        <div class=""item-container"" >
            <div class=""item selected""  >
                <span class=""icon mdi mdi-icon mdi-12px mdi-bookmark"" ></span>
                <span class=""item-name"" >Test Component 1</span>
            </div>
        <div class=""item""  >
            <span class=""icon mdi mdi-icon mdi-12px mdi-bookmark"" ></span>
            <span class=""item-name"" >Test Component 1</span>
        </div>
    </div>
</div>
</div>
    <div class=""accordion-select"" >
        <div class=""content"" >
            <div class=""accordion-header""  >
                <span class=""accordion-icon-btn mdi mdi-icon mdi-24px mdi-plus-box-outline"" ></span>
                <span class=""item-group-name"" >Object</span>
            </div>
        <div class=""item-container collapsed"" >
            <div class=""item""  >
                <span class=""icon mdi mdi-icon mdi-12px mdi-bookmark"" ></span>
                <span class=""item-name"" >Test Component 2</span>
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
        var componentStates = _fixture.CreateMany<ComponentState>().ToList();
        var expectedState = componentStates[1];
        var vm = new NavMenuViewModel(componentStates, 1);

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
