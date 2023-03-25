namespace Carlton.Base.Components.Test;

public class DropDownMenuElementComponentTests : TestContext
{
    private static readonly string DropDownMenuElementMarkup = @"
<span class=""mdi mdi-mdi-delete accent-color-2"" b-7gb23ut1dh></span>
<a href=""#"" blazor:onclick:preventDefault blazor:onclick=""1"" b-7gb23ut1dh>Test Menu Item</a>";

    [Fact]
    public void DropDownMenuElement_Markup_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<DropdownMenuElement<int>>(parameters => parameters
                .Add(p => p.MenuItemName, "Test Menu Item")
                .Add(p => p.Value, 1)
                .Add(p => p.MenuIcon, "mdi-delete")
                .Add(p => p.AccentColorIndex, 2)
                );

        //Assert
        cut.MarkupMatches(DropDownMenuElementMarkup);
    }

    [Theory]
    [InlineData("Test Menu Item")]
    [InlineData("Test Menu Item Again")]
    [InlineData("Test Menu Items Yet Another Time")]
    public void DropDownMenuElement_MenuItemNameParam_RendersCorrectly(string menuItemName)
    {
        //Act
        var cut = RenderComponent<DropdownMenuElement<int>>(parameters => parameters
                .Add(p => p.MenuItemName, menuItemName)
                .Add(p => p.Value, 1)
                .Add(p => p.MenuIcon, "mdi-delete")
                .Add(p => p.AccentColorIndex, 2)
                );

        var itemName = cut.Find("a").TextContent;

        //Assert
        Assert.Equal(menuItemName, itemName);
    }

    [Theory]
    [InlineData(5)]
    [InlineData(10)]
    [InlineData(12)]
    public void DropDownMenuElement_MenuItemValueParam_RendersCorrectly(int value)
    {
        //Act
        var cut = RenderComponent<DropdownMenuElement<int>>(parameters => parameters
                .Add(p => p.MenuItemName, "Test Menu Item")
                .Add(p => p.Value, value)
                .Add(p => p.MenuIcon, "mdi-delete")
                .Add(p => p.AccentColorIndex, 2)
                );

        var item = cut.Instance;

        //Assert
        Assert.Equal(value, item.Value);
    }

    [Theory]
    [InlineData("icon-1")]
    [InlineData("icon-2")]
    [InlineData("icon-3")]
    public void DropDownMenuElement_MenuItemIconParam_RendersCorrectly(string icon)
    {
        //Act
        var cut = RenderComponent<DropdownMenuElement<int>>(parameters => parameters
                        .Add(p => p.MenuItemName, "Test Menu Item")
                        .Add(p => p.Value, 1)
                        .Add(p => p.MenuIcon, icon)
                        .Add(p => p.AccentColorIndex, 3)
                        );


        var spanElement = cut.Find("span");
        var expectedResult = $"mdi-{icon}";

        //Assert
        Assert.Contains(expectedResult, spanElement.ClassList);
    }

    [Theory]
    [InlineData(1, "accent-color-1")]
    [InlineData(2, "accent-color-2")]
    [InlineData(3, "accent-color-3")]
    public void DropDownMenuElement_MenuItemAccentColorIndexParam_RendersCorrectly(int accentColorIndex, string expectedValue)
    {
        //Act
        var cut = RenderComponent<DropdownMenuElement<int>>(parameters => parameters
                        .Add(p => p.MenuItemName, "Test Menu Item")
                        .Add(p => p.Value, 1)
                        .Add(p => p.MenuIcon, "mdi-delete")
                        .Add(p => p.AccentColorIndex, accentColorIndex)
                        );


        var spanElement = cut.Find("span");

        //Assert
        Assert.Contains(expectedValue, spanElement.ClassList);
    }

    [Theory]
    [InlineData(7)]
    [InlineData(8)]
    [InlineData(1)]
    public void DropDownMenuElement_OnMenuItemSelectedParam_RendersCorrectly(int value)
    {
        //Arrange
        var eventCalled = false;
        var eventCalledValue = 0;

        var cut = RenderComponent<DropdownMenuElement<int>>(parameters => parameters
                        .Add(p => p.MenuItemName, "Test Menu Item")
                        .Add(p => p.Value, value)
                        .Add(p => p.MenuIcon, "mdi-delete")
                        .Add(p => p.AccentColorIndex, 3)
                        .Add(p => p.OnMenuItemSelected, (_) => { eventCalled = true; eventCalledValue = _; })
                        );

        
        var aElement = cut.Find("a");

        //Act
        aElement.Click();

        //Assert
        Assert.True(eventCalled);
        Assert.Equal(value, eventCalledValue);
    }
}
