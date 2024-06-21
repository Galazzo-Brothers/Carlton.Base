using Carlton.Core.Foundation.Web.ViewState;
using Carlton.Core.Lab.Components.NavMenu;
using Carlton.Core.Lab.Models.Common;
using Microsoft.Extensions.DependencyInjection;
namespace Carlton.Core.Lab.Test.Components.NavMenu;

public class ConnectedNavMenuComponentTests : TestContext
{
	[Theory, AutoData]
	public void ConnectedNavMenu_Markup_RendersCorrectly(NavMenuViewModel viewModel)
	{
		//Arrange
		var componentIndex = RandomUtilities.GetRandomIndex(viewModel.MenuItems.Count());
		var stateIndex = RandomUtilities.GetRandomIndex(viewModel.MenuItems.ElementAt(componentIndex).ComponentStates.Count());
		viewModel = viewModel with { SelectedComponentIndex = componentIndex, SelectedStateIndex = stateIndex };
		var expectedMarkup = BuildExpectedGroupsMarkup(viewModel);
		Services.AddSingleton<IViewStateService>(new ViewStateService());

		//Act
		var cut = RenderComponent<ConnectedNavMenu>(parameters => parameters
					.Add(p => p.ViewModel, viewModel));

		//Assert
		cut.MarkupMatches(expectedMarkup);
	}

	[Theory, AutoData]
	public void ConnectedNavMenu_OnGroupExpanded_RaisesComponentEvent(
		NavMenuViewModel viewModel)
	{
		//Arrange
		var onComponentEventCalled = false;
		var actualArgs = (object)null;
		var componentIndex = RandomUtilities.GetRandomIndex(viewModel.MenuItems.Count());
		var stateIndex = RandomUtilities.GetRandomIndex(viewModel.MenuItems.ElementAt(componentIndex).ComponentStates.Count());
		var expectedItem = viewModel.MenuItems.ElementAt(componentIndex).ComponentStates.ElementAt(stateIndex);
		viewModel = viewModel with { SelectedComponentIndex = componentIndex, SelectedStateIndex = stateIndex };
		Services.AddSingleton<IViewStateService>(new ViewStateService());

		var cut = RenderComponent<ConnectedNavMenu>(parameters => parameters
				.Add(p => p.ViewModel, viewModel)
				.Add(p => p.OnComponentEvent, args =>
				{
					onComponentEventCalled = true;
					actualArgs = args;
				}));

		var accordionElement = cut.FindAll(".accordion-select")[componentIndex];
		var btn = accordionElement.QuerySelectorAll(".item")[stateIndex];


		//Act
		btn.Click();

		//Assert
		onComponentEventCalled.ShouldBeTrue();
		var command = actualArgs.ShouldBeOfType<SelectMenuItemCommand>();
		command.ComponentIndex.ShouldBe(componentIndex);
		command.ComponentStateIndex.ShouldBe(stateIndex);
	}

	[Theory, AutoData]
	public void ConnectedNavMenu_OnItemSelected_RaisesComponentEvent(
		NavMenuViewModel viewModel)
	{
		//Arrange
		var onComponentEventCalled = false;
		var actualArgs = (object)null;
		var componentIndex = RandomUtilities.GetRandomIndex(viewModel.MenuItems.Count());
		var stateIndex = RandomUtilities.GetRandomIndex(viewModel.MenuItems.ElementAt(componentIndex).ComponentStates.Count());
		var expectedItem = viewModel.MenuItems.ElementAt(componentIndex).ComponentStates.ElementAt(stateIndex);
		viewModel = viewModel with { SelectedComponentIndex = componentIndex, SelectedStateIndex = stateIndex };
		Services.AddSingleton<IViewStateService>(new ViewStateService());

		var cut = RenderComponent<ConnectedNavMenu>(parameters => parameters
				.Add(p => p.ViewModel, viewModel)
				.Add(p => p.OnComponentEvent, args =>
				{
					onComponentEventCalled = true;
					actualArgs = args;
				}));

		var accordionElement = cut.FindAll(".accordion-select")[componentIndex];
		var btn = accordionElement.QuerySelectorAll(".item")[stateIndex];


		//Act
		btn.Click();

		//Assert
		onComponentEventCalled.ShouldBeTrue();
		var command = actualArgs.ShouldBeOfType<SelectMenuItemCommand>();
		command.ComponentIndex.ShouldBe(componentIndex);
		command.ComponentStateIndex.ShouldBe(stateIndex);
	}

	private static string BuildExpectedGroupsMarkup(NavMenuViewModel viewModel)
	{
		var selectedGroupIndex = viewModel.SelectedComponentIndex;
		var selectedItemIndex = viewModel.SelectedStateIndex;
		static bool isExpanded(int i) => i == 0;

		var groupMarkup = string.Join(Environment.NewLine, viewModel.MenuItems
			.Select((component, i) => $@"
<div class=""accordion-select"">
    <div class=""content"">
        <div class=""accordion-header {(selectedGroupIndex == i && selectedItemIndex != -1 ? "selected" : string.Empty)}"">
            <span class=""accordion-icon-btn mdi mdi-icon mdi-24px mdi-{(isExpanded(i) ? "minus" : "plus")}-box-outline""></span>
            <span class=""item-group-name"">{component.ComponentType.GetDisplayName()}</span>
        </div>
        <div class=""item-container {(isExpanded(i) ? string.Empty : "collapsed")}"">
            {BuildExpectedItemsMarkup(component.ComponentStates, (selectedGroupIndex == i ? selectedItemIndex : -1))}
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
