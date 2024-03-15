using Carlton.Core.Components.Navigation;
namespace Carlton.Core.Components.Tests.Navigation;

[Trait("Component", nameof(AccordionSelectGroup<int>))]
public class AccordionSelectGroupComponentTests : TestContext
{
	[Fact(DisplayName = "Markup Test")]
	public void AccordionSelectGroup_Markup_RendersCorrectly()
	{
		//Arrange
		var expectedGroups = TestAccordionSelectGroupBuilder<int>.BuildTestSelectGroups();
		var selectedGroupIndex = RandomUtilities.GetRandomIndex(expectedGroups.Count());
		var selectedItemIndex = RandomUtilities.GetRandomIndex(expectedGroups.ElementAt(selectedGroupIndex).Items.Count());

		//Act
		var cut = RenderComponent<AccordionSelectGroup<int>>(parameters => parameters
			.Add(p => p.Groups, expectedGroups)
			.Add(p => p.SelectedGroupIndex, selectedGroupIndex)
			.Add(p => p.SelectedItemIndex, selectedItemIndex));

		//Assert
		var expectedMarkup = BuildExpectedGroupsMarkup(expectedGroups, selectedGroupIndex, selectedItemIndex);
		cut.MarkupMatches(expectedMarkup);
	}

	[Fact(DisplayName = "Groups Parameter Test")]
	public void AccordionSelectGroup_GroupsParameter_RendersCorrectly()
	{
		//Arrange
		var expectedGroups = TestAccordionSelectGroupBuilder<int>.BuildTestSelectGroups();
		var selectedGroupIndex = RandomUtilities.GetRandomIndex(expectedGroups.Count());
		var selectedItemIndex = RandomUtilities.GetRandomIndex(expectedGroups.ElementAt(selectedGroupIndex).Items.Count());
		var expectedCount = expectedGroups.Count();
		var expectedGroupNames = expectedGroups.Select(_ => _.Name);

		//Act
		var cut = RenderComponent<AccordionSelectGroup<int>>(parameters => parameters
			.Add(p => p.Groups, expectedGroups)
			.Add(p => p.SelectedGroupIndex, selectedGroupIndex)
			.Add(p => p.SelectedItemIndex, selectedItemIndex));

		var actualCount = cut.FindAll(".accordion-select").Count;
		var actualGroupNames = cut.FindAll(".item-group-name").Select(_ => _.TextContent);

		//Assert
		actualCount.ShouldBe(expectedCount);
		actualGroupNames.ShouldBe(expectedGroupNames);
	}

	[Fact(DisplayName = "SelectedIndex Parameters Test")]
	public void AccordionSelectGroup_SelectedIndexParameter_RendersCorrectly()
	{
		//Arrange
		var expectedGroups = TestAccordionSelectGroupBuilder<int>.BuildTestSelectGroups();
		var selectedGroupIndex = RandomUtilities.GetRandomIndex(expectedGroups.Count());
		var selectedItemIndex = RandomUtilities.GetRandomIndex(expectedGroups.ElementAt(selectedGroupIndex).Items.Count());

		//Act
		var cut = RenderComponent<AccordionSelectGroup<int>>(parameters => parameters
			.Add(p => p.Groups, expectedGroups)
			.Add(p => p.SelectedGroupIndex, selectedGroupIndex)
			.Add(p => p.SelectedItemIndex, selectedItemIndex));

		var headers = cut.FindAll(".accordion-header").ToList();
		var allItems = cut.FindAll(".item").ToList();

		var selectedHeader = headers.ElementAt(selectedGroupIndex);
		headers.RemoveAt(selectedGroupIndex);
		var unselectedHeaderClassList = headers.SelectMany(_ => _.ClassList);

		var selectedItem = cut.Find(".item.selected");
		var unselectedItems = allItems.Where(_ => !_.Equals(selectedItem));
		var unselectedItemsClassList = unselectedItems.SelectMany(_ => _.ClassList);

		//Assert
		selectedHeader.ClassList.ShouldContain("selected");
		unselectedHeaderClassList.ShouldNotContain("selected");
		selectedItem.ClassList.ShouldContain("selected");
		unselectedItemsClassList.ShouldNotContain("selected");
	}

