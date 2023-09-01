namespace Carlton.Core.Components.Library.Tests;

[Trait("Component", nameof(Checkbox))]
public class CheckboxComponentTests : TestContext
{
    private const string CheckboxCheckedMarkup =
        @"<div class=""checkbox mdi mdi-24px mdi-check-circle""></div>";

    private const string CheckboxUncheckedMarkup =
        @"<div class=""checkbox mdi mdi-24px mdi-checkbox-blank-circle-outline""></div>";

    [Theory(DisplayName = "Markup Test")]
    [InlineData(true, CheckboxCheckedMarkup)]
    [InlineData(false, CheckboxUncheckedMarkup)]

    public void Checkbox_Markup_RendersCorrectly(bool isChecked, string expectedMarkup)
    {
        //Act
        var cut = RenderComponent<Checkbox>(parameters => parameters
            .Add(p => p.IsChecked, isChecked)
            );

        //Assert
        cut.MarkupMatches(expectedMarkup);
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
