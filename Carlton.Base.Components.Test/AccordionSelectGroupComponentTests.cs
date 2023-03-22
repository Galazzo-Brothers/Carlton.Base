namespace Carlton.Base.Components.Test;

public class AccordionSelectGroupComponentTests : TestContext
{
    private static readonly string AccordionSelectGroupMarkup =
  @"
  <div class=""accordion-select-group"" b-pnd1og41cd>
    <div class=""accordion-select"" b-6835cu0hu3>
        <div class=""content"" b-6835cu0hu3>
            <div class=""accordion-header"" blazor:onclick=""1"" b-6835cu0hu3>
                <span class=""accordion-icon-btn mdi mdi-icon mdi-24px mdi-plus-box-outline"" b-6835cu0hu3></span>
                <span class=""item-group-name"" b-6835cu0hu3>Group 1</span>
            </div>
            <div class=""item-container collapsed"" b-6835cu0hu3>
                <div class=""item"" blazor:onclick=""2"" b-6835cu0hu3>
                    <span class=""icon mdi mdi-icon mdi-12px mdi-bookmark"" b-6835cu0hu3></span>
                    <span class=""item-name"" b-6835cu0hu3>Item 1</span>
                </div>
                <div class=""item"" blazor:onclick=""3"" b-6835cu0hu3>
                    <span class=""icon mdi mdi-icon mdi-12px mdi-bookmark"" b-6835cu0hu3></span>
                    <span class=""item-name"" b-6835cu0hu3>Item 2</span>
                </div>
            </div>
        </div>
    </div>
    <div class=""accordion-select"" b-6835cu0hu3>
        <div class=""content"" b-6835cu0hu3>
            <div class=""accordion-header"" blazor:onclick=""4"" b-6835cu0hu3>
                <span class=""accordion-icon-btn mdi mdi-icon mdi-24px mdi-plus-box-outline"" b-6835cu0hu3></span>
                <span class=""item-group-name"" b-6835cu0hu3>Group 2</span>
            </div>
            <div class=""item-container collapsed"" b-6835cu0hu3>
                <div class=""item"" blazor:onclick=""5"" b-6835cu0hu3>
                    <span class=""icon mdi mdi-icon mdi-12px mdi-bookmark"" b-6835cu0hu3></span>
                    <span class=""item-name"" b-6835cu0hu3>Item 3</span>
                </div>
                <div class=""item"" blazor:onclick=""6"" b-6835cu0hu3>
                    <span class=""icon mdi mdi-icon mdi-12px mdi-bookmark"" b-6835cu0hu3></span>
                    <span class=""item-name"" b-6835cu0hu3>Item 4</span>
                </div>
            </div>
        </div>
    </div>
  </div>";

    private readonly IEnumerable<SelectGroup<int>> Groups = new List<SelectGroup<int>>
    {
        new SelectGroup<int>("Group 1", 0,
            new List<SelectItem<int>>
            {
                new SelectItem<int>("Item 1", 0, 1),
                new SelectItem<int>("Item 2", 1, 2)
            }
         ),
         new SelectGroup<int>("Group 2", 1,
            new List<SelectItem<int>>
            {
                new SelectItem<int>("Item 3", 0, 3),
                new SelectItem<int>("Item 4", 1, 4)
            }
         )
    };

    [Fact]
    public void AccordionSelectGroup_Markup_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<AccordionSelectGroup<int>>(parameters => parameters
            .Add(p => p.Groups, Groups)
            );

