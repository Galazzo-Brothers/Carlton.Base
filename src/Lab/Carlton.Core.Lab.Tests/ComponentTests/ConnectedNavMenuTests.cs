using AutoFixture;
using Bunit;
using Carlton.Core.Components.Flux.Models;
using Carlton.Core.Components.Lab.Models.Common;
using Carlton.Core.Components.Lab.Test.Common;
using Carlton.Core.Utilities.Extensions;

namespace Carlton.Core.Components.Lab.Test.ComponentTests;

public class ConnectedNavMenuTests : TestContext
{
    [Theory]
    [InlineData(3, 3, 0, 0)]
    [InlineData(5, 3, 0, 2)]
    [InlineData(10, 3, 0, 1)]
    public void ConnectedNavMenuComponentRendersCorrectly(
         int numOfItems,
        int numOfStates,
        int componentIndex,
        int stateIndex)
    {
        //Arrange
        var vm = CreateNavMenuViewModel(numOfItems, numOfStates, componentIndex, stateIndex);
        var expectedMarkup = BuildExpectedGroupsMarkup(vm.MenuItems, componentIndex, stateIndex);

        //Act
        var cut = RenderComponent<ConnectedNavMenu>(parameters => parameters
                .Add(p => p.ViewModel, vm));

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory]
    [InlineData(3, 3, 0, 0)]
    [InlineData(5, 3, 0, 2)]
    [InlineData(10, 3, 0, 1)]
    public void ConnectedNavMenuComponent_EventCallback(
        int numOfItems,
        int numOfStates,
        int componentIndex,
        int stateIndex)
    {
        //Arrange
        var command = new MutationCommand();
        var onComponentEventCalled = false;
        var vm = CreateNavMenuViewModel(numOfItems, numOfStates, componentIndex, stateIndex);
        var expectedState = vm.MenuItems.ElementAt(componentIndex).ComponentStates.ElementAt(stateIndex);

        var cut = RenderComponent<ConnectedNavMenu>(parameters => parameters
                .Add(p => p.ViewModel, vm)
                .Add(p => p.OnComponentEvent, cmd =>
                {
                    onComponentEventCalled = true;
                    command = cmd;
                }));

        var accordionElement = cut.FindAll(".accordion-select")[componentIndex];
        var btn = accordionElement.QuerySelectorAll(".item")[stateIndex];

        //Act
        btn.Click();

        //Assert
        Assert.True(onComponentEventCalled);
        Assert.IsType<SelectMenuItemCommand>(command);
        Assert.Equal(expectedState, command.Cast<SelectMenuItemCommand>().SelectedComponentState);
    }

    private static NavMenuViewModel CreateNavMenuViewModel(int numOfItems, int numOfStates, int componentIndex, int stateIndex)
    {
        var fixture = new Fixture();
        var availableStates = new List<ComponentAvailableStates>();

        for (var i = 0; i < numOfItems; i++)
        {
            var states = fixture.CreateMany<ComponentState>(numOfStates);
            availableStates.Add(new ComponentAvailableStates(fixture.Create<Type>(), fixture.Create<bool>(), states));
        }

        return new NavMenuViewModel(availableStates, componentIndex, stateIndex);
    }

    private static string BuildExpectedGroupsMarkup(IEnumerable<ComponentAvailableStates> components, int selectedGroupIndex, int selectedItemIndex)
    {
        var groupMarkup = string.Join(Environment.NewLine, components
            .Select((component, i) => $@"
<div class=""accordion-select"">
    <div class=""content"">
        <div class=""accordion-header {(selectedGroupIndex == i && selectedItemIndex != -1 ? "selected" : string.Empty)}"">
            <span class=""accordion-icon-btn mdi mdi-icon mdi-24px mdi-{(component.IsExpanded ? "minus" : "plus")}-box-outline""></span>
            <span class=""item-group-name"">{component.ComponentType.GetDisplayName()}</span>
        </div>
        <div class=""item-container {(component.IsExpanded ? string.Empty : "collapsed")}"">
            {BuildExpectedItemsMarkup(component.ComponentStates, ((selectedGroupIndex == i) ? selectedItemIndex : -1))}
        </div>
    </div>
</div>"));

        return $@"
<div class=""accordion-select-group"">
    {groupMarkup}
</div>";
    }

    private static string BuildExpectedItemsMarkup(IEnumerable<ComponentState> items, int selectedItemIndex)
    {
        return string.Join(Environment.NewLine, items.Select((item, i) =>
@$"<div class=""item {(selectedItemIndex == i ? "selected" : string.Empty)}"">
        <span class=""icon mdi mdi-icon mdi-12px mdi-bookmark""></span>
        <span class=""item-name"">{item.DisplayName}</span>
</div>"));
    }
}
