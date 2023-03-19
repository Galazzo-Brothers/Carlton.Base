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

    private static readonly List<SelectItem<int>> Items = new()
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

    [Fact]
    public void AccordionSelect_TitlePara_RendersTitleCorrectly()
    {
        //Act
        var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
            .Add(p => p.Title, "Test Title")
            );

        var titleElm = cut.Find(".item-group-name");
        
        //Assert
        Assert.Equal("Test Title", titleElm.InnerHtml);
    }

    [Fact]
    public void AccordionSelect_IsExpandedParam_False_RendersCollapsedStateCorrectly()
    {
        //Act
        var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
            .Add(p => p.Title, "AccordionSelect Title")
            .Add(p => p.IsExpanded, false)
            );
        
        var itemContainerElm = cut.Find(".item-container");


        //Assert
        Assert.True(itemContainerElm.ClassList.Contains("collapsed"));
    }

    [Fact]
    public void AccordionSelect_IsExpandedParam_False_RendersCorrectIcon()
    {
        //Act
        var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
            .Add(p => p.Title, "AccordionSelect Title")
            .Add(p => p.IsExpanded, false)
            );

        var itemContainerElm = cut.Find(".accordion-icon-btn");

        //Assert
        Assert.True(itemContainerElm.ClassList.Contains("mdi-plus-box-outline"));
    }

    [Fact]
    public void AccordionSelect_IsExpandedParam_True_RendersExpandedStateCorrectly()
    {
        //Act
        var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
            .Add(p => p.Title, "AccordionSelect Title")
            .Add(p => p.IsExpanded, true)
            );

        var itemContainerElm = cut.Find(".item-container");

        //Assert
        Assert.False(itemContainerElm.ClassList.Contains("collapsed"));
    }

    [Fact]
    public void AccordionSelect_IsExpandedParam_True_RendersCorrectIcon()
    {
        //Act
        var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
            .Add(p => p.Title, "AccordionSelect Title")
            .Add(p => p.IsExpanded, true)
            );

        var itemContainerElm = cut.Find(".accordion-icon-btn");

        //Assert
        Assert.True(itemContainerElm.ClassList.Contains("mdi-minus-box-outline"));
    }

    [Fact]
    public void AccordionSelect_IsExpandedParam_True_WithItems_RendersItemsCorrectly()
    {
        //Act
        var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
            .Add(p => p.Title, "AccordionSelect Title")
            .Add(p => p.IsExpanded, true)
            .Add(p => p.Items, Items)
            );

        var itemElms = cut.FindAll(".item");

        //Assert
        Assert.Equal(3, itemElms.Count);
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

    [Fact]
    public void AccordionSelect_AfterItemSelection_ReturnsCorrectSelectedItem()
    {
        //Arrange
        var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
            .Add(p => p.Title, "AccordionSelect Title")
            .Add(p => p.IsExpanded, true)
            .Add(p => p.Items, Items)
            );

        var itemElms = cut.FindAll(".item");
        var lastItem = itemElms[itemElms.Count - 1];
        
        //Act
        lastItem.Click();
        
        //Assert
        Assert.Equal(3, cut.Instance.SelectedValue);
    }

    [Fact]
    public void AccordionSelect_SelectedValueParam_Exists_Should_HaveSelectedClass()
    {
        //Act
        var cut = RenderComponent<AccordionSelect<int>>(parameters => parameters
            .Add(p => p.Title, "AccordionSelect Title")
            .Add(p => p.IsExpanded, true)
            .Add(p => p.Items, Items)
            .Add(p => p.SelectedValue, 3)
            );

        var itemElms = cut.FindAll(".accordion-header");
        var lastItem = itemElms[itemElms.Count - 1];

        //Assert
        Assert.True(lastItem.ClassList.Contains("selected"));
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