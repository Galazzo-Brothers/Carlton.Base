namespace Carlton.Core.Components.Tests;

[Trait("Component", nameof(Checkbox))]
public class CheckboxComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test"), AutoData]
    public void Checkbox_Markup_RendersCorrectly(bool expectedIsChecked)
    {
        //Arrange
        var expectedMarkup = @$"<div class=""checkbox mdi mdi-24px mdi-{(expectedIsChecked ? "check" : "blank")}-circle""></div>";
        //Act
        var cut = RenderComponent<Checkbox>(parameters => parameters
            .Add(p => p.IsChecked, true));

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory(DisplayName = "IsChecked Parameter Test")]
    [InlineData(true)]
    [InlineData(false)]
    public void Checkbox_IsCheckedParam_FiresCallback(bool expectedIsChecked)
    {
        //Arrange
        var eventCalled = false;
        var actualCheckedState = expectedIsChecked;
        var expectedState = !expectedIsChecked;

        var cut = RenderComponent<Checkbox>(parameters => parameters
            .Add(p => p.IsChecked, expectedIsChecked)
            .Add(p => p.OnCheckboxChangeCallback, (state) => { eventCalled = true; actualCheckedState = state; }));

        //Act
        cut.Find(".checkbox").Click();

        //Assert
        eventCalled.ShouldBeTrue();
        actualCheckedState.ShouldBe(expectedState);
    }
}
