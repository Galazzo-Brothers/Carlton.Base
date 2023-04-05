namespace Carlton.Base.Components.Test;

public class AccordionSelectComponentTests : TestContext
{
    private static readonly string AccordionSelectNoItemsMarkup =
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

    private static readonly string AccordionSelectWithItemsMarkup =
    @"
    <div class=""accordion-select"" b-6835cu0hu3>
      <div class=""content"" b-6835cu0hu3>
        <div class=""accordion-header"" blazor:onclick=""1"" b-6835cu0hu3>
          <span class=""accordion-icon-btn mdi mdi-icon mdi-24px mdi-minus-box-outline"" b-6835cu0hu3></span>
          <span class=""item-group-name"" b-6835cu0hu3>AccordionSelect Title</span>
        </div>
        <div class=""item-container"" b-6835cu0hu3>
          <div class=""item"" blazor:onclick=""2"" b-6835cu0hu3>
            <span class=""icon mdi mdi-icon mdi-12px mdi-bookmark"" b-6835cu0hu3></span>
            <span class=""item-name"" b-6835cu0hu3>Item 1</span>
          </div>
          <div class=""item"" blazor:onclick=""3"" b-6835cu0hu3>
            <span class=""icon mdi mdi-icon mdi-12px mdi-bookmark"" b-6835cu0hu3></span>
            <span class=""item-name"" b-6835cu0hu3>Item 2</span>
          </div>
          <div class=""item"" blazor:onclick=""4"" b-6835cu0hu3>
            <span class=""icon mdi mdi-icon mdi-12px mdi-bookmark"" b-6835cu0hu3></span>
            <span class=""item-name"" b-6835cu0hu3>Item 3</span>
          </div>
        </div>
      </div>
    </div>";

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

    private static readonly List<SelectItem<int>> Items = new()
    {
        new SelectItem<int>("Item 1", 0, 1),
        new SelectItem<int>("Item 2", 1, 2),
        new SelectItem<int>("Item 3", 2, 3)
    };

    [Fact]
    public void AccordionSelect_MarkupWithhNoItems_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
            .Add(p => p.Title, "AccordionSelect Title")
            );

        //Assert
        cut.MarkupMatches(AccordionSelectNoItemsMarkup);
    }

    [Fact]
    public void AccordionSelect_MarkupWithItems_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
            .Add(p => p.Title, "AccordionSelect Title")
            .Add(p => p.IsExpanded, true)
            .Add(p => p.Items, Items)
            );

        //Assert
        cut.MarkupMatches(AccordionSelectWithItemsMarkup);
    }

    [Theory]
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

    [Theory]
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

    [Theory]
    [MemberData(nameof(GetItems))]
    public void AccordionSelect_ItemsParams_RendersCorrectly(IEnumerable<SelectItem<int>> items)
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

    [Theory]
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
            .Add(p => p.Items, Items)
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

    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 2)]
    [InlineData(2, 3)]
    public void AccordionSelect_SelectedValueParam_RendersCorreectly(int selectedIndex, int selectedValue)
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
    public void AccordionSelect_SelectedValueParamDoesNotExist_RendersCorrectly()
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

    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 2)]
    [InlineData(2, 3)]
    public void AccordionSelect_ItemClick_RendersCorrectly(int selectedIndex, int expectedValue)
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
}