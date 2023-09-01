using AutoFixture;
using AutoFixture.Xunit2;

namespace Carlton.Core.Components.Library.Tests;

[Trait("Component", nameof(DropdownMenu<int>))]
public class DropDownMenuComponentTests : TestContext
{
    private const string HeaderTemplate = "<span>{0}</span>";
    private const string MenuItemTemplate = "<span class=\"item-name\">{0}</span><span class=\"item-value\">{1}</span><span class=\"item-icon\">{2}</span>";

    [Theory(DisplayName = "Markup Test"), AutoData]
    public void DropDownMenuElement_Markup_RendersCorrectly(IFixture fixture,
        string headerMarkup, DropdownMenuStyle style)
    {
        //Arrange
        var items = fixture.CreateMany<DropdownMenuItem<int>>(3);
        var expectedMarkup =
@$"
<div class=""dropdown-menu"">
    <div class=""menu"" >
        <span class=""menu-template""></span>
    </div>
    <div class=""dropdown"" style=""--dropdown-left:{style.Left}px;--dropdown-top:{style.Top}px;--dropdown-top-mobile:{style.Top_Mobile}px;"">
    <ul>
      <div class=""header"">
        <span>{headerMarkup}</span>
      </div>
      <li >
        <span class=""item-name"">{items.ElementAt(0).MenuItemName}</span>
        <span class=""item-value"">{items.ElementAt(0).Value}</span>
        <span class=""item-icon"">{items.ElementAt(0).MenuIcon}</span>
      </li>
      <li>
        <span class=""item-name"">{items.ElementAt(1).MenuItemName}</span>
        <span class=""item-value"">{items.ElementAt(1).Value}</span>
        <span class=""item-icon"">{items.ElementAt(1).MenuIcon}</span>
      </li>
      <li>
        <span class=""item-name"">{items.ElementAt(2).MenuItemName}</span>
        <span class=""item-value"">{items.ElementAt(2).Value}</span>
        <span class=""item-icon"">{items.ElementAt(2).MenuIcon}</span>
      </li>
    </ul>
  </div>
</div>";

        //Act
        var cut = RenderComponent<DropdownMenu<int>>(parameters => parameters
                .Add(p => p.HeaderTemplate, string.Format(HeaderTemplate, headerMarkup))
                .Add(p => p.MenuItemTemplate, item => string.Format(MenuItemTemplate, item.MenuItemName, item.Value, item.MenuIcon))
                .Add(p => p.MenuTemplate, isActive => GetMenuTemplate(isActive))
                .Add(p => p.MenuItems, items)
                .Add(p => p.Style, style)
                );

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    public void DropDownMenuElement_EmptyList_Markup_RendersCorrectly(IFixture fixture,
      string headerMarkup, DropdownMenuStyle style)
    {
        //Arrange
        var items = fixture.CreateMany<DropdownMenuItem<int>>(0);
        var expectedMarkup =
@$"
<div class=""dropdown-menu"">
    <div class=""menu"" >
        <span class=""menu-template""></span>
    </div>
    <div class=""dropdown"" style=""--dropdown-left:{style.Left}px;--dropdown-top:{style.Top}px;--dropdown-top-mobile:{style.Top_Mobile}px;"">
    <ul>
      <div class=""header"">
        <span>{headerMarkup}</span>
      </div>
    </ul>
  </div>
</div>";

        //Act
        var cut = RenderComponent<DropdownMenu<int>>(parameters => parameters
                .Add(p => p.HeaderTemplate, string.Format(HeaderTemplate, headerMarkup))
                .Add(p => p.MenuItemTemplate, item => string.Format(MenuItemTemplate, item.MenuItemName, item.Value, item.MenuIcon))
                .Add(p => p.MenuTemplate, isActive => GetMenuTemplate(isActive))
                .Add(p => p.MenuItems, items)
                .Add(p => p.Style, style)
                );

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory(DisplayName = "MenuItems Parameter Test"), AutoData]
    public void DropDownMenuElement_MenuItemsParams_RendersCorrectly(IEnumerable<DropdownMenuItem<int>> items, string headerMarkup, DropdownMenuStyle style)
    {
        //Arrange
        var expectedNames = items.Select(_ => _.MenuItemName);
        var expectedValues = items.Select(_ => _.Value);
        var expectedIcons = items.Select(_ => _.MenuIcon);

        //Act
        var cut = RenderComponent<DropdownMenu<int>>(parameters => parameters
                .Add(p => p.HeaderTemplate, string.Format(HeaderTemplate, headerMarkup))
                .Add(p => p.MenuItemTemplate, item => string.Format(MenuItemTemplate, item.MenuItemName, item.Value, item.MenuIcon))
                .Add(p => p.MenuTemplate, isActive => GetMenuTemplate(isActive))
                .Add(p => p.MenuItems, items)
                .Add(p => p.Style, style));


        var actualCount = cut.FindAll("li").Count;
        var actualItemNames = cut.FindAll(".item-name").Select(_ => _.TextContent);
        var actualItemValues = cut.FindAll(".item-value").Select(_ => int.Parse(_.TextContent));
        var actualItemIcons = cut.FindAll(".item-icon").Select(_ => _.TextContent);

        //Assert
        Assert.Equal(items.Count(), actualCount);
        Assert.Equal(expectedNames, actualItemNames);
        Assert.Equal(expectedValues, actualItemValues);
        Assert.Equal(expectedIcons, actualItemIcons);
    }

    [Theory(DisplayName = "HeaderTemplate Parameter Test"), AutoData]
    public void DropDownMenuElement_HeaderTemplateParam_RendersCorrectly(IEnumerable<DropdownMenuItem<int>> items, string headerMarkup, DropdownMenuStyle style)
    {
        //Arrange
        var expectedHeaderTemplate = string.Format(HeaderTemplate, headerMarkup);

        //Act
        var cut = RenderComponent<DropdownMenu<int>>(parameters => parameters
              .Add(p => p.HeaderTemplate, string.Format(HeaderTemplate, headerMarkup))
              .Add(p => p.MenuItemTemplate, item => string.Format(MenuItemTemplate, item.MenuItemName, item.Value, item.MenuIcon))
              .Add(p => p.MenuTemplate, isActive => GetMenuTemplate(isActive))
              .Add(p => p.MenuItems, items)
              .Add(p => p.Style, style));

        var actualHeaderMarkup = cut.Find(".header").InnerHtml;

        //Assert
        Assert.Equal(expectedHeaderTemplate, actualHeaderMarkup);
    }

    [Theory(DisplayName = "MenuItemTemplate Parameter Test"), AutoData]
    public void DropDownMenuElement_MenuItemTemplateParam_RendersCorrectly(IEnumerable<DropdownMenuItem<int>> items, string headerMarkup, DropdownMenuStyle style)
    {
        //Act
        var cut = RenderComponent<DropdownMenu<int>>(parameters => parameters
             .Add(p => p.HeaderTemplate, string.Format(HeaderTemplate, headerMarkup))
             .Add(p => p.MenuItemTemplate, item => string.Format(MenuItemTemplate, item.MenuItemName, item.Value, item.MenuIcon))
             .Add(p => p.MenuTemplate, isActive => GetMenuTemplate(isActive))
             .Add(p => p.MenuItems, items)
             .Add(p => p.Style, style));

        var itemsElements = cut.FindAll("li");

        //Assert
        Assert.Equal(items.Count(), itemsElements.Count);
        Assert.All(itemsElements, (item, i) =>
        {
            var expectedTemplate = string.Format(MenuItemTemplate, items.ElementAt(i).MenuItemName, items.ElementAt(i).Value, items.ElementAt(i).MenuIcon);
            Assert.Equal(expectedTemplate, item.InnerHtml);
        });
    }

    [Theory(DisplayName = "MenuTemplate Parameter Test"), AutoData]
    public void DropDownMenuElement_MenuTemplateParam_RendersCorrectly(IEnumerable<DropdownMenuItem<int>> items, string headerMarkup, DropdownMenuStyle style)
    {
        //Arrange
        var expectedMenutTemplate = GetMenuTemplate(false);

        //Act
        var cut = RenderComponent<DropdownMenu<int>>(parameters => parameters
              .Add(p => p.HeaderTemplate, string.Format(HeaderTemplate, headerMarkup))
              .Add(p => p.MenuItemTemplate, item => string.Format(MenuItemTemplate, item.MenuItemName, item.Value, item.MenuIcon))
              .Add(p => p.MenuTemplate, isActive => GetMenuTemplate(isActive))
              .Add(p => p.MenuItems, items)
              .Add(p => p.Style, style));

        var menuTemplateMarkup = cut.Find(".menu").InnerHtml;

        //Assert
        Assert.Equal(expectedMenutTemplate, menuTemplateMarkup);
    }

    [Theory(DisplayName = "Style Parameter Test"), AutoData]
    public void DropDownMenuElement_StyleParam_RendersCorrectly(IEnumerable<DropdownMenuItem<int>> items, string headerMarkup, DropdownMenuStyle style)
    {
        //Arrange
        var expectedStyle = $"--dropdown-left:{style.Left}px;--dropdown-top:{style.Top}px;--dropdown-top-mobile:{style.Top_Mobile}px;";

        //Act
        var cut = RenderComponent<DropdownMenu<int>>(parameters => parameters
             .Add(p => p.HeaderTemplate, string.Format(HeaderTemplate, headerMarkup))
             .Add(p => p.MenuItemTemplate, item => string.Format(MenuItemTemplate, item.MenuItemName, item.Value, item.MenuIcon))
             .Add(p => p.MenuTemplate, isActive => GetMenuTemplate(isActive))
             .Add(p => p.MenuItems, items)
             .Add(p => p.Style, style));


        var dropdown = cut.Find(".dropdown");
        var styleAttribute = dropdown?.Attributes.GetNamedItem("style")?.TextContent;

        //Assert
        Assert.Equal(expectedStyle, styleAttribute);
    }

    [Theory(DisplayName = "MenuItem Click Test"), AutoData]
    public void DropDownMenuElement_ClickEvent_RendersCorrectly(IEnumerable<DropdownMenuItem<int>> items, string headerMarkup, DropdownMenuStyle style)
    {
        //Arrange
        var cut = RenderComponent<DropdownMenu<int>>(parameters => parameters
            .Add(p => p.HeaderTemplate, string.Format(HeaderTemplate, headerMarkup))
            .Add(p => p.MenuItemTemplate, item => string.Format(MenuItemTemplate, item.MenuItemName, item.Value, item.MenuIcon))
            .Add(p => p.MenuTemplate, isActive => GetMenuTemplate(isActive))
            .Add(p => p.MenuItems, items)
            .Add(p => p.Style, style));

        var dropdown = cut.Find(".dropdown-menu");

        //Act
        dropdown.Click();
        var menu = cut.Find(".menu-template");

        //Assert
        Assert.Contains("active", menu.ClassList);
    }

    private static string GetMenuTemplate(bool isActive)
        => isActive ? "<span class=\"menu-template active\"></span>"
        : "<span class=\"menu-template\"></span>";
}
