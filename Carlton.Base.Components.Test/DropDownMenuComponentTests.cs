namespace Carlton.Base.Components.Test;

public class DropDownMenuComponentTests : TestContext
{
    private static readonly string DropDownMenuMarkup = @"
<div class=""dropdown-menu"" blazor:onclick=""1"" b-e7te7pxji7>
  <div class=""menu"" b-e7te7pxji7><i class='False'></i></div>
  <div class=""dropdown "" style=""--dropdown-left:50px;--dropdown-top:75px;--dropdown-top-mobile:37px;"" b-e7te7pxji7>
    <ul b-e7te7pxji7>
      <div class=""header"" b-e7te7pxji7>  
        <span>Header</span>
      </div>
      <li b-e7te7pxji7>
        <span class='item'>Item 1</span>
      </li>
      <li b-e7te7pxji7>
        <span class='item'>Item 2</span>
      </li>
      <li b-e7te7pxji7>
        <span class='item'>Item 3</span>
      </li>
    </ul>
  </div>
</div>";

    private readonly IEnumerable<DropdownMenuItem<int>> _items = new List<DropdownMenuItem<int>>
    {
        new DropdownMenuItem<int>("Item 1", 1, "icon-1", 1),
        new DropdownMenuItem<int>("Item 2", 2, "icon-2", 2),
        new DropdownMenuItem<int>("Item 3", 3, "icon-3", 3)
    };

    private readonly DropdownMenuStyle _style = new(50, 75, 37);

    [Fact]
    public void DropDownMenuElement_Markup_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<DropdownMenu<int>>(parameters => parameters
                .Add(p => p.HeaderTemplate, "<span>Header</span>")
                .Add(p => p.MenuItemTemplate, item => $"<span class='item'>{item.MenuItemName}</span>")
                .Add(p => p.MenuTemplate, isActive => $"<i class='{isActive}'></i>")
                .Add(p => p.MenuItems, _items)
                .Add(p => p.Style, _style)
                );

        //Assert
        cut.MarkupMatches(DropDownMenuMarkup);
    }

    [Fact]
    public void DropDownMenuElement_ShouldRenderCorrecetNumberOfItems()
    {
        //Act
        var cut = RenderComponent<DropdownMenu<int>>(parameters => parameters
                .Add(p => p.HeaderTemplate, "<span>Header</span>")
                .Add(p => p.MenuItemTemplate, item => $"<span class='item'>{item.MenuItemName}</span>")
                .Add(p => p.MenuTemplate, isActive => $"<i class='{isActive}'></i>")
                .Add(p => p.MenuItems, _items)
                .Add(p => p.Style, _style)
                );

        var expectedItemsCount = _items.Count();
        var itemsCount = cut.FindAll("li").Count;

        //Assert
        Assert.Equal(expectedItemsCount, itemsCount);
    }

    [Fact]
    public void DropDownMenuElement_HeaderTemplateParam_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<DropdownMenu<int>>(parameters => parameters
                .Add(p => p.HeaderTemplate, "<span>Testing Header</span>")
                .Add(p => p.MenuItemTemplate, item => $"<span class='item'>{item.MenuItemName}</span>")
                .Add(p => p.MenuTemplate, isActive => $"<i class='{isActive}'></i>")
                .Add(p => p.MenuItems, _items)
                .Add(p => p.Style, _style)
                );

        var header = cut.Find(".header").InnerHtml;

        //Assert
        Assert.Equal("<span>Testing Header</span>", header);
    }

    [Fact]
    public void DropDownMenuElement_MenuItemTemplateParam_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<DropdownMenu<int>>(parameters => parameters
                .Add(p => p.HeaderTemplate, "<span>Testing Header</span>")
                .Add(p => p.MenuItemTemplate, item => $"<span class='item'>{item.MenuItemName}</span>")
                .Add(p => p.MenuTemplate, isActive => $"<i class='{isActive}'></i>")
                .Add(p => p.MenuItems, _items)
                .Add(p => p.Style, _style)
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
                .Add(p => p.MenuItems, _items)
                .Add(p => p.Style, _style)
                );

        var menuTemplateMarkup = cut.Find(".menu").InnerHtml;

        //Assert
        Assert.Equal("<i class=\"False testing\"></i>", menuTemplateMarkup);
    }

    [Fact]
    public void DropDownMenuElement_ClickEvent_ShouldExpandDropdown()
    {
        //Arrange
        var cut = RenderComponent<DropdownMenu<int>>(parameters => parameters
                .Add(p => p.HeaderTemplate, "<span>Testing Header</span>")
                .Add(p => p.MenuItemTemplate, item => $"<span class='item'>{item.MenuItemName}</span>")
                .Add(p => p.MenuTemplate, isActive => $"<i class=\"{(isActive ? "Active" : string.Empty)} testing\"></i>")
                .Add(p => p.MenuItems, _items)
                .Add(p => p.Style, _style)
                );

        var dropdown = cut.Find(".dropdown-menu");
        var menu = cut.Find(".menu");
        var i = cut.Find("i");

        //Act
        dropdown.Click();

        //Assert
        Assert.Contains("Active", i.ClassList);
    }

    [Fact]
    public void DropDownMenuElement_StyleParam_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<DropdownMenu<int>>(parameters => parameters
                .Add(p => p.HeaderTemplate, "<span>Testing Header</span>")
                .Add(p => p.MenuItemTemplate, item => $"<span class='item'>{item.MenuItemName}</span>")
                .Add(p => p.MenuTemplate, isActive => $"<i class='{isActive} testing'></i>")
                .Add(p => p.MenuItems, _items)
                .Add(p => p.Style, _style)
                );

        var dropdown = cut.Find(".dropdown");
        var styleAttribute = dropdown?.Attributes.GetNamedItem("style")?.TextContent;

        //Assert
        Assert.Equal("--dropdown-left:50px;--dropdown-top:75px;--dropdown-top-mobile:37px;", styleAttribute);
    }
}
