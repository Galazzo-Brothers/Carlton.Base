using Carlton.Core.Components.Library.Tests.Common;

namespace Carlton.Core.Components.Library.Tests;

[Trait("Component", nameof(AccordionSelectGroup<int>))]
public class AccordionSelectGroupComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test")]
    [InlineData(2, new int[] { 2, 2 }, new bool[] {true, false}, 1, 1)]
    [InlineData(5, new int[] { 1, 3, 1, 4, 5 }, new bool[] { true, true, false, true, false }, 3, 2)]
    [InlineData(0, new int[] { }, new bool[] { }, -1, -1)]
    [InlineData(1, new int[] { 0 }, new bool[] { false }, 0, -1)]
    public void AccordionSelectGroup_Markup_RendersCorrectly(int numOfGroups, int[] numOfItems, bool[] isExpanded,
        int selectedGroupIndex, int selectedItemIndex)
    {
        //Arrange
        var groups = new TestAccordionSelectGroupBuilder<int>()
            .BuildTestSelectGroup(numOfGroups, numOfItems, isExpanded);

        //Act
        var cut = RenderComponent<AccordionSelectGroup<int>>(parameters => parameters
            .Add(p => p.Groups, groups)
            .Add(p => p.SelectedGroupIndex, selectedGroupIndex)
            .Add(p => p.SelectedItemIndex, selectedItemIndex));

        //Assert
        var expectedMarkup = BuildExpectedGroupsMarkup(groups, selectedGroupIndex, selectedItemIndex);
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory(DisplayName = "Groups Parameter Test")]
    [InlineData(2, new int[] { 2, 2 }, new bool[] { true, false }, 1, 1)]
    [InlineData(5, new int[] { 1, 3, 1, 4, 5 }, new bool[] { true, true, false, true, false }, 3, 2)]
    [InlineData(0, new int[] { }, new bool[] { }, -1, -1)]
    [InlineData(1, new int[] { 0 }, new bool[] { false }, 0, -1)]
    public void AccordionSelectGroup_GroupsParam_RendersCorrectly(int numOfGroups, int[] numOfItems, bool[] isExpanded,
        int selectedGroupIndex, int selectedItemIndex)
    {
        //Arrange
        var groups = new TestAccordionSelectGroupBuilder<int>()
            .BuildTestSelectGroup(numOfGroups, numOfItems, isExpanded);
        var expectedCount = groups.Count();
        var expectedGroupNames = groups.Select(_ => _.Name);

        //Act
        var cut = RenderComponent<AccordionSelectGroup<int>>(parameters => parameters
            .Add(p => p.Groups, groups)
            .Add(p => p.SelectedGroupIndex, selectedGroupIndex)
            .Add(p => p.SelectedItemIndex, selectedItemIndex));

        var actualCount = cut.FindAll(".accordion-select").Count;
        var actualGroupNames = cut.FindAll(".item-group-name").Select(_ => _.TextContent);

        //Assert
        Assert.Equal(expectedCount, actualCount);
        Assert.Equal(expectedGroupNames, actualGroupNames);
    }

    [Theory(DisplayName = "SelectedIndex Parameters Test")]
    [InlineData(2, new int[] { 2, 2 }, new bool[] { true, false }, 1, 1)]
    [InlineData(5, new int[] { 1, 3, 1, 4, 5 }, new bool[] { true, true, false, true, false }, 3, 2)]
    public void AccordionSelectGroup_SelectedIndexParam_RendersCorrectly(
        int numOfGroups, int[] numOfItems, bool[] isExpanded,
        int selectedGroupIndex, int selectedItemIndex)
    {
        //Arrange
        var groups = new TestAccordionSelectGroupBuilder<int>()
            .BuildTestSelectGroup(numOfGroups, numOfItems, isExpanded);
      
        //Act
        var cut = RenderComponent<AccordionSelectGroup<int>>(parameters => parameters
            .Add(p => p.Groups, groups)
            .Add(p => p.SelectedGroupIndex, selectedGroupIndex)
            .Add(p => p.SelectedItemIndex, selectedItemIndex));

        var headers = cut.FindAll(".accordion-header").ToList();
        var allItems = cut.FindAll(".item").ToList();

        var selectedHeader = headers.ElementAt(selectedGroupIndex);
        headers.RemoveAt(selectedGroupIndex);
        var unselectedHeaders = headers;

        var selectedItem = cut.Find(".item.selected");
        var unselectedItems = allItems.Where(_ => !_.Equals(selectedItem));

        //Assert
        Assert.Contains("selected", selectedHeader.ClassList);
        Assert.DoesNotContain("selected", unselectedHeaders.SelectMany(_ => _.ClassList));
        Assert.Contains("selected", selectedItem.ClassList);
        Assert.DoesNotContain("selected", unselectedItems.SelectMany(_ => _.ClassList));
    }

    [Theory(DisplayName = "SelectedIndex Empty Groups Parameters Test")]
    [InlineData(0, new int[] { }, new bool[] { }, -1, -1)]
    public void AccordionSelectGroup_EmptyGroups_SelectedIndexParam_RendersCorrectly(
    int numOfGroups, int[] numOfItems, bool[] isExpanded,
    int selectedGroupIndex, int selectedItemIndex)
    {
        //Arrange
        var groups = new TestAccordionSelectGroupBuilder<int>()
            .BuildTestSelectGroup(numOfGroups, numOfItems, isExpanded);

        //Act
        var cut = RenderComponent<AccordionSelectGroup<int>>(parameters => parameters
            .Add(p => p.Groups, groups)
            .Add(p => p.SelectedGroupIndex, selectedGroupIndex)
            .Add(p => p.SelectedItemIndex, selectedItemIndex));

        var headers = cut.FindAll(".accordion-header").ToList();
        var allItems = cut.FindAll(".item").ToList();

        //Assert
        Assert.Empty(headers);
        Assert.Empty(allItems);
    }


    [Theory(DisplayName = "SelectedIndex Empty Items Parameters Test")]
    [InlineData(1, new int[] { 0 }, new bool[] { false }, 0, -1)]
    [InlineData(3, new int[] { 0, 0, 0 }, new bool[] { false, false, false }, 0, -1)]
    public void AccordionSelectGroup_EmptyItems_SelectedIndexParam_RendersCorrectly(
    int numOfGroups, int[] numOfItems, bool[] isExpanded,
    int selectedGroupIndex, int selectedItemIndex)
    {
        //Arrange
        var groups = new TestAccordionSelectGroupBuilder<int>()
            .BuildTestSelectGroup(numOfGroups, numOfItems, isExpanded);

        //Act
        var cut = RenderComponent<AccordionSelectGroup<int>>(parameters => parameters
            .Add(p => p.Groups, groups)
            .Add(p => p.SelectedGroupIndex, selectedGroupIndex)
            .Add(p => p.SelectedItemIndex, selectedItemIndex));

        var headers = cut.FindAll(".accordion-header").ToList();
        var allItems = cut.FindAll(".item").ToList();

        //Assert
        Assert.Empty(allItems);
    }

    [Theory(DisplayName = "SelectedValue Property Test")]
    [InlineData(2, new int[] { 2, 2 }, new bool[] { true, false }, 1, 1)]
    [InlineData(5, new int[] { 1, 3, 1, 4, 5 }, new bool[] { true, true, false, true, false }, 3, 2)]
    [InlineData(0, new int[] { }, new bool[] { }, -1, -1)]
    [InlineData(1, new int[] { 0 }, new bool[] { false }, 0, -1)]
    public void AccordionSelectGroup_SelectedValueProperty_ReturnsCorrectValue(
      int numOfGroups, int[] numOfItems, bool[] isExpanded,
      int selectedGroupIndex, int selectedItemIndex)
    {
        //Arrange
        var groups = new TestAccordionSelectGroupBuilder<int>()
            .BuildTestSelectGroup(numOfGroups, numOfItems, isExpanded);
        var itemValue = (selectedGroupIndex == -1 || selectedItemIndex == -1) ? default :
            groups.ElementAt(selectedGroupIndex).Items.ElementAt(selectedItemIndex).Value;

        //Act
        var cut = RenderComponent<AccordionSelectGroup<int>>(parameters => parameters
            .Add(p => p.Groups, groups)
            .Add(p => p.SelectedGroupIndex, selectedGroupIndex)
            .Add(p => p.SelectedItemIndex, selectedItemIndex));

        //Assert
        Assert.Equal(itemValue, cut.Instance.SelectedValue);
    }

    [Theory(DisplayName = "Invalid SelectedItem Parameter Test")]
    [InlineData(5, new int[] { 1, 3, 1, 4, 5 }, new bool[] { true, true, false, true, false }, -3, 2)]
    [InlineData(5, new int[] { 1, 3, 1, 4, 5 }, new bool[] { true, true, false, true, false }, 3, -2)]
    [InlineData(5, new int[] { 1, 3, 1, 4, 5 }, new bool[] { true, true, false, true, false }, -3, -2)]
    [InlineData(5, new int[] { 1, 3, 1, 4, 5 }, new bool[] { true, true, false, true, false }, 10, 2)]
    [InlineData(5, new int[] { 1, 3, 1, 4, 5 }, new bool[] { true, true, false, true, false }, 3, 10)]
    [InlineData(0, new int[] { }, new bool[] { }, 5, -1)]
    [InlineData(1, new int[] { 0 }, new bool[] { false }, 0, 1)]
    public void AccordionSelectGroup_InvalidSelectedIndexParam_shouldThrowArgumentException(
        int numOfGroups, int[] numOfItems, bool[] isExpanded,
        int selectedGroupIndex, int selectedItemIndex)
    {
        //Arrange
        var groups = new TestAccordionSelectGroupBuilder<int>()
            .BuildTestSelectGroup(numOfGroups, numOfItems, isExpanded);

        //Act
        var ex = Assert.Throws<ArgumentException>(() => RenderComponent<AccordionSelectGroup<int>>(parameters => parameters
            .Add(p => p.Groups, groups)
            .Add(p => p.SelectedGroupIndex, selectedGroupIndex)
            .Add(p => p.SelectedItemIndex, selectedItemIndex)));

        Assert.Equal("The provided index parameters are not valid.", ex.Message);
    }


    [Theory(DisplayName = "OnSelectedItemChanged Callback Parameter Test")]
    [InlineData(2, new int[] { 2, 2 }, new bool[] { true, false }, 1, 1)]
    [InlineData(5, new int[] { 1, 3, 1, 4, 5 }, new bool[] { true, true, false, true, false }, 3, 2)]
    public void AccordionSelectGroup_OnSelectedItemChangedParam_FiresCallback(
        int numOfGroups, int[] numOfItems, bool[] isExpanded,
        int selectedGroupIndex, int selectedItemIndex)
    {
        //Arrange
        var groups = new TestAccordionSelectGroupBuilder<int>()
          .BuildTestSelectGroup(numOfGroups, numOfItems, isExpanded);
        var eventCalled = false;
        SelectItemChangedEventArgs<int>? evt = null;
        var cut = RenderComponent<AccordionSelectGroup<int>>(parameters => parameters
            .Add(p => p.Groups, groups)
            .Add(p => p.SelectedGroupIndex, selectedGroupIndex)
            .Add(p => p.SelectedItemIndex, selectedItemIndex)
            .Add(p => p.OnSelectedItemChanged, (_) => { eventCalled = true; evt = _; }));

        var itemToClick = cut.Find($".accordion-select-group > .accordion-select:nth-child({selectedGroupIndex + 1}) > .content > .item-container > .item:nth-child({selectedItemIndex + 1})");

        //Act
        itemToClick.Click();

        //Assert
        Assert.True(eventCalled);
        Assert.Equal(selectedGroupIndex, evt?.GroupIndexID);
        Assert.Equal(selectedItemIndex, evt?.ItemIndexID);
    }

    [Theory(DisplayName = "Group Click Event Render Test")]
    [InlineData(2, new int[] { 2, 2 }, new bool[] { true, false }, 1, 1)]
    [InlineData(5, new int[] { 1, 3, 1, 4, 5 }, new bool[] { true, true, false, true, false }, 3, 2)]
    public void AccordionSelectGroup_ClickEvent_RendersCorrectly(
        int numOfGroups, int[] numOfItems, bool[] isExpanded,
        int selectedGroupIndex, int selectedItemIndex)
    {
        //Arrange
        var groups = new TestAccordionSelectGroupBuilder<int>()
            .BuildTestSelectGroup(numOfGroups, numOfItems, isExpanded);

        var expectedValue = groups.ElementAt(selectedGroupIndex).Items.ElementAt(selectedItemIndex).Value;
        var cut = RenderComponent<AccordionSelectGroup<int>>(parameters => parameters
            .Add(p => p.Groups, groups));

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

        //Assert
        Assert.Contains("selected", selectedHeader.ClassList);
        Assert.DoesNotContain("selected", unselectedHeaders.SelectMany(_ => _.ClassList));
        Assert.Contains("selected", selectedItem.ClassList);
        Assert.DoesNotContain("selected", unselectedItems.SelectMany(_ => _.ClassList));
    }

    [Theory(DisplayName = "Group Click Event Selected Value Test")]
    [InlineData(2, new int[] { 2, 2 }, new bool[] { true, false }, 1, 1)]
    [InlineData(5, new int[] { 1, 3, 1, 4, 5 }, new bool[] { true, true, false, true, false }, 3, 2)]
    public void AccordionSelectGroup_ClickEvent_SelectedValuePropertyCorrect(
       int numOfGroups, int[] numOfItems, bool[] isExpanded,
       int selectedGroupIndex, int selectedItemIndex)
    {
        //Arrange
        var groups = new TestAccordionSelectGroupBuilder<int>()
            .BuildTestSelectGroup(numOfGroups, numOfItems, isExpanded);

        var expectedValue = groups.ElementAt(selectedGroupIndex).Items.ElementAt(selectedItemIndex).Value;
        var cut = RenderComponent<AccordionSelectGroup<int>>(parameters => parameters
            .Add(p => p.Groups, groups));

        var itemToClick = cut.Find($".accordion-select-group > .accordion-select:nth-child({selectedGroupIndex + 1}) > .content > .item-container > .item:nth-child({selectedItemIndex + 1})");

        //Act
        itemToClick.Click();
        var value = cut.Instance.SelectedValue;

        //Assert
        Assert.Equal(expectedValue, value);;
    }

    private static string BuildExpectedGroupsMarkup(IEnumerable<SelectGroup<int>> groups , int selectedGroupIndex, int selectedItemIndex)
    {
        var groupMakerup = string.Join(Environment.NewLine, groups
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
    {groupMakerup}
</div>";
    }

    private static string BuildExpectedItemsMarkup(IEnumerable<SelectItem<int>> items, int selectedItemIndex)
    {
        return string.Join(Environment.NewLine, items.Select((item, i) =>
@$"<div class=""item {(selectedItemIndex == i ? "selected" : string.Empty)}"">
        <span class=""icon mdi mdi-icon mdi-12px mdi-bookmark""></span>
        <span class=""item-name"">{item.Name}</span>
</div>"));
    }
}
