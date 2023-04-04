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

    public static IEnumerable<object[]> GetGroups()
    {
        yield return new object[]
           {
               new List<SelectGroup<int>>
               {
                    new SelectGroup<int>("Group 1", 0,
                        new List<SelectItem<int>>
                        {
                            new SelectItem<int>("Item 1", 0, 1),
                            new SelectItem<int>("Item 2", 1, 2)
                        })
               }
           };
        yield return new object[]
           {
               new List<SelectGroup<int>>
               {
                 new SelectGroup<int>("Group 1", 0,
                    new List<SelectItem<int>>
                    {
                        new SelectItem<int>("Item 1", 0, 1),
                        new SelectItem<int>("Item 2", 1, 2)
                    }),
                 new SelectGroup<int>("Group 2", 1,
                    new List<SelectItem<int>>
                    {
                        new SelectItem<int>("Item 3", 0, 3),
                        new SelectItem<int>("Item 4", 1, 4)
                    })
               }
           };
        yield return new object[]
            {
                new List<SelectGroup<int>>
                {
                 new SelectGroup<int>("Group 1", 0,
                    new List<SelectItem<int>>
                    {
                        new SelectItem<int>("Item 1", 0, 1),
                        new SelectItem<int>("Item 2", 1, 2)
                    }),
                 new SelectGroup<int>("Group 2", 1,
                    new List<SelectItem<int>>
                    {
                        new SelectItem<int>("Item 3", 0, 3),
                        new SelectItem<int>("Item 4", 1, 4)
                    }),
                  new SelectGroup<int>("Group 3", 2,
                    new List<SelectItem<int>>
                    {
                        new SelectItem<int>("Item 5", 0, 3)
                    })
                }
           };
    }

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

    [Theory]
    [MemberData(nameof(GetGroups))]
    public void AccordionSelectGroup_GroupsParam_RendersCorreectly(IEnumerable<SelectGroup<int>> groups)
    {
        //Act
        var cut = RenderComponent<AccordionSelectGroup<int>>(parameters => parameters
            .Add(p => p.Groups, groups)
            );

        var accordionSelects = cut.FindAll(".accordion-select");
        var expectedCount = groups.Count();

        //Assert
        Assert.Equal(expectedCount, accordionSelects.Count);
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
            .Add(p => p.Groups, Groups)
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

    [Theory]
    [InlineData(0, 0, 1)]
    [InlineData(0, 1, 2)]
    [InlineData(1, 2, 3)]
    [InlineData(1, 3, 4)]
    public void AccordionSelectGroup_ClickEvent_RendersCorrectly(int groupIndex, int itemIndex, int expectedValue)
    {
        //Arrange
        var cut = RenderComponent<AccordionSelectGroup<int>>(parameters => parameters
            .Add(p => p.Groups, Groups)
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
