using AutoFixture;
using AutoFixture.Xunit2;
using Carlton.Core.Components.Library.Tests.Common;
using Carlton.Core.Utilities.UnitTesting;

namespace Carlton.Core.Components.Library.Tests;

[Trait("Component", nameof(AccordionSelectGroup<int>))]
public class AccordionSelectGroupComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test"), AutoData]
    public void AccordionSelectGroup_Markup_RendersCorrectly(IEnumerable<SelectGroup<int>> templateGroups)
    {
        //Arrange
        var groups = templateGroups.FixIndexes().ToList();

        var expectedMarkup = @$"
<div class=""accordion-select-group"">
    <div class=""accordion-select"">
        <div class=""content"">
            <div class=""accordion-header"">
                <span class=""accordion-icon-btn mdi mdi-icon mdi-24px mdi-minus-box-outline""></span>
                <span class=""item-group-name"">{groups[0].Name}</span>
            </div>
        <div class=""item-container"">
            <div class=""item"">
                <span class=""icon mdi mdi-icon mdi-12px mdi-bookmark""></span>
                <span class=""item-name"">{groups[0].Items.ElementAt(0).Name}</span>
            </div>
            <div class=""item"">
                <span class=""icon mdi mdi-icon mdi-12px mdi-bookmark""></span>
                <span class=""item-name"">{groups[0].Items.ElementAt(1).Name}</span>
            </div>
        </div>
    </div>
</div>
<div class=""accordion-select"">
    <div class=""content"">
        <div class=""accordion-header"">
            <span class=""accordion-icon-btn mdi mdi-icon mdi-24px mdi-plus-box-outline""></span>
            <span class=""item-group-name"">{groups[1].Name}</span>
        </div>
        <div class=""item-container collapsed"">
            <div class=""item"">
                <span class=""icon mdi mdi-icon mdi-12px mdi-bookmark""></span>
                <span class=""item-name"">{groups[1].Items.ElementAt(0).Name}</span>
            </div>
            <div class=""item"">
                <span class=""icon mdi mdi-icon mdi-12px mdi-bookmark""></span>
                <span class=""item-name"">{groups[1].Items.ElementAt(1).Name}</span>
            </div>
        </div>
    </div>
</div>";
      

        //Act
        var cut = RenderComponent<AccordionSelectGroup<int>>(parameters => parameters
            .Add(p => p.Groups, groups));

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory(DisplayName = "Groups Parameter Test"), AutoData]
    public void AccordionSelectGroup_GroupsParam_RendersCorrectly(IEnumerable<SelectGroup<int>> groups)
    {
        //Arrange
        var expectedCount = groups.Count();
        var expectedGroupNames = groups.Select(_ => _.Name);

        //Act
        var cut = RenderComponent<AccordionSelectGroup<int>>(parameters => parameters
            .Add(p => p.Groups, groups));

        var actualCount = cut.FindAll(".accordion-select").Count;
        var actualGroupNames = cut.FindAll(".item-group-name").Select(_ => _.TextContent);

        //Assert
        Assert.Equal(expectedCount, actualCount);
        Assert.Equal(expectedGroupNames, actualGroupNames);
    }

    [Theory(DisplayName = "SelectedItem Parameter Test"), AutoData]
    public void AccordionSelectGroup_SelectedItemParam_shouldRenderCorrectly(IEnumerable<SelectGroup<int>> templateGroups)
    {
        //Arrange
        var groups = templateGroups.FixIndexes();

        var groupIndex = TestingRndUtilities.GetRandomActiveIndex(groups.Count());
        var itemIndex = TestingRndUtilities.GetRandomActiveIndex(groups.ElementAt(groupIndex).Items.Count());
        var itemValue = groups.ElementAt(groupIndex).Items.ElementAt(itemIndex).Value;

        //Act
        var cut = RenderComponent<AccordionSelectGroup<int>>(parameters => parameters
            .Add(p => p.Groups, groups)
            .Add(p => p.SelectedItem, itemValue));

        var headers = cut.FindAll(".accordion-header").ToList();
        var allItems = cut.FindAll(".item").ToList();
       
        var selectedHeader = headers.ElementAt(groupIndex);
        headers.RemoveAt(groupIndex);
        var unselectedHeaders = headers;

        var selectedItem = cut.Find(".item.selected");
        var unselectedItems = allItems.Where(_ => !_.Equals(selectedItem));

        //Assert
        Assert.Equal(itemValue, cut.Instance.SelectedItem);
        Assert.Contains("selected", selectedHeader.ClassList);
        Assert.DoesNotContain("selected", unselectedHeaders.SelectMany(_ => _.ClassList));
        Assert.Contains("selected", selectedItem.ClassList);
        Assert.DoesNotContain("selected", unselectedItems.SelectMany(_ => _.ClassList));
    }

    [Theory(DisplayName = "OnSelectedItemChanged Callback Parameter Test"), AutoData]
    public void AccordionSelectGroup_OnSelectedItemChangedParam_FiresCallback(IEnumerable<SelectGroup<int>> groupTemplates)
    {
        //Arrange
        var groups = groupTemplates.FixIndexes();
        var selectedGroupIndex = TestingRndUtilities.GetRandomActiveIndex(groups.Count());
        var selectedItemIndex = TestingRndUtilities.GetRandomActiveIndex(groups.ElementAt(selectedGroupIndex).Items.Count());
        
        var eventCalled = false;
        SelectItemChangedEventArgs<int>? evt = null;
        var cut = RenderComponent<AccordionSelectGroup<int>>(parameters => parameters
            .Add(p => p.Groups, groups)
            .Add(p => p.OnSelectedItemChanged, (_) => { eventCalled = true; evt = _; }));

        var itemToClick = cut.Find($".accordion-select-group > .accordion-select:nth-child({selectedGroupIndex + 1}) > .content > .item-container > .item:nth-child({selectedItemIndex + 1})");

        //Act
        itemToClick.Click();

        //Assert
        Assert.True(eventCalled);
        Assert.Equal(selectedGroupIndex, evt?.GroupIndexID);
        Assert.Equal(selectedItemIndex, evt?.ItemIndexID);
    }

    [Theory(DisplayName = "Group Click Event Test"), AutoData]
    public void AccordionSelectGroup_ClickEvent_RendersCorrectly(IEnumerable<SelectGroup<int>> groupTemplates)
    {
        //Arrange
        var groups = groupTemplates.FixIndexes();
        var selectedGroupIndex = TestingRndUtilities.GetRandomActiveIndex(groups.Count());
        var selectedItemIndex = TestingRndUtilities.GetRandomActiveIndex(groups.ElementAt(selectedGroupIndex).Items.Count());
        var expectedValue = groups.ElementAt(selectedGroupIndex).Items.ElementAt(selectedItemIndex).Value;
        var cut = RenderComponent<AccordionSelectGroup<int>>(parameters => parameters
            .Add(p => p.Groups, groups));

        var itemToClick = cut.Find($".accordion-select-group > .accordion-select:nth-child({selectedGroupIndex + 1}) > .content > .item-container > .item:nth-child({selectedItemIndex + 1})");

        //Act
        itemToClick.Click();

        var value = cut.Instance.SelectedItem;
        var headers = cut.FindAll(".accordion-header").ToList();
        var allItems = cut.FindAll(".item").ToList();

        var selectedHeader = headers.ElementAt(selectedGroupIndex);
        headers.RemoveAt(selectedGroupIndex);
        var unselectedHeaders = headers;

        var selectedItem = cut.Find(".item.selected");
        var unselectedItems = allItems.Where(_ => !_.Equals(selectedItem));

        //Assert
        Assert.Equal(expectedValue, value);
        Assert.Contains("selected", selectedHeader.ClassList);
        Assert.DoesNotContain("selected", unselectedHeaders.SelectMany(_ => _.ClassList));
        Assert.Contains("selected", selectedItem.ClassList);
        Assert.DoesNotContain("selected", unselectedItems.SelectMany(_ => _.ClassList));
    }

    private static string GenerateExpectedMarkup(string title, bool isExpanded, IEnumerable<SelectGroup<int>> items)
    {
        var iconType = isExpanded ? "minus" : "plus";
        var itemMarkup = string.Join(Environment.NewLine, items
            .Select(item => $@"
                <div class=""item"">
                    <span class=""icon mdi mdi-icon mdi-12px mdi-bookmark""></span>
                    <span class=""item-name"">{item.Name}</span>
                </div>
            "));

        return $@"
            <div class=""accordion-select"">
                <div class=""content"">
                    <div class=""accordion-header"">
                        <span class=""accordion-icon-btn mdi mdi-icon mdi-24px mdi-{iconType}-box-outline""></span>
                        <span class=""item-group-name"">{title}</span>
                    </div>
                    <div class=""item-container {(isExpanded ? string.Empty : "collapsed")}"">
                        {itemMarkup}
                    </div>
                </div>
            </div>
        ";
    }
}
