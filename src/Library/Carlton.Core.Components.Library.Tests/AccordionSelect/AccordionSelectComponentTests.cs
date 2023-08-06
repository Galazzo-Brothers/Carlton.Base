namespace Carlton.Core.Components.Library.Tests;

[Trait("Component", nameof(AccordionSelect<int>))]
public class AccordionSelectComponentTests : TestContext
{
    [Fact(DisplayName = "Markup Test, No Items")]
    public void AccordionSelect_MarkupWithNoItems_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
            .Add(p => p.Title, "AccordionSelect Title")
            );

        //Assert
        cut.MarkupMatches(AccordionSelectTestHelper.AccordionSelectNoItemsMarkup);
    }

    [Fact(DisplayName = "Markup Test, With Items")]
    public void AccordionSelect_MarkupWithItems_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
            .Add(p => p.Title, "AccordionSelect Title")
            .Add(p => p.IsExpanded, true)
            .Add(p => p.Items, AccordionSelectTestHelper.SelectItems)
            );

        //Assert
        cut.MarkupMatches(AccordionSelectTestHelper.AccordionSelectWithItemsMarkup);
    }

    [Theory(DisplayName = "Title Parameter Test")]
    [InlineData("Test 1")]
    [InlineData("Test 2")]
    [InlineData("Test 3")]
    public void AccordionSelect_TitleParam_RendersCorrectly(string expectedTitle)
    {
        //Act
        var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
            .Add(p => p.Title, expectedTitle)
            );

        var titleElm = cut.Find(".item-group-name");

        //Assert
        Assert.Equal(expectedTitle, titleElm.InnerHtml);
    }

    [Theory(DisplayName = "IsExpanded Parameter Test")]
    [InlineData(false, "mdi-plus-box-outline")]
    [InlineData(true, "mdi-minus-box-outline")]
    public void AccordionSelect_IsExpandedParam_RendersCorrectly(bool isExpanded, string expectedClass)
    {
        //Act
        var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
            .Add(p => p.Title, "AccordionSelect Title")
            .Add(p => p.IsExpanded, isExpanded)
            );

        var itemContainerElement = cut.Find(".item-container");
        var accordionContainerElement = cut.Find(".accordion-icon-btn");
        var expectedAccordionValue = !isExpanded;

        //Assert
        Assert.Equal(expectedAccordionValue, itemContainerElement.ClassList.Contains("collapsed"));
        Assert.True(accordionContainerElement.ClassList.Contains(expectedClass));
    }

    [Theory(DisplayName = "Items Parameter Test")]
    [MemberData(nameof(AccordionSelectTestHelper.GetItems), MemberType = typeof(AccordionSelectTestHelper))]
    public void AccordionSelect_ItemsParams_RendersCorrectly(ReadOnlyCollection<SelectItem<int>> items)
    {
        //Arrange
        var expectedCount = items.Count;
        var expectedItemNames = items.Select(_ => _.Name);

        //Act
        var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
                .Add(p => p.Title, "AccordionSelect Title")
                .Add(p => p.IsExpanded, true)
                .Add(p => p.Items, items)
                );

        var actualCount = cut.FindAll(".item").Count();
        var actualItemNames = cut.FindAll(".item-name").Select(_ => _.TextContent);


        //Assert
        Assert.Equal(expectedCount, actualCount);
        Assert.Equal(expectedItemNames, actualItemNames);
    }

    [Theory(DisplayName = "SelectedItemChanged Callback Parameter Test")]
    [InlineData(0, 1)]
    [InlineData(1, 2)]
    [InlineData(2, 3)]
    public void AccordionSelect_SelectedItemChangedParam_FiresCallback(int itemIndex, int expectedValue)
    {
        //Arrange
        var eventCalled = false;
        SelectItem<int>? selectedItem = null;

        var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
            .Add(p => p.Title, "AccordionSelect Title")
            .Add(p => p.IsExpanded, true)
            .Add(p => p.Items, AccordionSelectTestHelper.SelectItems)
            .Add(p => p.SelectedItemChanged, (selected) => { eventCalled = true; selectedItem = selected; })
            );

        var itemElms = cut.FindAll(".item");
        var lastItem = itemElms[itemIndex];

        //Act
        lastItem.Click();

        //Assert
        Assert.True(eventCalled);
        Assert.NotNull(selectedItem);
        Assert.Equal(expectedValue, selectedItem.Value);
    }

    [Theory(DisplayName = "SelectedValue Parameter Test")]
    [InlineData(0, 1)]
    [InlineData(1, 2)]
    [InlineData(2, 3)]
    public void AccordionSelect_SelectedValueParam_RendersCorrectly(int selectedIndex, int selectedValue)
    {
        //Act
        var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
            .Add(p => p.Title, "AccordionSelect Title")
            .Add(p => p.IsExpanded, true)
            .Add(p => p.Items, AccordionSelectTestHelper.SelectItems)
            .Add(p => p.SelectedValue, selectedValue)
            );

        var itemElms = cut.FindAll(".item");
        var selectedItem = itemElms[selectedIndex];

        //Assert
        Assert.True(selectedItem.ClassList.Contains("selected"));
    }

    [Fact(DisplayName = "SelectedValue Parameter, Not Provided Test")]
    public void AccordionSelect_SelectedValueParamDoesNotExist_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
            .Add(p => p.Title, "AccordionSelect Title")
            .Add(p => p.IsExpanded, true)
            .Add(p => p.Items, AccordionSelectTestHelper.SelectItems)
            .Add(p => p.SelectedValue, -1)
            );

        var itemElms = cut.FindAll(".accordion-header");

        //Assert
        Assert.DoesNotContain("selected", itemElms.SelectMany(_ => _.ClassList));
    }

    [Theory(DisplayName = "Item Click Test")]
    [InlineData(0, 1)]
    [InlineData(1, 2)]
    [InlineData(2, 3)]
    public void AccordionSelect_ItemClick_RendersCorrectly(int selectedIndex, int expectedValue)
    {
        //Arrange
        var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
            .Add(p => p.Title, "AccordionSelect Title")
            .Add(p => p.IsExpanded, true)
            .Add(p => p.Items, AccordionSelectTestHelper.SelectItems)
            );

        var itemElms = cut.FindAll(".item");
        var selectedItem = itemElms[selectedIndex];

        //Act
        selectedItem.Click();

        //Assert
        Assert.Equal(expectedValue, cut.Instance.SelectedValue);
    }
}