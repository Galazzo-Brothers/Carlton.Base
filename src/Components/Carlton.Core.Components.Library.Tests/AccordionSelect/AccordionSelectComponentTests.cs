using AutoFixture;
using AutoFixture.Xunit2;
using Carlton.Core.Components.Library.AccordionSelect;

namespace Carlton.Core.Components.Library.Tests;

[Trait("Component", nameof(AccordionSelect<int>))]
public class AccordionSelectComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test")]
    [InlineAutoData(true, 1, 0)]
    [InlineAutoData(false, 10, 3)]
    [InlineAutoData(true, 10, -1)]
    [InlineAutoData(false, 10, -1)]
    public void AccordionSelect_Markup_RendersCorrectly(bool isExpanded, int numOfItems, int selectedIndex, string title)
    {
        //Arrange
        var kvp = new Fixture().CreateMany<KeyValuePair<string, int>>(numOfItems);
        var itemDictionary = new Dictionary<string, int>(kvp);
        var items = new AccordionSelectItemBuilder<int>()
                        .AddItems(itemDictionary)
                        .Build();

        //Act
        var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
            .Add(p => p.Title, title)
            .Add(p => p.IsExpanded, isExpanded)
            .Add(p => p.SelectedIndex, selectedIndex)
            .Add(p => p.Items, items));

        //Assert
        cut.MarkupMatches(GenerateExpectedMarkup(title, isExpanded, selectedIndex, items));
    }

    [Theory(DisplayName = "Title Parameter Test")]
    [AutoData]
    [InlineData("")]
    [InlineData(null)]
    public void AccordionSelect_TitleParam_RendersCorrectly(string title)
    {
        //Act
        var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
            .Add(p => p.Title, title));

        var titleElm = cut.Find(".item-group-name");

        //Assert
        var expected = title ?? string.Empty;
        Assert.Equal(expected, titleElm.TextContent);
    }

    [Theory(DisplayName = "IsExpanded Parameter Test")]
    [InlineAutoData(false, "mdi-plus-box-outline")]
    [InlineAutoData(true, "mdi-minus-box-outline")]
    public void AccordionSelect_IsExpandedParam_RendersCorrectly(bool isExpanded, string expectedClass, string title)
    {
        //Act
        var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
            .Add(p => p.Title, title)
            .Add(p => p.IsExpanded, isExpanded));

        var itemContainerElement = cut.Find(".item-container");
        var accordionContainerElement = cut.Find(".accordion-icon-btn");
        var expectedAccordionValue = !isExpanded;

        //Assert
        Assert.Equal(expectedAccordionValue, itemContainerElement.ClassList.Contains("collapsed"));
        Assert.True(accordionContainerElement.ClassList.Contains(expectedClass));
    }

    [Theory(DisplayName = "Items Parameter Test")]
    [InlineAutoData(0)]
    [InlineAutoData(3)]
    [InlineAutoData(10)]
    public void AccordionSelect_ItemsParams_RendersCorrectly(int numOfItems, string title, bool isExapnded)
    {
        //Arrange
        var kvp = new Fixture().CreateMany<KeyValuePair<string, int>>(numOfItems);
        var itemDictionary = new Dictionary<string, int>(kvp);
        var items = new AccordionSelectItemBuilder<int>()
                        .AddItems(itemDictionary)
                        .Build();

        var expectedCount = items.Count();
        var expectedItemNames = items.Select(_ => _.Name);

        //Act
        var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
                .Add(p => p.Title, title)
                .Add(p => p.IsExpanded, isExapnded)
                .Add(p => p.Items, items));

        var actualCount = cut.FindAll(".item").Count;
        var actualItemNames = cut.FindAll(".item-name").Select(_ => _.TextContent);


        //Assert
        Assert.Equal(expectedCount, actualCount);
        Assert.Equal(expectedItemNames, actualItemNames);
    }

    [Theory(DisplayName = "SelectedIndex Parameter Test")]
    [InlineAutoData(2, 0)]
    [InlineAutoData(5, 3)]
    [InlineAutoData(10, 1)]
    public void AccordionSelect_SelectedIndexParam_RendersCorrectly(int numOfItems, int selectedIndex, string title)
    {
        //Arrange
        var kvp = new Fixture().CreateMany<KeyValuePair<string, int>>(numOfItems);
        var itemDictionary = new Dictionary<string, int>(kvp);
        var items = new AccordionSelectItemBuilder<int>()
                        .AddItems(itemDictionary)
                        .Build();

        //Act
        var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
            .Add(p => p.Title, title)
            .Add(p => p.IsExpanded, true)
            .Add(p => p.Items, items)
            .Add(p => p.SelectedIndex, selectedIndex));

        var itemElms = cut.FindAll(".item");
        var selectedItem = itemElms[selectedIndex];

        //Assert
        Assert.True(selectedItem.ClassList.Contains("selected"));
    }

    [Theory(DisplayName = "SelectedValue Property Test")]
    [InlineAutoData(2, 0)]
    [InlineAutoData(5, 3)]
    [InlineAutoData(10, 1)]
    public void AccordionSelect_SelectedValueProperty_RendersCorrectly(int numOfItems, int selectedIndex, string title)
    {
        //Arrange
        var kvp = new Fixture().CreateMany<KeyValuePair<string, int>>(numOfItems);
        var itemDictionary = new Dictionary<string, int>(kvp);
        var items = new AccordionSelectItemBuilder<int>()
                        .AddItems(itemDictionary)
                        .Build();
        var expectedValue = items.ElementAt(selectedIndex).Value;

        //Act
        var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
            .Add(p => p.Title, title)
            .Add(p => p.IsExpanded, true)
            .Add(p => p.Items, items)
            .Add(p => p.SelectedIndex, selectedIndex));

        //Assert
        Assert.Equal(expectedValue, cut.Instance.SelectedValue);
    }

    [Theory(DisplayName = "SelectedIndex Parameter Not Provided Render Test")]
    [InlineAutoData(2)]
    [InlineAutoData(5)]
    [InlineAutoData(0)]
    public void AccordionSelect_SelectedIndexParamNotProvided_RendersCorrectly(int numOfItems, string title, bool isExpanded)
    {
        //Arrange
        var kvp = new Fixture().CreateMany<KeyValuePair<string, int>>(numOfItems);
        var itemDictionary = new Dictionary<string, int>(kvp);
        var items = new AccordionSelectItemBuilder<int>()
                        .AddItems(itemDictionary)
                        .Build();

        //Act
        var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
            .Add(p => p.Title, title)
            .Add(p => p.IsExpanded, isExpanded)
            .Add(p => p.Items, items));

        var itemElms = cut.FindAll(".accordion-header");

        //Assert
        Assert.DoesNotContain("selected", itemElms.SelectMany(_ => _.ClassList));
    }


    [Theory(DisplayName = "SelectedIndex Parameter Not Provided Value Test")]
    [InlineAutoData(2, -10)]
    [InlineAutoData(2, 5)]
    [InlineAutoData(0, 5)]
    public void AccordionSelect_InvalidSelectedIndexParam_ShouldThrowArgumentException(int numOfItems, int selectedIndex, string title, bool isExpanded)
    {
        //Arrange
        var kvp = new Fixture().CreateMany<KeyValuePair<string, int>>(numOfItems);
        var itemDictionary = new Dictionary<string, int>(kvp);
        var items = new AccordionSelectItemBuilder<int>()
                        .AddItems(itemDictionary)
                        .Build();

        //Act
        var ex = Assert.Throws<ArgumentException>(() => RenderComponent<AccordionSelect<int>>(parameters => parameters
            .Add(p => p.Title, title)
            .Add(p => p.IsExpanded, isExpanded)
            .Add(p => p.SelectedIndex, selectedIndex)
            .Add(p => p.Items, items)));

        //Assert
        Assert.Equal("The SelectedIndex parameter value is invalid.", ex.Message);
    }

    [Theory(DisplayName = "SelectedItemChanged Callback Parameter Test")]
    [InlineAutoData(2, 0)]
    [InlineAutoData(5, 3)]
    [InlineAutoData(10, 1)]
    public void AccordionSelect_SelectedItemChangedParam_FiresCallback(int numOfItems, int selectedIndex, string title)
    {
        //Arrange
        var kvp = new Fixture().CreateMany<KeyValuePair<string, int>>(numOfItems);
        var itemDictionary = new Dictionary<string, int>(kvp);
        var items = new AccordionSelectItemBuilder<int>()
                        .AddItems(itemDictionary)
                        .Build();
        var eventCalled = false;
        SelectItem<int>? selectedItem = null;
        var expectedValue = items.ElementAt(selectedIndex).Value;

        var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
            .Add(p => p.Title, title)
            .Add(p => p.IsExpanded, true)
            .Add(p => p.Items, items)
            .Add(p => p.SelectedItemChanged, (selected) => { eventCalled = true; selectedItem = selected; }));

        var itemElms = cut.FindAll(".item");
        var selectedElement = itemElms[selectedIndex];

        //Act
        selectedElement.Click();

        //Assert
        Assert.True(eventCalled);
        Assert.NotNull(selectedItem);
        Assert.Equal(expectedValue, selectedItem.Value);
    }


    [Theory(DisplayName = "Item Click Value Test")]
    [InlineAutoData(2, 0)]
    [InlineAutoData(5, 3)]
    [InlineAutoData(10, 1)]
    public void AccordionSelect_ItemClick_RendersCorrectly(int numOfItems, int selectedIndex, string title)
    {
        //Arrange
        var kvp = new Fixture().CreateMany<KeyValuePair<string, int>>(numOfItems);
        var itemDictionary = new Dictionary<string, int>(kvp);
        var items = new AccordionSelectItemBuilder<int>()
                        .AddItems(itemDictionary)
                        .Build();

        var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
            .Add(p => p.Title, title)
            .Add(p => p.IsExpanded, true)
            .Add(p => p.Items, items));
        var itemElms = cut.FindAll(".item");
        var selectedItem = itemElms[selectedIndex];

        //Act
        selectedItem.Click();

        //Assert
        itemElms = cut.FindAll(".item");
        selectedItem = itemElms[selectedIndex];
        Assert.True(selectedItem.ClassList.Contains("selected"));
    }

    [Theory(DisplayName = "Item Click Value Test")]
    [InlineAutoData(2, 0)]
    [InlineAutoData(5, 3)]
    [InlineAutoData(10, 1)]
    public void AccordionSelect_ItemClick_SetsValueCorrectly(int numOfItems, int selectedIndex, string title)
    {
        //Arrange
        var kvp = new Fixture().CreateMany<KeyValuePair<string, int>>(numOfItems);
        var itemDictionary = new Dictionary<string, int>(kvp);
        var items = new AccordionSelectItemBuilder<int>()
                        .AddItems(itemDictionary)
                        .Build();
        var selectedValue = items.ElementAt(selectedIndex).Value;

        var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
            .Add(p => p.Title, title)
            .Add(p => p.IsExpanded, true)
            .Add(p => p.Items, items));


        var itemElms = cut.FindAll(".item");
        var selectedItem = itemElms[selectedIndex];

        //Act
        selectedItem.Click();

        //Assert
        Assert.Equal(selectedValue, cut.Instance.SelectedValue);
    }

    
    private static string GenerateExpectedMarkup(string title, bool isExpanded, int selectedIndex, IEnumerable<SelectItem<int>> items)
    {
        var iconType = isExpanded ? "minus" : "plus";
        var itemMarkup = string.Join(Environment.NewLine, items
            .Select(item => $@"
                    <div class=""item {(item.Index == selectedIndex ? "selected" : string.Empty)}"">
                        <span class=""icon mdi mdi-icon mdi-12px mdi-bookmark""></span>
                        <span class=""item-name"">{item.Name}</span>
                    </div>
                "));

        return $@"
                <div class=""accordion-select"">
                    <div class=""content"">
                        <div class=""accordion-header {(selectedIndex != -1 ? "selected" : string.Empty)}"">
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






