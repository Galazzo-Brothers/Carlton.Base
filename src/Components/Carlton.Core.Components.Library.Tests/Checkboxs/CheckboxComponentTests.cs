using AutoFixture.Xunit2;

namespace Carlton.Core.Components.Library.Tests;

[Trait("Component", nameof(Checkbox))]
public class CheckboxComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test"), AutoData]
    public void Checkbox_Markup_RendersCorrectly(bool isChecked)
    {
        //Arrange
        var expectedMarkup = @$"<div class=""checkbox mdi mdi-24px mdi-{(isChecked ? "check" : "blank")}-circle""></div>";
        //Act
        var cut = RenderComponent<Checkbox>(parameters => parameters
            .Add(p => p.IsChecked, true));

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
