using AutoFixture;
using Bunit;
using Carlton.Core.Components.Flux.Models;
using Carlton.Core.Components.Lab.Test.Common;

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
        var vm = _fixture.Create<NavMenuViewModel>() with { SelectedIndex = 1 };
        ComponentFactories.AddStub(type => type != typeof(ConnectedNavMenu)); //shallow render

        //Act
        var cut = RenderComponent<ConnectedNavMenu>(parameters => parameters
                .Add(p => p.ViewModel, vm));

        //Assert
        //No Exceptions
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
        Assert.Equal(expectedState, command.Cast<SelectMenuItemCommand>().SelectedComponentState);
    }
}