	[Fact(DisplayName = "SelectedIndex Empty Groups Parameters Test")]
	public void AccordionSelectGroup_EmptyGroups_SelectedIndexParameter_RendersCorrectly()
	{
		//Arrange
		var expectedGroups = Enumerable.Empty<AccordionSelectGroupModel<int>>();
		var selectedGroupIndex = -1;
		var selectedItemIndex = -1;

		//Act
		var cut = RenderComponent<AccordionSelectGroup<int>>(parameters => parameters
			.Add(p => p.Groups, expectedGroups)
			.Add(p => p.SelectedGroupIndex, selectedGroupIndex)
			.Add(p => p.SelectedItemIndex, selectedItemIndex));

		var headers = cut.FindAll(".accordion-header").ToList();
		var allItems = cut.FindAll(".item").ToList();

		//Assert
		headers.ShouldBeEmpty();
		allItems.ShouldBeEmpty();
	}


	[Fact(DisplayName = "SelectedIndex Empty Items Parameters Test")]
	public void AccordionSelectGroup_EmptyItems_SelectedIndexParameter_RendersCorrectly()
	{
		//Arrange
		var expectedGroups = Enumerable.Empty<AccordionSelectGroupModel<int>>();
		var selectedGroupIndex = -1;
		var selectedItemIndex = -1;

		//Act
		var cut = RenderComponent<AccordionSelectGroup<int>>(parameters => parameters
			.Add(p => p.Groups, expectedGroups)
			.Add(p => p.SelectedGroupIndex, selectedGroupIndex)
			.Add(p => p.SelectedItemIndex, selectedItemIndex));

		var allItems = cut.FindAll(".item").ToList();

		//Assert
		allItems.ShouldBeEmpty();
	}

	[Fact(DisplayName = "SelectedValue Property Test")]
	public void AccordionSelectGroup_SelectedValueProperty_ReturnsCorrectValue()
	{
		//Arrange
		var expectedGroups = TestAccordionSelectGroupBuilder<int>.BuildTestSelectGroups();
		var selectedGroupIndex = RandomUtilities.GetRandomIndex(expectedGroups.Count());
		var selectedItemIndex = RandomUtilities.GetRandomIndex(expectedGroups.ElementAt(selectedGroupIndex).Items.Count());
		var itemValue = (selectedGroupIndex == -1 || selectedItemIndex == -1) ? default :
		   expectedGroups.ElementAt(selectedGroupIndex).Items.ElementAt(selectedItemIndex).Value;

		//Act
		var cut = RenderComponent<AccordionSelectGroup<int>>(parameters => parameters
			.Add(p => p.Groups, expectedGroups)
			.Add(p => p.SelectedGroupIndex, selectedGroupIndex)
			.Add(p => p.SelectedItemIndex, selectedItemIndex));

		//Assert
		cut.Instance.SelectedValue.ShouldBe(itemValue);
	}

	[Fact(DisplayName = "Invalid SelectedItem Parameter Test")]
	public void AccordionSelectGroup_InvalidSelectedGroupIndexParameter_shouldThrowArgumentException()
	{
		//Arrange
		var expectedGroups = TestAccordionSelectGroupBuilder<int>.BuildTestSelectGroups();
		var selectedGroupIndex = RandomUtilities.GetRandomIndex(expectedGroups.Count());
		var selectedItemIndex = RandomUtilities.GetRandomIndex(expectedGroups.ElementAt(selectedGroupIndex).Items.Count());
		var badGroupIndex = expectedGroups.Count() + 10;

		//Act
		IRenderedComponent<AccordionSelectGroup<int>> act() => RenderComponent<AccordionSelectGroup<int>>(parameters => parameters
			.Add(p => p.Groups, expectedGroups)
			.Add(p => p.SelectedGroupIndex, badGroupIndex)
			.Add(p => p.SelectedItemIndex, selectedItemIndex));

		//Assert
		Should.Throw<ArgumentException>((Func<IRenderedComponent<AccordionSelectGroup<int>>>)act, "The provided index parameters are not valid.");
	}

