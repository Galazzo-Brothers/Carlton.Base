namespace Carlton.Core.Components.Library.Tests;

[Trait("Component", nameof(Checkbox))]
public class CheckboxComponentTests : TestContext
{
    private static readonly string CheckboxMarkup =
    @"<div class=""checkbox mdi mdi-24px mdi-check-circle"" blazor:onclick=""1""></div>";


    [Fact(DisplayName = "Markup Test")]
    public void Checkbox_Markup_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<Checkbox>(parameters => parameters
            .Add(p => p.IsChecked, true)
            );

        //Assert
        cut.MarkupMatches(CheckboxMarkup);
    }

    [Theory(DisplayName = "IsChecked Parameter Test")]
    [InlineData(true)]
    [InlineData(false)]
    public void Checkbox_IsCheckedParam_FiresCallback(bool isChecked)
    {
        //Arrange
        var eventCalled = false;
        var checkedState = isChecked;
        var expectedState = !isChecked;

        var cut = RenderComponent<Checkbox>(parameters => parameters
            .Add(p => p.IsChecked, isChecked)
            .Add(p => p.OnCheckboxChangeCallback, (state) => { eventCalled = true; checkedState = state; })
            );

        //Act
        cut.Find(".checkbox").Click();

        //Assert
        Assert.True(eventCalled);
        Assert.Equal(expectedState, checkedState);
    }
}
