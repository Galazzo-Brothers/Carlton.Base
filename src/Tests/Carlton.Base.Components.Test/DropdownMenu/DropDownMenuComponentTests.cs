namespace Carlton.Base.Components.Test;

public class DropDownMenuComponentTests : TestContext
{
    [Fact]
    public void DropDownMenuElement_Markup_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<DropdownMenu<int>>(parameters => parameters
                .Add(p => p.HeaderTemplate, "<span>Header</span>")
                .Add(p => p.MenuItemTemplate, item => $"<span class='item'>{item.MenuItemName}</span>")
                .Add(p => p.MenuTemplate, isActive => $"<i class='{isActive}'></i>")
                .Add(p => p.MenuItems, DropdownMenuTestHelper.DropdownMenuItems)
                .Add(p => p.Style, DropdownMenuTestHelper.Style)
                );

        //Assert
        cut.MarkupMatches(DropdownMenuTestHelper.DropDownMenuMarkup);
    }

    [Theory]
    [MemberData(nameof(DropdownMenuTestHelper.GetItems), MemberType = typeof(DropdownMenuTestHelper))]
    public void DropDownMenuElement_MenuItemsParams_RendersCorrectly(ReadOnlyCollection<DropdownMenuItem<int>> items)
    {
        //Arrange
        var expectedItemsCount = items.Count;
        var expectedNames = items.Select(_ => _.MenuItemName);
        var expectedValues = items.Select(_ => _.Value);
        var expectedIcons = items.Select(_ => _.MenuIcon);

        //Act
        var cut = RenderComponent<DropdownMenu<int>>(parameters => parameters
                .Add(p => p.HeaderTemplate, "<span>Header</span>")
                .Add(p => p.MenuItemTemplate, item => $"<span class='item-name'>{item.MenuItemName}</span><span class='item-value'>{item.Value}</span><span class='item-icon'>{item.MenuIcon}</span>")
                .Add(p => p.MenuTemplate, isActive => $"<i class='{isActive}'></i>")
                .Add(p => p.MenuItems, items)
                .Add(p => p.Style, DropdownMenuTestHelper.Style)
                );


        var actualCount = cut.FindAll("li").Count;
        var actualItemNames = cut.FindAll(".item-name").Select(_ => _.TextContent);
        var actualItemValues = cut.FindAll(".item-value").Select(_ => int.Parse(_.TextContent));
        var actualItemIcons = cut.FindAll(".item-icon").Select(_ => _.TextContent);

        //Assert
        Assert.Equal(expectedItemsCount, actualCount);
        Assert.Equal(expectedNames, actualItemNames);
        Assert.Equal(expectedValues, actualItemValues);
        Assert.Equal(expectedIcons, actualItemIcons);
    }

    [Theory]
    [InlineData("<span>Testing Header</span>")]
    [InlineData("<span>More Testing Header</span>")]
    [InlineData("<span>Event Testing Header</span>")]
    public void DropDownMenuElement_HeaderTemplateParam_RendersCorrectly(string headerTemplate)
    {
        //Act
        var cut = RenderComponent<DropdownMenu<int>>(parameters => parameters
                .Add(p => p.HeaderTemplate, headerTemplate)
                .Add(p => p.MenuItemTemplate, item => $"<span class='item'>{item.MenuItemName}</span>")
                .Add(p => p.MenuTemplate, isActive => $"<i class='{isActive}'></i>")
                .Add(p => p.MenuItems, DropdownMenuTestHelper.DropdownMenuItems)
                .Add(p => p.Style, DropdownMenuTestHelper.Style)
                );

        var header = cut.Find(".header").InnerHtml;

        //Assert
        Assert.Equal(headerTemplate, header);
    }

    [Fact]
    public void DropDownMenuElement_MenuItemTemplateParam_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<DropdownMenu<int>>(parameters => parameters
                .Add(p => p.HeaderTemplate, "<span>Testing Header</span>")
                .Add(p => p.MenuItemTemplate, item => $"<span class=\"item\">{item.MenuItemName}</span>")
                .Add(p => p.MenuTemplate, isActive => $"<i class='{isActive}'></i>")
                .Add(p => p.MenuItems, DropdownMenuTestHelper.DropdownMenuItems)
                .Add(p => p.Style, DropdownMenuTestHelper.Style)
                );

        var items = cut.FindAll("li");

        //Assert
        Assert.Collection(items,
            item => Assert.Equal("<span class=\"item\">Item 1</span>", item.InnerHtml),
            item => Assert.Equal("<span class=\"item\">Item 2</span>", item.InnerHtml),
            item => Assert.Equal("<span class=\"item\">Item 3</span>", item.InnerHtml)
            );
    }

    [Fact]
    public void DropDownMenuElement_MenuTemplateParam_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<DropdownMenu<int>>(parameters => parameters
                .Add(p => p.HeaderTemplate, "<span>Testing Header</span>")
                .Add(p => p.MenuItemTemplate, item => $"<span class='item'>{item.MenuItemName}</span>")
                .Add(p => p.MenuTemplate, isActive => $"<i class='{isActive} testing'></i>")
                .Add(p => p.MenuItems, DropdownMenuTestHelper.DropdownMenuItems)
                .Add(p => p.Style, DropdownMenuTestHelper.Style)
                );

        var menuTemplateMarkup = cut.Find(".menu").InnerHtml;

        //Assert
        Assert.Equal("<i class=\"False testing\"></i>", menuTemplateMarkup);
    }

    [Fact]
    public void DropDownMenuElement_StyleParam_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<DropdownMenu<int>>(parameters => parameters
                .Add(p => p.HeaderTemplate, "<span>Testing Header</span>")
                .Add(p => p.MenuItemTemplate, item => $"<span class='item'>{item.MenuItemName}</span>")
                .Add(p => p.MenuTemplate, isActive => $"<i class='{isActive} testing'></i>")
                .Add(p => p.MenuItems, DropdownMenuTestHelper.DropdownMenuItems)
                .Add(p => p.Style, DropdownMenuTestHelper.Style)
                );

        var dropdown = cut.Find(".dropdown");
        var styleAttribute = dropdown?.Attributes.GetNamedItem("style")?.TextContent;

        //Assert
        Assert.Equal("--dropdown-left:50px;--dropdown-top:75px;--dropdown-top-mobile:37px;", styleAttribute);
    }

    [Fact]
    public void DropDownMenuElement_ClickEvent_RendersCorrectly()
    {
        //Arrange
        var cut = RenderComponent<DropdownMenu<int>>(parameters => parameters
                .Add(p => p.HeaderTemplate, "<span>Testing Header</span>")
                .Add(p => p.MenuItemTemplate, item => $"<span class='item'>{item.MenuItemName}</span>")
                .Add(p => p.MenuTemplate, isActive => $"<i class=\"{(isActive ? "Active" : string.Empty)} testing\"></i>")
                .Add(p => p.MenuItems, DropdownMenuTestHelper.DropdownMenuItems)
                .Add(p => p.Style, DropdownMenuTestHelper.Style)
                );

        var dropdown = cut.Find(".dropdown-menu");
        var i = cut.Find("i");

        //Act
        dropdown.Click();

        //Assert
        Assert.Contains("Active", i.ClassList);
    }
}