	[Fact(DisplayName = "Invalid SelectedItem Parameter Test")]
	public void AccordionSelectGroup_InvalidSelectedItemIndexParameter_shouldThrowArgumentException()
	{
		//Arrange
		var expectedGroups = TestAccordionSelectGroupBuilder<int>.BuildTestSelectGroups();
		var selectedGroupIndex = RandomUtilities.GetRandomIndex(expectedGroups.Count());
		var selectedItemIndex = RandomUtilities.GetRandomIndex(expectedGroups.ElementAt(selectedGroupIndex).Items.Count());
		var badItemIndex = expectedGroups.ElementAt(selectedGroupIndex).Items.Count() + 10;

		//Act
		IRenderedComponent<AccordionSelectGroup<int>> act() => RenderComponent<AccordionSelectGroup<int>>(parameters => parameters
			.Add(p => p.Groups, expectedGroups)
			.Add(p => p.SelectedGroupIndex, selectedGroupIndex)
			.Add(p => p.SelectedItemIndex, badItemIndex));

		//Assert
		Should.Throw<ArgumentException>((Func<IRenderedComponent<AccordionSelectGroup<int>>>)act, "The provided index parameters are not valid.");
	}


	[Fact(DisplayName = "OnSelectedItemChanged Callback Parameter Test")]
	public void AccordionSelectGroup_OnSelectedItemChangedParameter_FiresCallback()
	{
		//Arrange
		var expectedGroups = TestAccordionSelectGroupBuilder<int>.BuildTestSelectGroups();
		var selectedGroupIndex = RandomUtilities.GetRandomIndex(expectedGroups.Count());
		var selectedItemIndex = RandomUtilities.GetRandomIndex(expectedGroups.ElementAt(selectedGroupIndex).Items.Count());
		var eventCalled = false;
		ItemSelectedEventArgs<int>? evt = null;
		var cut = RenderComponent<AccordionSelectGroup<int>>(parameters => parameters
			.Add(p => p.Groups, expectedGroups)
			.Add(p => p.SelectedGroupIndex, selectedGroupIndex)
			.Add(p => p.SelectedItemIndex, selectedItemIndex)
			.Add(p => p.OnItemSelected, (_) => { eventCalled = true; evt = _; }));

		var itemToClick = cut.Find($".accordion-select-group > .accordion-select:nth-child({selectedGroupIndex + 1}) > .content > .item-container > .item:nth-child({selectedItemIndex + 1})");

		//Act
		itemToClick.Click();

		//Assert
		eventCalled.ShouldBeTrue();
		evt?.GroupIndexID.ShouldBe(selectedGroupIndex);
		evt?.ItemIndexID.ShouldBe(selectedItemIndex);
	}

	[Fact(DisplayName = "OnGroupExpansionChanged Callback Parameter Test")]
	public void AccordionSelectGroup_OnGroupExpansionChangedParameter_FiresCallback()
	{
		//Arrange
		var eventCalled = false;
		GroupExpansionChangeEventArgs? evt = null;
		var expectedGroups = TestAccordionSelectGroupBuilder<int>.BuildTestSelectGroups();
		var selectedGroupIndex = RandomUtilities.GetRandomIndex(expectedGroups.Count());
		var selectedItemIndex = RandomUtilities.GetRandomIndex(expectedGroups.ElementAt(selectedGroupIndex).Items.Count());
		var expectedGroup = expectedGroups.ElementAt(selectedGroupIndex);

		var cut = RenderComponent<AccordionSelectGroup<int>>(parameters => parameters
			.Add(p => p.Groups, expectedGroups)
			.Add(p => p.SelectedGroupIndex, selectedGroupIndex)
			.Add(p => p.SelectedItemIndex, selectedItemIndex)
			.Add(p => p.OnGroupExpansionChange, (_) => { eventCalled = true; evt = _; }));

		var itemToClick = cut.Find($".accordion-select-group > .accordion-select:nth-child({selectedGroupIndex + 1}) > .content > .accordion-header");

		//Act
		itemToClick.Click();

		//Assert
		eventCalled.ShouldBeTrue();
		evt?.GroupIndexID.ShouldBe(selectedGroupIndex);
		evt?.IsExpanded.ShouldBe(!expectedGroup.IsExpanded);
	}

