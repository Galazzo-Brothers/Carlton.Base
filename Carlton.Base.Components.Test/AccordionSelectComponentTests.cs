namespace Carlton.Base.Components.Test;

public class AccordionSelectComponentTests : TestContext
{
    private static readonly string AccordionSelectMarkup =
    @"
    <div class=""accordion-select"" b-6835cu0hu3>
        <div class=""content"" b-6835cu0hu3>
                <div class=""accordion-header"" blazor:onclick=""1"" b-6835cu0hu3>
                <span class=""accordion-icon-btn mdi mdi-icon mdi-24px mdi-plus-box-outline"" b-6835cu0hu3></span>
                <span class=""item-group-name"" b-6835cu0hu3>AccordionSelect Title</span>
        </div>        
        <div class=""item-container collapsed"" b-6835cu0hu3></div>
        </div>
    </div>";

    private static readonly string AccordionSelectItemMarkup =
    @"
        <div class=""item"" blazor:onclick=""2"" b-6835cu0hu3="""">
            <span class=""icon mdi mdi-icon mdi-12px mdi-bookmark"" b-6835cu0hu3=""""></span>
            <span class=""item-name"" b-6835cu0hu3="""">Item 1</span>
        </div>
    ";

    public static IEnumerable<object[]> GetItems()
    {
        yield return new object[]
           {
                new List<SelectItem<int>>()
                {
                    new SelectItem<int>("Item 1", 0, 1),
                }
           };
        yield return new object[]
           {
                new List<SelectItem<int>>()
                {
                    new SelectItem<int>("Item 1", 0, 1),
                    new SelectItem<int>("Item 2", 1, 2),
                }
           };
        yield return new object[]
            {
                new List<SelectItem<int>>()
                {
                    new SelectItem<int>("Item 1", 0, 1),
                    new SelectItem<int>("Item 2", 1, 2),
                    new SelectItem<int>("Item 3", 2, 3)
                }
            };
    }

    private static readonly List<SelectItem<int>> Items = new List<SelectItem<int>>()
                {
                    new SelectItem<int>("Item 1", 0, 1),
                    new SelectItem<int>("Item 2", 1, 2),
                    new SelectItem<int>("Item 3", 2, 3)
                };

    [Fact]
    public void AccordionSelect_Markup_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
            .Add(p => p.Title, "AccordionSelect Title")
            );

        //Assert
        cut.MarkupMatches(AccordionSelectMarkup);
    }

    [Theory]
    [InlineData("Test 1")]
    [InlineData("Test 2")]
    [InlineData("Test 3")]
    public void AccordionSelect_TitleParam_RendersTitleCorrectly(string expectedTitle)
    {
        //Act
        var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
            .Add(p => p.Title, expectedTitle)
            );

        var titleElm = cut.Find(".item-group-name");

        //Assert
        Assert.Equal(expectedTitle, titleElm.InnerHtml);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void AccordionSelect_IsExpandedParam_RendersCollapsedStateCorrectly(bool isExpanded)
    {
        //Act
        var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
            .Add(p => p.Title, "AccordionSelect Title")
            .Add(p => p.IsExpanded, isExpanded)
            );

        var itemContainerElm = cut.Find(".item-container");
        var expectedValue = !isExpanded;

        //Assert
        Assert.Equal(expectedValue, itemContainerElm.ClassList.Contains("collapsed"));
    }

    [Theory]
    [InlineData(false, "mdi-plus-box-outline")]
    [InlineData(true, "mdi-minus-box-outline")]
    public void AccordionSelect_IsExpandedParam_RendersCorrectIcon(bool isExpanded, string expectedClass)
    {
        //Act
        var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
            .Add(p => p.Title, "AccordionSelect Title")
            .Add(p => p.IsExpanded, isExpanded)
            );

        var itemContainerElm = cut.Find(".accordion-icon-btn");

        //Assert
        Assert.True(itemContainerElm.ClassList.Contains(expectedClass));
    }

    [Theory]
    [MemberData(nameof(GetItems))]
    public void AccordionSelect_IsExpandedParam_True_WithItems_RendersItemsCorrectly(IEnumerable<SelectItem<int>> items)
    {
        //Act
        var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
                .Add(p => p.Title, "AccordionSelect Title")
                .Add(p => p.IsExpanded, true)
                .Add(p => p.Items, items)
                );

        var itemElms = cut.FindAll(".item");
        var expectedCount = items.Count();

        //Assert
        Assert.Equal(expectedCount, itemElms.Count);
    }

    [Fact]
    public void AccordionSelect_WithItems_Markup_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
            .Add(p => p.Title, "AccordionSelect Title")
            .Add(p => p.IsExpanded, true)
            .Add(p => p.Items, Items)
            );

        var itemElms = cut.FindAll(".item");
        var firstItem = itemElms[0];

        //Assert
        firstItem.MarkupMatches(AccordionSelectItemMarkup);
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 2)]
    [InlineData(2, 3)]
    public void AccordionSelect_AfterItemSelection_ReturnsCorrectSelectedItem(int selectedIndex, int expectedValue)
    {
        //Arrange
        var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
            .Add(p => p.Title, "AccordionSelect Title")
            .Add(p => p.IsExpanded, true)
            .Add(p => p.Items, Items)
            );

        var itemElms = cut.FindAll(".item");
        var selectedItem = itemElms[selectedIndex];

        //Act
        selectedItem.Click();

        //Assert
        Assert.Equal(expectedValue, cut.Instance.SelectedValue);
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 2)]
    [InlineData(2, 3)]
    public void AccordionSelect_SelectedValueParam_Exists_Should_HaveSelectedClass(int selectedIndex, int selectedValue)
    {
        //Act
        var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
            .Add(p => p.Title, "AccordionSelect Title")
            .Add(p => p.IsExpanded, true)
            .Add(p => p.Items, Items)
            .Add(p => p.SelectedValue, selectedValue)
            );

        var itemElms = cut.FindAll(".item");
        var selectedItem = itemElms[selectedIndex];

        //Assert
        Assert.True(selectedItem.ClassList.Contains("selected"));
    }

    [Fact]
    public void AccordionSelect_SelectedValueParam_DoesNotExist_ShouldNot_HaveSelectedClass()
    {
        //Act
        var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
            .Add(p => p.Title, "AccordionSelect Title")
            .Add(p => p.IsExpanded, true)
            .Add(p => p.Items, Items)
            .Add(p => p.SelectedValue, -1)
            );

        var itemElms = cut.FindAll(".accordion-header");

        //Assert
        Assert.DoesNotContain("selected", itemElms.SelectMany(_ => _.ClassList));
    }

    [Fact]
    public void AccordionSelect_AfeterItemSelection_Eventcallback()
    {
        //Arrange
        var eventCalled = false;
        SelectItem<int>? selectedItem = null;

        var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
            .Add(p => p.Title, "AccordionSelect Title")
            .Add(p => p.IsExpanded, true)
            .Add(p => p.Items, Items)
            .Add(p => p.SelectedItemChanged, (selected) => { eventCalled = true; selectedItem = selected; })
            );

        var itemElms = cut.FindAll(".item");
        var lastItem = itemElms[itemElms.Count - 1];

        //Act
        lastItem.Click();

        //Assert
        Assert.True(eventCalled);
        Assert.NotNull(selectedItem);
        Assert.Equal(3, selectedItem.Value);
    }
}