        //Assert
        cut.MarkupMatches(AccordionSelectGroupMarkup);
    }

    [Fact]
    public void AccordionSelectGroup_ShouldContainTwoGroups()
    {
        //Act
        var cut = RenderComponent<AccordionSelectGroup<int>>(parameters => parameters
            .Add(p => p.Groups, Groups)
            );

        var accordionSelects = cut.FindAll(".accordion-select");

        //Assert
        Assert.Equal(2, accordionSelects.Count);
    }

    [Fact]
    public void AccordionSelectGroup_ClickItemTwoInGroupOne_SelectedItem_ShouldHaveValue2()
    {
        //Arrange
        var cut = RenderComponent<AccordionSelectGroup<int>>(parameters => parameters
            .Add(p => p.Groups, Groups)
            );

        var itemToClick = cut.FindAll(".item")[1];

        //Act
        itemToClick.Click();
        var selectedItem = cut.Instance.SelectedItem;


        //Assert
        Assert.Equal(2, selectedItem);
    }

    [Fact]
    public void AccordionSelectGroup_ClickItemTwoInGroupOne_GroupOneHeader_shouldHaveSelectedClass()
    {
        //Arrange
        var cut = RenderComponent<AccordionSelectGroup<int>>(parameters => parameters
            .Add(p => p.Groups, Groups)
            );

        var itemToClick = cut.FindAll(".item")[1];

        //Act
        itemToClick.Click();
        var headers = cut.FindAll(".accordion-header");
        var items = cut.FindAll(".item");

        //Assert
        Assert.Collection(headers,
            item => Assert.Contains("selected", item.ClassList),
            item => Assert.DoesNotContain("selected", item.ClassList));
    }

    [Fact]
    public void AccordionSelectGroup_ClickItemTwoInGroupOne_GroupOneItemTwo_shouldHaveSelectedClass()
    {
        //Arrange
        var cut = RenderComponent<AccordionSelectGroup<int>>(parameters => parameters
            .Add(p => p.Groups, Groups)
            );

        var itemToClick = cut.FindAll(".item")[1];

        //Act
        itemToClick.Click();
        var items = cut.FindAll(".item");

        //Assert
        Assert.Collection(items,
            item => Assert.DoesNotContain("selected", item.ClassList),
            item => Assert.Contains("selected", item.ClassList),
            item => Assert.DoesNotContain("selected", item.ClassList),
            item => Assert.DoesNotContain("selected", item.ClassList)
        );
    }

    [Fact]
    public void AccordionSelectGroup_ClickItemOneInGroupTwo_SelectedItem_ShouldHaveValue3()
    {
        //Arrange
        var cut = RenderComponent<AccordionSelectGroup<int>>(parameters => parameters
            .Add(p => p.Groups, Groups)
            );

        var itemToClick = cut.FindAll(".item")[2];

        //Act
        itemToClick.Click();
        var selectedItem = cut.Instance.SelectedItem;


        //Assert
        Assert.Equal(3, selectedItem);
    }

    [Fact]
    public void AccordionSelectGroup_ClickItemOneInGroupTwo_GroupTwoHeader_shouldHaveSelectedClass()
    {
        //Arrange
        var cut = RenderComponent<AccordionSelectGroup<int>>(parameters => parameters
            .Add(p => p.Groups, Groups)
            );

        var itemToClick = cut.FindAll(".item")[3];

        //Act
        itemToClick.Click();
        var headers = cut.FindAll(".accordion-header");

        //Assert
        Assert.Collection(headers,
            item => Assert.DoesNotContain("selected", item.ClassList),
            item => Assert.Contains("selected", item.ClassList));
    }

    [Fact]
    public void AccordionSelectGroup_ClickItemOneInGroupTwo_GroupTwoItemOne_shouldHaveSelectedClass()
    {
        //Arrange
        var cut = RenderComponent<AccordionSelectGroup<int>>(parameters => parameters
            .Add(p => p.Groups, Groups)
            );

        var itemToClick = cut.FindAll(".item")[2];

        //Act
        itemToClick.Click();
        var items = cut.FindAll(".item");


        //Assert
        Assert.Collection(items,
            item => Assert.DoesNotContain("selected", item.ClassList),
            item => Assert.DoesNotContain("selected", item.ClassList),
            item => Assert.Contains("selected", item.ClassList),
            item => Assert.DoesNotContain("selected", item.ClassList)
        );
    }

    [Fact]
    public void AccordionSelectGroup_WithDefaultParameter_shouldSelectCorrectItem()
    {
        //Act
        var cut = RenderComponent<AccordionSelectGroup<int>>(parameters => parameters
            .Add(p => p.Groups, Groups)
            .Add(p => p.SelectedItem, 3)
            );

        var headers = cut.FindAll(".accordion-header");
        var items = cut.FindAll(".item");

        //Assert
        Assert.DoesNotContain("selected", headers[0].ClassList);
        Assert.Contains("selected", headers[1].ClassList);

        //Assert
        Assert.Equal(3, cut.Instance.SelectedItem);
        Assert.Collection(headers,
              item => Assert.DoesNotContain("selected", item.ClassList),
              item => Assert.Contains("selected", item.ClassList));
        Assert.Collection(items,
             item => Assert.DoesNotContain("selected", item.ClassList),
             item => Assert.DoesNotContain("selected", item.ClassList),
             item => Assert.Contains("selected", item.ClassList),
             item => Assert.DoesNotContain("selected", item.ClassList)
         );
    }

    [Fact]
    public void AccordionSelectGroup_OnSelectedItemChangedParam_shouldBeCalled()
    {
        //Arrange
        var eventCalled = false;
        SelectItemChangedEvent<int>? evt = null;
        var cut = RenderComponent<AccordionSelectGroup<int>>(parameters => parameters
            .Add(p => p.Groups, Groups)
            .Add(p => p.OnSelectedItemChanged, (_) => { eventCalled = true; evt = _; })
            )
        ;

        var itemToClick = cut.FindAll(".item")[3];

        //Act
        itemToClick.Click();
        var items = cut.FindAll(".item");


        //Assert
        Assert.True(eventCalled);
        Assert.Equal(1, evt?.GroupIndexID);
        Assert.Equal(1, evt?.ItemIndexID);
    }
}
