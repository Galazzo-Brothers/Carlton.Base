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

    [Fact]
    public void DropDownMenuElement_MenuItemNameParam_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<DropdownMenuElement<int>>(parameters => parameters
                .Add(p => p.MenuItemName, "Test Menu Item")
                .Add(p => p.Value, 1)
                .Add(p => p.MenuIcon, "mdi-delete")
                .Add(p => p.AccentColorIndex, 2)
                );

        var itemName = cut.Find("a").TextContent;

        //Assert
        Assert.Equal("Test Menu Item", itemName);
    }

    [Fact]
    public void DropDownMenuElement_MenuItemValueParam_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<DropdownMenuElement<int>>(parameters => parameters
                .Add(p => p.MenuItemName, "Test Menu Item")
                .Add(p => p.Value, 5)
                .Add(p => p.MenuIcon, "mdi-delete")
                .Add(p => p.AccentColorIndex, 2)
                );

        var item = cut.Instance;

        //Assert
        Assert.Equal(5, item.Value);
    }

    [Fact]
    public void DropDownMenuElement_MenuItemIconParam_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<DropdownMenuElement<int>>(parameters => parameters
                        .Add(p => p.MenuItemName, "Test Menu Item")
                        .Add(p => p.Value, 1)
                        .Add(p => p.MenuIcon, "camera")
                        .Add(p => p.AccentColorIndex, 3)
                        );


        var spanElement = cut.Find("span");

        //Assert
        Assert.Contains("mdi-camera", spanElement.ClassList);
    }

    [Fact]
    public void DropDownMenuElement_MenuItemAccentColorIndexParam_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<DropdownMenuElement<int>>(parameters => parameters
                        .Add(p => p.MenuItemName, "Test Menu Item")
                        .Add(p => p.Value, 1)
                        .Add(p => p.MenuIcon, "mdi-delete")
                        .Add(p => p.AccentColorIndex, 3)
                        );


        var spanElement = cut.Find("span");

        //Assert
        Assert.Contains("accent-color-3", spanElement.ClassList);
    }

    [Fact]
    public void DropDownMenuElement_OnMenuItemSelectedParam_RendersCorrectly()
    {
        //Arrange
        var eventCalled = false;
        var eventCalledValue = 0;

        var cut = RenderComponent<DropdownMenuElement<int>>(parameters => parameters
                        .Add(p => p.MenuItemName, "Test Menu Item")
                        .Add(p => p.Value, 7)
                        .Add(p => p.MenuIcon, "mdi-delete")
                        .Add(p => p.AccentColorIndex, 3)
                        .Add(p => p.OnMenuItemSelected, (_) => { eventCalled = true; eventCalledValue = _; })
                        );

        
        var aElement = cut.Find("a");

        //Act
        aElement.Click();

        //Assert
        Assert.True(eventCalled);
        Assert.Equal(7, eventCalledValue);
    }
}