	[Fact(DisplayName = "Group Click Event Render Test")]
	public void AccordionSelectGroup_ClickEvent_RendersCorrectly()
	{
		//Arrange
		var expectedGroups = TestAccordionSelectGroupBuilder<int>.BuildTestSelectGroups();
		var selectedGroupIndex = RandomUtilities.GetRandomIndex(expectedGroups.Count());
		var selectedItemIndex = RandomUtilities.GetRandomIndex(expectedGroups.ElementAt(selectedGroupIndex).Items.Count());

		var expectedValue = expectedGroups.ElementAt(selectedGroupIndex).Items.ElementAt(selectedItemIndex).Value;
		var cut = RenderComponent<AccordionSelectGroup<int>>(parameters => parameters
			.Add(p => p.Groups, expectedGroups));

		var itemToClick = cut.Find($".accordion-select-group > .accordion-select:nth-child({selectedGroupIndex + 1}) > .content > .item-container > .item:nth-child({selectedItemIndex + 1})");

		//Act
		itemToClick.Click();

		var headers = cut.FindAll(".accordion-header").ToList();
		var allItems = cut.FindAll(".item").ToList();
		var selectedHeader = headers.ElementAt(selectedGroupIndex);
		headers.RemoveAt(selectedGroupIndex);
		var unselectedHeaders = headers;
		var selectedItem = cut.Find(".item.selected");
		var unselectedItems = allItems.Where(_ => !_.Equals(selectedItem));
		var unselectedHeaderClassList = unselectedHeaders.SelectMany(_ => _.ClassList);
		var unselectedItemsClassList = unselectedItems.SelectMany(_ => _.ClassList);

		//Assert
		selectedHeader.ClassList.ShouldContain("selected");
		unselectedHeaderClassList.ShouldNotContain("selected");
		selectedItem.ClassList.ShouldContain("selected");
		unselectedItemsClassList.ShouldNotContain("selected");
	}

	[Fact(DisplayName = "Group Click Event Selected Value Test")]
	public void AccordionSelectGroup_ClickEvent_SelectedValuePropertyCorrect()
	{
		//Arrange
		var expectedGroups = TestAccordionSelectGroupBuilder<int>.BuildTestSelectGroups();
		var selectedGroupIndex = RandomUtilities.GetRandomIndex(expectedGroups.Count());
		var selectedItemIndex = RandomUtilities.GetRandomIndex(expectedGroups.ElementAt(selectedGroupIndex).Items.Count());

		var expectedValue = expectedGroups.ElementAt(selectedGroupIndex).Items.ElementAt(selectedItemIndex).Value;
		var cut = RenderComponent<AccordionSelectGroup<int>>(parameters => parameters
			.Add(p => p.Groups, expectedGroups));

		var itemToClick = cut.Find($".accordion-select-group > .accordion-select:nth-child({selectedGroupIndex + 1}) > .content > .item-container > .item:nth-child({selectedItemIndex + 1})");

		//Act
		itemToClick.Click();
		var value = cut.Instance.SelectedValue;

		//Assert
		value.ShouldBe(expectedValue);
	}

	private static string BuildExpectedGroupsMarkup(IEnumerable<AccordionSelectGroupModel<int>> groups, int selectedGroupIndex, int selectedItemIndex)
	{
		var groupMarkup = string.Join(Environment.NewLine, groups
			.Select((group, i) => $@"
<div class=""accordion-select"">
    <div class=""content"">
        <div class=""accordion-header {(selectedGroupIndex == i && selectedItemIndex != -1 ? "selected" : string.Empty)}"">
            <span class=""accordion-icon-btn mdi mdi-icon mdi-24px mdi-{(group.IsExpanded ? "minus" : "plus")}-box-outline""></span>
            <span class=""item-group-name"">{group.Name}</span>
        </div>
        <div class=""item-container {(group.IsExpanded ? string.Empty : "collapsed")}"">
            {BuildExpectedItemsMarkup(group.Items, ((selectedGroupIndex == i) ? selectedItemIndex : -1))}
        </div>
    </div>
</div>"));

		return $@"
<div class=""accordion-select-group"">
    {groupMarkup}
</div>";
	}

	private static string BuildExpectedItemsMarkup(IEnumerable<AccordionSelectModel<int>> items, int selectedItemIndex)
	{
		return string.Join(Environment.NewLine, items.Select((item, i) =>
@$"<div class=""item {(selectedItemIndex == i ? "selected" : string.Empty)}"">
        <span class=""icon mdi mdi-icon mdi-12px mdi-bookmark""></span>
        <span class=""item-name"">{item.Name}</span>
</div>"));
	}
}
