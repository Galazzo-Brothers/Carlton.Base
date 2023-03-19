namespace Carlton.Base.Components.Test;

public class CheckboxComponentTests : TestContext
{
    private static readonly string CheckboxMarkup =
    @"<div class=""checkbox mdi mdi-24px mdi-check-circle"" blazor:onclick=""1""></div>";


    [Fact]
    public void Checkbox_Markup_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<Checkbox>(parameters => parameters
            .Add(p => p.IsChecked, true)
            );

        //Assert
        cut.MarkupMatches(CheckboxMarkup);
    }

    [Fact]
    public void Checkbox_Markup_IsCheckedParam_True_OnClickCallback()
    {
        //Arrange
        var eventCalled = false;
        var checkedState = false;

        var cut = RenderComponent<Checkbox>(parameters => parameters
            .Add(p => p.IsChecked, true)
            .Add(p => p.OnCheckboxChangeCallback, (state) => { eventCalled = true; checkedState = state; })
            );

        //Act
        cut.Find(".checkbox").Click();

        //Assert
        Assert.True(eventCalled);
        Assert.False(checkedState);
    }

    [Fact]
    public void Checkbox_Markup_IsCheckedParam_False_OnClickCallback()
    {
        //Arrange
        var eventCalled = false;
        var checkedState = false;

        var cut = RenderComponent<Checkbox>(parameters => parameters
            .Add(p => p.IsChecked, false)
            .Add(p => p.OnCheckboxChangeCallback, (state) => { eventCalled = true; checkedState = state; })
            );

        //Act
        cut.Find(".checkbox").Click();

        //Assert
        Assert.True(eventCalled);
        Assert.True(checkedState);
    }
}
