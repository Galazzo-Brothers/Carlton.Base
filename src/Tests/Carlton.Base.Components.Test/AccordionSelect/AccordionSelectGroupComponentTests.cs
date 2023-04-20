namespace Carlton.Base.Components.Test;

public class AccordionSelectGroupComponentTests : TestContext
{
    [Fact]
    public void AccordionSelectGroup_Markup_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<AccordionSelectGroup<int>>(parameters => parameters
            .Add(p => p.Groups, AccordionSelectTestHelper.Groups)
            );

        //Assert
        cut.MarkupMatches(AccordionSelectTestHelper.AccordionSelectGroupMarkup);
    }

    [Theory]
    [MemberData(nameof(AccordionSelectTestHelper.GetGroups), MemberType = typeof(AccordionSelectTestHelper))]
    public void AccordionSelectGroup_GroupsParam_RendersCorrectly(ReadOnlyCollection<SelectGroup<int>> groups)
    {
        //Arrange
        var expectedCount = groups.Count;
        var expectedGroupNames = groups.Select(_ => _.Name);

        //Act
        var cut = RenderComponent<AccordionSelectGroup<int>>(parameters => parameters
            .Add(p => p.Groups, groups)
            );

        var actualCount = cut.FindAll(".accordion-select").Count;
        var actualGroupNames = cut.FindAll(".item-group-name").Select(_ => _.TextContent);

        //Assert
        Assert.Equal(expectedCount, actualCount);
        Assert.Equal(expectedGroupNames, actualGroupNames);
    }

    [Theory]
    [InlineData(0, 0, 1)]
    [InlineData(0, 1, 2)]
    [InlineData(1, 2, 3)]
    [InlineData(1, 3, 4)]
    public void AccordionSelectGroup_SelectedItemParam_shouldRenderCorrectly(int groupIndex, int itemIndex, int itemValue)
    {
        //Act
        var cut = RenderComponent<AccordionSelectGroup<int>>(parameters => parameters
            .Add(p => p.Groups, AccordionSelectTestHelper.Groups)
            .Add(p => p.SelectedItem, itemValue)
            );

        var headers = cut.FindAll(".accordion-header").ToList();
        var items = cut.FindAll(".item").ToList();

        var selectedHeader = headers.ElementAt(groupIndex);
        headers.RemoveAt(groupIndex);
        var unselectedHeaders = headers;

        var selectedItem = items.ElementAt(itemIndex);
        items.RemoveAt(itemIndex);
        var unselectedItems = items;

        //Assert
        Assert.Equal(itemValue, cut.Instance.SelectedItem);

        Assert.Contains("selected", selectedHeader.ClassList);
        Assert.DoesNotContain("selected", unselectedHeaders.SelectMany(_ => _.ClassList));

        Assert.Contains("selected", selectedItem.ClassList);
        Assert.DoesNotContain("selected", unselectedItems.SelectMany(_ => _.ClassList));
    }

    [Fact]
    public void AccordionSelectGroup_OnSelectedItemChangedParam_FiresCallback()
    {
        //Arrange
        var eventCalled = false;
        SelectItemChangedEvent<int>? evt = null;
        var cut = RenderComponent<AccordionSelectGroup<int>>(parameters => parameters
            .Add(p => p.Groups, AccordionSelectTestHelper.Groups)
            .Add(p => p.OnSelectedItemChanged, (_) => { eventCalled = true; evt = _; })
            );

        var itemToClick = cut.FindAll(".item")[3];

        //Act
        itemToClick.Click();

        //Assert
        Assert.True(eventCalled);
        Assert.Equal(1, evt?.GroupIndexID);
        Assert.Equal(1, evt?.ItemIndexID);
    }

    [Theory]
    [InlineData(0, 0, 1)]
    [InlineData(0, 1, 2)]
    [InlineData(1, 2, 3)]
    [InlineData(1, 3, 4)]
    public void AccordionSelectGroup_ClickEvent_RendersCorrectly(int groupIndex, int itemIndex, int expectedValue)
    {
        //Arrange
        var cut = RenderComponent<AccordionSelectGroup<int>>(parameters => parameters
            .Add(p => p.Groups, AccordionSelectTestHelper.Groups)
            );

        var itemToClick = cut.FindAll(".item")[itemIndex];

        //Act
        itemToClick.Click();

        var value = cut.Instance.SelectedItem;
        var headers = cut.FindAll(".accordion-header").ToList();
        var items = cut.FindAll(".item").ToList();

        var selectedHeader = headers.ElementAt(groupIndex);
        headers.RemoveAt(groupIndex);
        var unselectedHeaders = headers;

        var selectedItem = items.ElementAt(itemIndex);
        items.RemoveAt(itemIndex);
        var unselectedItems = items;

        //Assert
        Assert.Equal(expectedValue, value);

        Assert.Contains("selected", selectedHeader.ClassList);
        Assert.DoesNotContain("selected", unselectedHeaders.SelectMany(_ => _.ClassList));

        Assert.Contains("selected", selectedItem.ClassList);
        Assert.DoesNotContain("selected", unselectedItems.SelectMany(_ => _.ClassList));
    }
}
