using AutoFixture.Xunit2;

namespace Carlton.Core.Components.Library.Tests;

[Trait("Component", nameof(DropdownMenuElement<int>))]
public class DropDownMenuElementComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test"), AutoData]
    public void DropDownMenuElement_Markup_RendersCorrectly(
        string menuItemName,
        int menuItemValue,
        string menuIcon,
        int accentColorIndex)
    {
        //Arrange
        var expectedMarkup = @$"
<span class=""mdi mdi-{menuIcon} accent-color-{accentColorIndex}""></span>
    <a href=""#"">{menuItemName}</a>";

        //Act
        var cut = RenderComponent<DropdownMenuElement<int>>(parameters => parameters
                         .Add(p => p.MenuItemName, menuItemName)
                         .Add(p => p.Value, menuItemValue)
                         .Add(p => p.MenuIcon, menuIcon)
                         .Add(p => p.AccentColorIndex, accentColorIndex));

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory(DisplayName = "MenuItemName Parameter Test"), AutoData]
    public void DropDownMenuElement_MenuItemNameParam_RendersCorrectly(
        string menuItemName,
        int menuItemValue,
        string menuIcon,
        int accentColorIndex)
    {
        //Act
        var cut = RenderComponent<DropdownMenuElement<int>>(parameters => parameters
                         .Add(p => p.MenuItemName, menuItemName)
                         .Add(p => p.Value, menuItemValue)
                         .Add(p => p.MenuIcon, menuIcon)
                         .Add(p => p.AccentColorIndex, accentColorIndex));

        var itemName = cut.Find("a").TextContent;

        //Assert
        Assert.Equal(menuItemName, itemName);
    }

    [Theory(DisplayName = "MenuItemValue Parameter Test"), AutoData]
    public void DropDownMenuElement_MenuItemValueParam_RendersCorrectly(
        string menuItemName,
        int menuItemValue,
        string menuIcon,
        int accentColorIndex)
    {
        //Act
        var cut = RenderComponent<DropdownMenuElement<int>>(parameters => parameters
                          .Add(p => p.MenuItemName, menuItemName)
                          .Add(p => p.Value, menuItemValue)
                          .Add(p => p.MenuIcon, menuIcon)
                          .Add(p => p.AccentColorIndex, accentColorIndex));

        var item = cut.Instance;

        //Assert
        Assert.Equal(menuItemValue, item.Value);
    }

    [Theory(DisplayName = "MenutItemIcon Parameter Test"), AutoData]
    public void DropDownMenuElement_MenuItemIconParam_RendersCorrectly(
        string menuItemName,
        int menuItemValue,
        string menuIcon,
        int accentColorIndex)
    {
        //Act
        var cut = RenderComponent<DropdownMenuElement<int>>(parameters => parameters
                        .Add(p => p.MenuItemName, menuItemName)
                        .Add(p => p.Value, menuItemValue)
                        .Add(p => p.MenuIcon, menuIcon)
                        .Add(p => p.AccentColorIndex, accentColorIndex));


        var spanElement = cut.Find("span");
        var expectedResult = $"mdi-{menuIcon}";

        //Assert
        Assert.Contains(expectedResult, spanElement.ClassList);
    }

    [Theory(DisplayName = "MenuItemAccentColorIndex Parameter Test"), AutoData]
    public void DropDownMenuElement_MenuItemAccentColorIndexParam_RendersCorrectly(
        string menuItemName,
        int menuItemValue,
        string menuIcon,
        int accentColorIndex)
    {
        //Arrange
        var expectedAccentClass = $"accent-color-{accentColorIndex}";

        //Act
        var cut = RenderComponent<DropdownMenuElement<int>>(parameters => parameters
                        .Add(p => p.MenuItemName, menuItemName)
                        .Add(p => p.Value, menuItemValue)
                        .Add(p => p.MenuIcon, menuIcon)
                        .Add(p => p.AccentColorIndex, accentColorIndex));


        var spanElement = cut.Find("span");

        //Assert
        Assert.Contains(expectedAccentClass, spanElement.ClassList);
    }

    [Theory(DisplayName = "OnMenuItemSelected Parameter Test"), AutoData]
    public void DropDownMenuElement_OnMenuItemSelectedParam_RendersCorrectly(
        string menuItemName,
        int menuItemValue,
        string menuIcon,
        int accentColorIndex)
    {
        //Arrange
        var eventCalled = false;
        var eventCalledValue = 0;

        var cut = RenderComponent<DropdownMenuElement<int>>(parameters => parameters
                        .Add(p => p.MenuItemName, menuItemName)
                        .Add(p => p.Value, menuItemValue)
                        .Add(p => p.MenuIcon, menuIcon)
                        .Add(p => p.AccentColorIndex, accentColorIndex)
                        .Add(p => p.OnMenuItemSelected, (_) => { eventCalled = true; eventCalledValue = _; }));

        
        var aElement = cut.Find("a");

        //Act
        aElement.Click();

        //Assert
        Assert.True(eventCalled);
        Assert.Equal(menuItemValue, eventCalledValue);
    }
}
